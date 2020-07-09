using SchoolEnterpriseManageSys.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolEnterpriseManageSys.Entities
{
    /// <summary>
    /// 项目类型
    /// </summary>
    [Table("SEMS_PROJECT_TYPE")]
    public class ProjectTypeEntity : BaseEntity
    {
        /// <summary>
        /// 类型名称
        /// </summary>
        [Column("PROJECT_TYPE_NAME")]
        [StringLength(32)]
        public string ProjectTypeName { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        [Column("TYPE")]
        public ProjectType Type { get; set; }
        /// <summary>
        /// 填写说明
        /// </summary>
        [Column("INSTRUCTIONS")]
        [StringLength(1024)]
        public string Instructions { get; set; }
        /// <summary>
        /// 上传文件说明
        /// </summary>
        [Column("UPLOAD_FILE_DESCRIPTION"), StringLength(128)]
        public string UploadFileDescription { get; set; }
    }
}
