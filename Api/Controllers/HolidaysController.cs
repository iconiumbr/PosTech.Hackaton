using Application.Notifications;
using Application.Queries;
using Application.UseCases.Doctors.CreateHoliday;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Authorize(Roles = "Doctor")]
    [Route("api/holidays")]
    [ApiController]
    public class HolidaysController : MainController
    {
        private readonly IMediator _mediator;
        private readonly IHolidayQueries _holidayQueries;
        public HolidaysController(INotificator notificator, IMediator mediator,
            IHolidayQueries holidayQueries) : base(notificator)
        {
            _mediator = mediator;
            _holidayQueries = holidayQueries;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
            => CustomResponse(await _holidayQueries.GetAllAsync());

        [HttpGet("id:int")]
        public async Task<IActionResult> GetByIdAsync(int id)
            => CustomResponse(await _holidayQueries.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateHolidayRequest request)
        {
            await _mediator.Send(request);
            return CustomResponse();
        }
    }
}
