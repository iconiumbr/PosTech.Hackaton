using Application.Gateways;
using Application.Gateways.DataAccess;
using Application.Notifications;
using Domain.Entities;
using FluentValidation;
using Identidade;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.UseCases.Doctors.InformPeriodService
{
    public class InformPeriodServiceRequestHandler : IRequestHandler<InformPeriodServiceRequest>
    {
        private readonly INotificator _notificator;
        private readonly IRepositoryDoctor _repositoryDoctor;
        private readonly IValidator<InformPeriodServiceRequest> _validator;
        private readonly IUser _user;
        private readonly UserManager<User> _userManager;

        public InformPeriodServiceRequestHandler(INotificator notificator,
            IRepositoryDoctor repositoryDoctor, IValidator<InformPeriodServiceRequest> validator,
            IUser user, UserManager<User> userManager)
        {
            _notificator = notificator;
            _repositoryDoctor = repositoryDoctor;
            _validator = validator;
            _user = user;
            _userManager = userManager;
        }

        public async Task Handle(InformPeriodServiceRequest request, CancellationToken cancellationToken)
        {
            var validation = _validator.Validate(request);
            if (!validation.IsValid)
            {
                _notificator.AddNotifications(validation);
                return;
            }
            var user = await _userManager.FindByEmailAsync(_user.Email);
            var carWashes = await _repositoryDoctor.SearchAsync(x => x.Email == user.Email);
            var carWashId = carWashes.FirstOrDefault().Id;

            var carWash = await _repositoryDoctor.GetByIdAsync(carWashId);
            var schedules =
                from day in request.Days
                select
                    new DoctorSchedule(
                        day.DayOfWeek,
                        carWash, day.MorningStartHour, day.MorningEndHour,
                        day.AfternoonStartHour, day.AfternoonEndHour
                    );

            carWash.InformPeriod(schedules);
            await _repositoryDoctor.UpdateAsync(carWash);
            await _repositoryDoctor.RemoveSchedules();
        }
    }
}
