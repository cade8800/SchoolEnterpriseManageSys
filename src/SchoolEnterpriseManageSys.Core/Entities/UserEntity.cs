using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SchoolEnterpriseManageSys.Enum;
using Abp.Timing;

namespace SchoolEnterpriseManageSys.Entities
{
    /// <summary>
    /// 用户
    /// </summary>
    [Table("SEMS_USER")]
    public class UserEntity : BaseEntity
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Required, StringLength(32)]
        [Column("USERNAME")]
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Column("PASSWORD")]
        [Required, StringLength(128)]
        public string Password { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        [Column("NICKNAME")]
        [StringLength(32)]
        public string Nickname { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        [StringLength(32)]
        [Column("ACTUAL_NAME")]
        public string ActualName { get; set; }
        /// <summary>
        /// 头像路径
        /// </summary>
        [StringLength(128)]
        [Column("AVATAR_URL")]
        public string AvatarUrl { get; set; }
        /// <summary>
        /// 移动电话
        /// </summary>
        [StringLength(24)]
        [Column("MOBILEPHONE")]
        public string Mobilephone { get; set; }
        /// <summary>
        /// 固定电话
        /// </summary>
        [StringLength(24)]
        [Column("FIXED_TELEPHONE")]
        public string FixedTelephone { get; set; }
        /// <summary>
        /// 电子邮箱
        /// </summary>
        [StringLength(64)]
        [Column("EMAIL")]
        public string Email { get; set; }
        /// <summary>
        /// 部门标识
        /// </summary>
        [Column("DEPARTMENT_ID")]
        public Guid? DepartmentId { get; set; }
        /// <summary>
        /// 职务
        /// </summary>
        [StringLength(16)]
        [Column("POSITION")]
        public string Position { get; set; }
        /// <summary>
        /// 角色标识
        /// </summary>
        [Required]
        [Column("ROLE_ID")]
        public Guid? RoleId { get; set; }
        /// <summary>
        /// 用户类型
        /// </summary>
        [Column("USER_TYPE")]
        public UserType UserType { get; set; }
        /// <summary>
        /// 最后登录时间
        /// </summary>
        [Column("LAST_LOGIN_TIME")]
        public DateTime LastLoginTime { get; set; } = Clock.Now;
    }
}
