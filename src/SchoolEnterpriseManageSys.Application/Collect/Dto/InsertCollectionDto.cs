using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Runtime.Validation;

namespace SchoolEnterpriseManageSys.Collect.Dto
{
    public class InsertCollectDto : ICustomValidate
    {
        public Guid? Id { get; set; }
        /// <summary>
        /// 学年
        /// </summary>
        [StringLength(16)]
        [Required]
        public string SchoolYear { get; set; }
        /// <summary>
        /// 截止提交时间
        /// </summary>
        [Required]
        public DateTime? DeadlineSubmission { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        [StringLength(512)]
        public string Description { get; set; }
        /// <summary>
        /// 说明 校外实习、实训基地
        /// </summary>
        [StringLength(512)]
        public string BaseDescription { get; set; }
        /// <summary>
        /// 说明 校友会与社会合作
        /// </summary>
        [StringLength(512)]
        public string CooperationDescription { get; set; }

        public void AddValidationErrors(CustomValidationContext context)
        {
            if (this.SchoolYear.Length != 9 || this.SchoolYear.IndexOf('-') != 4)
            {
                context.Results.Add(new ValidationResult("学年格式错误"));
            }
        }
    }
}
