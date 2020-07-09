using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolEnterpriseManageSys.Project.Dto
{
    public class ImportOrderTrainingInput
    {
        /// <summary>
        /// 部门标识
        /// </summary>
        public string DepartmentName { get; set; }
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
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }
        /// <summary>
        /// 企业标识
        /// </summary>
        public string EnterpriseName { get; set; }
    }
}
