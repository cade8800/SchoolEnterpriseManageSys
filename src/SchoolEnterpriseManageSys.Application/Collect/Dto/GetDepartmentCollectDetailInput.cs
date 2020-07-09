using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Runtime.Validation;

namespace SchoolEnterpriseManageSys.Collect.Dto
{
    public class GetDepartmentCollectDetailInput : ICustomValidate
    {
        public Guid? CollectId { get; set; }
        public Guid? DepartmentCollectId { get; set; }

        public void AddValidationErrors(CustomValidationContext context)
        {
            if (!this.CollectId.HasValue && !DepartmentCollectId.HasValue)
                context.Results.Add(new ValidationResult("请求标识不可为空"));
        }
    }
}

