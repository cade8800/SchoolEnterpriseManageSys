using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolEnterpriseManageSys.Advisory.Dto;
using SchoolEnterpriseManageSys.User;
using Abp.Domain.Repositories;
using SchoolEnterpriseManageSys.Entities;
using Abp.AutoMapper;
using Abp.UI;

namespace SchoolEnterpriseManageSys.Advisory
{
    public class AdvisoryService : IAdvisoryService
    {
        private readonly IUserService _userService;
        private readonly IRepository<AdvisoryEntity, Guid> _advisoryRepository;
        private readonly IRepository<UserEntity, Guid> _userRepository;
        private readonly IRepository<RoleEntity, Guid> _roleRepository;
        private readonly IRepository<DepartmentEntity, Guid> _departmentRepository;
        private readonly IRepository<EnterpriseInfoEntity, Guid> _enterpriseRepository;

        public AdvisoryService(
            IUserService userService,
            IRepository<AdvisoryEntity, Guid> advisoryRepository,
            IRepository<UserEntity, Guid> userRepository,
            IRepository<RoleEntity, Guid> roleRepository,
            IRepository<DepartmentEntity, Guid> departmentRepository,
            IRepository<EnterpriseInfoEntity, Guid> enterpriseRepository
            )
        {
            _userService = userService;
            _advisoryRepository = advisoryRepository;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _departmentRepository = departmentRepository;
            _enterpriseRepository = enterpriseRepository;
        }

        public GetAdvisoryOutput Get(GetAdvisoryInput input)
        {
            _userService.CheckUserPermissions(new List<Enum.RoleType> { Enum.RoleType.Administrator, Enum.RoleType.Department, Enum.RoleType.Enterprise });
            var userClaim = _userService.UserClaim();
            var targetId = input.EnterpriseUserId.HasValue ? input.EnterpriseUserId : userClaim.UserId;
            var advisoryList = _advisoryRepository.GetAll().Where(t => t.InitiatorUserId == targetId || t.RecipientUserId == targetId && t.IsDeleted == false).OrderByDescending(t => t.CreateTime).ToList();

            #region 限制管理员仅可查看自己的咨询或回复
            if (input.EnterpriseUserId.HasValue)
                advisoryList = advisoryList.Where(t => t.RecipientUserId == userClaim.UserId || t.RecipientUserId == null || t.InitiatorUserId == userClaim.UserId).ToList();
            #endregion

            var output = new GetAdvisoryOutput
            {
                AdvisoryList = advisoryList.MapTo<List<AdvisoryDto>>()
            };
            output.AdvisoryList.ForEach(t =>
            {
                var initiator = _userRepository.FirstOrDefault(u => u.Id == t.InitiatorUserId);
                t.InitiatorAvatarUrl = !string.IsNullOrEmpty(initiator.AvatarUrl) ? initiator.AvatarUrl : "./assets/tmp/img/avatar.png";
                t.InitiatorName = GetRolePositionName(initiator);

                var recipient = _userRepository.FirstOrDefault(u => u.Id == t.RecipientUserId);
                t.RecipientUserName = GetRolePositionName(recipient);
            });
            return output;
        }

        public GetEnterpriseAdvisoryOutput GetEnterpriseAdvisory(GetEnterpriseAdvisoryInput input)
        {
            var query = _advisoryRepository.GetAll()
                .Where(t => t.InitiatorUserType == Enum.RoleType.Enterprise && t.IsDeleted == false)
                .GroupBy(t => t.InitiatorUserId).Select(t => t.OrderByDescending(a => a.CreateTime).FirstOrDefault());
            var output = new GetEnterpriseAdvisoryOutput
            {
                TotalCount = query.Count()
            };
            var advisoryList = query
                .OrderByDescending(t => t.CreateTime)
                .Skip(input.PageSize * (input.PageIndex - 1))
                .Take(input.PageSize).ToList();
            output.AdvisoryList = advisoryList.MapTo<List<AdvisoryDto>>();
            output.AdvisoryList.ForEach(t =>
            {
                var user = _userRepository.FirstOrDefault(u => u.Id == t.InitiatorUserId);
                t.InitiatorName = GetRolePositionName(user);
            });
            return output;
        }

        public void Insert(InsertAdvisoryInput input)
        {
            _userService.CheckUserPermissions(new List<Enum.RoleType> { Enum.RoleType.Administrator, Enum.RoleType.Enterprise, Enum.RoleType.Department });
            var userClaim = _userService.UserClaim();

            var advisory = new AdvisoryEntity
            {
                Id = Guid.NewGuid(),
                Content = input.Content,
                CreateUserId = userClaim.UserId,
                InitiatorUserId = userClaim.UserId,
                //RecipientUserId
                //InitiatorUserType
            };

            if (userClaim.Role == "administrator")
            {
                advisory.InitiatorUserType = Enum.RoleType.Administrator;
                if (!input.RecipientUserId.HasValue) throw new UserFriendlyException("请求错误");
                advisory.RecipientUserId = input.RecipientUserId;
            }
            else if (userClaim.Role == "enterprise")
            {
                if (_enterpriseRepository.Count(t => t.Id == userClaim.UserId) < 1) throw new UserFriendlyException("请先完善企业资料、个人资料");
                advisory.InitiatorUserType = Enum.RoleType.Enterprise;
                var lastInitiatorUserId = _advisoryRepository.GetAll().Where(t => t.RecipientUserId == userClaim.UserId).OrderByDescending(t => t.CreateTime).Take(1).Select(t => t.InitiatorUserId).FirstOrDefault();
                advisory.RecipientUserId = lastInitiatorUserId;
            }
            else if (userClaim.Role == "department")
            {
                advisory.InitiatorUserType = Enum.RoleType.Department;
                if (!input.RecipientUserId.HasValue) throw new UserFriendlyException("请求错误");
                advisory.RecipientUserId = input.RecipientUserId;
            }
            _advisoryRepository.InsertAsync(advisory);
        }

        /// <summary>
        /// 根据用户角色类型获取用户单位+职务+真实姓名
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private string GetRolePositionName(UserEntity user)
        {
            if (user == null) return string.Empty;
            string targetName = string.Empty;
            var initiatorRole = _roleRepository.FirstOrDefault(r => r.Id == user.RoleId && r.IsDeleted == false);
            if (initiatorRole != null)
            {
                switch (initiatorRole.RoleType)
                {
                    case Enum.RoleType.Administrator:
                        targetName = string.IsNullOrEmpty(user.Position) ? "校企办管理员" : user.Position + " " + user.ActualName;
                        break;
                    case Enum.RoleType.Department:
                        var dep = _departmentRepository.FirstOrDefault(d => d.Id == user.DepartmentId);
                        targetName = dep?.Name + " " + (string.IsNullOrEmpty(user.Position) ? "系管理员" : user.Position) + " " + user.ActualName;
                        break;
                    case Enum.RoleType.Enterprise:
                        var enterprise = _enterpriseRepository.FirstOrDefault(e => e.Id == user.Id);
                        targetName = string.IsNullOrEmpty(enterprise?.FullName) ? enterprise?.NameAbbreviation : enterprise?.FullName + " " + user.Position + " " + user.ActualName;
                        break;
                }
            }
            return targetName;
        }
    }
}
