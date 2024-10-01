using FluentValidation;

namespace Application.UseCases.Doctors.AddService
{
    public class AddServiceValidator : AbstractValidator<AddServiceRequest>
    {
        public AddServiceValidator()
        {
            RuleFor(request => request.Name).NotEmpty();
            RuleFor(request => request.Description).NotEmpty();
            RuleFor(request => request.Duration).GreaterThanOrEqualTo(new TimeSpan(0, 10, 0));
        }
    }
}
