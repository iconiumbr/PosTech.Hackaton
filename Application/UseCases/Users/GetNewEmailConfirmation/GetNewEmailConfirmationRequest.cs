using MediatR;

namespace Application.UseCases.Users.GetNewEmailConfirmation
{
    public class GetNewEmailConfirmationRequest : IRequest
    {
        public string Email { get; set; }
    }
}
