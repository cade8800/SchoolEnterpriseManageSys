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
    /// 考核文件
    /// </summary>
    [Table("SEMS_ASSESSMENT_FILE")]
    public class AssessmentFileEntity : BaseEntity
    {
        /// <summary>
        /// 系考核指标标识
        /// </summary>
        [Column("DEPARTMENT_INDEX_ID")]
        [Required]
        public Guid? DepartmentIndexId { get; set; }
        /// <summary>
        /// 文件标识
        /// </summary>
        [Column("FILE_ID")]
        [Required]
        public Guid? FileId { get; set; }
    }
}
