using SchoolEnterpriseManageSys.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolEnterpriseManageSys.Project.Dto
{
    /// <summary>
    /// 社会服务Dto
    /// </summary>
    public class SocialServiceDto
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
        public string Principal { get; set; }
        /// <summary>
        /// 企业标识
        /// </summary>
        //public Guid EnterpriseId { get; set; }
        [StringLength(64)]
        public string EnterpriseName { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// 关联项目标识 
        /// </summary>
        public Guid? RelateProjectId { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(256)]
        public string Remark { get; set; }
    }
}
