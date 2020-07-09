using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolEnterpriseManageSys.Entities
{
    [Table("SEMS_COLLECT_FILE")]
    public class CollectFileEntity : BaseEntity
    {
        /// <summary>
        /// 采集项标识 
        /// </summary>
        [Column("COLLECTION_ITEM_ID")]
        public Guid CollectionItemId { get; set; }
        /// <summary>
        /// 文件标识
        /// </summary>
        [Column("FILE_ID")]
        public Guid FileId { get; set; }
    }
}
