using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.Property(e => e.Contact).HasColumnType("varchar(15)");
            builder.HasOne(x => x.Doctor).WithMany(x => x.Appointments).HasForeignKey("DoctorId");
            builder.HasOne(x => x.Service).WithMany().HasForeignKey("ServiceId");

            builder.Navigation(x => x.Doctor).AutoInclude();
            builder.Navigation(x => x.Service).AutoInclude();

            builder.ToTable("Appointments");
        }
    }
}
