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
    /// 用户消息
    /// </summary>
    [Table("SEMS_USER_MESSAGE")]
    public class UserMessageEntity : BaseEntity
    {
        /// <summary>
        /// 内容
        /// </summary>
        [StringLength(32)]
        [Column("CONTENT")]
        public string Content { get; set; }
        /// <summary>
        /// 消息类型
        /// </summary>
        [Column("TYPE")]
        public int Type { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        [Column("STATUS")]
        public int Status { get; set; }
        /// <summary>
        /// 菜单标识
        /// </summary>
        [Column("MENU_ID")]
        public Guid MenuId { get; set; }
        /// <summary>
        /// 用户标识
        /// </summary>
        [Column("USER_ID")]
        public Guid UserId { get; set; }
    }
}
