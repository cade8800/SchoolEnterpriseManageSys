using Abp.Timing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolEnterpriseManageSys.Collect.Dto
{
    public class CollectOutput
    {
        public Guid Id { get; set; }
        /// <summary>
        /// 学年
        /// </summary>
        [StringLength(16)]
        public string SchoolYear { get; set; }
        /// <summary>
        /// 截止提交时间
        /// </summary>
        public DateTime DeadlineSubmission { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        [StringLength(512)]
        public string Description { get; set; }



        public Guid? CollectDepartmentId { get; set; }
        public bool IsDeadline
        {
            get
            {
                return this.DeadlineSubmission < Clock.Now ? true : false;
            }
        }
    }
}
