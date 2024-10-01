using Application.Gateways.DataAccess;
using Domain.Entities;

namespace Application.Queries
{
    public interface IAvailabilityQueries
    {
        Task<IEnumerable<DateTime>> GetAvailabilityAsync(int doctorId, int derviceId,
                                                        DateTime startDate, DateTime endDate);
        bool ScheduledTime(Doctor doctor, DateTime currenttime, TimeSpan duration);
    }
    public class AvailabilityQueries : IAvailabilityQueries
    {
        private readonly IRepositoryDoctor _repositoryDoctor;

        public AvailabilityQueries(IRepositoryDoctor repositoryCarWash)
        {
            _repositoryDoctor = repositoryCarWash;
        }

        public async Task<IEnumerable<DateTime>> GetAvailabilityAsync(int carWashId, int carWashServiceId, DateTime startDate, DateTime endDate)
        {
            var doctor = await _repositoryDoctor.GetWithAllRelations(carWashId);
            var service = await _repositoryDoctor.GetServiceByIdAsync(carWashServiceId);
            var letterDurationService = doctor.Services.OrderBy(x => x.Duration).FirstOrDefault();

            var availability = new List<DateTime>();

            for (DateTime date = startDate.Date; date <= endDate.Date; date = date.AddDays(1))
            {
                if (doctor.Holidays.Select(x => x.Date).Contains(date))
                    continue;

                var hoursService = doctor.Schedules.FirstOrDefault(x => x.DayOfWeek == date.DayOfWeek);
                if (hoursService is null)
                    continue;

                var currenttime = date.Add(hoursService.MorningStartHour);
                while (currenttime.TimeOfDay < hoursService.MorningEndHour)
                {
                    if (currenttime.Add(service.Duration).TimeOfDay <= hoursService.MorningEndHour
                        && !ScheduledTime(doctor, currenttime, service.Duration)
                        && currenttime > DateTime.Now)
                        availability.Add(currenttime);

                    currenttime = currenttime.Add(letterDurationService.Duration);
                }

                currenttime = date.Add(hoursService.AfternoonStartHour);
                while (currenttime.TimeOfDay < hoursService.AfternoonEndHour)
                {
                    if (currenttime.Add(service.Duration).TimeOfDay <= hoursService.AfternoonEndHour
                        && !ScheduledTime(doctor, currenttime, service.Duration)
                        && currenttime > DateTime.Now)
                        availability.Add(currenttime);

                    currenttime = currenttime.Add(letterDurationService.Duration);
                }
            }

            return availability;
        }

        public bool ScheduledTime(Doctor doctor, DateTime currenttime, TimeSpan duration)
        {
            var endHour = currenttime.Add(duration);
            var capacity = doctor.Appointments.Count(x => x.DateTime < endHour && x.DateTime.Add(x.Service.Duration) > currenttime);
            return capacity >= 1;
        }
    }
}
