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
    /// 菜单
    /// </summary>
    [Table("SEMS_MENU")]
    public class MenuEntity : BaseEntity
    {
        /// <summary>
        /// 父节点标识
        /// </summary>
        [Column("PARENT_ID")]
        public Guid? ParentId { get; set; }
        /// <summary>
        /// 菜单名称
        /// </summary>
        [StringLength(32)]
        [Column("TEXT")]
        public string Text { get; set; }
        /// <summary>
        /// 路径
        /// </summary>
        [StringLength(64)]
        [Column("LINK")]
        public string Link { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        [StringLength(32)]
        [Column("ICON")]
        public string Icon { get; set; }
        /// <summary>
        /// 消息数量
        /// </summary>
        [Column("BADGE")]
        public int Badge { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        [Column("SORT")]
        public int Sort { get; set; }
    }
}
