using MediatR;

namespace Application.UseCases.Users.GetForgotPasswordEmail
{
    public class GetForgotPasswordEmailRequest : IRequest
    {
        public string Email { get; set; }
    }
}
