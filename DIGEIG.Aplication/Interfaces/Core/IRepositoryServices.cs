using DIGEIG.Application.Filter;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DIGEIG.Application.Interfaces.Core
{
    public interface IRepositoryServices<TEntity> where TEntity : class
    {
        Task<TEntity> GetAsync(Guid id);
        Task<PagesPagination<TEntity>> GetFilterRecords(PaginationFilter paginationFilter);
        Task<List<TEntity>> GetAllAsync();
        Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
        Task<List<TEntity>> SearchRegistryAsync(string search);
        Task ValidateAsync(TEntity entity, bool isNew);
        Task<bool> InsertAsync(TEntity entity);
        Task<bool> UpdateAsync(TEntity entity);
        Task<bool> DeleteAsync(Guid id, string useEmail);
        int TotalRecords { get; set; }
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
