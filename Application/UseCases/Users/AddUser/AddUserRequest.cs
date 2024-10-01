using MediatR;

namespace Application.UseCases.Users.AddUser
{
    public class AddUserRequest : IRequest
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
    }
}
