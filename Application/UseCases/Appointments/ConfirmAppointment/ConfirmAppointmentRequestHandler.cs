using Application.Gateways.DataAccess;
using Application.Notifications;
using MediatR;

namespace Application.UseCases.Appointments.ConfirmAppointment
{
    public class ConfirmAppointmentRequestHandler : IRequestHandler<ConfirmAppointmentRequest>
    {

        private readonly IRepositoryAppointment _repositoryAppointment;
        private readonly INotificator _notificator;

        public ConfirmAppointmentRequestHandler(IRepositoryAppointment repositoryAppointment, INotificator notificator)
        {
            _repositoryAppointment = repositoryAppointment;
            _notificator = notificator;
        }

        public async Task Handle(ConfirmAppointmentRequest request, CancellationToken cancellationToken)
        {
            var appointment = await _repositoryAppointment.GetByIdAsync(request.Id);
            appointment.Confirm();

            await _repositoryAppointment.UpdateAsync(appointment);
        }
    }
}
