using Domain.Shared;
using FluentValidation;
using static Domain.Entities.Doctor;

namespace Domain.Entities
{
    public class DoctorSchedule : BaseAuditableEntity
    {
        protected DoctorSchedule() { }
        public DoctorSchedule(DayOfWeek dayOfWeek, Doctor doctor,
            TimeSpan morningStartHour, TimeSpan morningEndHour,
            TimeSpan afternoonStartHour, TimeSpan afternoonEndHour)
        {
            DayOfWeek = dayOfWeek;
            Doctor = doctor;
            MorningStartHour = morningStartHour;
            MorningEndHour = morningEndHour;
            AfternoonStartHour = afternoonStartHour;
            AfternoonEndHour = afternoonEndHour;
        }

        public DayOfWeek DayOfWeek { get; private set; }
        public Doctor Doctor { get; private set; }
        public TimeSpan MorningStartHour { get; private set; }
        public TimeSpan MorningEndHour { get; private set; }
        public TimeSpan AfternoonStartHour { get; private set; }
        public TimeSpan AfternoonEndHour { get; private set; }
        public class ValidatorDoctorScheduleValido : AbstractValidator<DoctorSchedule>
        {
            public ValidatorDoctorScheduleValido()
            {
                RuleFor(x => x.Doctor).SetValidator(new ValidatorDoctorValido())
                    .NotEmpty()
                    .WithMessage("Necessário um Médico válido"); 

                RuleFor(x => new { x.MorningStartHour, x.MorningEndHour, x.AfternoonStartHour, x.AfternoonEndHour })
                    .Must(times =>
                        times.MorningStartHour != TimeSpan.Zero ||
                        times.MorningEndHour != TimeSpan.Zero ||
                        times.AfternoonStartHour != TimeSpan.Zero ||
                        times.AfternoonEndHour != TimeSpan.Zero)
                    .WithMessage("Todos os horários não podem ficar sem preenchimento.");

                RuleFor(x => new { x.MorningStartHour, x.MorningEndHour, x.AfternoonStartHour, x.AfternoonEndHour })
                    .Must(times =>
                    {
                        int filledCount = 0;
                        if (times.MorningStartHour != TimeSpan.Zero) filledCount++;
                        if (times.MorningEndHour != TimeSpan.Zero) filledCount++;
                        if (times.AfternoonStartHour != TimeSpan.Zero) filledCount++;
                        if (times.AfternoonEndHour != TimeSpan.Zero) filledCount++;
                        return filledCount >= 2;
                    })
                    .WithMessage("Ao menos dois horários devem estar preenchidos.");

                RuleFor(x => new { x.MorningStartHour, x.MorningEndHour })
                    .Must(times =>
                        (times.MorningStartHour == TimeSpan.Zero && times.MorningEndHour == TimeSpan.Zero) ||
                        (times.MorningStartHour != TimeSpan.Zero && times.MorningEndHour != TimeSpan.Zero && times.MorningStartHour < times.MorningEndHour))
                    .WithMessage("Se os horários de início e término da manhã estiverem preenchidos, o horário de início deve ser menor que o horário de término.");

                RuleFor(x => new { x.AfternoonStartHour, x.AfternoonEndHour })
                    .Must(times =>
                        (times.AfternoonStartHour == TimeSpan.Zero && times.AfternoonEndHour == TimeSpan.Zero) ||
                        (times.AfternoonStartHour != TimeSpan.Zero && times.AfternoonEndHour != TimeSpan.Zero && times.AfternoonStartHour < times.AfternoonEndHour))
                    .WithMessage("Se os horários de início e término da tarde estiverem preenchidos, o horário de início deve ser menor que o horário de término.");
            }
        }
    }
}
