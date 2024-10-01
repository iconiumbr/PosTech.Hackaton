using Domain.Entities;
using MediatR;

namespace Application.UseCases.Doctors.AddService
{
    public class AddServiceRequest : IRequest
    {
        public string Name { get; init; }
        public string Description { get; init; }
        public decimal Price { get; init; }
        public TimeSpan Duration { get; init; }

        public DoctorService MapToEntitie()
        {
            return new DoctorService(Name, Description, Duration, Price);
        }

    }
}
