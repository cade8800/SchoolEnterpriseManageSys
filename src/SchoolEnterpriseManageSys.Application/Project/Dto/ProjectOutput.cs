using SchoolEnterpriseManageSys.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolEnterpriseManageSys.Project.Dto
{
    public class ProjectOutput
    {
        public Guid Id { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public Enum.ProjectType Type { get; set; }
        /// <summary>
        /// 类型标识
        /// </summary>
        public Guid ProjectTypeId { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 项目编号
        /// </summary>
        [StringLength(32)]
        public string Number { get; set; }

        ///// <summary>
        ///// 编号值
        ///// </summary>
        ////[Column("NUMBER_VALUE")]
        ////public int NumberValue { get; set; }

        /// <summary>
        /// 企业标识
        /// </summary>
        public Guid? EnterpriseId { get; set; }
        public string EnterpriseName { get; set; }
        
        /// <summary>
        /// 部门标识
        /// </summary>
        public Guid? DepartmentId { get; set; }
        /// <summary>
        /// 负责人
        /// </summary>
        [StringLength(32)]
        public string Principal { get; set; }
        /// <summary>
        /// 联系方式
        /// </summary>
        [StringLength(32)]
        public string Contact { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
        /// <summary>
        /// 申报等级 校/省/部
        /// </summary>
        public ReportLevel? ReportLevel { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// 专业/学科
        /// </summary>
        [StringLength(32)]
        public string Science { get; set; }
        /// <summary>
        /// 班级名称
        /// </summary>
        [StringLength(32)]
        public string ClassName { get; set; }
        /// <summary>
        /// 班级学生人数
        /// </summary>
        public int ClassStudentCount { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        [StringLength(32)]
        public string ProjectName { get; set; }
        /// <summary>
        /// 共编教材或课程
        /// </summary>
        public CoAuthoredType? CoAuthoredType { get; set; }
        /// <summary>
        /// ISBN号
        /// </summary>
        [StringLength(32)]
        public string ISBN { get; set; }
        /// <summary>
        /// ISSN号
        /// </summary>
        [StringLength(32)]
        public string ISSN { get; set; }
        /// <summary>
        /// CN号
        /// </summary>
        [StringLength(32)]
        public string CN { get; set; }
        /// <summary>
        /// 刊物名称
        /// </summary>
        [StringLength(32)]
        public string PublicationName { get; set; }
        /// <summary>
        /// 刊物主办单位
        /// </summary>
        [StringLength(32)]
        public string PublicationsOrganizer { get; set; }

        /// <summary>
        /// 关联项目标识 
        /// </summary>
        public Guid? RelateProjectId { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(256)]
        public string Remark { get; set; }


        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }








        #region 自定义属性

        public string RelateProjectTypeName { get; set; }
        public string RelateProjectName { get; set; }
        public string DepartmentName { get; set; }

        /// <summary>
        /// 校内/外基地到期或即将到期说明
        /// </summary>
        public string OverdueShow { get; set; }

        public string CoAuthoredTypeText
        {
            get
            {
                return this.CoAuthoredType.HasValue ? Utilities.EnumHelper.EnumExtensions.GetDescription(this.CoAuthoredType) : "";
            }
        }
        public string ReportLevelText
        {
            get
            {
                return this.ReportLevel.HasValue ? Utilities.EnumHelper.EnumExtensions.GetDescription(this.ReportLevel) : "";
            }
        }



        public List<ProjectFileOutput> FileList { get; set; } = new List<ProjectFileOutput>();

        public List<BeAssociatedProjectDto> BeAssociatedProjectList { get; set; } = new List<BeAssociatedProjectDto>();

        #endregion
    }
}
