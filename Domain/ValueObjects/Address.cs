using Domain.Shared;
using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace Domain.ValueObjects
{
    public class Address : ValueObject
    {
        public Address(string street, string number, string complement, string zipCode,
            string neighborhood, string city, string state)
        {
            Street = street;
            Number = number;
            Complement = complement;
            ZipCode = zipCode;
            Neighborhood = neighborhood;
            City = city;
            State = state;
        }

        [MaxLength(100)]
        public string Street { get; private set; }

        [MaxLength(10)]
        public string Number { get; private set; }

        [MaxLength(100)]
        public string Complement { get; private set; }

        [MaxLength(8)]
        public string ZipCode { get; private set; }

        [MaxLength(50)]
        public string Neighborhood { get; private set; }

        [MaxLength(50)]
        public string City { get; private set; }

        [MaxLength(2)]
        public string State { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return ZipCode;
            yield return Number;
        }

        public class ValidatorAddressValido : AbstractValidator<Address>
        {
            public ValidatorAddressValido()
            {
                RuleFor(x => x.Street)
                    .NotEmpty()
                    .WithMessage("Rua é obrigatório.")
                    .MaximumLength(100)
                    .WithMessage("Nome deve ter no máximo 100 caracteres.")
                    .Matches("^[a-zA-Z0-9 ]*$")
                    .WithMessage("Nome deve conter apenas caracteres alfanuméricos e espaços.");

                RuleFor(x => x.Number)
                    .NotEmpty()
                    .WithMessage("Número é obrigatório.")
                    .MaximumLength(10)
                    .WithMessage("Numero deve ter no máximo 10 caracteres.")
                    .Matches("^[a-zA-Z0-9 ]*$");

                RuleFor(x => x.Complement)
                    .MaximumLength(100)
                    .MinimumLength(5)
                    .WithMessage("Numero do Contrato deve ter no mínimo 5 e no máximo 100 caracteres.");

                RuleFor(x => x.ZipCode)
                    .NotEmpty()
                    .WithMessage("Cep é obrigatório.")
                    .MaximumLength(8)
                    .WithMessage("Cep deve ter no máximo 8 caracteres.")
                    .Matches("^[0-9]*$")
                    .WithMessage("Cep deve conter apenas Números.");

                RuleFor(x => x.Neighborhood)
                    .NotEmpty()
                    .WithMessage("Bairro é obrigatório.")
                    .MaximumLength(50)
                    .WithMessage("Bairro deve ter no máximo 50 caracteres.");

                RuleFor(x => x.City)
                    .NotEmpty()
                    .WithMessage("Cidade é obrigatório.")
                    .MaximumLength(50)
                    .WithMessage("Cidade deve ter no máximo 50 caracteres.");

                RuleFor(x => x.State)
                    .NotEmpty()
                    .WithMessage("Estado é obrigatório.")
                    .MaximumLength(2)
                    .WithMessage("Estado deve ter no máximo 2 caracteres.");
            }
        }
    }
}