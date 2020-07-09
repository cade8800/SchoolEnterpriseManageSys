using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolEnterpriseManageSys.Department.Dto
{
    public class DepartmentDto
    {
        [Required]
        public Guid? Id { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        [StringLength(32), Required]
        public string Name { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
