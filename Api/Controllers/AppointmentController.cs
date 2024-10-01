using Application.Notifications;
using Application.Queries;
using Application.UseCases.Appointments.CancelAppointment;
using Application.UseCases.Appointments.ConfirmAppointment;
using Application.UseCases.Appointments.CreateAppointment;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/appointments")]
    [ApiController]
    [Authorize]
    public class AppointmentController : MainController
    {
        private readonly IAvailabilityQueries _availabilityQueries;
        private readonly IAppointmentsQueries _appointmentsQueries;
        private readonly IMediator _mediator;
        public AppointmentController(INotificator notificator, IAvailabilityQueries availabilityQueries,
            IMediator mediator, IAppointmentsQueries appointmentsQueries) : base(notificator)
        {
            _availabilityQueries = availabilityQueries;
            _mediator = mediator;
            _appointmentsQueries = appointmentsQueries;
        }

        [HttpGet]
        public async Task<IActionResult> GetAvailability(int carWashId, int carWashService,
            DateTime date)
        {
            var availability = await _availabilityQueries.GetAvailabilityAsync(carWashId, carWashService, date, date);
            return CustomResponse(availability);
        }

        [HttpGet("my")]
        public async Task<IActionResult> GetAppointmentsAsync()
        {
            var appointments = await _appointmentsQueries.MyAppointments();
            return CustomResponse(appointments);
        }

        [HttpGet("car-wash")]
        [Authorize(Roles = "CarWash")]
        public async Task<IActionResult> GetCarWashAppointmentsAsync()
        {
            var appointments = await _appointmentsQueries.DoctorAppointments();
            return CustomResponse(appointments);
        }


        [HttpPost]
        public async Task<IActionResult> AddAppointment(CreateAppointmentRequest createAppointmentRequest)
        {
            var id = await _mediator.Send(createAppointmentRequest);
            return CustomResponse(id);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAppointment(int id)
        {
            var appointment = await _appointmentsQueries.GetAppointment(id);
            return CustomResponse(appointment);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> ConfirmAppointment(int id)
        {
            var request = new ConfirmAppointmentRequest() { Id = id };
            await _mediator.Send(request);
            return CustomResponse();
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> CancelAppointment(int id)
        {
            var request = new CancelAppointmentRequest() { Id = id };
            await _mediator.Send(request);
            return CustomResponse();
        }
    }
}
