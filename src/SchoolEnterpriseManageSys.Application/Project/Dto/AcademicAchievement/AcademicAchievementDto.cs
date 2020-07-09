using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolEnterpriseManageSys.Project.Dto
{
    public class AcademicAchievementDto
    {
        public Guid? Id { get; set; }
        /// <summary>
        /// 部门标识
        /// </summary>
        public Guid? DepartmentId { get; set; }
        /// <summary>
        /// 负责人
        /// </summary>
        [StringLength(32)]
        [Required]
        public string Principal { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        [StringLength(32)]
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
        [Required]
        public DateTime? StartTime { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(256)]
        public string Remark { get; set; }


        /// <summary>
        /// 关联项目标识 
        /// </summary>
        public Guid? RelateProjectId { get; set; }
    }
}
