using Application.Gateways.DataAccess;
using FluentValidation;

namespace Application.UseCases.Doctors.CreateHoliday
{
    public class CreateHolidayValidator : AbstractValidator<CreateHolidayRequest>
    {
        public CreateHolidayValidator(IRepositoryHoliday repositoryHoliday)
        {
            RuleFor(x => x.Description).NotEmpty();
        }
    }
}
