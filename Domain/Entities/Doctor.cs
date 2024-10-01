using Domain.Shared;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public class Doctor : BaseAuditableEntity
    {
        protected Doctor() { }
        public Doctor(string name, string contactNumber, string email,
             string street, string number, string complement, string zipCode, string neighborhood,
            string city, string state, string cpf, string crm)
        {
            Name = name;
            ContactNumber = contactNumber;
            Email = email;
            Address = new Address(street, number, complement, zipCode, neighborhood, city, state);
            Cpf = new Cpf(cpf);
            Crm = crm;
        }

        public string Name { get; private set; }
        public Address Address { get; private set; }
        public Cpf Cpf { get; private set; }
        public string Crm { get; private set; }
        public string ContactNumber { get; private set; }
        public string Email { get; private set; }
        public ICollection<DoctorSchedule> Schedules { get; private set; }
        public ICollection<Appointment> Appointments { get; private set; }
        public ICollection<Holiday> Holidays { get; private set; }
        public ICollection<DoctorService> Services { get; private set; } = new List<DoctorService>();

        public void InformPeriod(IEnumerable<DoctorSchedule> schedules)
        {
            Schedules = new List<DoctorSchedule>();

            foreach (var schedule in schedules)
                Schedules.Add(schedule);
        }

        public void AddService(DoctorService service)
        {
            if (Services.Contains(service))
                return;

            Services.Add(service);
        }
    }
}
