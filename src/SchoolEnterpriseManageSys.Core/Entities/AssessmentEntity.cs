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
    /// 考核
    /// </summary>
    [Table("SEMS_ASSESSMENT")]
    public class AssessmentEntity : BaseEntity
    {
        /// <summary>
        /// 学年
        /// </summary>
        [Column("SCHOOL_YEAR")]
        [StringLength(16)]
        public string SchoolYear { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        [Column("START_TIME")]
        [Required]
        public DateTime? StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        [Column("END_TIME")]
        [Required]
        public DateTime? EndTime { get; set; }
        /// <summary>
        /// 截止提交时间
        /// </summary>
        [Column("DEADLINE")]
        [Required]
        public DateTime? Deadline { get; set; }
    }
}
