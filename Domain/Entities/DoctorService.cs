using Domain.Shared;
using FluentValidation;
using System.ComponentModel.DataAnnotations;
using static Domain.Entities.Doctor;

namespace Domain.Entities
{
    public class DoctorService : BaseAuditableEntity
    {
        public DoctorService(string name, string description, TimeSpan duration, decimal price)
        {
            Name = name;
            Description = description;
            Duration = duration;
            Price = price;
        }

        [MaxLength(100)]
        public string Name { get; private set; }

        [MaxLength(255)]
        public string Description { get; private set; }
        public TimeSpan Duration { get; private set; }
        public decimal Price { get; private set; }
        public virtual Doctor Doctor { get; private set; }

        public void UpdateInfo(string name, string description, TimeSpan duration, decimal price)
        {
            Name = name;
            Description = description;
            Duration = duration;
            Price = price;
        }

        public class ValidatorDoctorServiceValido : AbstractValidator<DoctorService>
        {
            public ValidatorDoctorServiceValido()
            {
                RuleFor(x => x.Doctor).SetValidator(new ValidatorDoctorValido());

                RuleFor(x => x.Name)
                    .NotEmpty()
                    .WithMessage("Nome é obrigatório.")
                    .MaximumLength(100)
                    .WithMessage("Nome ter no máximo 100 caracteres.")
                    .Matches("^[a-zA-ZÀ-ÖØ-öø-ÿ0-9 ]*$")
                    .WithMessage("Nome deve conter apenas caracteres alfanuméricos e espaços.");

                RuleFor(x => x.Price)
                    .NotEmpty()
                    .WithMessage("Preço é obrigatório.")
                    .GreaterThanOrEqualTo(0)
                    .WithMessage("O preço não pode ser menor que zero.");

                RuleFor(x => x.Description)
                    .NotEmpty()
                    .WithMessage("Descrição é obrigatório.")
                    .MaximumLength(255)
                    .WithMessage("Descrição deve ter no máximo 255 caracteres.")
                    .Matches("^[a-zA-ZÀ-ÖØ-öø-ÿ0-9 ]*$")
                    .WithMessage("Descrção deve conter apenas caracteres alfanuméricos e espaços.");

                RuleFor(x => x.Duration)
                    .NotEmpty().WithMessage("Duração é obrigatório.");

                RuleFor(x => x.Price)
                    .NotEmpty().WithMessage("Preço é obrigatório.");

            }
        }


    }
}
