using Application.Gateways.DataAccess;
using FluentValidation;

namespace Application.UseCases.Doctors.DoctorRegistration
{
    public class DoctorRegistrationValidator : AbstractValidator<DoctorRegistrationRequest>
    {
        public DoctorRegistrationValidator(IRepositoryDoctor repositoryDoctor)
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .Length(2, 100)
                .WithName("Nome do estabelecimento");

            RuleFor(c => c.ContactNumber).NotEmpty();
            RuleFor(c => c.Email).EmailAddress().NotEmpty();
            RuleFor(c => c.Password).MinimumLength(8);

            RuleFor(c => c.Email)
                .Must((x) => !repositoryDoctor.SearchAsync(y => y.Email.Equals(x)).Result.Any())
                .WithMessage("E-mail já cadastrado na base de dados!");
        }
    }
}
