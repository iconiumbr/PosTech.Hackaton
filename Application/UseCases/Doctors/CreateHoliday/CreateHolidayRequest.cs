using Domain.Entities;
using MediatR;

namespace Application.UseCases.Doctors.CreateHoliday
{
    public class CreateHolidayRequest : IRequest
    {
        public string Description { get; init; }
        public DateTime Date { get; set; }
        public Holiday MapToHoliday(Doctor doctor)
        {
            return new Holiday(Description, Date, doctor);
        }
    }
}
