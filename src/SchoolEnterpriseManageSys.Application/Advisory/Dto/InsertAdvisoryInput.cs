using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolEnterpriseManageSys.Advisory.Dto
{
    public class InsertAdvisoryInput
    {
        /// <summary>
        /// 内容
        /// </summary>
        [Required, StringLength(512)]
        public string Content { get; set; }
        /// <summary>
        /// 接受者
        /// </summary>
        public Guid? RecipientUserId { get; set; }
    }
}
