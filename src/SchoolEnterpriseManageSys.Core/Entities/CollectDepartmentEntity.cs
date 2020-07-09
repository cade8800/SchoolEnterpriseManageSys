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
    /// 系数据采集
    /// </summary>
    [Table("SEMS_COLLECT_DEPARTMENT")]
    public class CollectDepartmentEntity : BaseEntity
    {
        /// <summary>
        /// 数据采集标识
        /// </summary>
        [Column("COLLECTION_ID")]
        public Guid CollectionId { get; set; }
        /// <summary>
        /// 系标识
        /// </summary>
        [Column("DEPARTMENT_ID")]
        public Guid DepartmentId { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Column("REMARK"), StringLength(512)]
        public string Remark { get; set; }
    }
}
