using Domain.Shared;
using System.ComponentModel.DataAnnotations;
using FluentValidation;
using static Domain.Entities.Doctor;

namespace Domain.Entities
{
    public class Holiday : BaseAuditableEntity
    {
        protected Holiday() { }
        public Holiday(string description, DateTime date, Doctor doctor)
        {
            Description = description;
            Date = date;
            Doctor = doctor;
        }

        [MaxLength(100)]
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public Doctor Doctor { get; set; }

        public class ValidatorHolidayValido : AbstractValidator<Holiday>
        {
            public ValidatorHolidayValido()
            {
                RuleFor(x => x.Description)
                    .NotEmpty()
                    .WithMessage("Descrição é obrigatório.")
                    .MaximumLength(100)
                    .WithMessage("Descrição deve ter no máximo 100 caracteres.");

                RuleFor(x => x.Date)
               .NotEmpty().WithMessage("O campo Data precisa ser fornecido");

                RuleFor(x => x.Doctor).SetValidator(new ValidatorDoctorValido());

            }
        }
    }
}
