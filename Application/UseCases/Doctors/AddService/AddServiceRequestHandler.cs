using Application.Gateways;
using Application.Gateways.DataAccess;
using Application.Notifications;
using FluentValidation;
using Identidade;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.UseCases.Doctors.AddService
{
    public class AddServiceRequestHandler : IRequestHandler<AddServiceRequest>
    {
        private readonly IRepositoryDoctor _repositoryDoctor;
        private readonly INotificator _notificator;
        private readonly IValidator<AddServiceRequest> _validator;
        private readonly IUser _user;
        private readonly UserManager<User> _userManager;
        public AddServiceRequestHandler(IRepositoryDoctor repositoryDoctor, INotificator notificator,
            IValidator<AddServiceRequest> validator, UserManager<User> userManager, IUser user)
        {
            _repositoryDoctor = repositoryDoctor;
            _notificator = notificator;
            _validator = validator;
            _userManager = userManager;
            _user = user;
        }

        public async Task Handle(AddServiceRequest request, CancellationToken cancellationToken)
        {
            var validation = new AddServiceValidator().Validate(request);
            if (!validation.IsValid)
            {
                _notificator.AddNotifications(validation);
                return;
            }

            var user = await _userManager.FindByEmailAsync(_user.Email);
            var doctors = await _repositoryDoctor.SearchAsync(x => x.Email == user.Email);
            var doctorId = doctors.FirstOrDefault().Id;

            var doctor = await _repositoryDoctor.GetByIdAsync(doctorId);
            if (doctor == null)
            {
                _notificator.AddNotification("NotFound");
                return;
            }

            doctor.AddService(request.MapToEntitie());
            await _repositoryDoctor.UpdateAsync(doctor);
        }
    }
}
