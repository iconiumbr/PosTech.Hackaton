using Domain.Enums;
using Domain.Shared;
using FluentValidation;
using System.ComponentModel.DataAnnotations;
using static Domain.Entities.Doctor;
using static Domain.Entities.DoctorService;

namespace Domain.Entities
{
    public class Appointment : BaseAuditableEntity
    {
        protected Appointment() { }
        public Appointment(string contact, Doctor doctor, DoctorService service, DateTime dateTime, AppointmentStatus status)
        {
            Contact = contact;
            Doctor = doctor;
            Service = service;
            DateTime = dateTime;
            Status = status;
        }
        
        [MaxLength(15)]
        public string Contact { get; set; }
        public Doctor Doctor { get; private set; }
        public DoctorService Service { get; private set; }
        public DateTime DateTime { get; private set; }
        public AppointmentStatus Status { get; set; }

        public void Confirm() => Status = AppointmentStatus.Confirmed;
        public void Cancel() => Status = AppointmentStatus.Canceled;

        public class ValidatorAppointmentValido : AbstractValidator<Appointment>
        {
            public ValidatorAppointmentValido()
            {
                RuleFor(x => x.Doctor).SetValidator(new ValidatorDoctorValido())
                    .NotEmpty()
                    .WithMessage("Necessário um Médico válido");

                RuleFor(x => x.Service).SetValidator(new ValidatorDoctorServiceValido())
                    .NotEmpty()
                    .WithMessage("Necessário Serviço Médico válido");

                RuleFor(x => x.Contact)
                    .NotEmpty()
                    .WithMessage("Contato é obrigatório.")
                    .MaximumLength(100)
                    .WithMessage("Nome ter no máximo 100 caracteres.")
                    .Matches("^[a-zA-ZÀ-ÖØ-öø-ÿ0-9 ]*$")
                    .WithMessage("Nome deve conter apenas caracteres alfanuméricos e espaços.");

                RuleFor(x => x.DateTime)
                    .NotEmpty().WithMessage("Data é obrigatório.");

            }
        }

    }
}
