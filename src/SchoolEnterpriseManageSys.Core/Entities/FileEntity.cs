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
    /// 文件
    /// </summary>
    [Table("SEMS_FILE")]
    public class FileEntity : BaseEntity
    {
        /// <summary>
        /// 文件名
        /// </summary>
        [StringLength(128)]
        [Column("FILE_NAME")]
        public string FileName { get; set; }
        /// <summary>
        /// 文件路径
        /// </summary>
        [StringLength(256)]
        [Column("FILE_URL")]
        public string FileUrl { get; set; }
        /// <summary>
        /// 文件后缀
        /// </summary>
        [StringLength(16)]
        [Column("FILE_SUFFIX")]
        public string FileSuffix { get; set; }
    }
}
