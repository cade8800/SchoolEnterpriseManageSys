using SchoolEnterpriseManageSys.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolEnterpriseManageSys.Project.Dto
{
   public class InsertProjectInput 
    {
        /// <summary>
        /// 企业标识
        /// </summary>
        [Column("ENTERPRISE_ID")]
        public Guid? EnterpriseId { get; set; }
        /// <summary>
        /// 部门标识
        /// </summary>
        [Column("DEPARTMENT_ID")]
        public Guid? DepartmentId { get; set; }
        /// <summary>
        /// 负责人
        /// </summary>
        [Column("PRINCIPAL"), StringLength(32)]
        public string Principal { get; set; }
        /// <summary>
        /// 联系方式
        /// </summary>
        [Column("CONTACT"), StringLength(32)]
        public string Contact { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        [Column("START_TIME")]
        public DateTime? StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        [Column("END_TIME")]
        public DateTime? EndTime { get; set; }
        /// <summary>
        /// 申报等级 校/省/部
        /// </summary>
        [Column("REPORT_LEVEL")]
        public ReportLevel? ReportLevel { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        [Column("AMOUNT")]
        public decimal Amount { get; set; }
        /// <summary>
        /// 专业/学科
        /// </summary>
        [Column("SCIENCE"), StringLength(32)]
        public string Science { get; set; }
        /// <summary>
        /// 班级名称
        /// </summary>
        [Column("CLASS_NAME"), StringLength(32)]
        public string ClassName { get; set; }
        /// <summary>
        /// 班级学生人数
        /// </summary>
        [Column("CLASS_STUDENT_COUNT")]
        public int ClassStudentCount { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        [Column("PROJECTN_AME"), StringLength(32)]
        public string ProjectName { get; set; }
        /// <summary>
        /// 共编教材或课程
        /// </summary>
        [Column("COAUTHORED_TYPE")]
        public CoAuthoredType? CoAuthoredType { get; set; }
        /// <summary>
        /// ISBN号
        /// </summary>
        [Column("ISBN"), StringLength(32)]
        public string ISBN { get; set; }
        /// <summary>
        /// ISSN号
        /// </summary>
        [Column("ISSN"), StringLength(32)]
        public string ISSN { get; set; }
        /// <summary>
        /// CN号
        /// </summary>
        [Column("CN"), StringLength(32)]
        public string CN { get; set; }
        /// <summary>
        /// 刊物名称
        /// </summary>
        [Column("PUBLICATION_NAME"), StringLength(32)]
        public string PublicationName { get; set; }
        /// <summary>
        /// 刊物主办单位
        /// </summary>
        [Column("PUBLICATIONS_ORGANIZER"), StringLength(32)]
        public string PublicationsOrganizer { get; set; }

        /// <summary>
        /// 关联项目标识 
        /// </summary>
        [Column("RELATE_PROJECT_ID")]
        public Guid? RelateProjectId { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Column("REMARK"), StringLength(256)]
        public string Remark { get; set; }


        public List<Guid> FileIdList { get; set; } = new List<Guid>();
    }
}
