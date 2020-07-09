using SchoolEnterpriseManageSys.BaseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolEnterpriseManageSys.Appointment.Dto
{
    public class GetEnterpriseAppointmentOutput : BasePagingOutput
    {
        public List<AppointmentDto> AppointmentList { get; set; } = new List<AppointmentDto>();
    }
}
