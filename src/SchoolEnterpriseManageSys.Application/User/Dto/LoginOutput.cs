using SchoolEnterpriseManageSys.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;
using SchoolEnterpriseManageSys.Entities;

namespace SchoolEnterpriseManageSys.User.Dto
{
    [AutoMap(typeof(UserEntity))]
    public class LoginOutput
    {
        /// <summary>
        /// 用户标识
        /// </summary>
        public Guid? Id { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 角色标识
        /// </summary>
        public Guid? RoleId { get; set; }
        /// <summary>
        /// 用户类型
        /// </summary>
        public UserType? UserType { get; set; }

        public string Role { get; set; }


        /// <summary>
        /// 昵称
        /// </summary>
        public string Nickname { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string ActualName { get; set; }
    }
}
