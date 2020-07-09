using SchoolEnterpriseManageSys.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolEnterpriseManageSys.Project.Dto
{
    public class ImportCoAuthoredBookOrCourseInput
    {
        /// <summary>
        /// 部门标识
        /// </summary>
        public string DepartmentName { get; set; }
        /// <summary>
        /// 共编教材或课程
        /// </summary>
        public CoAuthoredType? CoAuthoredType { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        [StringLength(32)]
        public string ProjectName { get; set; }
        /// <summary>
        /// 负责人
        /// </summary>
        [StringLength(32)]
        public string Principal { get; set; }
        /// <summary>
        /// 专业/学科
        /// </summary>
        [StringLength(32)]
        public string Science { get; set; }
        /// <summary>
        /// 企业标识
        /// </summary>
        public string EnterpriseName { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }
        /// <summary>
        /// ISBN号
        /// </summary>
        [StringLength(32)]
        public string ISBN { get; set; }
    }
}
