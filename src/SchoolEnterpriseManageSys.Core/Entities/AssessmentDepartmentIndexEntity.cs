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
    /// 考核系指标
    /// </summary>
    [Table("SEMS_DEPARTMENT_INDEX")]
    public class AssessmentDepartmentIndexEntity : BaseEntity
    {
        /// <summary>
        /// 考核系标识 
        /// </summary>
        [Column("ASSESSMENT_DEPARTMENT_ID")]
        [Required]
        public Guid? AssessmentDepartmentId { get; set; }
        /// <summary>
        /// 考核指标标识
        /// </summary>
        [Column("ASSESSMENT_INDEX_ID")]
        [Required]
        public Guid? AssessmentIndexId { get; set; }


        /// <summary>
        /// 自评分
        /// </summary>
        [Column("SELF_EVALUATION_SCORE")]
        public decimal SelfEvaluationScore { get; set; }
        /// <summary>
        /// 自评语
        /// </summary>
        [Column("SELF_EVALUATION")]
        [StringLength(512)]
        public string SelfEvaluation { get; set; }
        /// <summary>
        /// 专家评分
        /// </summary>
        [Column("EXPERT_RATING_SCORE")]
        public decimal ExpertRatingScore { get; set; }
        /// <summary>
        /// 专家评语
        /// </summary>
        [Column("EXPERT_RATING")]
        [StringLength(512)]
        public string ExpertRating { get; set; }
    }
}
