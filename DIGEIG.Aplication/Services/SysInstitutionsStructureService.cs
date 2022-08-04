using DIGEIG.Application.Filter;
using DIGEIG.Application.Interfaces;
using DIGEIG.Application.Interfaces.Core;
using DIGEIG.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Linq;

namespace DIGEIG.Application.Services
{
    public class SysInstitutionsStructureService : ISysInstitutionsStructureService<Sys_Tb_InstitutionsStructure>
    {
        private readonly IBaseRepository<Sys_Tb_InstitutionsStructure> _repositoryService;
        private readonly IUnitOfWork _unitOfWork;

        public SysInstitutionsStructureService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repositoryService = (IBaseRepository<Sys_Tb_InstitutionsStructure>)_unitOfWork.GetRepository<IBaseRepository<Sys_Tb_InstitutionsStructure>>();
        }

        public int TotalRecords { get; set; }

        public async Task<bool> DeleteAsync(Guid id, string useEmail)
        {
           return await _repositoryService.DeleteRecordAsync(id);
        }

        public async Task<bool> ExistsAsync(Expression<Func<Sys_Tb_InstitutionsStructure, bool>> predicate)
        {
            return await _repositoryService.ExistRecordAsync(predicate);
        }

        public async Task<List<Sys_Tb_InstitutionsStructure>> FindAsync(Expression<Func<Sys_Tb_InstitutionsStructure, bool>> predicate)
        {
            return await _repositoryService.FindRecordAsync(predicate);
        }

        public async Task<List<Sys_Tb_InstitutionsStructure>> GetAllAsync()
        {
            return await _repositoryService.GetAllRecordsAsync();
        }

        public async Task<Sys_Tb_InstitutionsStructure> GetAsync(Guid id)
        {
            return await _repositoryService.GetRecordAsync(t => t.Id == id);
        }

        public async Task<Sys_Tb_InstitutionsStructure> GetDiagramRecordsByInstitutionId(int InstitutionId)
        {
            var _listinstitutionsStructures = await GetRecordsByInstitutionIdAsync(InstitutionId);

            var main = _listinstitutionsStructures.Where(t => t.MainInstitutionStructureId == 0).FirstOrDefault();
             
            

            foreach (Sys_Tb_InstitutionsStructure item in _listinstitutionsStructures.Where(t=>t.MainInstitutionStructureId == main.InstitutionStructureId))
            {
                Sys_Tb_InstitutionsStructure Tb_InstitutionsStructure = item;

                foreach (var subItem in _listinstitutionsStructures.Where(t=>t.MainInstitutionStructureId == Tb_InstitutionsStructure.InstitutionStructureId))
                {                    

                    foreach (var lastsubItem in _listinstitutionsStructures.Where(t => t.MainInstitutionStructureId == subItem.InstitutionStructureId))
                    {
                       
                        foreach (var SubLasyItem in _listinstitutionsStructures.Where(t => t.MainInstitutionStructureId == lastsubItem.InstitutionStructureId))
                        {
                            lastsubItem.ListInstitutionsStructure.Add(SubLasyItem);
                        }
                        subItem.ListInstitutionsStructure.Add(lastsubItem);
                    }
                    Tb_InstitutionsStructure.ListInstitutionsStructure.Add(subItem);
                }

                main.ListInstitutionsStructure.Add(Tb_InstitutionsStructure);
            }

            return main;
        }

        public async Task<PagesPagination<Sys_Tb_InstitutionsStructure>> GetFilterRecords(PaginationFilter paginationFilter)
        {
            var records = await _repositoryService.GetFilterRecords(paginationFilter, t => ((t.Name.ToLower().Contains(paginationFilter.Search.Trim().ToLower()))
           || paginationFilter.Search.Equals(string.Empty, StringComparison.Ordinal)) && t.IsActive);

            return records;
        }

        public Task<List<Sys_Tb_InstitutionsStructure>> GetRecordsByInstitutionIdAsync(int InstitutionId)
        {
            return FindAsync(t => t.InstitutionId == InstitutionId);
        }

        public async Task<bool> InsertAsync(Sys_Tb_InstitutionsStructure entity)
        {
            entity.InstitutionStructureId = await _repositoryService.GetNextRecordIdAsync("Sys_Tb_InstitutionsStructure", "InstitutionStructureId");

            return await _repositoryService.InsertRecordAsync(entity);
        }

        public async Task<List<Sys_Tb_InstitutionsStructure>> SearchRegistryAsync(string search)
        {
            if (string.IsNullOrEmpty(search))
                search = string.Empty;

            var pagedData = await _repositoryService.FindRecordAsync(t => (t.Name.ToLower().Contains(search.Trim().ToLower()))
                || search.Equals(string.Empty, StringComparison.Ordinal));

            return pagedData;
        }

        public async Task<bool> UpdateAsync(Sys_Tb_InstitutionsStructure entity)
        {
            //Validacion 
            await ValidateAsync(entity, false);
            //Validacion 



            return await _repositoryService.UpdateRecordAsync(entity); 
        }

        public async Task ValidateAsync(Sys_Tb_InstitutionsStructure entity, bool isNew)
        {
            if (entity == null)
                throw new ApplicationException("La Estructura Organizacional no fue espesificada");

            if (string.IsNullOrEmpty(entity.Name))
                throw new ApplicationException("El Nombre de la Estructura Organizacional es necesario");

            if (isNew && !string.IsNullOrEmpty(entity.Name))
            {
                if (await ExistsAsync(t=>t.Name.Equals(entity.Name)))
                    throw new ApplicationException($@"La Estructura Organizacional {entity.Name} ya fue agregada.");
            }
        }
    }
}
