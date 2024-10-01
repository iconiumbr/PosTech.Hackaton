using Application.Gateways.DataAccess;
using Application.Notifications;
using MediatR;

namespace Application.UseCases.Appointments.CancelAppointment
{
    public class CancelAppointmentRequestHandler : IRequestHandler<CancelAppointmentRequest>
    {

        private readonly IRepositoryAppointment _repositoryAppointment;
        private readonly INotificator _notificator;

        public CancelAppointmentRequestHandler(IRepositoryAppointment repositoryAppointment, INotificator notificator)
        {
            _repositoryAppointment = repositoryAppointment;
            _notificator = notificator;
        }

        public async Task Handle(CancelAppointmentRequest request, CancellationToken cancellationToken)
        {
            var appointment = await _repositoryAppointment.GetByIdAsync(request.Id);
            appointment.Cancel();

            await _repositoryAppointment.UpdateAsync(appointment);
        }
    }
}
