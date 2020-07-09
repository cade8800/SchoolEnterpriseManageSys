using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace SchoolEnterpriseManageSys.Entities
{
    /// <summary>
    /// 角色权限
    /// </summary>
    [Table("SEMS_ROLE_PERMISSIONS")]
    public class RolePermissionsEntity : BaseEntity
    {
        /// <summary>
        /// 角色标识
        /// </summary>
        [Required]
        [Column("ROLE_ID")]
        public Guid? RoleId { get; set; }
        /// <summary>
        /// 菜单标识
        /// </summary>
        [Required]
        [Column("MENU_ID")]
        public Guid? MenuId { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        [Column("STATUS")]
        public int Status { get; set; }
    }
}
