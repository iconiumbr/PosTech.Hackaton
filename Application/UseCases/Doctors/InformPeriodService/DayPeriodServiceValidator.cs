using FluentValidation;

namespace Application.UseCases.Doctors.InformPeriodService
{
    public class DayPeriodServiceValidator : AbstractValidator<DayPeriodService>
    {
        public DayPeriodServiceValidator()
        {
            RuleFor(x => x.DayOfWeek).IsInEnum();
            When(c => c.MorningStartHour.TotalMicroseconds > 0, () =>
            {
                RuleFor(x => x.MorningStartHour).LessThan(x => x.MorningEndHour);
                RuleFor(x => x.AfternoonStartHour).LessThan(x => x.AfternoonEndHour);
            });
        }
    }
}
