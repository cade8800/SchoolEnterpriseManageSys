using Abp.Application.Services;
using SchoolEnterpriseManageSys.Appointment.Dto;
using System.Web.Http;

namespace SchoolEnterpriseManageSys.Appointment
{
    public interface IAppointmentService : IApplicationService
    {
        [Authorize]
        AppointmentDto Get(GetAppointmentInput input);

        [Authorize]
        void EditAppointment(AppointmentDto input);

        [Authorize]
        GetEnterpriseAppointmentOutput GetEnterpriseAppointment(GetEnterpriseAppointmentInput input);

        [Authorize]
        void Delete([FromUri]DeleteInput input);

        [Authorize]
        void Confirm([FromUri]ConfirmInput input);

        [Authorize]
        void SendToDepartment(SendToDepartmentInput input);
    }
}
