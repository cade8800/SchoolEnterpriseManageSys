using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolEnterpriseManageSys.Menu.Dto
{
    public class UserInfoOutput
    {
        public string Name { get; set; }
        public string Avatar { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }

        /// <summary>
        /// 职务
        /// </summary>
        public string Position { get; set; }

        /// <summary>
        /// 部门标识
        /// </summary>
        public Guid? DepartmentId { get; set; }
        public string DepartmentName { get; set; }
    }
}
