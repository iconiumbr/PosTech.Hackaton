using Application.Notifications;
using Identidade;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.UseCases.Users.ChangePassword
{
    public class ChangePasswordRequestHandler : IRequestHandler<ChangePasswordRequest>
    {
        private readonly UserManager<User> _userManager;
        private readonly INotificator _notificator;

        public ChangePasswordRequestHandler(UserManager<User> userManager, INotificator notificator)
        {
            _userManager = userManager;
            _notificator = notificator;
        }

        public async Task Handle(ChangePasswordRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            var result = await _userManager.ResetPasswordAsync(user, request.Token, request.Password);
            if (!result.Succeeded)
            {
                _notificator.AddNotification(result.Errors.FirstOrDefault().Description);
            }
        }
    }
}
