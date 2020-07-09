using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolEnterpriseManageSys.Appointment.Dto
{
    public class AppointmentDto
    {
        public Guid? Id { get; set; }
        /// <summary>
        /// 企业标识
        /// </summary>
        public Guid EnterpriseId { get; set; }
        /// <summary>
        /// 部门标识
        /// </summary>
        public Guid? DepartmentId { get; set; }
        /// <summary>
        /// 到访时间
        /// </summary>
        [Required]
        public DateTime? VisitsTime { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        [Required]
        [StringLength(256)]
        public string Content { get; set; }
        /// <summary>
        /// 是否已确认
        /// </summary>
        public bool IsConfirm { get; set; } = false;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 是否已删除
        /// </summary>
        public bool IsDeleted { get; set; }



        public string EnterpriseName { get; set; }
        /// <summary>
        /// 固定电话
        /// </summary>
        public string FixedTelephone { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string ContactName { get; set; }
        public string Status { get; set; }
        public string DepartmentName { get; set; }
    }
}
