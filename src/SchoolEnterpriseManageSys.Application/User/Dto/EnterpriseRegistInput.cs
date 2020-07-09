using Abp.Runtime.Validation;
using SchoolEnterpriseManageSys.Utilities.StringHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolEnterpriseManageSys.User.Dto
{
    public class EnterpriseRegistInput : ICustomValidate
    {
        /// <summary>
        /// 电子邮箱
        /// </summary>
        [Required, StringLength(64), RegularExpression("^([a-zA-Z0-9_\\.\\-])+\\@(([a-zA-Z0-9\\-])+\\.)+([a-zA-Z0-9]{2,4})+$", ErrorMessage = "邮箱格式不正确，请重新输入。")]
        public string Email { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required, MaxLength(16), MinLength(8)]
        public string Password { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Required, MaxLength(16), MinLength(8)]
        public string ConfirmPassword { get; set; }

        public void AddValidationErrors(CustomValidationContext context)
        {
            if (Password != ConfirmPassword)
            {
                context.Results.Add(new ValidationResult("两次输入的密码不一致！"));
            }
        }
    }
}
