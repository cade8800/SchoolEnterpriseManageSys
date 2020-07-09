using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolEnterpriseManageSys.Project.Dto
{
    public class ProjectFileOutput
    {
        public Guid Id { get; set; }
        /// <summary>
        /// 项目标识 
        /// </summary>
        public Guid ProjectId { get; set; }
        /// <summary>
        /// 文件标识
        /// </summary>
        public Guid FileId { get; set; }

        public string Name { get; set; }
        public string Status { get; set; } = "done";
        public string Url { get; set; }
        public Guid Uid { get; set; }
    }
}
