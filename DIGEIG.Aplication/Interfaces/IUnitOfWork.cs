namespace DIGEIG.Application.Interfaces.Core
{
    public interface IUnitOfWork
    {
        object GetRepository<TEntity>();
        void Commit();

    }
}
