using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolEnterpriseManageSys.Menu.Dto
{
    public class MenuOutput
    {
        public Guid Id { get; set; }
        public Guid? ParentId { get; set; }
        public string Text { get; set; }
        public string Link { get; set; }
        public string Icon { get; set; }
        public int Badge { get; set; }
        public int Sort { get; set; }
        public List<MenuOutput> Children { get; set; } = new List<MenuOutput>();
    }
}
