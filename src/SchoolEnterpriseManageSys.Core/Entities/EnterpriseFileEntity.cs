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
    /// 企业文件
    /// </summary>
    [Table("SEMS_ENTERPRISE_FILE")]
    public class EnterpriseFileEntity : BaseEntity
    {
        /// <summary>
        /// 用户标识
        /// </summary>
        [Required]
        [Column("USER_ID")]
        public Guid? UserId { get; set; }
        /// <summary>
        /// 文件标识
        /// </summary>
        [Required]
        [Column("FILE_ID")]
        public Guid? FileId { get; set; }
    }
}
