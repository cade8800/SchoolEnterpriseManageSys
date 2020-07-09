using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolEnterpriseManageSys.Menu.Dto;
using System.Configuration;
using SchoolEnterpriseManageSys.User;
using Abp.Domain.Repositories;
using SchoolEnterpriseManageSys.Entities;
using Abp.AutoMapper;

namespace SchoolEnterpriseManageSys.Menu
{
    public class MenuService : IMenuService
    {
        private readonly IUserService _userService;
        private readonly IRepository<RolePermissionsEntity, Guid> _rolePermissionsRepository;
        private readonly IRepository<MenuEntity, Guid> _menuRepository;
        private readonly IRepository<UserEntity, Guid> _userRepository;
        private readonly IRepository<DepartmentEntity, Guid> _departmentRepository;

        public MenuService(IUserService userService,
            IRepository<RolePermissionsEntity, Guid> rolePermissionsRepository,
            IRepository<MenuEntity, Guid> menuRepository,
            IRepository<UserEntity, Guid> userRepository,
            IRepository<DepartmentEntity, Guid> departmentRepository)
        {
            _userService = userService;
            _rolePermissionsRepository = rolePermissionsRepository;
            _menuRepository = menuRepository;
            _userRepository = userRepository;
            _departmentRepository = departmentRepository;
        }

        public GetAppDateOutput GetAppDate()
        {
            var userClaim = _userService.UserClaim();
            var user = _userRepository.FirstOrDefault(t => t.Id == userClaim.UserId && t.IsDeleted == false);
            var roleMenuIdList = _rolePermissionsRepository.GetAll().Where(t => t.RoleId == userClaim.RoleId && t.IsDeleted == false).Select(t => t.MenuId).ToList();
            var menuEntityList = _menuRepository.GetAll().Where(t => roleMenuIdList.Contains(t.Id) && t.IsDeleted == false).ToList();
            List<MenuOutput> menuList = menuEntityList.MapTo<List<MenuOutput>>();

            var output = new GetAppDateOutput()
            {
                App = new AppInfoOutput
                {
                    Name = ConfigurationManager.AppSettings.Get("AppName"),
                    Description = ConfigurationManager.AppSettings.Get("AppDescription")
                },
                User = new UserInfoOutput
                {
                    Avatar = !string.IsNullOrEmpty(user.AvatarUrl) ? user.AvatarUrl : "./assets/tmp/img/avatar.png",
                    UserName = user.UserName,
                    Name = !string.IsNullOrEmpty(user.ActualName) ? user.ActualName : user.Nickname,
                    Role = userClaim.Role,
                    Email = user.Email,
                    Position = user.Position,
                    DepartmentId = user.DepartmentId,
                    DepartmentName = user.DepartmentId.HasValue ? _departmentRepository.FirstOrDefault(t => t.Id == user.DepartmentId && t.IsDeleted == false)?.Name : ""
                }
            };
            output.Menu = menuList.Where(t => !t.ParentId.HasValue).OrderBy(t => t.Sort).ToList();

            output.Menu = GetChildMenu(output.Menu, menuList);

            return output;
        }

        private List<MenuOutput> GetChildMenu(List<MenuOutput> target, List<MenuOutput> menuList)
        {
            target.ForEach(t =>
            {
                t.Children = menuList.Where(m => m.ParentId == t.Id).OrderBy(m => m.Sort).ToList();
                t.Children = GetChildMenu(t.Children, menuList);
            });
            return target;
        }
    }
}
