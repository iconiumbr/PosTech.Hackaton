using MediatR;

namespace Application.UseCases.Appointments.CancelAppointment
{
    public class CancelAppointmentRequest : IRequest
    {
        public int Id { get; set; }
    }
}
