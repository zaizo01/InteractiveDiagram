using DIGEIG.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DIGEIG.Infrastructure.TableEntityConfiguration
{
    internal class AuditableEntityTypeConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
       where TEntity : Audit
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {

            builder.Property(x => x.CreateBy).HasMaxLength(250).IsRequired();
            builder.Property(x => x.LastModifyBy).HasMaxLength(250).IsRequired();
            builder.Property(x => x.LastModifyDate).IsRequired();
            builder.Property(x => x.IsActive).IsRequired();

        }
    }
}
