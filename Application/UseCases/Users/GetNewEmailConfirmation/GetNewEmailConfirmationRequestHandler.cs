using Application.Notifications;
using Identidade;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;

namespace Application.UseCases.Users.GetNewEmailConfirmation
{
    public class GetNewEmailConfirmationRequestHandler : IRequestHandler<GetNewEmailConfirmationRequest>
    {
        private readonly INotificator _notificator;
        private readonly UserManager<User> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly IUrlHelper _urlHelper;

        public GetNewEmailConfirmationRequestHandler(INotificator notificator, UserManager<User> userManager,
            IEmailSender emailSender, IUrlHelper urlHelper)
        {
            _notificator = notificator;
            _userManager = userManager;
            _emailSender = emailSender;
            _urlHelper = urlHelper;
        }

        public async Task Handle(GetNewEmailConfirmationRequest request, CancellationToken cancellationToken)
        {

            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user.EmailConfirmed)
            {
                _notificator.AddNotification("E-mail já confirmado!");
                return;
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var link = _urlHelper.Action("ConfirmEmail", "Account", new { email = user.Email, token }, "https", "localhost:7492");

            var html = @$"<h1> Hackaton </h1>
                          <h5> Confirme seu e-mail</h5>
                          <p> Prezado usuário, confirme seu e-mail através do link abaixo </p>
                          <a href='{link}'>Confirme aqui</a>
                          ";
            await _userManager.AddToRoleAsync(user, "User");
            await _emailSender.SendEmailAsync(user.Email, "Confirme seu e-mail", html);
        }
    }
}
