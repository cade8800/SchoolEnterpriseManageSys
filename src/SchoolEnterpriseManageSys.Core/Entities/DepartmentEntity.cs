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
    /// 部门
    /// </summary>
    [Table("SEMS_DEPARTMENT")]
    public class DepartmentEntity : BaseEntity
    {
        /// <summary>
        /// 部门名称
        /// </summary>
        [StringLength(32)]
        [Column("NAME")]
        public string Name { get; set; }
    }
}
