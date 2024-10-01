using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.Property(e => e.Name).HasColumnType("varchar(100)").IsRequired();
            builder.Property(e => e.Email).HasColumnType("varchar(255)").IsRequired();

            builder.OwnsOne(e => e.Address, a =>
            {
                a.Property(e => e.Street).HasColumnType("varchar(100)");
                a.Property(e => e.Number).HasColumnType("varchar(10)");
                a.Property(e => e.Complement).HasColumnType("varchar(100)");
                a.Property(e => e.ZipCode).HasColumnType("varchar(8)");
                a.Property(e => e.Neighborhood).HasColumnType("varchar(50)");
                a.Property(e => e.City).HasColumnType("varchar(50)");
                a.Property(e => e.State).HasColumnType("varchar(2)");
            });

            builder.Property(e => e.ContactNumber).HasColumnType("varchar(12)").IsRequired();
            builder.Property(e => e.Crm).HasColumnType("varchar(12)").IsRequired();

            builder.OwnsOne(e => e.Cpf, a =>
            {
                a.Property(e => e.Numero).HasColumnType("varchar(11)").HasColumnName("Document");

            });

            builder.HasMany(x => x.Schedules).WithOne(x => x.Doctor);
            builder.HasMany(x => x.Services).WithOne(x => x.Doctor);

            builder.Navigation(m => m.Cpf).IsRequired();
            builder.Navigation(m => m.Address).IsRequired().AutoInclude();
            builder.Navigation(m => m.Schedules).AutoInclude();

            builder.ToTable("Doctors");
        }
    }
}
