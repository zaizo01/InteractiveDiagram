using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using DIGEIG.Infrastructure.Contexts;
using DIGEIG.Infrastructure.Uow;
using DIGEIG.Infrastructure.Persistence.Repository;
using DIGEIG.Application.Interfaces.Core;

namespace DIGEIG.Infrastructure
{
    public static class ServiceExtensions
    {
        public static void AddIdentityInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<ApplicationContext>(options =>
                 options.UseSqlServer(
                     configuration.GetConnectionString("DefaultConnection"),
                     b => b.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName)));

            #region Services   
            
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>)); 

            #endregion

        }
    }
}
