using Application.Gateways.DataAccess;
using Application.Notifications;
using FluentValidation;
using Identidade;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.UseCases.Doctors.DoctorRegistration
{
    public class DoctorRegistrationRequestHandler : IRequestHandler<DoctorRegistrationRequest>
    {
        private readonly INotificator _notificator;
        private readonly IValidator<DoctorRegistrationRequest> _validator;
        private readonly IRepositoryDoctor _repository;
        private readonly UserManager<User> _userManager;

        public DoctorRegistrationRequestHandler(INotificator notificator,
            IValidator<DoctorRegistrationRequest> validator, IRepositoryDoctor repository,
            UserManager<User> userManager)
        {
            _notificator = notificator;
            _validator = validator;
            _repository = repository;
            _userManager = userManager;
        }

        public async Task Handle(DoctorRegistrationRequest request, CancellationToken cancellationToken)
        {
            var validation = _validator.Validate(request);
            if (!validation.IsValid)
            {
                _notificator.AddNotifications(validation);
                return;
            }

            var user = new User
            {
                UserName = request.Email,
                Email = request.Email,
                EmailConfirmed = false,
                PhoneNumber = request.ContactNumber
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    _notificator.AddNotification(error.Description);
                return;
            }

            await _userManager.AddToRoleAsync(user, "Doctor");
            await _repository.AddAsync(request.MapToDoctor());
        }
    }
}
