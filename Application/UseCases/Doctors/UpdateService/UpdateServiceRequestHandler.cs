using Application.Gateways.DataAccess;
using Application.Notifications;
using FluentValidation;
using MediatR;

namespace Application.UseCases.Doctors.UpdateService
{
    public class UpdateServiceRequestHandler : IRequestHandler<UpdateServiceRequest>
    {
        private readonly IRepositoryDoctor _repositoryDoctor;
        private readonly IValidator<UpdateServiceRequest> _validator;
        private readonly INotificator _notificator;

        public UpdateServiceRequestHandler(IRepositoryDoctor repositoryDoctor, IValidator<UpdateServiceRequest> validator, INotificator notificator)
        {
            _repositoryDoctor = repositoryDoctor;
            _validator = validator;
            _notificator = notificator;
        }

        public async Task Handle(UpdateServiceRequest request, CancellationToken cancellationToken)
        {
            var validation = _validator.Validate(request);
            if (!validation.IsValid)
            {
                _notificator.AddNotifications(validation);
                return;
            }

            var service = await _repositoryDoctor.GetServiceByIdAsync(request.Id);
            if (service == null)
            {
                _notificator.AddNotification("NotFound");
                return;
            }

            service.UpdateInfo(request.Name, request.Description, request.Duration, request.Price);
            await _repositoryDoctor.UpdateServiceAsync(service);
        }
    }
}
