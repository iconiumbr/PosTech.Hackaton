using Application.Gateways.DataAccess;
using Domain.Entities;

namespace Data.Repositories
{
    public class RepositoryAppointment : Repository<Appointment>, IRepositoryAppointment
    {
        public RepositoryAppointment(HackatonDbContext dbContext) : base(dbContext)
        {
        }
    }
}
