using Abp.Domain.Entities;
using Abp.Timing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolEnterpriseManageSys.Entities
{
    public class BaseEntity : Entity<Guid>
    {
        [Column("ID")]
        public override Guid Id { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [Column("CREATE_TIME")]
        public DateTime CreateTime { get; set; } = Clock.Now;
        /// <summary>
        /// 创建用户标识
        /// </summary>
        [Column("CREATE_USER_ID")]
        public Guid? CreateUserId { get; set; }
        /// <summary>
        /// 最后更新时间
        /// </summary>
        [Column("UPDATE_TIME")]
        public DateTime UpdateTime { get; set; } = Clock.Now;
        /// <summary>
        /// 更新人标识 
        /// </summary>
        [Column("UPDATE_USER_ID")]
        public Guid? UpdateUserId { get; set; }
        /// <summary>
        /// 是否已删除
        /// </summary>
        [Column("IS_DELETED")]
        public bool IsDeleted { get; set; } = false;
    }
}
