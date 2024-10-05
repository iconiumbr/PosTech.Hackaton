using Domain.Shared;
using Domain.ValueObjects;
using FluentValidation;
using System.ComponentModel.DataAnnotations;
using static Domain.ValueObjects.Address;

namespace Domain.Entities
{
    public class Doctor : BaseAuditableEntity
    {
        protected Doctor() { }
        public Doctor(string name, string contactNumber, string email,
             string street, string number, string complement, string zipCode, string neighborhood,
            string city, string state, string cpf, string crm)
        {
            Name = name;
            ContactNumber = contactNumber;
            Email = email;
            Address = new Address(street, number, complement, zipCode, neighborhood, city, state);
            Cpf = new Cpf(cpf);
            Crm = crm;
        }

        [MaxLength(100)]
        public string Name { get; private set; }

        public Address Address { get; private set; }

        public Cpf Cpf { get; private set; }

        [MaxLength(12)]
        public string Crm { get; private set; }

        [MaxLength(12)]
        public string ContactNumber { get; private set; }

        [MaxLength(100)]
        public string Email { get; private set; }

        public ICollection<DoctorSchedule> Schedules { get; private set; }
        public ICollection<Appointment> Appointments { get; private set; }
        public ICollection<Holiday> Holidays { get; private set; }
        public ICollection<DoctorService> Services { get; private set; } = new List<DoctorService>();

        public void InformPeriod(IEnumerable<DoctorSchedule> schedules)
        {
            Schedules = new List<DoctorSchedule>();

            foreach (var schedule in schedules)
                Schedules.Add(schedule);
        }

        public void AddService(DoctorService service)
        {
            if (Services.Contains(service))
                return;

            Services.Add(service);
        }

        public class ValidatorDoctorValido : AbstractValidator<Doctor>
        {
            public ValidatorDoctorValido()
            {
                RuleFor(x => x.Name)
                    .NotEmpty()
                    .WithMessage("Nome do Médico é obrigatório.")
                    .MaximumLength(100)
                    .WithMessage("Nome deve ter no máximo 100 caracteres.")
                    .Matches("^[a-zA-Z0-9 ]*$")
                    .WithMessage("Nome deve conter apenas caracteres alfanuméricos e espaços.");

                RuleFor(x => x.Crm)
                    .NotEmpty()
                    .WithMessage("Crm do Médico é obrigatório.")
                    .MaximumLength(12)
                    .WithMessage("Crm deve ter no máximo 12 caracteres.")
                    .Matches("^[a-zA-Z0-9 ]*$")
                    .WithMessage("Crm deve conter apenas caracteres alfanuméricos e espaços.");

                RuleFor(x => x.ContactNumber)
                    .NotEmpty()
                    .WithMessage("Numero do Contrato do Médico é obrigatório.")
                    .MaximumLength(12)
                    .WithMessage("Numero do Contrato deve ter no máximo 12 caracteres.")
                    .Matches("^[a-zA-Z0-9 ]*$")
                    .WithMessage("Numero do Contrato deve conter apenas caracteres alfanuméricos e espaços.");

                RuleFor(x => x.Email)
                    .NotEmpty()
                    .WithMessage("Email do Médico é obrigatório.")
                    .EmailAddress().WithMessage("Email em formato inválido");

                RuleFor(x => x.Cpf).Must(Cpf.ValidarCpf).WithMessage("Cpf inválido!");
               
                RuleFor(x => x.Address).SetValidator(new ValidatorAddressValido());

            }
        }
    }
}
