using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;

namespace SchoolEnterpriseManageSys.EntityFramework.Repositories
{
    public abstract class SchoolEnterpriseManageSysRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<SchoolEnterpriseManageSysDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected SchoolEnterpriseManageSysRepositoryBase(IDbContextProvider<SchoolEnterpriseManageSysDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //add common methods for all repositories
    }

    public abstract class SchoolEnterpriseManageSysRepositoryBase<TEntity> : SchoolEnterpriseManageSysRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected SchoolEnterpriseManageSysRepositoryBase(IDbContextProvider<SchoolEnterpriseManageSysDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //do not add any method here, add to the class above (since this inherits it)
    }
}
