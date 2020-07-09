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
    /// 项目文件
    /// </summary>
    [Table("SEMS_PROJECT_FILE")]
    public class ProjectFileEntity : BaseEntity
    {
        /// <summary>
        /// 项目标识 
        /// </summary>
        [Column("PROJECT_ID")]
        public Guid ProjectId { get; set; }
        /// <summary>
        /// 文件标识
        /// </summary>
        [Column("FILE_ID")]
        public Guid FileId { get; set; }
        /// <summary>
        /// 文件类型
        /// </summary>
        [Column("FILE_TYPE"), StringLength(16)]
        public string FileType { get; set; }
    }
}
