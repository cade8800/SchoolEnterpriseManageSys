using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolEnterpriseManageSys.Assessment.Dto
{
    public class ExpertRatingInput
    {
        public Guid? Id { get; set; }
        /// <summary>
        /// 考核系标识 
        /// </summary>
        [Required]
        public Guid? AssessmentDepartmentId { get; set; }
        /// <summary>
        /// 考核指标标识
        /// </summary>
        [Required]
        public Guid? AssessmentIndexId { get; set; }
        /// <summary>
        /// 专家评分
        /// </summary>
        public decimal ExpertRatingScore { get; set; }
        /// <summary>
        /// 专家评语
        /// </summary>
        [StringLength(512)]
        public string ExpertRating { get; set; }
    }
}
