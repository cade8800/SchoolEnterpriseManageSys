using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using SchoolEnterpriseManageSys.Enum;

namespace SchoolEnterpriseManageSys.Entities
{
    /// <summary>
    /// 角色
    /// </summary>
    [Table("SEMS_ROLE")]
    public class RoleEntity : BaseEntity
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        [StringLength(32)]
        [Column("ROLE_NAME")]
        public string RoleName { get; set; }
        /// <summary>
        /// 角色类型
        /// </summary>
        [Column("ROLE_TYPE")]
        public RoleType RoleType { get; set; }
    }
}
