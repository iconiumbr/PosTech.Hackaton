using MediatR;

namespace Application.UseCases.Users.ChangePassword
{
    public class ChangePasswordRequest : IRequest
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public string Password { get; set; }
    }
}
