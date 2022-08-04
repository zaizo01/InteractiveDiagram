using DIGEIG.Application.Filter;
using DIGEIG.Application.Interfaces.Core;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DIGEIG.Application.Services
{
    public class BaseRepositoryService<T> : IBaseRepositoryService<T> where T : class 
    {
        private  IBaseRepository<T> _repositoryService; 
        public BaseRepositoryService( IBaseRepository<T> repositoryService)
        {
            _repositoryService = repositoryService;
        }

        public async Task<bool> DeleteRecord(Guid id)
        {
            return await _repositoryService.DeleteRecordAsync(id);
        }

        public async Task<bool> ExistRecordAsync(Expression<Func<T, bool>> predicate)
        {
            return await _repositoryService.ExistRecordAsync(predicate);
        }

        public async Task<List<T>> FindRecordAsync(Expression<Func<T, bool>> predicate)
        {
            return await _repositoryService.FindRecordAsync(predicate);
        }

        public async Task<List<T>> GetAllRecordsAsync()
        {
           
            return await _repositoryService.GetAllRecordsAsync();
        }

        public async Task<PagesPagination<T>> GetFilterRecords(PaginationFilter paginationFilter, Expression<Func<T, bool>> predicate)
        {
            var records = await _repositoryService.GetFilterRecords(paginationFilter, predicate);

            return records;
        }

        public async Task<T> GetRecordAsync(Expression<Func<T, bool>> predicate)
        {
            return await _repositoryService.GetRecordAsync(predicate);

        }

        public async Task<bool> InsertRecordAsync(T entity)
        {
            return await _repositoryService.InsertRecordAsync(entity);
        }

        public async Task<bool> OkAsync()
        {
            return await _repositoryService.OkAsync();
        }

        public async Task<bool> UpdateRecord(T entity)
        {
            return await _repositoryService.UpdateRecordAsync(entity);
        }

   
    }
}
