using Application.Notifications;
using FluentValidation;
using Identidade;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;

namespace Application.UseCases.Users.AddUser
{
    internal class AddUserRequestHandler : IRequestHandler<AddUserRequest>
    {
        private readonly INotificator _notificator;
        private readonly IValidator<AddUserRequest> _validator;
        private readonly UserManager<User> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly IUrlHelper _urlHelper;
        public AddUserRequestHandler(INotificator notificator, IValidator<AddUserRequest> validator,
            UserManager<User> userManager, IEmailSender emailSender, IUrlHelper urlHelper)
        {
            _notificator = notificator;
            _validator = validator;
            _userManager = userManager;
            _emailSender = emailSender;
            _urlHelper = urlHelper;
        }

        public async Task Handle(AddUserRequest request, CancellationToken cancellationToken)
        {
            var validation = _validator.Validate(request);
            if (!validation.IsValid)
            {
                _notificator.AddNotifications(validation);
                return;
            }

            var user = new User
            {
                UserName = request.Email,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                FullName = request.FullName,
                Cpf = request.Cpf,
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    _notificator.AddNotification(error.Description);
                return;
            }


            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var link = _urlHelper.Action("ConfirmEmail", "Account", new { email = user.Email, token }, "https", "localhost:7482");

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
