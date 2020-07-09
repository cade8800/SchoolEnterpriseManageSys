using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolEnterpriseManageSys.Advisory.Dto
{
    public class AdvisoryDto
    {
        /// <summary>
        /// 内容
        /// </summary>
        [StringLength(512)]
        public string Content { get; set; }
        /// <summary>
        /// 发起者
        /// </summary>
        [Required]
        public Guid? InitiatorUserId { get; set; }
        /// <summary>
        /// 接受者
        /// </summary>
        public Guid? RecipientUserId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }






        public string InitiatorName { get; set; }
        public string InitiatorAvatarUrl { get; set; }

        public string RecipientUserName { get; set; }
    }
}
