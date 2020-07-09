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
    /// 企业预约
    /// </summary>
    [Table("SEMS_APPOINTMENT")]
    public class AppointmentEntity : BaseEntity
    {
        /// <summary>
        /// 企业标识
        /// </summary>
        [Column("ENTERPRISE_ID")]
        public Guid EnterpriseId { get; set; }
        /// <summary>
        /// 部门标识
        /// </summary>
        [Column("DEPARTMENT_ID")]
        public Guid? DepartmentId { get; set; }
        /// <summary>
        /// 到访时间
        /// </summary>
        [Column("VISITS_TIME")]
        public DateTime VisitsTime { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        [StringLength(256)]
        [Column("CONTENT")]
        public string Content { get; set; }
        /// <summary>
        /// 是否已确认
        /// </summary>
        [Column("IS_CONFIRM")]
        public bool IsConfirm { get; set; } = false;
    }
}
