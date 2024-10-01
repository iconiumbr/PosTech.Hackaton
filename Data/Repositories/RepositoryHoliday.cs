using Application.Gateways.DataAccess;
using Data;
using Domain.Entities;

namespace Data.Repositories
{
    public class RepositoryHoliday : Repository<Holiday>, IRepositoryHoliday
    {
        public RepositoryHoliday(HackatonDbContext dbContext) : base(dbContext)
        {
        }

        public bool HolidayInSameDay(DateTime date, int doctorId)
        {
            return !_dbSet.Any(x => x.Date == date && x.Doctor.Id == doctorId);
        }
    }
}
