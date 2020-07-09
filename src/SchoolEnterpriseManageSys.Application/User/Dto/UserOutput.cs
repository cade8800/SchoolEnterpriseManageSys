using SchoolEnterpriseManageSys.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolEnterpriseManageSys.User.Dto
{
    public class UserOutput
    {
        public Guid Id { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        [Required, StringLength(32)]
        public string UserName { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        [StringLength(32)]
        public string Nickname { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        [StringLength(32)]
        public string ActualName { get; set; }
        /// <summary>
        /// 头像路径
        /// </summary>
        [StringLength(128)]
        public string AvatarUrl { get; set; }
        /// <summary>
        /// 移动电话
        /// </summary>
        [StringLength(24)]
        public string Mobilephone { get; set; }
        /// <summary>
        /// 固定电话
        /// </summary>
        [StringLength(24)]
        public string FixedTelephone { get; set; }
        /// <summary>
        /// 电子邮箱
        /// </summary>
        [StringLength(64)]
        public string Email { get; set; }
        /// <summary>
        /// 部门标识
        /// </summary>
        public Guid? DepartmentId { get; set; }
        /// <summary>
        /// 职务
        /// </summary>
        [StringLength(16)]
        public string Position { get; set; }
        /// <summary>
        /// 角色标识
        /// </summary>
        [Required]
        public Guid? RoleId { get; set; }
        /// <summary>
        /// 用户类型
        /// </summary>
        public UserType UserType { get; set; }
        /// <summary>
        /// 最后登录时间
        /// </summary>
        public DateTime LastLoginTime { get; set; }


        public string RoleTypeText { get; set; }
        public string UserTypeText { get { return SchoolEnterpriseManageSys.Utilities.EnumHelper.EnumExtensions.GetDescription(this.UserType); } }
        public string EnterpriseName { get; set; }
        public string DepartmentName { get; set; }
        public DateTime CreateTime { get; set; }
        public bool IsDelete { get; set; }
    }
}
