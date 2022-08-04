using DIGEIG.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DIGEIG.Infrastructure.TableEntityConfiguration
{
    internal class Sys_Tb_InstitutionsConfiguration : AuditableEntityTypeConfiguration<Sys_Tb_Institutions>
    {
        public override void Configure(EntityTypeBuilder<Sys_Tb_Institutions> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.InstitutionId).IsRequired();
            builder.Property(x => x.ReferenceID).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(200).IsRequired();
            builder.Property(x => x.InstitutionId).IsRequired();

            base.Configure(builder);

        }
    }

}