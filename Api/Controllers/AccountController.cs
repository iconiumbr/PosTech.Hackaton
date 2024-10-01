using Api.ViewModels;
using Application.Notifications;
using Application.UseCases.Users.ChangePassword;
using Application.UseCases.Users.GetForgotPasswordEmail;
using Application.UseCases.Users.GetNewEmailConfirmation;
using Identidade;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : MainController
    {
        private readonly AuthenticationService _authenticationService;
        private readonly IMediator _mediator;

        public AccountController(INotificator notificator,
                                 AuthenticationService authenticationService,
                                 IMediator mediator) : base(notificator)
        {
            _authenticationService = authenticationService;
            _mediator = mediator;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginViewModel usuarioLogin)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var result = await _authenticationService.SignInManager.PasswordSignInAsync(usuarioLogin.Email, usuarioLogin.Password,
                false, true);

            if (result.Succeeded)
            {
                return CustomResponse(await _authenticationService.GerarJwt(usuarioLogin.Email));
            }

            if (result.IsLockedOut)
            {
                NotifyError("Usuário temporariamente bloqueado por tentativas inválidas");
                return CustomResponse();
            }

            if (result.IsNotAllowed)
            {
                NotifyError("E-mail precisa ser confirmado");
                return CustomResponse();
            }


            NotifyError("Usuário ou Senha incorretos");
            return CustomResponse();
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult> RefreshToken(GetRefreshTokenViewModel getRefreshToken)
        {
            if (!ModelState.IsValid)
            {
                NotifyError("Refresh Token inválido");
                return CustomResponse();
            }

            var token = await _authenticationService.ObterRefreshToken(getRefreshToken.RefreshToken);

            if (token is null)
            {
                NotifyError("Refresh Token expirado");
                return CustomResponse();
            }
            return CustomResponse(await _authenticationService.GerarJwt(token.Username));
        }

        [HttpGet("confirm-email")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string email, string token)
        {
            var response = await _authenticationService.ConfirmEmail(email, token);
            return Ok(response);
        }

        [HttpGet("forgot-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            await _mediator.Send(new ForgotPasswordRequest { Email = email });
            return CustomResponse();
        }

        [HttpPost("email-confirmation")]
        [AllowAnonymous]
        public async Task<IActionResult> EmailConfirmation(GetNewEmailConfirmationRequest getNewEmailConfirmationRequest)
        {
            await _mediator.Send(getNewEmailConfirmationRequest);
            return CustomResponse();
        }

        [HttpPost("change-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequest changePasswordRequest)
        {
            await _mediator.Send(changePasswordRequest);
            return CustomResponse();
        }

        [HttpPost("change-password-email")]
        [AllowAnonymous]
        public async Task<IActionResult> GetForgotPasswordEmail(GetForgotPasswordEmailRequest getForgotPasswordEmailRequest)
        {
            await _mediator.Send(getForgotPasswordEmailRequest);
            return CustomResponse();
        }
    }
}
