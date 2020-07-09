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
    /// 考核系
    /// </summary>
    [Table("SEMS_ASSESSMENT_DEPARTMENT")]
    public class AssessmentDepartmentEntity : BaseEntity
    {
        /// <summary>
        /// 考核标识
        /// </summary>
        [Column("ASSESSMENT_ID")]
        [Required]
        public Guid? AssessmentId { get; set; }
        /// <summary>
        /// 系标识
        /// </summary>
        [Column("DEPARTMENT_ID")]
        [Required]
        public Guid? DepartmentId { get; set; }
    }
}
