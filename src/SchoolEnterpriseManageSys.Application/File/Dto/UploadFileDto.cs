using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolEnterpriseManageSys.File.Dto
{
    public class UploadFileDto
    {
        public Guid Id { get; set; }
        /// <summary>
        /// 文件名
        /// </summary>
        [StringLength(128)]
        public string FileName { get; set; }
        /// <summary>
        /// 文件路径
        /// </summary>
        [StringLength(256)]
        public string FileUrl { get; set; }
        /// <summary>
        /// 文件后缀
        /// </summary>
        [StringLength(16)]
        public string FileSuffix { get; set; }
    }
}
