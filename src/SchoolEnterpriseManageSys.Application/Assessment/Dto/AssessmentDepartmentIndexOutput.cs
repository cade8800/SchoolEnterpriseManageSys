using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolEnterpriseManageSys.Assessment.Dto
{
    public class AssessmentDepartmentIndexOutput
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
        /// 自评分
        /// </summary>
        public decimal SelfEvaluationScore { get; set; }
        /// <summary>
        /// 自评语
        /// </summary>
        [StringLength(512)]
        public string SelfEvaluation { get; set; }
        /// <summary>
        /// 专家评分
        /// </summary>
        public decimal ExpertRatingScore { get; set; }
        /// <summary>
        /// 专家评语
        /// </summary>
        [StringLength(512)]
        public string ExpertRating { get; set; }


        /// <summary>
        /// 指标类型
        /// </summary>
        [StringLength(16)]
        public string IndexType { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        [StringLength(512)]
        public string Content { get; set; }
        /// <summary>
        /// 完成标准
        /// </summary>
        [StringLength(512)]
        public string CompleteStandard { get; set; }
        /// <summary>
        /// 标准分
        /// </summary>
        public decimal StandardScore { get; set; }


        public List<AssessmentFileDto> FileList { get; set; } = new List<AssessmentFileDto>();

        public List<AssessmentProjectOutput> ProjectList { get; set; } = new List<AssessmentProjectOutput>();
    }
}
