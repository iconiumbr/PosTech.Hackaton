using Domain.Enums;
using Domain.Shared;

namespace Domain.Entities
{
    public class Appointment : BaseAuditableEntity
    {
        protected Appointment() { }
        public Appointment(string contact, Doctor carWash, DoctorService service, DateTime dateTime, AppointmentStatus status)
        {
            Contact = contact;
            Doctor = carWash;
            Service = service;
            DateTime = dateTime;
            Status = status;
        }

        public string Contact { get; set; }
        public Doctor Doctor { get; private set; }
        public DoctorService Service { get; private set; }
        public DateTime DateTime { get; private set; }
        public AppointmentStatus Status { get; set; }

        public void Confirm() => Status = AppointmentStatus.Confirmed;
        public void Cancel() => Status = AppointmentStatus.Canceled;

    }
}
