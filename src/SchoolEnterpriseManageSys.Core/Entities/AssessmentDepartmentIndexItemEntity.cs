using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace SchoolEnterpriseManageSys.Entities
{
    /// <summary>
    /// 考核系指标项
    /// </summary>
    [Table("SEMS_DEPARTMENT_INDEX_ITEM")]
    public class AssessmentDepartmentIndexItemEntity : BaseEntity
    {
        /// <summary>
        /// 考核系指标标识
        /// </summary>
        [Column("ASSESSMENT_DEPARTMENT_INDEX_ID")]
        public Guid AssessmentDepartmentIndexId { get; set; }
        /// <summary>
        /// 文件标识 
        /// </summary>
        [Column("FILE_ID")]
        public Guid FileId { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [Column("DESCRIPTION"), StringLength(256)]
        public string Description { get; set; }
    }
}
