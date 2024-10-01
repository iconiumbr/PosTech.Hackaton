using MediatR;

namespace Application.UseCases.Appointments.ConfirmAppointment
{
    public class ConfirmAppointmentRequest : IRequest
    {
        public int Id { get; set; }
    }
}
