using DIGEIG.Application.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DIGEIG.Application.Interfaces.Core
{
    public interface IBaseRepositoryService<TEntity> where TEntity : class
    {
        Task<TEntity> GetRecordAsync(Expression<Func<TEntity, bool>> predicate);
        Task<PagesPagination<TEntity>> GetFilterRecords(PaginationFilter paginationFilter, Expression<Func<TEntity, bool>> predicate);
        Task<bool> OkAsync();
        Task<List<TEntity>> GetAllRecordsAsync();
        Task<List<TEntity>> FindRecordAsync(Expression<Func<TEntity, bool>> predicate);
        Task<bool> InsertRecordAsync(TEntity entity);
        Task<bool> UpdateRecord(TEntity entity);
        Task<bool> DeleteRecord(Guid id);
        Task<bool> ExistRecordAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
