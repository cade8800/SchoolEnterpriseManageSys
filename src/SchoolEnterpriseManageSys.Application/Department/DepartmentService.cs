using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolEnterpriseManageSys.Department.Dto;
using Abp.Domain.Repositories;
using SchoolEnterpriseManageSys.Entities;
using SchoolEnterpriseManageSys.User;
using System.Web.Http;
using Abp.UI;
using Abp.Timing;

namespace SchoolEnterpriseManageSys.Department
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IRepository<DepartmentEntity, Guid> _departmentRepository;
        private readonly IUserService _userService;

        public DepartmentService(IRepository<DepartmentEntity, Guid> departmentRepository, IUserService userService)
        {
            _departmentRepository = departmentRepository;
            _userService = userService;
        }

        public Task<Guid> Add(AddDepartmentInput input)
        {
            _userService.CheckUserPermissions(new List<Enum.RoleType> { Enum.RoleType.Administrator });
            if (_departmentRepository.Count(t => t.Name == input.Name && t.IsDeleted == false) > 0)
                throw new UserFriendlyException("系【" + input.Name + "】已经存在");
            var user = _userService.UserClaim();
            return _departmentRepository.InsertAndGetIdAsync(new DepartmentEntity
            {
                CreateUserId = user.UserId,
                Id = Guid.NewGuid(),
                IsDeleted = false,
                Name = input.Name,
                UpdateUserId = user.UserId
            });
        }

        public void Delete(Guid? id)
        {
            _userService.CheckUserPermissions(new List<Enum.RoleType> { Enum.RoleType.Administrator });
            _departmentRepository.DeleteAsync((Guid)id);
        }

        public GetDepartmentOutput Get()
        {
            _userService.CheckUserPermissions(new List<Enum.RoleType> { Enum.RoleType.Administrator });
            var departmentList = _departmentRepository.GetAll().OrderBy(t => t.Name).ToList();
            var output = new GetDepartmentOutput();
            departmentList.ForEach(t =>
            {
                output.DepartmentList.Add(new DepartmentDto
                {
                    Id = t.Id,
                    Name = t.Name,
                    CreateTime = t.CreateTime
                });
            });
            return output;
        }

        public void Update(DepartmentDto input)
        {
            _userService.CheckUserPermissions(new List<Enum.RoleType> { Enum.RoleType.Administrator });
            var user = _userService.UserClaim();
            var dep = _departmentRepository.Get((Guid)input.Id);
            dep.Name = input.Name;
            dep.UpdateTime = Clock.Now;
            dep.UpdateUserId = user.UserId;
            _departmentRepository.UpdateAsync(dep);
        }
    }
}
