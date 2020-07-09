using Abp.Runtime.Validation;
using SchoolEnterpriseManageSys.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolEnterpriseManageSys.Project.Dto
{
    /// <summary>
    /// 校外实践基地Dto
    /// </summary>
    public class OffCampusBaseDto : ICustomValidate
    {
        public Guid? Id { get; set; }
        /// <summary>
        /// 部门标识
        /// </summary>
        public Guid? DepartmentId { get; set; }
        /// <summary>
        /// 专业/学科
        /// </summary>
        [StringLength(32)]
        public string Science { get; set; }
        /// <summary>
        /// 企业标识
        /// </summary>
        //public Guid EnterpriseId { get; set; }
        [StringLength(64)]
        public string EnterpriseName { get; set; }
        /// <summary>
        /// 负责人
        /// </summary>
        [StringLength(32)]
        public string Principal { get; set; }

        ///// <summary>
        ///// 开始时间
        ///// </summary>
        //public DateTime StartTime { get; set; }
        ///// <summary>
        ///// 结束时间
        ///// </summary>
        //public DateTime EndTime { get; set; }

        /// <summary>
        /// 申报等级 校/省/部
        /// </summary>
        public ReportLevel? ReportLevel { get; set; }
        /// <summary>
        /// 关联项目标识 
        /// </summary>
        public Guid? RelateProjectId { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(256)]
        public string Remark { get; set; }


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
