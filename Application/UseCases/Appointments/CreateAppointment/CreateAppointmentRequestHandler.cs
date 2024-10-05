
using Application.Gateways;
using Application.Gateways.DataAccess;
using Application.Notifications;
using Application.Queries;
using Domain.Entities;
using Domain.Enums;
using FluentValidation;
using Identidade;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace Application.UseCases.Appointments.CreateAppointment
{
    public class CreateAppointmentRequestHandler : IRequestHandler<CreateAppointmentRequest, int>
    {
        private readonly INotificator _notificator;
        private readonly IRepositoryAppointment _repositoryAppointment;
        private readonly IValidator<CreateAppointmentRequest> _validator;
        private readonly IRepositoryDoctor _repositoryDoctor;
        private readonly IAvailabilityQueries _availabilityQueries;
        private readonly IEmailSender _emailSender;
        private readonly IUser _user;
        private readonly UserManager<User> _userManager;

        public CreateAppointmentRequestHandler(INotificator notificator, IRepositoryAppointment repositoryAppointment,
            IValidator<CreateAppointmentRequest> validator, IRepositoryDoctor repositoryDoctor,
            IAvailabilityQueries availabilityQueries, IUser user, UserManager<User> userManager, IEmailSender emailSender)
        {
            _notificator = notificator;
            _repositoryAppointment = repositoryAppointment;
            _validator = validator;
            _repositoryDoctor = repositoryDoctor;
            _availabilityQueries = availabilityQueries;
            _user = user;
            _userManager = userManager;
            _emailSender = emailSender; 
        }

        public async Task<int> Handle(CreateAppointmentRequest request, CancellationToken cancellationToken)
        {
            var validation = _validator.Validate(request);
            if (!validation.IsValid)
            {
                _notificator.AddNotifications(validation);
                return 0;
            }

            var doctor = await _repositoryDoctor.GetWithAllRelations(request.DoctorId);
            if (doctor == null)
            {
                _notificator.AddNotification("NotFound");
                return 0;
            }

            var doctorService = await _repositoryDoctor.GetServiceByIdAsync(request.ServiceId);
            if (doctorService == null)
            {
                _notificator.AddNotification("NotFound");
                return 0;
            }

            if (_availabilityQueries.ScheduledTime(doctor, request.Date, doctorService.Duration))
            {
                _notificator.AddNotification("There is already a schedule for this time.");
                return 0;
            }

            var user = await _userManager.FindByIdAsync(_user.Id);

            var appointment = new Appointment(user.PhoneNumber, doctor, doctorService, request.Date, AppointmentStatus.Scheduled);
            await _repositoryAppointment.AddAsync(appointment);


            var html = @$"<h1> Nova consulta agendada </h1>
                          <h5> ˮOlá, Dr. {doctor.Name}</h5>
                          <p> Você tem uma nova consulta marcada! </p>
                          <p> Paciente: {user.FullName}. </p>
                          <p> Data e horário: {appointment.DateTime.ToString()}. </p>";
            
            await _emailSender.SendEmailAsync(doctor.Email, "Nova consulta agendada", html);
            return appointment.Id;

        }
    }
}
