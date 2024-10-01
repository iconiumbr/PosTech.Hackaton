using Domain.Entities;
using MediatR;

namespace Application.UseCases.Doctors.DoctorRegistration
{
    public class DoctorRegistrationRequest : IRequest
    {
        public string Name { get; init; }
        public string Cpf { get; init; }
        public string Crm { get; init; }
        public string ContactNumber { get; init; }
        public string Email { get; init; }
        public string Password { get; init; }
        public string Street { get; private set; }
        public string Number { get; private set; }
        public string Complement { get; private set; }
        public string ZipCode { get; private set; }
        public string Neighborhood { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }

        public Doctor MapToDoctor() => new(Name, ContactNumber, Email, Street, Number, Complement, ZipCode, Neighborhood,
            City, State, Cpf, Crm);

    }
}
