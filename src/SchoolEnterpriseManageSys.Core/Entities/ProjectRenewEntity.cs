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
    /// 项目续约
    /// </summary>
    [Table("SEMS_PROJECT_RENEW")]
    public class ProjectRenewEntity : BaseEntity
    {
        /// <summary>
        /// 发起用户标识
        /// </summary>
        [Column("INITIATOR_USER_ID")]
        public Guid InitiatorUserId { get; set; }
        /// <summary>
        /// 处理用户标识
        /// </summary>
        [Column("PROCESSOR_USER_ID")]
        public Guid ProcessorUserId { get; set; }
        /// <summary>
        /// 项目标识
        /// </summary>
        [Column("PROJECT_ID")]
        public Guid ProjectId { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        [Column("STATUS")]
        public int Status { get; set; }
    }
}
