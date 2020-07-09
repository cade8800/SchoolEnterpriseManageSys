using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolEnterpriseManageSys.Appointment.Dto;
using Abp.Domain.Repositories;
using SchoolEnterpriseManageSys.Entities;
using Abp.AutoMapper;
using SchoolEnterpriseManageSys.User;
using Abp.UI;
using System.Web.Http;
using Abp.Timing;

namespace SchoolEnterpriseManageSys.Appointment
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IRepository<AppointmentEntity, Guid> _appointmentRepository;
        private readonly IUserService _userService;
        private readonly IRepository<EnterpriseInfoEntity, Guid> _enterpriseInfoRepository;
        private readonly IRepository<UserEntity, Guid> _userRepository;
        private readonly IRepository<DepartmentEntity, Guid> _departmentRepository;

        public AppointmentService(
            IRepository<AppointmentEntity, Guid> appointmentRepository,
            IUserService userService,
            IRepository<EnterpriseInfoEntity, Guid> enterpriseInfoRepository,
            IRepository<UserEntity, Guid> userRepository,
            IRepository<DepartmentEntity, Guid> departmentRepository
            )
        {
            _appointmentRepository = appointmentRepository;
            _userService = userService;
            _enterpriseInfoRepository = enterpriseInfoRepository;
            _userRepository = userRepository;
            _departmentRepository = departmentRepository;
        }

        public void Confirm([FromUri] ConfirmInput input)
        {
            _userService.CheckUserPermissions(new List<Enum.RoleType> { Enum.RoleType.Administrator, Enum.RoleType.Department });
            var appoinetment = _appointmentRepository.FirstOrDefault(t => t.Id == input.Id);
            if (appoinetment != null)
            {
                var userCliam = _userService.UserClaim();
                appoinetment.IsConfirm = true;
                appoinetment.UpdateTime = Clock.Now;
                appoinetment.UpdateUserId = userCliam.UserId;
                _appointmentRepository.UpdateAsync(appoinetment);
            }
        }

        public void Delete([FromUri] DeleteInput input)
        {
            var appoinetment = _appointmentRepository.FirstOrDefault(t => t.Id == input.Id);
            if (appoinetment != null)
            {
                var userCliam = _userService.UserClaim();
                appoinetment.IsDeleted = !appoinetment.IsDeleted;
                appoinetment.UpdateTime = Clock.Now;
                appoinetment.UpdateUserId = userCliam.UserId;
                _appointmentRepository.UpdateAsync(appoinetment);
            }
        }

        public void EditAppointment(AppointmentDto input)
        {
            _userService.CheckUserPermissions(new List<Enum.RoleType> { Enum.RoleType.Enterprise });
            var userCliam = _userService.UserClaim();
            if (_enterpriseInfoRepository.Count(t => t.Id == userCliam.UserId) < 1) throw new UserFriendlyException("请先完善企业资料、个人资料");
            if (input.Id.HasValue)
            {
                var appointment = _appointmentRepository.FirstOrDefault(t => t.Id == input.Id && t.IsDeleted == false);
                if (appointment != null)
                {
                    appointment.VisitsTime = (DateTime)input.VisitsTime;
                    appointment.Content = input.Content;
                    appointment.UpdateTime = Clock.Now;
                    appointment.UpdateUserId = userCliam.UserId;
                    _appointmentRepository.UpdateAsync(appointment);
                }
            }
            else
            {
                _appointmentRepository.InsertAsync(new AppointmentEntity
                {
                    Id = Guid.NewGuid(),
                    Content = input.Content,
                    CreateUserId = userCliam.UserId,
                    EnterpriseId = userCliam.UserId,
                    VisitsTime = (DateTime)input.VisitsTime
                });
            }
        }

        public AppointmentDto Get(GetAppointmentInput input)
        {
            var appointment = _appointmentRepository.Get((Guid)input.Id);
            return appointment.MapTo<AppointmentDto>();
        }

        public GetEnterpriseAppointmentOutput GetEnterpriseAppointment(GetEnterpriseAppointmentInput input)
        {
            _userService.CheckUserPermissions(new List<Enum.RoleType> { Enum.RoleType.Administrator, Enum.RoleType.Department, Enum.RoleType.Enterprise });
            var userCliam = _userService.UserClaim();
            var query = _appointmentRepository.GetAll();
            if (userCliam.Role == "enterprise")
            {
                query = query.Where(t => t.EnterpriseId == userCliam.UserId);
            }
            else
            {
                query = query.Where(t => t.IsDeleted == false);
            }
            if (userCliam.Role == "department")
            {
                var user = _userRepository.FirstOrDefault(t => t.Id == userCliam.UserId && t.IsDeleted == false);
                query = query.Where(t => t.DepartmentId == user.DepartmentId);
            }
            var output = new GetEnterpriseAppointmentOutput
            {
                TotalCount = query.Count(),
                AppointmentList = query.OrderByDescending(t => t.CreateTime).Skip(input.PageSize * (input.PageIndex - 1)).Take(input.PageSize).ToList().MapTo<List<AppointmentDto>>()
            };
            output.AppointmentList.ForEach(t =>
            {
                var enterprise = _enterpriseInfoRepository.FirstOrDefault(e => e.Id == t.EnterpriseId);
                t.EnterpriseName = !string.IsNullOrEmpty(enterprise?.FullName) ? enterprise?.FullName : enterprise?.NameAbbreviation;
                t.FixedTelephone = enterprise?.FixedTelephone;
                t.ContactName = enterprise?.ContactName;
                if (t.DepartmentId.HasValue)
                {
                    t.DepartmentName = _departmentRepository.GetAll().Where(d => d.Id == t.DepartmentId && d.IsDeleted == false).Select(d => d.Name).FirstOrDefault();
                }
                if (t.VisitsTime < Clock.Now)
                {
                    t.Status = t.IsConfirm ? "已完成" : "已过期";
                }
                else
                {
                    t.Status = "预约中";
                }
            });
            return output;
        }

        public void SendToDepartment(SendToDepartmentInput input)
        {
            _userService.CheckUserPermissions(new List<Enum.RoleType> { Enum.RoleType.Administrator });
            var userCliam = _userService.UserClaim();
            var appointment = _appointmentRepository.FirstOrDefault(t => t.Id == input.AppointmentId && t.IsDeleted == false);
            if (appointment != null)
            {
                appointment.DepartmentId = input.DepartmentId;
                appointment.UpdateTime = Clock.Now;
                appointment.UpdateUserId = userCliam.UserId;
                _appointmentRepository.UpdateAsync(appointment);
            }
        }
    }
}
