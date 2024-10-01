using FluentValidation;

namespace Application.UseCases.Doctors.UpdateService
{
    public class UpdateServiceValidator : AbstractValidator<UpdateServiceRequest>
    {
        public UpdateServiceValidator()
        {
            RuleFor(request => request.Name).NotEmpty();
            RuleFor(request => request.Description).NotEmpty();
            RuleFor(request => request.Duration).GreaterThanOrEqualTo(new TimeSpan(0, 10, 0));
        }
    }
}
