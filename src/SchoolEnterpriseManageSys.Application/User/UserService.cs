using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Timing;
using Abp.UI;
using SchoolEnterpriseManageSys.Entities;
using SchoolEnterpriseManageSys.Enum;
using SchoolEnterpriseManageSys.User.Dto;
using SchoolEnterpriseManageSys.Utilities.DEncryptHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;

namespace SchoolEnterpriseManageSys.User
{
    public class UserService : ApiController, IUserService
    {
        private readonly IRepository<UserEntity, Guid> _userRepository;
        private readonly IRepository<RoleEntity, Guid> _roleRepository;
        private readonly IRepository<DepartmentEntity, Guid> _departmentRepository;
        private readonly IRepository<EnterpriseInfoEntity, Guid> _enterpriseInfoRepository;

        public UserService(IRepository<UserEntity, Guid> userRepository,
            IRepository<RoleEntity, Guid> roleRepository,
            IRepository<DepartmentEntity, Guid> departmentRepository,
            IRepository<EnterpriseInfoEntity, Guid> enterpriseInfoRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _departmentRepository = departmentRepository;
            _enterpriseInfoRepository = enterpriseInfoRepository;
        }

        public GetUserInfoOutput GetUserInfo()
        {
            var userClaim = UserClaim();
            if (userClaim == null) throw new UserFriendlyException("用户不存在");
            var user = _userRepository.Get(userClaim.UserId);
            var output = user.MapTo<GetUserInfoOutput>();
            return output;
        }

        public LoginOutput Login(LoginInput input)
        {
            string passwordMd5 = Encrypt.MD5ByHash(input.Password);
            var user = _userRepository.FirstOrDefault(t => t.UserName == input.UserName && t.Password == passwordMd5 && t.IsDeleted == false);
            LoginOutput output = new LoginOutput();
            if (user == null)
                return output;
            output = user.MapTo<LoginOutput>();
            if (output.RoleId.HasValue)
                output.Role = _roleRepository.Get((Guid)output.RoleId)?.RoleName;

            user.LastLoginTime = Clock.Now;
            _userRepository.UpdateAsync(user);

            return output;
        }

        public void EnterpriseRegist(EnterpriseRegistInput input)
        {
            if (_userRepository.FirstOrDefault(t => t.UserName == input.Email) != null)
                throw new UserFriendlyException("邮箱已被占用");
            UserEntity user = new UserEntity
            {
                Email = input.Email,
                Id = Guid.NewGuid(),
                IsDeleted = false,
                Nickname = input.Email,
                Password = Encrypt.MD5ByHash(input.Password),
                UserName = input.Email,
                UserType = Enum.UserType.BusinessUser,
                RoleId = _roleRepository.FirstOrDefault(t => t.IsDeleted == false && t.RoleType == Enum.RoleType.Enterprise)?.Id
            };
            _userRepository.Insert(user);
        }

        public void UpdatePassword(UpdatePasswordInput input)
        {
            var userClaim = UserClaim();
            if (userClaim == null) throw new UserFriendlyException("用户不存在");
            var user = _userRepository.Get(userClaim.UserId);
            if (user.Password != Encrypt.MD5ByHash(input.OldPassword)) throw new UserFriendlyException("旧密码错误");
            user.Password = Encrypt.MD5ByHash(input.NewPassword);
            user.UpdateTime = Clock.Now;
            user.UpdateUserId = userClaim.UserId;
            _userRepository.UpdateAsync(user);
        }

        public void UpdateUserInfo(GetUserInfoOutput input)
        {
            var userClaim = UserClaim();
            if (userClaim == null) throw new UserFriendlyException("用户不存在");
            var user = _userRepository.Get(userClaim.UserId);
            user.ActualName = input.ActualName;
            user.AvatarUrl = input.AvatarUrl;
            user.Email = input.Email;
            user.FixedTelephone = input.FixedTelephone;
            user.Mobilephone = input.Mobilephone;
            user.Nickname = input.Nickname;
            user.Position = input.Position;
            user.UpdateTime = Clock.Now;
            user.UpdateUserId = userClaim.UserId;
            _userRepository.UpdateAsync(user);
        }

        public UserClaimOutput UserClaim()
        {
            var identity = User.Identity as ClaimsIdentity;
            UserClaimOutput output = new UserClaimOutput();
            identity.Claims.ToList().ForEach(t =>
            {
                if (t.Type.ToLower() == "userid" && Guid.TryParse(t.Value, out Guid guid)) output.UserId = guid;
                if (t.Type.ToLower() == "nickname") output.Nickname = t.Value;
                if (t.Type.ToLower() == "username") output.UserName = t.Value;
                if (t.Type.ToLower() == "usertype" && int.TryParse(t.Value, out int type)) output.UserType = (UserType)type;


                if (t.Type.ToLower() == "roleid" && Guid.TryParse(t.Value, out Guid roleId)) output.RoleId = roleId;
                if (t.Type.ToLower() == "role") output.Role = t.Value;


                //if (t.Type.ToLower() == "iss") output.iss = t.Value;
                //if (t.Type.ToLower() == "aud") output.aud = t.Value;
                //if (t.Type.ToLower() == "exp") output.exp = t.Value;
                //if (t.Type.ToLower() == "nbf") output.nbf = t.Value;
            });
            return output;
        }

        public void CheckUserPermissions(List<RoleType> roleTypes)
        {
            var userClaim = UserClaim();
            if (!userClaim.RoleId.HasValue)
                throw new HttpResponseException(System.Net.HttpStatusCode.Unauthorized);
            var roleType = _roleRepository.FirstOrDefault(t => t.Id == userClaim.RoleId && t.IsDeleted == false)?.RoleType;
            if (roleType == null)
                throw new HttpResponseException(System.Net.HttpStatusCode.Unauthorized);
            var isContains = roleTypes.Contains((RoleType)roleType);
            if (!isContains)
                throw new HttpResponseException(System.Net.HttpStatusCode.Unauthorized);
        }

        public GetUsersOutput GetUsers(GetUsersInput input)
        {
            CheckUserPermissions(new List<RoleType> { RoleType.Administrator });
            var currentUser = UserClaim();
            var userQuery = _userRepository.GetAll().Where(t => t.Id != currentUser.UserId);
            var userList = userQuery.OrderByDescending(t => t.CreateTime).Skip(input.PageSize * (input.PageIndex - 1)).Take(input.PageSize).ToList();
            var output = new GetUsersOutput
            {
                UserList = userList.MapTo<List<UserOutput>>(),
                TotalCount = userQuery.Count()
            };
            output.UserList.ForEach(t =>
            {
                if (t.UserType == UserType.OnCampusUser)
                {
                    if (t.DepartmentId.HasValue)
                    {
                        t.DepartmentName = _departmentRepository.FirstOrDefault(d => d.Id == t.DepartmentId)?.Name;
                    }
                }
                else
                {
                    var enterprise = _enterpriseInfoRepository.FirstOrDefault(e => e.Id == t.Id);
                    t.EnterpriseName = enterprise?.NameAbbreviation ?? enterprise?.FullName;
                }
                if (t.RoleId.HasValue)
                {
                    var role = _roleRepository.FirstOrDefault(r => r.Id == t.RoleId);
                    if (role != null)
                        t.RoleTypeText = Utilities.EnumHelper.EnumExtensions.GetDescription(role.RoleType);
                }
            });
            return output;
        }

        public void UpdateUserState(Guid UserId)
        {
            CheckUserPermissions(new List<RoleType> { RoleType.Administrator });
            var currentUser = UserClaim();
            var targetUser = _userRepository.FirstOrDefault(t => t.Id == UserId);
            if (targetUser == null)
                throw new UserFriendlyException("用户不存在");
            targetUser.IsDeleted = !targetUser.IsDeleted;
            targetUser.UpdateTime = Clock.Now;
            targetUser.UpdateUserId = currentUser.UserId;
            _userRepository.UpdateAsync(targetUser);
        }

        public void ResetUserPassword(Guid UserId)
        {
            CheckUserPermissions(new List<RoleType> { RoleType.Administrator });
            var currentUser = UserClaim();
            var targetUser = _userRepository.FirstOrDefault(t => t.Id == UserId);
            if (targetUser == null)
                throw new UserFriendlyException("用户不存在");
            targetUser.Password = Encrypt.MD5ByHash("12345678");
            targetUser.UpdateTime = Clock.Now;
            targetUser.UpdateUserId = currentUser.UserId;
            _userRepository.UpdateAsync(targetUser);
        }

        public void InsertCampustUser(InsertCampustUserInput input)
        {
            CheckUserPermissions(new List<RoleType> { RoleType.Administrator });
            if (_userRepository.Count(t => t.UserName == input.UserName) > 0)
                throw new UserFriendlyException("用户名已被占用");
            if (input.RoleType == RoleType.Department && _departmentRepository.Count(t => t.Id == input.DepartmentId) <= 0)
                throw new UserFriendlyException("指定的系不存在");
            var currentUser = UserClaim();
            var role = _roleRepository.FirstOrDefault(t => t.RoleType == input.RoleType);
            var newUser = input.MapTo<UserEntity>();
            newUser.Id = Guid.NewGuid();
            newUser.Password = Encrypt.MD5ByHash("12345678");
            newUser.RoleId = role?.Id;
            newUser.UserType = UserType.OnCampusUser;
            newUser.CreateUserId = currentUser.UserId;
            newUser.UpdateUserId = currentUser.UserId;
            _userRepository.InsertAsync(newUser);
        }

        public void UpdateAvatar(UpdateAvatarInput input)
        {
            var userClaim = UserClaim();
            var user = _userRepository.FirstOrDefault(t => t.Id == userClaim.UserId);
            if (user != null)
            {
                user.AvatarUrl = input.Url;
                user.UpdateTime = Clock.Now;
                user.UpdateUserId = userClaim.UserId;
                _userRepository.UpdateAsync(user);
            }
        }
    }
}
