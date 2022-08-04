using DIGEIG.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DIGEIG.Infrastructure.TableEntityConfiguration
{
    internal class InstitutionsStructureConfiguration : AuditableEntityTypeConfiguration<Sys_Tb_InstitutionsStructure>
    {
        public override void Configure(EntityTypeBuilder<Sys_Tb_InstitutionsStructure> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.InstitutionStructureId).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Description).IsRequired();
            builder.Property(x => x.InstitutionId).IsRequired();

            base.Configure(builder);

        }
    }

}