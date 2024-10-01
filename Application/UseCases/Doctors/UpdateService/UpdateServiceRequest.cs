using MediatR;

namespace Application.UseCases.Doctors.UpdateService
{
    public class UpdateServiceRequest : IRequest
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public TimeSpan Duration { get; init; }
        public decimal Price { get; init; }
    }
}
