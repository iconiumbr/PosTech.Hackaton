using Application.Gateways.DataAccess;
using Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class RepositoryDoctor : Repository<Doctor>, IRepositoryDoctor
    {
        public RepositoryDoctor(HackatonDbContext dbContext) : base(dbContext) { }
        public bool Exists(string cpf) => !_dbSet.Any(x => x.Cpf.Numero == cpf);

        public async Task<DoctorService> GetServiceByIdAsync(int id) => await _dbContext.Services.FindAsync(id);
        public async Task<IEnumerable<DoctorService>> GetServicesByIdAsync(int doctorId)
        {
            var services = _dbContext.Services.Where(x => x.Doctor.Id == doctorId);
            return await services.ToListAsync();
        }
        public async Task<Doctor> GetWithAllRelations(int id)
        {
            return await _dbSet.Include(x => x.Appointments)
                .Include(x => x.Schedules)
                .Include(x => x.Holidays)
                .Include(x => x.Services)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task RemoveSchedules()
            => await _dbContext.Database.ExecuteSqlAsync($"DELETE FROM Schedules WHERE DoctorId is null");
        public async Task UpdateServiceAsync(DoctorService service)
        {
            _dbContext.Services.Update(service);
            await _dbContext.SaveChangesAsync();
        }
    }
}
