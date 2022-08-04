
using DIGEIG.Application.Interfaces.Core;
using DIGEIG.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace DIGEIG.Infrastructure.Contexts
{
    public class ApplicationContext : DbContext
    {   private readonly ICurrentUserService _currentUserService;
        public ApplicationContext(DbContextOptions<ApplicationContext> options, ICurrentUserService currentUserService) : base(options)
        {
            _currentUserService = currentUserService;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {

            foreach (var entry in ChangeTracker.Entries<Audit>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Id = Guid.NewGuid();
                        entry.Entity.CreateDate = DateTime.Now;
                        //Todo: se necesita implementar Identity
                        //entry.Entity.CreateBy = _currentUserService.UserId;
                        //entry.Entity.LastModifyBy = _currentUserService.UserId;
                        entry.Entity.CreateBy = "Admin";
                        entry.Entity.LastModifyBy = "Admin";
                        entry.Entity.LastModifyDate = DateTime.Now;
                        entry.Entity.IsActive = true;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifyDate = DateTime.Now;
                        //entry.Entity.LastModifyBy = _currentUserService.UserId;
                        entry.Entity.LastModifyBy = "Admin";
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
