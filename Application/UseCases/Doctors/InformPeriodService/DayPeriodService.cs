namespace Application.UseCases.Doctors.InformPeriodService
{
    public class DayPeriodService
    {
        public DayOfWeek DayOfWeek { get; init; }
        public TimeSpan MorningStartHour { get; init; }
        public TimeSpan MorningEndHour { get; init; }
        public TimeSpan AfternoonStartHour { get; init; }
        public TimeSpan AfternoonEndHour { get; init; }

        public override bool Equals(object obj)
            => DayOfWeek == ((DayPeriodService)obj).DayOfWeek;
        public override int GetHashCode() => base.GetHashCode();
    }
}
