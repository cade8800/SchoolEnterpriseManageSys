using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolEnterpriseManageSys.Project.Dto
{
    public class BeAssociatedProjectDto
    {
        public Guid Id { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public Enum.ProjectType Type { get; set; }
        public string TypeText
        {
            get
            {
                return Utilities.EnumHelper.EnumExtensions.GetDescription(this.Type);
            }
        }
        /// <summary>
        /// 类型标识
        /// </summary>
        public Guid ProjectTypeId { get; set; }
        /// <summary>
        /// 项目编号
        /// </summary>
        [StringLength(32)]
        public string Number { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        [StringLength(32)]
        public string ProjectName { get; set; }
        /// <summary>
        /// 关联项目标识 
        /// </summary>
        public Guid? RelateProjectId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
