using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Data
{
    public class HackatonDbContext : DbContext
    {
        public HackatonDbContext(DbContextOptions<HackatonDbContext> options) : base(options) { }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Holiday> Holidays { get; set; }
        public DbSet<DoctorSchedule> Schedules { get; set; }
        public DbSet<DoctorService> Services { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
