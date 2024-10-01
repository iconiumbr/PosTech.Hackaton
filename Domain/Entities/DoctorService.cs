using Domain.Shared;

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

        public string Name { get; private set; }
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

    }
}
