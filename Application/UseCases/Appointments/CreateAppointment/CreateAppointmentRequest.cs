using MediatR;

namespace Application.UseCases.Appointments.CreateAppointment
{
    public class CreateAppointmentRequest : IRequest<int>
    {
        public int DoctorId { get; set; }
        public int ServiceId { get; set; }
        public DateTime Date { get; set; }
    }
}
