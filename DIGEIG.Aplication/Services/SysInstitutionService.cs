using DIGEIG.Application.Filter;
using DIGEIG.Application.Interfaces.Core;
using DIGEIG.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DIGEIG.Application.Services
{
    public class SysInstitutionService : IRepositoryServices<Sys_Tb_Institutions>
    {
        private readonly IBaseRepository<Sys_Tb_Institutions> _repositoryService;
        private readonly IUnitOfWork _unitOfWork;

        public SysInstitutionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repositoryService = (IBaseRepository<Sys_Tb_Institutions>)_unitOfWork.GetRepository<IBaseRepository<Sys_Tb_Institutions>>();
        }

        public int TotalRecords { get; set; }

        public async Task<bool> DeleteAsync(Guid id, string useEmail)
        {
           return await _repositoryService.DeleteRecordAsync(id);
        }

        public async Task<bool> ExistsAsync(Expression<Func<Sys_Tb_Institutions, bool>> predicate)
        {
            return await _repositoryService.ExistRecordAsync(predicate);
        }

        public async Task<List<Sys_Tb_Institutions>> FindAsync(Expression<Func<Sys_Tb_Institutions, bool>> predicate)
        {
            return await _repositoryService.FindRecordAsync(predicate);
        }

        public async Task<List<Sys_Tb_Institutions>> GetAllAsync()
        {
            return await _repositoryService.GetAllRecordsAsync();
        }

        public async Task<Sys_Tb_Institutions> GetAsync(Guid id)
        {
            return await _repositoryService.GetRecordAsync(t => t.Id == id);
        }

        public async Task<PagesPagination<Sys_Tb_Institutions>> GetFilterRecords(PaginationFilter paginationFilter)
        {
            var records = await _repositoryService.GetFilterRecords(paginationFilter, t => ((t.Name.ToLower().Contains(paginationFilter.Search.Trim().ToLower()))
           || paginationFilter.Search.Equals(string.Empty, StringComparison.Ordinal)) && t.IsActive);

            return records;
        }

        public async Task<bool> InsertAsync(Sys_Tb_Institutions entity)
        {

            entity.InstitutionId = await _repositoryService.GetNextRecordIdAsync("Sys_Tb_Institutions", "InstitutionId");
            return await _repositoryService.InsertRecordAsync(entity);

        }

        public async Task<List<Sys_Tb_Institutions>> SearchRegistryAsync(string search)
        {
            if (string.IsNullOrEmpty(search))
                search = string.Empty;

            var pagedData = await _repositoryService.FindRecordAsync(t => (t.Name.ToLower().Contains(search.Trim().ToLower()))
                || search.Equals(string.Empty, StringComparison.Ordinal));

            return pagedData;
        }

        public async Task<bool> UpdateAsync(Sys_Tb_Institutions entity)
        {
            //Validacion 
            await ValidateAsync(entity, false);
            //Validacion 
            return await _repositoryService.UpdateRecordAsync(entity);
        }

        public async Task ValidateAsync(Sys_Tb_Institutions entity, bool isNew)
        {
            if (entity == null)
                throw new ApplicationException("La Institución no fue espesificada");

            if (string.IsNullOrEmpty(entity.Name))
                throw new ApplicationException("El Nombre de la Institución es necesario");

            if (isNew && !string.IsNullOrEmpty(entity.Name))
            {
                if (await ExistsAsync(t=>t.Name.Equals(entity.Name)))
                    throw new ApplicationException($@"La Institución {entity.Name} ya fue agregada.");
            }
        }
    }
}
