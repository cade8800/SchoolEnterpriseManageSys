using SchoolEnterpriseManageSys.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;

namespace SchoolEnterpriseManageSys.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<SchoolEnterpriseManageSys.EntityFramework.SchoolEnterpriseManageSysDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationDataLossAllowed = true;
            AutomaticMigrationsEnabled = true;
            ContextKey = "SchoolEnterpriseManageSys";
            SetSqlGenerator("MySql.Data.MySqlClient", new MySql.Data.Entity.MySqlMigrationSqlGenerator());
        }

        protected override void Seed(SchoolEnterpriseManageSys.EntityFramework.SchoolEnterpriseManageSysDbContext context)
        {
            #region 合作项目类型
            List<ProjectTypeEntity> ProjectTypeList = new List<ProjectTypeEntity>
            {
                new ProjectTypeEntity{Id=Guid.NewGuid(),Type= Enum.ProjectType.JointlyEstablishedProfession,ProjectTypeName=Utilities.EnumHelper.EnumExtensions.GetDescription(Enum.ProjectType.JointlyEstablishedProfession),
                UploadFileDescription="共建协议/学生招生、教学、实习情况/调研报告、验收报告、年度总结等成果/基地合作、教学、实习图片"
                },
                new ProjectTypeEntity{Id=Guid.NewGuid(),Type= Enum.ProjectType.CampusBase,ProjectTypeName=Utilities.EnumHelper.EnumExtensions.GetDescription(Enum.ProjectType.CampusBase),
                UploadFileDescription="营业执照副本、简介/共建协议/实训教学安排/学年度总结/共同申报项目等成果材料/基地合作/教学图片"
                },
                new ProjectTypeEntity{Id=Guid.NewGuid(),Type= Enum.ProjectType.OffCampusBase,ProjectTypeName=Utilities.EnumHelper.EnumExtensions.GetDescription(Enum.ProjectType.OffCampusBase),
                UploadFileDescription="营业执照副本、简介/共建协议/基地建设规划/每届实习实训安排/毕业设计/实践教学总结等/合作成果（课程、教材、订单培养、社会服务、学术、教科研等）"
                },
                new ProjectTypeEntity{Id=Guid.NewGuid(),Type= Enum.ProjectType.AcademicAchievement,ProjectTypeName=Utilities.EnumHelper.EnumExtensions.GetDescription(Enum.ProjectType.AcademicAchievement),
                UploadFileDescription="论文刊物图片等"
                },
                new ProjectTypeEntity{Id=Guid.NewGuid(),Type= Enum.ProjectType.CoAuthoredBookOrCourse,ProjectTypeName=Utilities.EnumHelper.EnumExtensions.GetDescription(Enum.ProjectType.CoAuthoredBookOrCourse),
                UploadFileDescription="共同开发教材/课程协议/申报和审批材料/教材（课程）样书/使用情况等"
                },
                new ProjectTypeEntity{Id=Guid.NewGuid(),Type= Enum.ProjectType.OrderTraining,ProjectTypeName=Utilities.EnumHelper.EnumExtensions.GetDescription(Enum.ProjectType.OrderTraining),
                UploadFileDescription="人才培养协议/教学工作安排/实习就业安排情况/企业评价/社会影响等成果/教学或实习图片"
                },
                new ProjectTypeEntity{Id=Guid.NewGuid(),Type= Enum.ProjectType.SocialService,ProjectTypeName=Utilities.EnumHelper.EnumExtensions.GetDescription(Enum.ProjectType.SocialService),
                UploadFileDescription="服务协议/团队名单/资金账单等/验收总结报告、图片等"
                },
                new ProjectTypeEntity{Id=Guid.NewGuid(),Type= Enum.ProjectType.TeachingResearchFund,ProjectTypeName=Utilities.EnumHelper.EnumExtensions.GetDescription(Enum.ProjectType.TeachingResearchFund),
                UploadFileDescription="合作协议/基金申报/结题报告/证书/合作或获奖表彰图片"
                }
            };
            ProjectTypeList.ForEach(t =>
            {
                context.SemsProjectTypeDb.Add(t);
            });
            #endregion

            #region 角色 

            var administrator = new Entities.RoleEntity
            {
                Id = Guid.NewGuid(),
                RoleName = "administrator",
                RoleType = Enum.RoleType.Administrator
            };
            var department = new RoleEntity
            {
                Id = Guid.NewGuid(),
                RoleName = "department",
                RoleType = Enum.RoleType.Department
            };
            var enterprise = new Entities.RoleEntity
            {
                Id = Guid.NewGuid(),
                RoleName = "enterprise",
                RoleType = Enum.RoleType.Enterprise
            };
            var expert = new Entities.RoleEntity
            {
                Id = Guid.NewGuid(),
                RoleName = "expert",
                RoleType = Enum.RoleType.Expert

            };
            context.SemsRoleDb.Add(administrator);
            context.SemsRoleDb.Add(department);
            context.SemsRoleDb.Add(enterprise);
            context.SemsRoleDb.Add(expert);

            #endregion

            #region 系统管理员

            var SemsAdmin = new Entities.UserEntity
            {
                ActualName = "",
                AvatarUrl = "",
                Email = "root@admin.com",
                Id = Guid.NewGuid(),
                IsDeleted = false,
                Nickname = "SemsAdmin",
                Password = Utilities.DEncryptHelper.Encrypt.MD5ByHash("12345678"),
                Position = "校企办管理员",
                UserName = "admin",
                UserType = Enum.UserType.OnCampusUser,
                RoleId = administrator.Id

            };
            context.SemsUserDb.Add(SemsAdmin);

            #endregion

            #region 菜单

            var menu1 = new Entities.MenuEntity { Id = Guid.NewGuid(), Text = "主导航", Sort = 1 };
            var menu10 = new Entities.MenuEntity { Id = Guid.NewGuid(), Text = "企业咨询", ParentId = menu1.Id, Icon = "icon-note", Link = "/consult/list", Sort = 10 };
            var menu11 = new Entities.MenuEntity { Id = Guid.NewGuid(), Text = "在线咨询", ParentId = menu1.Id, Icon = "icon-note", Link = "/consult/index", Sort = 11 };
            var menu12 = new Entities.MenuEntity { Id = Guid.NewGuid(), Text = "企业预约", ParentId = menu1.Id, Icon = "icon-book-open", Link = "/appointment/list", Sort = 12 };

            var menu13 = new Entities.MenuEntity { Id = Guid.NewGuid(), Text = "企业管理", ParentId = menu1.Id, Icon = "anticon anticon-team", Link = "/enterprise/list", Sort = 13 };
            var menu131 = new Entities.MenuEntity { Id = Guid.NewGuid(), Text = "企业查询", ParentId = menu13.Id, Link = "/enterprise/list", Sort = 131 };
            var menu132 = new Entities.MenuEntity { Id = Guid.NewGuid(), Text = "企业信息", ParentId = menu13.Id, Link = "/enterprise/edit", Sort = 132 };

            var menu14 = new Entities.MenuEntity { Id = Guid.NewGuid(), Text = "档案管理", ParentId = menu1.Id, Icon = "anticon anticon-folder-add", Link = "/archives/summary", Sort = 14 };
            var menu141 = new Entities.MenuEntity { Id = Guid.NewGuid(), Text = "汇总统计", ParentId = menu14.Id, Link = "/archives/summary", Sort = 140 };
            var menu149 = new Entities.MenuEntity { Id = Guid.NewGuid(), Text = "文件管理", ParentId = menu14.Id, Link = "/archives/JointlyEstablishedProfession", Sort = 141 };
            var menu142 = new Entities.MenuEntity { Id = Guid.NewGuid(), Text = "校内共建基地", ParentId = menu14.Id, Link = "/archives/CampusBase", Sort = 142 };
            var menu143 = new Entities.MenuEntity { Id = Guid.NewGuid(), Text = "校外实践基地", ParentId = menu14.Id, Link = "/archives/OffCampusBase", Sort = 143 };
            var menu144 = new Entities.MenuEntity { Id = Guid.NewGuid(), Text = "订单培养", ParentId = menu14.Id, Link = "/archives/OrderTraining", Sort = 144 };
            var menu145 = new Entities.MenuEntity { Id = Guid.NewGuid(), Text = "社会服务", ParentId = menu14.Id, Link = "/archives/SocialService", Sort = 145 };
            var menu146 = new Entities.MenuEntity { Id = Guid.NewGuid(), Text = "共编教材/课程", ParentId = menu14.Id, Link = "/archives/CoAuthoredBookOrCourse", Sort = 146 };
            var menu147 = new Entities.MenuEntity { Id = Guid.NewGuid(), Text = "学术成果", ParentId = menu14.Id, Link = "/archives/AcademicAchievement", Sort = 147 };
            var menu148 = new Entities.MenuEntity { Id = Guid.NewGuid(), Text = "教学研基金", ParentId = menu14.Id, Link = "/archives/TeachingResearchFund", Sort = 148 };
            var menu150 = new Entities.MenuEntity { Id = Guid.NewGuid(), Text = "档案说明", ParentId = menu14.Id, Link = "/archives/explain", Sort = 150 };

            var menu15 = new Entities.MenuEntity { Id = Guid.NewGuid(), Text = "考核管理", ParentId = menu1.Id, Icon = "anticon anticon-hourglass", Link = "/assessment/list", Sort = 15 };
            var menu151 = new Entities.MenuEntity { Id = Guid.NewGuid(), Text = "考核查询", ParentId = menu15.Id, Link = "/assessment/list", Sort = 151 };
            var menu152 = new Entities.MenuEntity { Id = Guid.NewGuid(), Text = "考核指标", ParentId = menu15.Id, Link = "/assessment/index", Sort = 152 };
            var menu153 = new Entities.MenuEntity { Id = Guid.NewGuid(), Text = "专家评分", ParentId = menu15.Id, Link = "/assessment/list", Sort = 153 };
            var menu154 = new Entities.MenuEntity { Id = Guid.NewGuid(), Text = "系部考核", ParentId = menu15.Id, Link = "/assessment/list", Sort = 154 };

            var menu16 = new Entities.MenuEntity { Id = Guid.NewGuid(), Text = "本科数据采集", ParentId = menu1.Id, Icon = "anticon anticon-pushpin-o", Link = "/collect/list", Sort = 16 };
            var menu161 = new Entities.MenuEntity { Id = Guid.NewGuid(), Text = "采集列表", ParentId = menu16.Id, Link = "/collect/list", Sort = 161 };
            //var menu162 = new Entities.MenuEntity { Id = Guid.NewGuid(), Text = "新增采集", ParentId = menu16.Id, Link = "/collect/edit/", Sort = 162 };
            var menu163 = new Entities.MenuEntity { Id = Guid.NewGuid(), Text = "采集录入", ParentId = menu16.Id, Link = "/collect/list", Sort = 163 };
            //var menu164 = new Entities.MenuEntity { Id = Guid.NewGuid(), Text = "采集说明", ParentId = menu16.Id, Link = "/widgets", Sort = 164 };

            var menu17 = new Entities.MenuEntity { Id = Guid.NewGuid(), Text = "设置", ParentId = menu1.Id, Icon = "anticon anticon-setting", Link = "/extras/settings", Sort = 17 };
            var menu171 = new Entities.MenuEntity { Id = Guid.NewGuid(), Text = "系部管理", ParentId = menu17.Id, Link = "/extras/department", Sort = 171 };
            var menu172 = new Entities.MenuEntity { Id = Guid.NewGuid(), Text = "用户管理", ParentId = menu17.Id, Link = "/extras/user", Sort = 172 };
            var menu173 = new Entities.MenuEntity { Id = Guid.NewGuid(), Text = "个人中心", ParentId = menu17.Id, Link = "/extras/settings", Sort = 173 };

            context.SemsMenuDb.Add(menu1);
            context.SemsMenuDb.Add(menu10);
            context.SemsMenuDb.Add(menu11);
            context.SemsMenuDb.Add(menu12);
            context.SemsMenuDb.Add(menu13);
            context.SemsMenuDb.Add(menu131);
            context.SemsMenuDb.Add(menu132);
            context.SemsMenuDb.Add(menu14);
            context.SemsMenuDb.Add(menu141);
            context.SemsMenuDb.Add(menu142);
            context.SemsMenuDb.Add(menu143);
            context.SemsMenuDb.Add(menu144);
            context.SemsMenuDb.Add(menu145);
            context.SemsMenuDb.Add(menu146);
            context.SemsMenuDb.Add(menu147);
            context.SemsMenuDb.Add(menu148);
            context.SemsMenuDb.Add(menu149);
            context.SemsMenuDb.Add(menu150);
            context.SemsMenuDb.Add(menu15);
            context.SemsMenuDb.Add(menu151);
            context.SemsMenuDb.Add(menu152);
            context.SemsMenuDb.Add(menu153);
            context.SemsMenuDb.Add(menu154);
            context.SemsMenuDb.Add(menu16);
            context.SemsMenuDb.Add(menu161);
            //context.SemsMenuDb.Add(menu162);
            context.SemsMenuDb.Add(menu163);
            //context.SemsMenuDb.Add(menu164);
            context.SemsMenuDb.Add(menu17);
            context.SemsMenuDb.Add(menu171);
            context.SemsMenuDb.Add(menu172);
            context.SemsMenuDb.Add(menu173);

            #endregion

            #region 管理员权限

            var rolePer1 = new Entities.RolePermissionsEntity { Id = Guid.NewGuid(), RoleId = administrator.Id, MenuId = menu1.Id };
            var rolePer2 = new Entities.RolePermissionsEntity { Id = Guid.NewGuid(), RoleId = administrator.Id, MenuId = menu10.Id };
            var rolePer3 = new Entities.RolePermissionsEntity { Id = Guid.NewGuid(), RoleId = administrator.Id, MenuId = menu12.Id };
            var rolePer4 = new Entities.RolePermissionsEntity { Id = Guid.NewGuid(), RoleId = administrator.Id, MenuId = menu13.Id };
            var rolePer5 = new Entities.RolePermissionsEntity { Id = Guid.NewGuid(), RoleId = administrator.Id, MenuId = menu131.Id };
            //var rolePer6 = new Entities.RolePermissionsEntity { Id = Guid.NewGuid(), RoleId = administrator.Id, MenuId = menu132.Id };
            var rolePer7 = new Entities.RolePermissionsEntity { Id = Guid.NewGuid(), RoleId = administrator.Id, MenuId = menu14.Id };
            var rolePer8 = new Entities.RolePermissionsEntity { Id = Guid.NewGuid(), RoleId = administrator.Id, MenuId = menu141.Id };
            var rolePer9 = new Entities.RolePermissionsEntity { Id = Guid.NewGuid(), RoleId = administrator.Id, MenuId = menu142.Id };
            var rolePer10 = new Entities.RolePermissionsEntity { Id = Guid.NewGuid(), RoleId = administrator.Id, MenuId = menu143.Id };
            var rolePer11 = new Entities.RolePermissionsEntity { Id = Guid.NewGuid(), RoleId = administrator.Id, MenuId = menu144.Id };
            var rolePer12 = new Entities.RolePermissionsEntity { Id = Guid.NewGuid(), RoleId = administrator.Id, MenuId = menu145.Id };
            var rolePer13 = new Entities.RolePermissionsEntity { Id = Guid.NewGuid(), RoleId = administrator.Id, MenuId = menu146.Id };
            var rolePer14 = new Entities.RolePermissionsEntity { Id = Guid.NewGuid(), RoleId = administrator.Id, MenuId = menu147.Id };
            var rolePer15 = new Entities.RolePermissionsEntity { Id = Guid.NewGuid(), RoleId = administrator.Id, MenuId = menu148.Id };
            var rolePer16 = new Entities.RolePermissionsEntity { Id = Guid.NewGuid(), RoleId = administrator.Id, MenuId = menu149.Id };
            var rolePer161 = new Entities.RolePermissionsEntity { Id = Guid.NewGuid(), RoleId = administrator.Id, MenuId = menu150.Id };
            var rolePer17 = new Entities.RolePermissionsEntity { Id = Guid.NewGuid(), RoleId = administrator.Id, MenuId = menu15.Id };
            var rolePer18 = new Entities.RolePermissionsEntity { Id = Guid.NewGuid(), RoleId = administrator.Id, MenuId = menu151.Id };
            var rolePer19 = new Entities.RolePermissionsEntity { Id = Guid.NewGuid(), RoleId = administrator.Id, MenuId = menu152.Id };
            //var rolePer20 = new Entities.RolePermissionsEntity { Id = Guid.NewGuid(), RoleId = administrator.Id, MenuId = menu153.Id };
            //var rolePer21 = new Entities.RolePermissionsEntity { Id = Guid.NewGuid(), RoleId = administrator.Id, MenuId = menu154.Id };
            var rolePer22 = new Entities.RolePermissionsEntity { Id = Guid.NewGuid(), RoleId = administrator.Id, MenuId = menu16.Id };
            var rolePer23 = new Entities.RolePermissionsEntity { Id = Guid.NewGuid(), RoleId = administrator.Id, MenuId = menu161.Id };
            //var rolePer24 = new Entities.RolePermissionsEntity { Id = Guid.NewGuid(), RoleId = administrator.Id, MenuId = menu162.Id };
            //var rolePer25 = new Entities.RolePermissionsEntity { Id = Guid.NewGuid(), RoleId = administrator.Id, MenuId = menu163.Id };
            //var rolePer26 = new Entities.RolePermissionsEntity { Id = Guid.NewGuid(), RoleId = administrator.Id, MenuId = menu164.Id };
            var rolePer27 = new Entities.RolePermissionsEntity { Id = Guid.NewGuid(), RoleId = administrator.Id, MenuId = menu17.Id };
            var rolePer28 = new Entities.RolePermissionsEntity { Id = Guid.NewGuid(), RoleId = administrator.Id, MenuId = menu171.Id };
            var rolePer29 = new Entities.RolePermissionsEntity { Id = Guid.NewGuid(), RoleId = administrator.Id, MenuId = menu172.Id };
            var rolePer30 = new Entities.RolePermissionsEntity { Id = Guid.NewGuid(), RoleId = administrator.Id, MenuId = menu173.Id };
            context.SemsRolePermissionsDb.Add(rolePer1);
            context.SemsRolePermissionsDb.Add(rolePer2);
            context.SemsRolePermissionsDb.Add(rolePer3);
            context.SemsRolePermissionsDb.Add(rolePer4);
            context.SemsRolePermissionsDb.Add(rolePer5);
            //context.SemsRolePermissionsDb.Add(rolePer6);
            context.SemsRolePermissionsDb.Add(rolePer7);
            context.SemsRolePermissionsDb.Add(rolePer8);
            context.SemsRolePermissionsDb.Add(rolePer9);
            context.SemsRolePermissionsDb.Add(rolePer10);
            context.SemsRolePermissionsDb.Add(rolePer11);
            context.SemsRolePermissionsDb.Add(rolePer12);
            context.SemsRolePermissionsDb.Add(rolePer13);
            context.SemsRolePermissionsDb.Add(rolePer14);
            context.SemsRolePermissionsDb.Add(rolePer15);
            context.SemsRolePermissionsDb.Add(rolePer16);
            context.SemsRolePermissionsDb.Add(rolePer161);
            context.SemsRolePermissionsDb.Add(rolePer17);
            context.SemsRolePermissionsDb.Add(rolePer18);
            context.SemsRolePermissionsDb.Add(rolePer19);
            //context.SemsRolePermissionsDb.Add(rolePer20);
            //context.SemsRolePermissionsDb.Add(rolePer21);
            context.SemsRolePermissionsDb.Add(rolePer22);
            context.SemsRolePermissionsDb.Add(rolePer23);
            //context.SemsRolePermissionsDb.Add(rolePer24);
            //context.SemsRolePermissionsDb.Add(rolePer25);
            //context.SemsRolePermissionsDb.Add(rolePer26);
            context.SemsRolePermissionsDb.Add(rolePer27);
            context.SemsRolePermissionsDb.Add(rolePer28);
            context.SemsRolePermissionsDb.Add(rolePer29);
            context.SemsRolePermissionsDb.Add(rolePer30);

            #endregion

            #region 企业管理权限

            var rolePer301 = new Entities.RolePermissionsEntity { Id = Guid.NewGuid(), RoleId = enterprise.Id, MenuId = menu1.Id };
            var rolePer31 = new Entities.RolePermissionsEntity { Id = Guid.NewGuid(), RoleId = enterprise.Id, MenuId = menu11.Id };
            var rolePer32 = new Entities.RolePermissionsEntity { Id = Guid.NewGuid(), RoleId = enterprise.Id, MenuId = menu12.Id };
            var rolePer33 = new Entities.RolePermissionsEntity { Id = Guid.NewGuid(), RoleId = enterprise.Id, MenuId = menu13.Id };
            var rolePer34 = new Entities.RolePermissionsEntity { Id = Guid.NewGuid(), RoleId = enterprise.Id, MenuId = menu132.Id };
            //var rolePer35 = new Entities.RolePermissionsEntity { Id = Guid.NewGuid(), RoleId = enterprise.Id, MenuId = menu14.Id };
            //var rolePer36 = new Entities.RolePermissionsEntity { Id = Guid.NewGuid(), RoleId = enterprise.Id, MenuId = menu142.Id };
            //var rolePer37 = new Entities.RolePermissionsEntity { Id = Guid.NewGuid(), RoleId = enterprise.Id, MenuId = menu143.Id };
            //var rolePer38 = new Entities.RolePermissionsEntity { Id = Guid.NewGuid(), RoleId = enterprise.Id, MenuId = menu144.Id };
            //var rolePer39 = new Entities.RolePermissionsEntity { Id = Guid.NewGuid(), RoleId = enterprise.Id, MenuId = menu145.Id };
            //var rolePer40 = new Entities.RolePermissionsEntity { Id = Guid.NewGuid(), RoleId = enterprise.Id, MenuId = menu146.Id };
            ////var rolePer41 = new Entities.RolePermissionsEntity { Id = Guid.NewGuid(), RoleId = enterprise.Id, MenuId = menu147.Id };
            //var rolePer42 = new Entities.RolePermissionsEntity { Id = Guid.NewGuid(), RoleId = enterprise.Id, MenuId = menu148.Id };
            //var rolePer43 = new Entities.RolePermissionsEntity { Id = Guid.NewGuid(), RoleId = enterprise.Id, MenuId = menu149.Id };


            var rolePer44 = new Entities.RolePermissionsEntity { Id = Guid.NewGuid(), RoleId = enterprise.Id, MenuId = menu17.Id };
            var rolePer45 = new Entities.RolePermissionsEntity { Id = Guid.NewGuid(), RoleId = enterprise.Id, MenuId = menu173.Id };

            context.SemsRolePermissionsDb.Add(rolePer301);
            context.SemsRolePermissionsDb.Add(rolePer31);
            context.SemsRolePermissionsDb.Add(rolePer32);
            context.SemsRolePermissionsDb.Add(rolePer33);
            context.SemsRolePermissionsDb.Add(rolePer34);
            //context.SemsRolePermissionsDb.Add(rolePer35);
            //context.SemsRolePermissionsDb.Add(rolePer36);
            //context.SemsRolePermissionsDb.Add(rolePer37);
            //context.SemsRolePermissionsDb.Add(rolePer38);
            //context.SemsRolePermissionsDb.Add(rolePer39);
            //context.SemsRolePermissionsDb.Add(rolePer40);
            ////context.SemsRolePermissionsDb.Add(rolePer41);
            //context.SemsRolePermissionsDb.Add(rolePer42);
            //context.SemsRolePermissionsDb.Add(rolePer43);
            context.SemsRolePermissionsDb.Add(rolePer44);
            context.SemsRolePermissionsDb.Add(rolePer45);

            #endregion

            #region 专家权限

            context.SemsRolePermissionsDb.Add(new RolePermissionsEntity { Id = Guid.NewGuid(), RoleId = expert.Id, MenuId = menu1.Id });
            context.SemsRolePermissionsDb.Add(new RolePermissionsEntity { Id = Guid.NewGuid(), RoleId = expert.Id, MenuId = menu15.Id });
            context.SemsRolePermissionsDb.Add(new RolePermissionsEntity { Id = Guid.NewGuid(), RoleId = expert.Id, MenuId = menu153.Id });
            context.SemsRolePermissionsDb.Add(new RolePermissionsEntity { Id = Guid.NewGuid(), RoleId = expert.Id, MenuId = menu17.Id });
            context.SemsRolePermissionsDb.Add(new RolePermissionsEntity { Id = Guid.NewGuid(), RoleId = expert.Id, MenuId = menu173.Id });

            #endregion

            #region 系管理员权限 

            context.SemsRolePermissionsDb.Add(new RolePermissionsEntity { Id = Guid.NewGuid(), RoleId = department.Id, MenuId = menu1.Id });
            context.SemsRolePermissionsDb.Add(new RolePermissionsEntity { Id = Guid.NewGuid(), RoleId = department.Id, MenuId = menu12.Id });

            context.SemsRolePermissionsDb.Add(new RolePermissionsEntity { Id = Guid.NewGuid(), RoleId = department.Id, MenuId = menu14.Id });
            context.SemsRolePermissionsDb.Add(new RolePermissionsEntity { Id = Guid.NewGuid(), RoleId = department.Id, MenuId = menu142.Id });
            context.SemsRolePermissionsDb.Add(new RolePermissionsEntity { Id = Guid.NewGuid(), RoleId = department.Id, MenuId = menu143.Id });
            context.SemsRolePermissionsDb.Add(new RolePermissionsEntity { Id = Guid.NewGuid(), RoleId = department.Id, MenuId = menu144.Id });
            context.SemsRolePermissionsDb.Add(new RolePermissionsEntity { Id = Guid.NewGuid(), RoleId = department.Id, MenuId = menu145.Id });
            context.SemsRolePermissionsDb.Add(new RolePermissionsEntity { Id = Guid.NewGuid(), RoleId = department.Id, MenuId = menu146.Id });
            context.SemsRolePermissionsDb.Add(new RolePermissionsEntity { Id = Guid.NewGuid(), RoleId = department.Id, MenuId = menu147.Id });
            context.SemsRolePermissionsDb.Add(new RolePermissionsEntity { Id = Guid.NewGuid(), RoleId = department.Id, MenuId = menu148.Id });
            context.SemsRolePermissionsDb.Add(new RolePermissionsEntity { Id = Guid.NewGuid(), RoleId = department.Id, MenuId = menu149.Id });

            context.SemsRolePermissionsDb.Add(new RolePermissionsEntity { Id = Guid.NewGuid(), RoleId = department.Id, MenuId = menu15.Id });
            context.SemsRolePermissionsDb.Add(new RolePermissionsEntity { Id = Guid.NewGuid(), RoleId = department.Id, MenuId = menu154.Id });

            context.SemsRolePermissionsDb.Add(new RolePermissionsEntity { Id = Guid.NewGuid(), RoleId = department.Id, MenuId = menu16.Id });
            context.SemsRolePermissionsDb.Add(new RolePermissionsEntity { Id = Guid.NewGuid(), RoleId = department.Id, MenuId = menu163.Id });

            context.SemsRolePermissionsDb.Add(new RolePermissionsEntity { Id = Guid.NewGuid(), RoleId = department.Id, MenuId = menu17.Id });
            context.SemsRolePermissionsDb.Add(new RolePermissionsEntity { Id = Guid.NewGuid(), RoleId = department.Id, MenuId = menu173.Id });

            #endregion

            #region 考核指标

            var index1 = new AssessmentIndexEntity
            {
                Id = Guid.NewGuid(),
                CompleteStandard = "有专门会议记录。中心与系部网站内容应实时更新。",
                Content = "各系召开的校企合作工作专题会议。每次得2分。最高5分。校企网站或校企合作专栏报道每篇3分，最高10分。",
                IndexType = "量化指标",
                Sort = 1,
                StandardScore = 15
            };
            context.SemsAssessmentIndexDb.Add(index1);
            var index2 = new AssessmentIndexEntity
            {
                Id = Guid.NewGuid(),
                CompleteStandard = "以填报校企合作档案系统和上传佐证材料为准。共八项：校外实践基地、校内共建基地、社会服务、订单培养、共编教材、教学研基金、学术成果、共建专业。",
                Content = "建立完善的校企合作档案，校外实践基地4分，其余各项各3分。",
                IndexType = "量化指标",
                Sort = 2,
                StandardScore = 25
            };
            context.SemsAssessmentIndexDb.Add(index2);
            var index3 = new AssessmentIndexEntity
            {
                Id = Guid.NewGuid(),
                CompleteStandard = "新签约基地是较稳定合作企业，需提供签约协议、照片和相关学生实习名单等佐证材料。原基地过期续签时间以当年9月1日为限。",
                Content = "新签订协议并挂牌的较稳定的校外实习实训基地，每1家得4分；原有的实习基地过期续签不算分，协议过期不及时续签的扣2分/家。",
                IndexType = "量化指标",
                Sort = 3,
                StandardScore = 10
            };
            context.SemsAssessmentIndexDb.Add(index3);
            var index4 = new AssessmentIndexEntity
            {
                Id = Guid.NewGuid(),
                CompleteStandard = "教材以公开发行以出版合同或样书为准；院内使用以教务处证明及样书为准；课程开发则以开发协议、教务处证明为准。",
                Content = "校企共同编写教材（不含再版），该年度公开发行每部3分；共同开发课程每门2分。",
                IndexType = "量化指标",
                Sort = 4,
                StandardScore = 5
            };
            context.SemsAssessmentIndexDb.Add(index4);
            var index5 = new AssessmentIndexEntity
            {
                Id = Guid.NewGuid(),
                CompleteStandard = "以出版刊物为准。",
                Content = "在有正式刊号的公开出版物上发表的校企合作论文，每篇5分；****学院学刊，每篇3分。",
                IndexType = "量化指标",
                Sort = 5,
                StandardScore = 10
            };
            context.SemsAssessmentIndexDb.Add(index5);
            var index6 = new AssessmentIndexEntity
            {
                Id = Guid.NewGuid(),
                CompleteStandard = "以企业设立奖教、学、研协议，企业出资证明或校方收款凭证为准。",
                Content = "企业为本系设立奖教、学、研基金（含企业赞助技能竞赛活动经费），每满1万元得1分，企业给学生生活补贴不算。",
                IndexType = "量化指标",
                Sort = 6,
                StandardScore = 5
            };
            context.SemsAssessmentIndexDb.Add(index6);
            var index7 = new AssessmentIndexEntity
            {
                Id = Guid.NewGuid(),
                CompleteStandard = "每班20人以上，有培养协议、班级名单(开班合影)、报道等,以正式成立为准。",
                Content = "校企合作开办“订单班”、“委培班”等，得5分。",
                IndexType = "量化指标",
                Sort = 7,
                StandardScore = 5
            };
            context.SemsAssessmentIndexDb.Add(index7);
            var index8 = new AssessmentIndexEntity
            {
                Id = Guid.NewGuid(),
                CompleteStandard = "培训、讲座以讲义、讲稿、照片和报道稿为准。",
                Content = "邀请企业人员来校培训、讲座，每场得1分。",
                IndexType = "量化指标",
                Sort = 8,
                StandardScore = 3
            };
            context.SemsAssessmentIndexDb.Add(index8);
            var index9 = new AssessmentIndexEntity
            {
                Id = Guid.NewGuid(),
                CompleteStandard = "须提供合作协议（合同）、方案、平台制度文件和图片等等。",
                Content = "共建产学研育人平台，并正常运作，得5分；共建后每年取得成果，得2分。",
                IndexType = "量化指标",
                Sort = 9,
                StandardScore = 5
            };
            context.SemsAssessmentIndexDb.Add(index9);
            var index10 = new AssessmentIndexEntity
            {
                Id = Guid.NewGuid(),
                CompleteStandard = "横向课题须提供合作协议（合同）；纵向课题须提供批文、立项书。",
                Content = "取得的横向课题每万元2分，省部级产学研纵向课题每项5分。",
                IndexType = "量化指标",
                Sort = 10,
                StandardScore = 17
            };
            context.SemsAssessmentIndexDb.Add(index10);

            #endregion

            context.SaveChangesAsync();
        }
    }
}
