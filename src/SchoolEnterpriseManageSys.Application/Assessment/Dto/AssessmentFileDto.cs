using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolEnterpriseManageSys.Assessment.Dto
{
    public class AssessmentFileDto
    {
        public Guid? Id { get; set; }
        /// <summary>
        /// 考核标识
        /// </summary>
        public Guid? DepartmentIndexId { get; set; }
        /// <summary>
        /// 文件标识
        /// </summary>
        public Guid? FileId { get; set; }

        public string Name { get; set; }
        public string Status { get; set; } = "done";
        public string Url { get; set; }
        public Guid? Uid { get; set; }
    }
}
