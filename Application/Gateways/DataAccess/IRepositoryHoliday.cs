using Domain.Entities;

namespace Application.Gateways.DataAccess
{
    public interface IRepositoryHoliday : IRepository<Holiday>
    {
        bool HolidayInSameDay(DateTime date, int doctorId);
    }
}
