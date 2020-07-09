using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Abp.Application.Services;
using Abp.Web.Models;
using SchoolEnterpriseManageSys.Enum;
using SchoolEnterpriseManageSys.User.Dto;

namespace SchoolEnterpriseManageSys.User
{
    public interface IUserService : IApplicationService
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [IgnoreWebApi]
        LoginOutput Login(LoginInput input);

        /// <summary>
        /// 企业注册
        /// </summary>
        /// <param name="input"></param>
        void EnterpriseRegist(EnterpriseRegistInput input);

        /// <summary>
        /// 通过token解析当前用户信息
        /// </summary>
        /// <returns></returns>
        [Authorize]
        UserClaimOutput UserClaim();

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="input"></param>
        [Authorize]
        void UpdatePassword(UpdatePasswordInput input);

        /// <summary>
        /// 获取用户详细资料
        /// </summary>
        /// <returns></returns>
        [Authorize]
        GetUserInfoOutput GetUserInfo();

        /// <summary>
        /// 更新用户资料
        /// </summary>
        /// <param name="input"></param>
        [Authorize]
        void UpdateUserInfo(GetUserInfoOutput input);

        /// <summary>
        /// 检查用户权限
        /// </summary>
        /// <param name="roleTypes"></param>
        [Authorize]
        void CheckUserPermissions(List<RoleType> roleTypes);

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize]
        GetUsersOutput GetUsers(GetUsersInput input);

        /// <summary>
        /// 启用/停用用户
        /// </summary>
        /// <param name="UserId"></param>
        [Authorize]
        void UpdateUserState(Guid UserId);

        /// <summary>
        /// 重置用户密码（重置后密码：12345678）
        /// </summary>
        /// <param name="UserId"></param>
        [Authorize]
        void ResetUserPassword(Guid UserId);

        /// <summary>
        /// 增加校内用户
        /// </summary>
        /// <param name="input"></param>
        [Authorize]
        void InsertCampustUser(InsertCampustUserInput input);

        /// <summary>
        /// 更新用户头像
        /// </summary>
        /// <param name="input"></param>
        [Authorize]
        void UpdateAvatar(UpdateAvatarInput input);
    }
}
