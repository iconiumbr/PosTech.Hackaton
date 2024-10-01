using Application.Notifications;
using Application.UseCases.Users.AddUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : MainController
    {
        private readonly IMediator _mediator;
        public UserController(INotificator notificator, IMediator mediator) : base(notificator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddUserAsync(AddUserRequest request)
        {
            await _mediator.Send(request);
            return CustomResponse();
        }
    }
}
