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
    /// 企业信息
    /// </summary>
    [Table("SEMS_ENTERPRISE_INFO")]
    public class EnterpriseInfoEntity : BaseEntity
    {
        /// <summary>
        /// 企业全称
        /// </summary>
        [StringLength(48)]
        [Column("FULL_NAME")]
        public string FullName { get; set; }
        /// <summary>
        /// 企业简称
        /// </summary>
        [StringLength(48)]
        [Column("NAME_ABBREVIATION")]
        public string NameAbbreviation { get; set; }
        /// <summary>
        /// 法人
        /// </summary>
        [StringLength(32)]
        [Column("LEGAL_REPRESENTATIVE")]
        public string LegalRepresentative { get; set; }
        /// <summary>
        /// 固定电话
        /// </summary>
        [StringLength(24)]
        [Column("FIXED_TELEPHONE")]
        public string FixedTelephone { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        [StringLength(32)]
        [Column("CONTACT_NAME")]
        public string ContactName { get; set; }
        /// <summary>
        /// 规模
        /// </summary>
        [StringLength(24)]
        [Column("SCALE")]
        public string Scale { get; set; }
        /// <summary>
        /// 简介
        /// </summary>
        [StringLength(128)]
        [Column("COMPANY_PROFILE")]
        public string CompanyProfile { get; set; }

    }
}
