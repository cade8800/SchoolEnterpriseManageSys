using System.Data.Common;
using System.Data.Entity;
using Abp.EntityFramework;
using SchoolEnterpriseManageSys.Entities;

namespace SchoolEnterpriseManageSys.EntityFramework
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class SchoolEnterpriseManageSysDbContext : AbpDbContext
    {
        //TODO: Define an IDbSet for each Entity...

        //Example:
        //public virtual IDbSet<User> Users { get; set; }


        public virtual IDbSet<UserEntity> SemsUserDb { get; set; }
        public virtual IDbSet<AdvisoryEntity> SemsAdvisoryDb { get; set; }
        public virtual IDbSet<EnterpriseInfoEntity> SemsEnterpriseInfoDb { get; set; }
        public virtual IDbSet<RoleEntity> SemsRoleDb { get; set; }
        public virtual IDbSet<RolePermissionsEntity> SemsRolePermissionsDb { get; set; }
        public virtual IDbSet<EnterpriseFileEntity> SemsEnterpriseFileDb { get; set; }
        public virtual IDbSet<AppointmentEntity> SemsAppointmentDb { get; set; }
        public virtual IDbSet<DepartmentEntity> SemsDepartmentDb { get; set; }
        public virtual IDbSet<UserMessageEntity> SemsUserMessageDb { get; set; }
        public virtual IDbSet<MenuEntity> SemsMenuDb { get; set; }
        public virtual IDbSet<FileEntity> SemsFileDb { get; set; }


        public virtual IDbSet<AssessmentDepartmentEntity> SemsAssessmentDepartmentDb { get; set; }
        public virtual IDbSet<AssessmentDepartmentIndexEntity> SemsAssessmentDepartmentIndexDb { get; set; }
        public virtual IDbSet<AssessmentDepartmentIndexItemEntity> SemsAssessmentDepartmentIndexItemDb { get; set; }
        public virtual IDbSet<AssessmentEntity> SemsAssessmentDb { get; set; }
        public virtual IDbSet<AssessmentFileEntity> SemsAssessmentFileDb { get; set; }
        public virtual IDbSet<AssessmentIndexEntity> SemsAssessmentIndexDb { get; set; }
        

        public virtual IDbSet<CollectEntity> SemsCollectDb { get; set; }
        public virtual IDbSet<CollectDepartmentEntity> SemsCollectDepartmentDb { get; set; }
        public virtual IDbSet<CollectDepartmentBaseEntity> SemsCollectDepartmentBaseDb { get; set; }
        public virtual IDbSet<CollectDepartmentCooperationEntity> SemsCollectDepartmentCooperationDb { get; set; }
        public virtual IDbSet<CollectFileEntity> SemsCollectFileDb { get; set; }
        

        public virtual IDbSet<CooperationProjectEntity> SemsCooperationProjectDb { get; set; }
        public virtual IDbSet<ProjectFileEntity> SemsProjectFileDb { get; set; }
        public virtual IDbSet<ProjectRenewEntity> SemsProjectRenewDb { get; set; }
        public virtual IDbSet<ProjectTypeEntity> SemsProjectTypeDb { get; set; }



        /* NOTE: 
         *   Setting "Default" to base class helps us when working migration commands on Package Manager Console.
         *   But it may cause problems when working Migrate.exe of EF. If you will apply migrations on command line, do not
         *   pass connection string name to base classes. ABP works either way.
         */
        public SchoolEnterpriseManageSysDbContext()
            : base("Default")
        {

        }

        /* NOTE:
         *   This constructor is used by ABP to pass connection string defined in SchoolEnterpriseManageSysDataModule.PreInitialize.
         *   Notice that, actually you will not directly create an instance of SchoolEnterpriseManageSysDbContext since ABP automatically handles it.
         */
        public SchoolEnterpriseManageSysDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        //This constructor is used in tests
        public SchoolEnterpriseManageSysDbContext(DbConnection existingConnection)
         : base(existingConnection, false)
        {

        }

        public SchoolEnterpriseManageSysDbContext(DbConnection existingConnection, bool contextOwnsConnection)
         : base(existingConnection, contextOwnsConnection)
        {

        }
    }
}
