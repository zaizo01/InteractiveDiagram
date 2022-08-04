using DIGEIG.Application.Filter;
using DIGEIG.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DGEIG.Common;
using DIGEIG.Infrastructure.Extensions;
using DIGEIG.Application.Interfaces.Core;

namespace DIGEIG.Infrastructure.Persistence.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class, IModel
    {
        private DbSet<T> _entities;
        public readonly ApplicationContext DbContext; 
        public BaseRepository(ApplicationContext applicationDbContext ) 
        {
            _entities = applicationDbContext.Set<T>();
            DbContext = applicationDbContext;
        }

        public async Task<T> GetRecordAsync(Expression<Func<T, bool>> predicate) => await _entities.FirstOrDefaultAsync(predicate);
       
        public async Task<PagesPagination<T>> GetFilterRecords(PaginationFilter paginationFilter, Expression<Func<T, bool>> predicate)
        {
            int TotalRecordsEntity = await _entities.CountAsync(t=>t.IsActive);

            var entities = _entities.Where(predicate).AsQueryable();

            var pagesPagination = new PagesPagination<T>
            {
                Data = await entities.Pagination(paginationFilter).ToListAsync(),
                TotalRecords = TotalRecordsEntity
            };

            return pagesPagination;
        }

        public async Task<int> GetNextRecordIdAsync(string tableName, string primaryKeyName)
        {
            string sql = $@"SELECT TOP 1 {primaryKeyName} FROM [dbo].[{tableName}] ORDER BY {primaryKeyName} DESC";
            // Build command object  
            var cmd = DbContext.Database.GetDbConnection().CreateCommand();
            cmd.CommandText = sql;

            // Open database connection  
            DbContext.Database.OpenConnection();

            // Create a DataReader  
            var result = await cmd.ExecuteScalarAsync();

            if (result != DBNull.Value && result != null)
            {
                int id = Convert.ToInt32(result) + 1;

                return id;
            }
            else
            {
                return 1;
            }
        }

        public async Task<bool> OkAsync()
        {
            if (await DbContext.SaveChangesAsync() > 0)
                return true;
            else
                return false;
        }

        public async Task<List<T>> GetAllRecordsAsync()
        {
            var entities = await _entities.Where(t=>t.IsActive).AsNoTracking().ToListAsync();
            return entities;
        }

        public async Task<List<T>> FindRecordAsync(Expression<Func<T, bool>> predicate)
        {
            var entities = await _entities.Where(t=>t.IsActive).Where(predicate).ToListAsync();
            return entities;
        }

        public async Task<T> GetNextRecordId(Expression<Func<T, bool>> predicate) 
        {
            var entities = await _entities.Where(t => t.IsActive).Where(predicate).OrderByDescending(predicate).LastOrDefaultAsync();
            return entities;
        }

        public async Task<bool> InsertRecordAsync(T entity)
        {
            entity.IsActive = true;
            await DbContext.AddAsync(entity); 

            return await OkAsync();
        }

        public async Task<bool> UpdateRecordAsync(T entity) 
        {
            DbContext.Attach(entity);
            var entry = DbContext.Entry(entity);
            entry.State = EntityState.Modified;
            return await OkAsync();
        }

        public async Task<bool> DeleteRecordAsync(Guid id)
        {
            var entity = await GetRecordAsync(t => t.Id == id);

            if (entity == null)
             throw new ApplicationException("El registro no existe.");
           
            entity.IsActive = false;

            DbContext.Update(entity);
            return await OkAsync();
        }
 
        public async Task<bool> ExistRecordAsync(Expression<Func<T, bool>> predicate) => await _entities.Where(t=>t.IsActive).AnyAsync(predicate);

      
    }
}
