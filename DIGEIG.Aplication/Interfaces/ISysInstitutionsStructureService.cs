using DIGEIG.Application.Interfaces.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIGEIG.Application.Interfaces
{

    public interface ISysInstitutionsStructureService<TEntity> : IRepositoryServices<TEntity> where TEntity : class
    {
        Task<List<TEntity>> GetRecordsByInstitutionIdAsync(int InstitutionId);
        Task<TEntity> GetDiagramRecordsByInstitutionId(int InstitutionId); 
    }
}
