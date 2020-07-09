using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SchoolEnterpriseManageSys.Collect.Dto
{
    public class InsertDepartmentCollectInput
    {
        public Guid? Id { get; set; }
        /// <summary>
        /// 数据采集标识
        /// </summary>
        [Required]
        public Guid? CollectionId { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(512)]
        public string Remark { get; set; }

        public List<BaseInput> BaseList { get; set; } = new List<BaseInput>();

        public CooperationInput Cooperation { get; set; }
    }
}
