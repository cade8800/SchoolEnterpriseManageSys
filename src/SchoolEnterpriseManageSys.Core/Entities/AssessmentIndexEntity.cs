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
    /// 考核指标
    /// </summary>
    [Table("SEMS_ASSESSMENT_INDEX")]
    public class AssessmentIndexEntity : BaseEntity
    {
        /// <summary>
        /// 指标类型
        /// </summary>
        [Column("INDEX_TYPE")]
        [StringLength(16)]
        public string IndexType { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        [Column("SORT")]
        public int Sort { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        [Column("CONTENT"), StringLength(512)]
        public string Content { get; set; }
        /// <summary>
        /// 完成标准
        /// </summary>
        [Column("COMPLETE_STANDARD"), StringLength(512)]
        public string CompleteStandard { get; set; }
        /// <summary>
        /// 标准分
        /// </summary>
        [Column("STANDARD_SCORE")]
        public decimal StandardScore { get; set; }
        /// <summary>
        /// 关联合作项目类型
        /// </summary>
        [Column("ASSOCIAT_PROJECT_TYPE")]
        public ProjectType? AssociatProjectType { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Column("REMARK"), StringLength(512)]
        public string Remark { get; set; }
    }
}
