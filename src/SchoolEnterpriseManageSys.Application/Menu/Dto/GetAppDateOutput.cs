using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolEnterpriseManageSys.Menu.Dto
{
    public class GetAppDateOutput
    {
        public AppInfoOutput App { get; set; }
        public UserInfoOutput User { get; set; }
        public List<MenuOutput> Menu { get; set; } = new List<MenuOutput>();
    }
}
