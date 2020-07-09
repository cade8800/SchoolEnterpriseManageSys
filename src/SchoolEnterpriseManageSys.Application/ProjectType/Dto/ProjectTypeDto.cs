using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolEnterpriseManageSys.ProjectType.Dto
{
    public class ProjectTypeDto
    {
        [Required]
        public Guid? Id { get; set; }

        public Enum.ProjectType Type { get; set; }
        /// <summary>
        /// 类型名称
        /// </summary>
        [StringLength(32)]
        public string ProjectTypeName { get; set; }
        /// <summary>
        /// 填写说明
        /// </summary>
        [StringLength(1024)]
        public string Instructions { get; set; }
        /// <summary>
        /// 上传文件说明
        /// </summary>
        [StringLength(128)]
        public string UploadFileDescription { get; set; }
    }
}
