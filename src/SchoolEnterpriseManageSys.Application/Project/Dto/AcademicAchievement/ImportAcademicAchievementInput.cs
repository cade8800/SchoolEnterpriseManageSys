using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolEnterpriseManageSys.Project.Dto
{
    public class ImportAcademicAchievementInput
    {
        /// <summary>
        /// 部门标识
        /// </summary>
        public string DepartmentName { get; set; }
        /// <summary>
        /// 负责人
        /// </summary>
        [StringLength(32, ErrorMessage = "作者名称不可超出32字符")]
        [Required]
        public string Principal { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        [StringLength(32, ErrorMessage = "论文名称不可超出32字符")]
        [Required]
        public string ProjectName { get; set; }
        /// <summary>
        /// 刊物名称
        /// </summary>
        [StringLength(32)]
        [Required]
        public string PublicationName { get; set; }
        /// <summary>
        /// 刊物主办单位
        /// </summary>
        [StringLength(32)]
        [Required]
        public string PublicationsOrganizer { get; set; }
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
        /// 开始时间
        /// </summary>
        [Required(ErrorMessage = "发表时间不可为空")]
        public DateTime? StartTime { get; set; }
    }
}
