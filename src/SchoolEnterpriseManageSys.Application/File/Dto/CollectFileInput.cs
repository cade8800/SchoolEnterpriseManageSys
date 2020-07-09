using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolEnterpriseManageSys.File.Dto
{
    public class CollectFileInput
    {
        [Required]
        public Guid? CollectionItemId { get; set; }
        [Required]
        public Guid? FileId { get; set; }
    }
}
