using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolEnterpriseManageSys.User.Dto
{
    public class GetUserInfoOutput
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string Nickname { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string ActualName { get; set; }
        /// <summary>
        /// 头像路径
        /// </summary>
        public string AvatarUrl { get; set; }
        /// <summary>
        /// 移动电话
        /// </summary>
        public string Mobilephone { get; set; }
        /// <summary>
        /// 固定电话
        /// </summary>
        public string FixedTelephone { get; set; }
        /// <summary>
        /// 电子邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 职务
        /// </summary>
        public string Position { get; set; }
    }
}
