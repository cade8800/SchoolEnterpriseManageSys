using Abp.Runtime.Validation;
using SchoolEnterpriseManageSys.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SchoolEnterpriseManageSys.User.Dto
{
    public class InsertCampustUserInput : ICustomValidate
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Required, StringLength(32), MinLength(5)]
        public string UserName { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        [StringLength(32)]
        public string ActualName { get; set; }

        /// <summary>
        /// 移动电话
        /// </summary>
        [StringLength(24)]
        public string Mobilephone { get; set; }
        /// <summary>
        /// 固定电话
        /// </summary>
        [StringLength(24)]
        public string FixedTelephone { get; set; }
        /// <summary>
        /// 电子邮箱
        /// </summary>
        [StringLength(64)]
        public string Email { get; set; }
        /// <summary>
        /// 部门标识
        /// </summary>
        public Guid? DepartmentId { get; set; }
        /// <summary>
        /// 职务
        /// </summary>
        [StringLength(16)]
        public string Position { get; set; }

        [Required]
        public RoleType? RoleType { get; set; }

        public void AddValidationErrors(CustomValidationContext context)
        {
            if (RoleType == Enum.RoleType.Enterprise)
            {
                context.Results.Add(new ValidationResult("不可创建企业用户"));
            }
            if (RoleType == Enum.RoleType.Department && !DepartmentId.HasValue)
            {
                context.Results.Add(new ValidationResult("系标识不可为空"));
            }

            for (int i = 0; i < UserName.Length; i++)
            {
                Regex rx = new Regex("^[\u4e00-\u9fa5]$");
                if (rx.IsMatch(UserName[i].ToString()))
                {
                    context.Results.Add(new ValidationResult("用户名或邮箱不可包含中文"));
                    break;
                }
            }
        }
    }
}
