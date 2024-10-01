using FluentValidation;

namespace Application.UseCases.Appointments.CreateAppointment
{
    public class CreateAppointmentValidator : AbstractValidator<CreateAppointmentRequest>
    {
        public CreateAppointmentValidator()
        {
            RuleFor(x => x.DoctorId).GreaterThan(0);
            RuleFor(x => x.ServiceId).GreaterThan(0);
            RuleFor(x => x.Date).GreaterThan(DateTime.Now);
        }
    }
}
