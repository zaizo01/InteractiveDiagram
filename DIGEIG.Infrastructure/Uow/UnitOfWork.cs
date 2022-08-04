using DIGEIG.Application.Interfaces.Core;
using DIGEIG.Infrastructure.Contexts;
using System; 
namespace DIGEIG.Infrastructure.Uow
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _dbContext;
        private readonly IServiceProvider _serviceProvider;
        public UnitOfWork(ApplicationContext dbContext,
            IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _serviceProvider = serviceProvider;
        }


        public void Commit()
        {
            _dbContext.SaveChanges();
        }

        public object GetRepository<TEntity>()
            => (TEntity)this._serviceProvider.GetService(typeof(TEntity));
    }
}
