using DIGEIG.Application.Filter;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DIGEIG.Application.Interfaces.Core
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetRecordAsync(Expression<Func<TEntity, bool>> predicate);
        Task<PagesPagination<TEntity>> GetFilterRecords(PaginationFilter paginationFilter, Expression<Func<TEntity, bool>> predicate);
        Task<bool> OkAsync();
        Task<List<TEntity>> GetAllRecordsAsync();
        Task<List<TEntity>> FindRecordAsync(Expression<Func<TEntity, bool>> predicate);
        Task<bool> InsertRecordAsync(TEntity entity);
        Task<bool> UpdateRecordAsync(TEntity entity);
        Task<bool> DeleteRecordAsync(Guid id);
        Task<bool> ExistRecordAsync(Expression<Func<TEntity, bool>> predicate);
        Task<int> GetNextRecordIdAsync(string tableName, string primaryKeyName);
    }
}
