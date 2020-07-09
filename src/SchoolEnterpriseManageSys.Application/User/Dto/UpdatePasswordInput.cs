using Abp.Runtime.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolEnterpriseManageSys.User.Dto
{
    public class UpdatePasswordInput : ICustomValidate
    {
        [Required]
        public string OldPassword { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Required, MaxLength(16), MinLength(8)]
        public string NewPassword { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Required, MaxLength(16), MinLength(8)]
        public string ConfirmNewPassword { get; set; }

        public void AddValidationErrors(CustomValidationContext context)
        {
            if (NewPassword != ConfirmNewPassword)
            {
                context.Results.Add(new ValidationResult("两次输入的密码不一致！"));
            }
        }
    }
}
