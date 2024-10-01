using FluentValidation;

namespace Application.UseCases.Users.AddUser
{
    public class AddUserValidator : AbstractValidator<AddUserRequest>
    {
        public AddUserValidator()
        {
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.Password).MinimumLength(8);
            RuleFor(x => x.PhoneNumber).MinimumLength(10);
        }
    }
}
