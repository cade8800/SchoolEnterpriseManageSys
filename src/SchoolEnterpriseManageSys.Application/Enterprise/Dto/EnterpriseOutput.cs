using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolEnterpriseManageSys.Enterprise.Dto
{
    public class EnterpriseOutput
    {
        public Guid? Id { get; set; }
        /// <summary>
        /// 企业全称
        /// </summary>
        [Required]
        [StringLength(48)]
        public string FullName { get; set; }
        /// <summary>
        /// 企业简称
        /// </summary>
        [StringLength(48)]
        public string NameAbbreviation { get; set; }
        /// <summary>
        /// 法人
        /// </summary>
        [StringLength(32)]
        [Required]
        public string LegalRepresentative { get; set; }
        /// <summary>
        /// 固定电话
        /// </summary>
        [Required]
        [StringLength(24)]
        public string FixedTelephone { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        [Required]
        [StringLength(32)]
        public string ContactName { get; set; }
        /// <summary>
        /// 规模
        /// </summary>
        [StringLength(24)]
        public string Scale { get; set; }
        /// <summary>
        /// 简介
        /// </summary>
        [StringLength(128)]
        public string CompanyProfile { get; set; }
    }
}
