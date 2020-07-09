using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolEnterpriseManageSys.Assessment.Dto
{
    public class IndexDto
    {
        public Guid? Id { get; set; }
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
        /// <summary>
        /// 关联合作项目类型
        /// </summary>
        public Enum.ProjectType? AssociatProjectType { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(512)]
        public string Remark { get; set; }
        /// <summary>
        /// 是否已删除
        /// </summary>
        public bool IsDeleted { get; set; }


        public string AssociatProjectTypeText
        {
            get
            {
                return this.AssociatProjectType.HasValue ? Utilities.EnumHelper.EnumExtensions.GetDescription(this.AssociatProjectType) : "";
            }
        }
    }
}
