using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SchoolEnterpriseManageSys.Appointment.Dto
{
    public class GetAppointmentInput
    {
        [Required]
        public Guid? Id { get; set; }
    }
}
