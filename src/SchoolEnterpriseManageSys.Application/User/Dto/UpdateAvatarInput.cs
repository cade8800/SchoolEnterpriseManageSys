using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SchoolEnterpriseManageSys.User.Dto
{
    public class UpdateAvatarInput
    {
        [Required]
        public string Url { get; set; }
    }
}
