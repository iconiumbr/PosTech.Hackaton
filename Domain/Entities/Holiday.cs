using Domain.Shared;

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

        public string Description { get; set; }
        public DateTime Date { get; set; }
        public Doctor Doctor { get; set; }
    }
}
