using DIGEIG.Application.Interfaces;
using DIGEIG.Application.Interfaces.Core;
using DIGEIG.Application.Services;
using DIGEIG.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace DIGEIG.Application
{
    public static class ServiceExtensions
    {

        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddTransient<IRepositoryServices<Sys_Tb_Institutions>, SysInstitutionService>(); 
            services.AddTransient<ISysInstitutionsStructureService<Sys_Tb_InstitutionsStructure>, SysInstitutionsStructureService>();
            services.AddTransient(typeof(IBaseRepositoryService<>), typeof(BaseRepositoryService<>)); 
        }
    }
}
