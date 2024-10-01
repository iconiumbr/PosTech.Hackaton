using Application.Notifications;
using Identidade;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace Application.UseCases.Users.GetForgotPasswordEmail
{
    public class GetForgotPasswordEmailRequestHandler : IRequestHandler<GetForgotPasswordEmailRequest>
    {
        private readonly INotificator _notificator;
        private readonly UserManager<User> _userManager;
        private readonly IEmailSender _emailSender;

        public GetForgotPasswordEmailRequestHandler(INotificator notificator,
            UserManager<User> userManager, IEmailSender emailSender)
        {
            _notificator = notificator;
            _userManager = userManager;
            _emailSender = emailSender;
        }

        public async Task Handle(GetForgotPasswordEmailRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            token.Replace("+", " ");
            var link = new Uri($"https://localhost:7492/change-password/{user.Email}/{token}");

            var html = @$"<h1> Hackaton </h1>
                          <h5> Confirme seu e-mail</h5>
                          <p> Prezado usuário, altere sua senha através do link abaixo</p>
                          <a href='{link}'>Confirme aqui</a>
                          ";
            await _userManager.AddToRoleAsync(user, "User");
            await _emailSender.SendEmailAsync(user.Email, "Alteração de Senha", html);
        }
    }
}
