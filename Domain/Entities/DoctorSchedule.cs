using Domain.Shared;

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
    }
}
