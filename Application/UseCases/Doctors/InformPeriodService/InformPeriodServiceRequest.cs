using MediatR;

namespace Application.UseCases.Doctors.InformPeriodService
{
    public class InformPeriodServiceRequest : IRequest
    {
        public HashSet<DayPeriodService> Days { get; set; }
    }
}
