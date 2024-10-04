using Application.Gateways;
using Application.Gateways.DataAccess;
using Application.Notifications;
using FluentValidation;
using Identidade;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.UseCases.Doctors.CreateHoliday
{
    public class CreateHolidayRequestHandler : IRequestHandler<CreateHolidayRequest>
    {
        private readonly INotificator _notificator;
        private readonly IRepositoryDoctor _repositoryDoctor;
        private readonly IRepositoryHoliday _repositoryHoliday;
        private readonly IValidator<CreateHolidayRequest> _validator;
        private readonly IUser _user;
        private readonly UserManager<User> _userManager;

        public CreateHolidayRequestHandler(INotificator notificator, IRepositoryDoctor repositoryDoctor,
            IRepositoryHoliday repositoryHoliday, IValidator<CreateHolidayRequest> validator,
            IUser user, UserManager<User> userManager)
        {
            _notificator = notificator;
            _repositoryDoctor = repositoryDoctor;
            _repositoryHoliday = repositoryHoliday;
            _validator = validator;
            _user = user;
            _userManager = userManager;
        }

        public async Task Handle(CreateHolidayRequest request, CancellationToken cancellationToken)
        {
            var validation = _validator.Validate(request);
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
            await _repositoryHoliday.AddAsync(request.MapToHoliday(doctor));
        }
    }
}
