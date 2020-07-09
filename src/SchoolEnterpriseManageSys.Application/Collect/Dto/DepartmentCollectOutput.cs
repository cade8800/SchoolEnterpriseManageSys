using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolEnterpriseManageSys.Collect.Dto
{
    public class DepartmentCollectOutput
    {
        public Guid? DepartmentCollectId { get; set; }
        /// <summary>
        /// 数据采集标识
        /// </summary>
        public Guid? CollectionId { get; set; }
        /// <summary>
        /// 系标识
        /// </summary>
        public Guid DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        public DateTime? CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
    }
}
