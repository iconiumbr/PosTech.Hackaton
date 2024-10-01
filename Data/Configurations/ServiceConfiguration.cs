using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class ServiceConfiguration : IEntityTypeConfiguration<DoctorService>
    {
        public void Configure(EntityTypeBuilder<DoctorService> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.Property(e => e.Name).HasColumnType("varchar(100)").IsRequired();
            builder.Property(e => e.Description).HasColumnType("varchar(255)").IsRequired();
            builder.Property(e => e.Duration).IsRequired();

            builder.Navigation(x => x.Doctor).AutoInclude();

            builder.ToTable("Services");
        }
    }
}
