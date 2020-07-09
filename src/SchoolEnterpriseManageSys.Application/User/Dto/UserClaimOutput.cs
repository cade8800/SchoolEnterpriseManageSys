using SchoolEnterpriseManageSys.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolEnterpriseManageSys.User.Dto
{
    public class UserClaimOutput
    {
        /// <summary>
        /// 用户标识
        /// </summary>
        public Guid UserId { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        [StringLength(32)]
        public string Nickname { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        [StringLength(32)]
        public string UserName { get; set; }

        /// <summary>
        /// 用户类型
        /// </summary>
        public UserType UserType { get; set; }

        public Guid? RoleId { get; set; }
        public string Role { get; set; }


        //public string iss { get; set; }
        //public string aud { get; set; }
        //public string exp { get; set; }
        //public string nbf { get; set; }

    }
}
