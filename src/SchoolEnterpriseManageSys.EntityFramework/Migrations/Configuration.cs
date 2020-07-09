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
            #region ������Ŀ����
            List<ProjectTypeEntity> ProjectTypeList = new List<ProjectTypeEntity>
            {
                new ProjectTypeEntity{Id=Guid.NewGuid(),Type= Enum.ProjectType.JointlyEstablishedProfession,ProjectTypeName=Utilities.EnumHelper.EnumExtensions.GetDescription(Enum.ProjectType.JointlyEstablishedProfession),
                UploadFileDescription="����Э��/ѧ����������ѧ��ʵϰ���/���б��桢���ձ��桢����ܽ�ȳɹ�/���غ�������ѧ��ʵϰͼƬ"
                },
                new ProjectTypeEntity{Id=Guid.NewGuid(),Type= Enum.ProjectType.CampusBase,ProjectTypeName=Utilities.EnumHelper.EnumExtensions.GetDescription(Enum.ProjectType.CampusBase),
                UploadFileDescription="Ӫҵִ�ո��������/����Э��/ʵѵ��ѧ����/ѧ����ܽ�/��ͬ�걨��Ŀ�ȳɹ�����/���غ���/��ѧͼƬ"
                },
                new ProjectTypeEntity{Id=Guid.NewGuid(),Type= Enum.ProjectType.OffCampusBase,ProjectTypeName=Utilities.EnumHelper.EnumExtensions.GetDescription(Enum.ProjectType.OffCampusBase),
                UploadFileDescription="Ӫҵִ�ո��������/����Э��/���ؽ���滮/ÿ��ʵϰʵѵ����/��ҵ���/ʵ����ѧ�ܽ��/�����ɹ����γ̡��̲ġ�����������������ѧ�����̿��еȣ�"
                },
                new ProjectTypeEntity{Id=Guid.NewGuid(),Type= Enum.ProjectType.AcademicAchievement,ProjectTypeName=Utilities.EnumHelper.EnumExtensions.GetDescription(Enum.ProjectType.AcademicAchievement),
                UploadFileDescription="���Ŀ���ͼƬ��"
                },
                new ProjectTypeEntity{Id=Guid.NewGuid(),Type= Enum.ProjectType.CoAuthoredBookOrCourse,ProjectTypeName=Utilities.EnumHelper.EnumExtensions.GetDescription(Enum.ProjectType.CoAuthoredBookOrCourse),
                UploadFileDescription="��ͬ�����̲�/�γ�Э��/�걨����������/�̲ģ��γ̣�����/ʹ�������"
                },
                new ProjectTypeEntity{Id=Guid.NewGuid(),Type= Enum.ProjectType.OrderTraining,ProjectTypeName=Utilities.EnumHelper.EnumExtensions.GetDescription(Enum.ProjectType.OrderTraining),
                UploadFileDescription="�˲�����Э��/��ѧ��������/ʵϰ��ҵ�������/��ҵ����/���Ӱ��ȳɹ�/��ѧ��ʵϰͼƬ"
                },
                new ProjectTypeEntity{Id=Guid.NewGuid(),Type= Enum.ProjectType.SocialService,ProjectTypeName=Utilities.EnumHelper.EnumExtensions.GetDescription(Enum.ProjectType.SocialService),
                UploadFileDescription="����Э��/�Ŷ�����/�ʽ��˵���/�����ܽᱨ�桢ͼƬ��"
                },
                new ProjectTypeEntity{Id=Guid.NewGuid(),Type= Enum.ProjectType.TeachingResearchFund,ProjectTypeName=Utilities.EnumHelper.EnumExtensions.GetDescription(Enum.ProjectType.TeachingResearchFund),
                UploadFileDescription="����Э��/�����걨/���ⱨ��/֤��/������񽱱���ͼƬ"
                }
            };
            ProjectTypeList.ForEach(t =>
            {
                context.SemsProjectTypeDb.Add(t);
            });
            #endregion

            #region ��ɫ 

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

            #region ϵͳ����Ա

            var SemsAdmin = new Entities.UserEntity
            {
                ActualName = "",
                AvatarUrl = "",
                Email = "root@admin.com",
                Id = Guid.NewGuid(),
                IsDeleted = false,
                Nickname = "SemsAdmin",
                Password = Utilities.DEncryptHelper.Encrypt.MD5ByHash("12345678"),
                Position = "У������Ա",
                UserName = "admin",
                UserType = Enum.UserType.OnCampusUser,
                RoleId = administrator.Id

            };
            context.SemsUserDb.Add(SemsAdmin);

            #endregion

            #region �˵�

            var menu1 = new Entities.MenuEntity { Id = Guid.NewGuid(), Text = "������", Sort = 1 };
            var menu10 = new Entities.MenuEntity { Id = Guid.NewGuid(), Text = "��ҵ��ѯ", ParentId = menu1.Id, Icon = "icon-note", Link = "/consult/list", Sort = 10 };
            var menu11 = new Entities.MenuEntity { Id = Guid.NewGuid(), Text = "������ѯ", ParentId = menu1.Id, Icon = "icon-note", Link = "/consult/index", Sort = 11 };
            var menu12 = new Entities.MenuEntity { Id = Guid.NewGuid(), Text = "��ҵԤԼ", ParentId = menu1.Id, Icon = "icon-book-open", Link = "/appointment/list", Sort = 12 };

            var menu13 = new Entities.MenuEntity { Id = Guid.NewGuid(), Text = "��ҵ����", ParentId = menu1.Id, Icon = "anticon anticon-team", Link = "/enterprise/list", Sort = 13 };
            var menu131 = new Entities.MenuEntity { Id = Guid.NewGuid(), Text = "��ҵ��ѯ", ParentId = menu13.Id, Link = "/enterprise/list", Sort = 131 };
            var menu132 = new Entities.MenuEntity { Id = Guid.NewGuid(), Text = "��ҵ��Ϣ", ParentId = menu13.Id, Link = "/enterprise/edit", Sort = 132 };

            var menu14 = new Entities.MenuEntity { Id = Guid.NewGuid(), Text = "��������", ParentId = menu1.Id, Icon = "anticon anticon-folder-add", Link = "/archives/summary", Sort = 14 };
            var menu141 = new Entities.MenuEntity { Id = Guid.NewGuid(), Text = "����ͳ��", ParentId = menu14.Id, Link = "/archives/summary", Sort = 140 };
            var menu149 = new Entities.MenuEntity { Id = Guid.NewGuid(), Text = "�ļ�����", ParentId = menu14.Id, Link = "/archives/JointlyEstablishedProfession", Sort = 141 };
            var menu142 = new Entities.MenuEntity { Id = Guid.NewGuid(), Text = "У�ڹ�������", ParentId = menu14.Id, Link = "/archives/CampusBase", Sort = 142 };
            var menu143 = new Entities.MenuEntity { Id = Guid.NewGuid(), Text = "У��ʵ������", ParentId = menu14.Id, Link = "/archives/OffCampusBase", Sort = 143 };
            var menu144 = new Entities.MenuEntity { Id = Guid.NewGuid(), Text = "��������", ParentId = menu14.Id, Link = "/archives/OrderTraining", Sort = 144 };
            var menu145 = new Entities.MenuEntity { Id = Guid.NewGuid(), Text = "������", ParentId = menu14.Id, Link = "/archives/SocialService", Sort = 145 };
            var menu146 = new Entities.MenuEntity { Id = Guid.NewGuid(), Text = "����̲�/�γ�", ParentId = menu14.Id, Link = "/archives/CoAuthoredBookOrCourse", Sort = 146 };
            var menu147 = new Entities.MenuEntity { Id = Guid.NewGuid(), Text = "ѧ���ɹ�", ParentId = menu14.Id, Link = "/archives/AcademicAchievement", Sort = 147 };
            var menu148 = new Entities.MenuEntity { Id = Guid.NewGuid(), Text = "��ѧ�л���", ParentId = menu14.Id, Link = "/archives/TeachingResearchFund", Sort = 148 };
            var menu150 = new Entities.MenuEntity { Id = Guid.NewGuid(), Text = "����˵��", ParentId = menu14.Id, Link = "/archives/explain", Sort = 150 };

            var menu15 = new Entities.MenuEntity { Id = Guid.NewGuid(), Text = "���˹���", ParentId = menu1.Id, Icon = "anticon anticon-hourglass", Link = "/assessment/list", Sort = 15 };
            var menu151 = new Entities.MenuEntity { Id = Guid.NewGuid(), Text = "���˲�ѯ", ParentId = menu15.Id, Link = "/assessment/list", Sort = 151 };
            var menu152 = new Entities.MenuEntity { Id = Guid.NewGuid(), Text = "����ָ��", ParentId = menu15.Id, Link = "/assessment/index", Sort = 152 };
            var menu153 = new Entities.MenuEntity { Id = Guid.NewGuid(), Text = "ר������", ParentId = menu15.Id, Link = "/assessment/list", Sort = 153 };
            var menu154 = new Entities.MenuEntity { Id = Guid.NewGuid(), Text = "ϵ������", ParentId = menu15.Id, Link = "/assessment/list", Sort = 154 };

            var menu16 = new Entities.MenuEntity { Id = Guid.NewGuid(), Text = "�������ݲɼ�", ParentId = menu1.Id, Icon = "anticon anticon-pushpin-o", Link = "/collect/list", Sort = 16 };
            var menu161 = new Entities.MenuEntity { Id = Guid.NewGuid(), Text = "�ɼ��б�", ParentId = menu16.Id, Link = "/collect/list", Sort = 161 };
            //var menu162 = new Entities.MenuEntity { Id = Guid.NewGuid(), Text = "�����ɼ�", ParentId = menu16.Id, Link = "/collect/edit/", Sort = 162 };
            var menu163 = new Entities.MenuEntity { Id = Guid.NewGuid(), Text = "�ɼ�¼��", ParentId = menu16.Id, Link = "/collect/list", Sort = 163 };
            //var menu164 = new Entities.MenuEntity { Id = Guid.NewGuid(), Text = "�ɼ�˵��", ParentId = menu16.Id, Link = "/widgets", Sort = 164 };

            var menu17 = new Entities.MenuEntity { Id = Guid.NewGuid(), Text = "����", ParentId = menu1.Id, Icon = "anticon anticon-setting", Link = "/extras/settings", Sort = 17 };
            var menu171 = new Entities.MenuEntity { Id = Guid.NewGuid(), Text = "ϵ������", ParentId = menu17.Id, Link = "/extras/department", Sort = 171 };
            var menu172 = new Entities.MenuEntity { Id = Guid.NewGuid(), Text = "�û�����", ParentId = menu17.Id, Link = "/extras/user", Sort = 172 };
            var menu173 = new Entities.MenuEntity { Id = Guid.NewGuid(), Text = "��������", ParentId = menu17.Id, Link = "/extras/settings", Sort = 173 };

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

            #region ����ԱȨ��

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

            #region ��ҵ����Ȩ��

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

            #region ר��Ȩ��

            context.SemsRolePermissionsDb.Add(new RolePermissionsEntity { Id = Guid.NewGuid(), RoleId = expert.Id, MenuId = menu1.Id });
            context.SemsRolePermissionsDb.Add(new RolePermissionsEntity { Id = Guid.NewGuid(), RoleId = expert.Id, MenuId = menu15.Id });
            context.SemsRolePermissionsDb.Add(new RolePermissionsEntity { Id = Guid.NewGuid(), RoleId = expert.Id, MenuId = menu153.Id });
            context.SemsRolePermissionsDb.Add(new RolePermissionsEntity { Id = Guid.NewGuid(), RoleId = expert.Id, MenuId = menu17.Id });
            context.SemsRolePermissionsDb.Add(new RolePermissionsEntity { Id = Guid.NewGuid(), RoleId = expert.Id, MenuId = menu173.Id });

            #endregion

            #region ϵ����ԱȨ�� 

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

            #region ����ָ��

            var index1 = new AssessmentIndexEntity
            {
                Id = Guid.NewGuid(),
                CompleteStandard = "��ר�Ż����¼��������ϵ����վ����Ӧʵʱ���¡�",
                Content = "��ϵ�ٿ���У���������ר����顣ÿ�ε�2�֡����5�֡�У����վ��У�����ר������ÿƪ3�֣����10�֡�",
                IndexType = "����ָ��",
                Sort = 1,
                StandardScore = 15
            };
            context.SemsAssessmentIndexDb.Add(index1);
            var index2 = new AssessmentIndexEntity
            {
                Id = Guid.NewGuid(),
                CompleteStandard = "���У���������ϵͳ���ϴ���֤����Ϊ׼�������У��ʵ�����ء�У�ڹ������ء������񡢶�������������̲ġ���ѧ�л���ѧ���ɹ�������רҵ��",
                Content = "�������Ƶ�У�����������У��ʵ������4�֣���������3�֡�",
                IndexType = "����ָ��",
                Sort = 2,
                StandardScore = 25
            };
            context.SemsAssessmentIndexDb.Add(index2);
            var index3 = new AssessmentIndexEntity
            {
                Id = Guid.NewGuid(),
                CompleteStandard = "��ǩԼ�����ǽ��ȶ�������ҵ�����ṩǩԼЭ�顢��Ƭ�����ѧ��ʵϰ��������֤���ϡ�ԭ���ع�����ǩʱ���Ե���9��1��Ϊ�ޡ�",
                Content = "��ǩ��Э�鲢���ƵĽ��ȶ���У��ʵϰʵѵ���أ�ÿ1�ҵ�4�֣�ԭ�е�ʵϰ���ع�����ǩ����֣�Э����ڲ���ʱ��ǩ�Ŀ�2��/�ҡ�",
                IndexType = "����ָ��",
                Sort = 3,
                StandardScore = 10
            };
            context.SemsAssessmentIndexDb.Add(index3);
            var index4 = new AssessmentIndexEntity
            {
                Id = Guid.NewGuid(),
                CompleteStandard = "�̲��Թ��������Գ����ͬ������Ϊ׼��Ժ��ʹ���Խ���֤��������Ϊ׼���γ̿������Կ���Э�顢����֤��Ϊ׼��",
                Content = "У��ͬ��д�̲ģ������ٰ棩������ȹ�������ÿ��3�֣���ͬ�����γ�ÿ��2�֡�",
                IndexType = "����ָ��",
                Sort = 4,
                StandardScore = 5
            };
            context.SemsAssessmentIndexDb.Add(index4);
            var index5 = new AssessmentIndexEntity
            {
                Id = Guid.NewGuid(),
                CompleteStandard = "�Գ��濯��Ϊ׼��",
                Content = "������ʽ���ŵĹ����������Ϸ����У��������ģ�ÿƪ5�֣�****ѧԺѧ����ÿƪ3�֡�",
                IndexType = "����ָ��",
                Sort = 5,
                StandardScore = 10
            };
            context.SemsAssessmentIndexDb.Add(index5);
            var index6 = new AssessmentIndexEntity
            {
                Id = Guid.NewGuid(),
                CompleteStandard = "����ҵ�������̡�ѧ����Э�飬��ҵ����֤����У���տ�ƾ֤Ϊ׼��",
                Content = "��ҵΪ��ϵ�������̡�ѧ���л��𣨺���ҵ�������ܾ�������ѣ���ÿ��1��Ԫ��1�֣���ҵ��ѧ����������㡣",
                IndexType = "����ָ��",
                Sort = 6,
                StandardScore = 5
            };
            context.SemsAssessmentIndexDb.Add(index6);
            var index7 = new AssessmentIndexEntity
            {
                Id = Guid.NewGuid(),
                CompleteStandard = "ÿ��20�����ϣ�������Э�顢�༶����(�����Ӱ)��������,����ʽ����Ϊ׼��",
                Content = "У��������조�����ࡱ����ί��ࡱ�ȣ���5�֡�",
                IndexType = "����ָ��",
                Sort = 7,
                StandardScore = 5
            };
            context.SemsAssessmentIndexDb.Add(index7);
            var index8 = new AssessmentIndexEntity
            {
                Id = Guid.NewGuid(),
                CompleteStandard = "��ѵ�������Խ��塢���塢��Ƭ�ͱ�����Ϊ׼��",
                Content = "������ҵ��Ա��У��ѵ��������ÿ����1�֡�",
                IndexType = "����ָ��",
                Sort = 8,
                StandardScore = 3
            };
            context.SemsAssessmentIndexDb.Add(index8);
            var index9 = new AssessmentIndexEntity
            {
                Id = Guid.NewGuid(),
                CompleteStandard = "���ṩ����Э�飨��ͬ����������ƽ̨�ƶ��ļ���ͼƬ�ȵȡ�",
                Content = "������ѧ������ƽ̨����������������5�֣�������ÿ��ȡ�óɹ�����2�֡�",
                IndexType = "����ָ��",
                Sort = 9,
                StandardScore = 5
            };
            context.SemsAssessmentIndexDb.Add(index9);
            var index10 = new AssessmentIndexEntity
            {
                Id = Guid.NewGuid(),
                CompleteStandard = "����������ṩ����Э�飨��ͬ��������������ṩ���ġ������顣",
                Content = "ȡ�õĺ������ÿ��Ԫ2�֣�ʡ������ѧ���������ÿ��5�֡�",
                IndexType = "����ָ��",
                Sort = 10,
                StandardScore = 17
            };
            context.SemsAssessmentIndexDb.Add(index10);

            #endregion

            context.SaveChangesAsync();
        }
    }
}
