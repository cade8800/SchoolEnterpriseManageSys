using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Runtime.Validation;
using SchoolEnterpriseManageSys.Department.Dto;

namespace SchoolEnterpriseManageSys.Assessment.Dto
{
    public class AssessmentDto : ICustomValidate
    {
        public Guid? Id { get; set; }
        /// <summary>
        /// 学年
        /// </summary>
        [StringLength(16)]
        public string SchoolYear { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
        /// <summary>
        /// 截止提交时间
        /// </summary>
        [Required]
        public DateTime? Deadline { get; set; }




        public Guid? AssessmentDepartmentId { get; set; }


        public List<DepartmentDto> DepartmentList { get; set; } = new List<DepartmentDto>();
        public bool IsDeadline { get; set; }

        public DateTime[] RangeTime { get; set; }

        public void AddValidationErrors(CustomValidationContext context)
        {
            if (RangeTime.Count() != 2)
            {
                context.Results.Add(new ValidationResult("时间范围设置错误"));
            }
        }
    }
}
