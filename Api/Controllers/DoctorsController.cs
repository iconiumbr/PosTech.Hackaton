using Application.Notifications;
using Application.Queries;
using Application.UseCases.Doctors.AddService;
using Application.UseCases.Doctors.DoctorRegistration;
using Application.UseCases.Doctors.InformPeriodService;
using Application.UseCases.Doctors.UpdateService;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/doctors")]
    [ApiController]
    public class DoctorsController : MainController
    {
        private readonly IMediator _mediator;
        private readonly IDoctorQueries _doctorQueries;
        public DoctorsController(IMediator mediator, INotificator notificator,
            IDoctorQueries doctorQueries) : base(notificator)
        {
            _mediator = mediator;
            _doctorQueries = doctorQueries;
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpGet("all")]
        public async Task<IActionResult> GetAllAsync() => CustomResponse(await _doctorQueries.GetAllAsync());

        [Authorize(Roles = "User")]
        [HttpGet]
        public async Task<IActionResult> GetNearbyAllAsync(double latitutude, double longitude) => CustomResponse(await _doctorQueries.GetAllAsync());

        [Authorize(Roles = "User")]
        [HttpGet("id:int")]
        public async Task<IActionResult> GetByIdAsync(int id) => CustomResponse(await _doctorQueries.GetByIdAsync(id));

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> RequestRegistrationAsync(DoctorRegistrationRequest requestRegistration)
        {
            await _mediator.Send(requestRegistration);
            return CustomResponse();
        }

        [Authorize(Roles = "Doctor")]
        [HttpPut("inform-period")]
        public async Task<IActionResult> InformPeriod(InformPeriodServiceRequest informPeriodServiceRequest)
        {
            await _mediator.Send(informPeriodServiceRequest);
            return CustomResponse();
        }

        [Authorize(Roles = "Doctor")]
        [HttpPost("services")]
        public async Task<IActionResult> AddService(AddServiceRequest addServiceRequest)
        {
            await _mediator.Send(addServiceRequest);
            return CustomResponse();
        }

        [Authorize(Roles = "Doctor")]
        [HttpPut("services/{id:int}")]
        public async Task<IActionResult> UpdateService(int id, UpdateServiceRequest updateServiceRequest)
        {
            if (id != updateServiceRequest.Id)
            {
                NotifyError("NotFound");
                return CustomResponse();
            }

            await _mediator.Send(updateServiceRequest);
            return CustomResponse();
        }

        [Authorize(Roles = "User")]
        [HttpGet("services/{doctorId:int}")]
        public async Task<IActionResult> GetServicesByDoctor(int doctorId)
        {
            var services = await _doctorQueries.GetServicesByIdAsync(doctorId);
            return CustomResponse(services);
        }

        [Authorize(Roles = "Doctor")]
        [HttpGet("my-services")]
        public async Task<IActionResult> GetServicesByDoctor()
        {
            var services = await _doctorQueries.GetServices();
            return CustomResponse(services);
        }

        [Authorize(Roles = "Doctor")]
        [HttpGet("service/{id:int}")]
        public async Task<IActionResult> GetService(int id)
        {
            var services = await _doctorQueries.GetServiceByIdAsync(id);
            return CustomResponse(services);
        }

        [Authorize(Roles = "Doctor")]
        [HttpGet("periods")]
        public async Task<IActionResult> GetPeriod()
        {
            var periods = await _doctorQueries.GetPeriods();
            return CustomResponse(periods);
        }
    }
}
