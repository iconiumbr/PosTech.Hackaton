using FluentValidation;

namespace Application.UseCases.Doctors.InformPeriodService
{
    public class InformPeriodServiceValidator : AbstractValidator<InformPeriodServiceRequest>
    {
        public InformPeriodServiceValidator()
        {

            RuleForEach(x => x.Days).SetValidator(new DayPeriodServiceValidator());
        }
    }
}
