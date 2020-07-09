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
    /// 教学数据采集
    /// </summary>
    [Table("SEMS_COLLECT")]
    public class CollectEntity : BaseEntity
    {
        /// <summary>
        /// 学年
        /// </summary>
        [Column("SCHOOL_YEAR"), StringLength(16)]
        public string SchoolYear { get; set; }
        /// <summary>
        /// 截止提交时间
        /// </summary>
        [Column("DEADLINE_SUBMISSION")]
        public DateTime DeadlineSubmission { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        [Column("DESCRIPTION"), StringLength(512)]
        public string Description { get; set; }
        /// <summary>
        /// 说明 校外实习、实训基地
        /// </summary>
        [Column("BASE_DESCRIPTION"), StringLength(512)]
        public string BaseDescription { get; set; }
        /// <summary>
        /// 说明 校友会与社会合作
        /// </summary>
        [Column("COOPERATION_DESCRIPTION"), StringLength(512)]
        public string CooperationDescription { get; set; }
    }
}
