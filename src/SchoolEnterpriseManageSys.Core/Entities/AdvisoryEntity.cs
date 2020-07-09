using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using SchoolEnterpriseManageSys.Enum;

namespace SchoolEnterpriseManageSys.Entities
{
    /// <summary>
    /// 咨询
    /// </summary>
    [Table("SEMS_ADVISORY")]
    public class AdvisoryEntity : BaseEntity
    {
        /// <summary>
        /// 内容
        /// </summary>
        [StringLength(512)]
        [Column("CONTENT")]
        public string Content { get; set; }
        /// <summary>
        /// 发起者
        /// </summary>
        [Required]
        [Column("INITIATOR_USER_ID")]
        public Guid? InitiatorUserId { get; set; }
        /// <summary>
        /// 发起者角色
        /// </summary>
        [Column("INITIATOR_USER_TYPE")]
        public RoleType InitiatorUserType { get; set; }
        /// <summary>
        /// 接受者
        /// </summary>
        [Column("RECIPIENT_USER_ID")]
        public Guid? RecipientUserId { get; set; }
    }
}
