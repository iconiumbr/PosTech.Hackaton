using Application.Gateways;
using Application.Gateways.DataAccess;
using Identidade;
using Microsoft.AspNetCore.Identity;

namespace Application.Queries
{
    public interface IDoctorQueries
    {
        public Task<IEnumerable<DoctorDTO>> GetAllAsync();
        public Task<DoctorDTO> GetByIdAsync(int id);
        public Task<IEnumerable<ServiceDTO>> GetServicesByIdAsync(int carWashId);
        public Task<IEnumerable<ServiceDTO>> GetServices();
        Task<ServiceDTO> GetServiceByIdAsync(int id);
        Task<IEnumerable<PeriodDTO>> GetPeriods();
       
    }
    public class DoctorQueries : IDoctorQueries
    {
        private readonly IRepositoryDoctor _repository;
        private readonly IUser _user;
        private readonly UserManager<User> _userManager;

        public DoctorQueries(IRepositoryDoctor repository, IUser user, UserManager<User> userManager)
        {
            _repository = repository;
            _user = user;
            _userManager = userManager;
        }

        public async Task<IEnumerable<DoctorDTO>> GetAllAsync()
        {
            var washes = await _repository.GetAllAsync();
            return from wash in washes
                   select new DoctorDTO()
                   {
                       Id = wash.Id,
                       Name = wash.Name,
                       ContactNumber = wash.ContactNumber,
                       Email = wash.Email,
                       Address = $"{wash.Address.Street}, {wash.Address.Number}"
                   };
        }
        public async Task<DoctorDTO> GetByIdAsync(int id)
        {
            var wash = await _repository.GetByIdAsync(id);
            return new DoctorDTO()
            {
                Id = wash.Id,
                Name = wash.Name,
                ContactNumber = wash.ContactNumber,
                Email = wash.Email,
                Address = $"{wash.Address.State}, {wash.Address.Number}"
            };
        }
        public async Task<IEnumerable<ServiceDTO>> GetServices()
        {
            var user = await _userManager.FindByEmailAsync(_user.Email);
            var carWashes = await _repository.SearchAsync(x => x.Email == user.Email);
            var carWashId = carWashes.FirstOrDefault().Id;

            var services = await _repository.GetServicesByIdAsync(carWashId);
            return from service in services
                   select new ServiceDTO()
                   {
                       CarWashId = carWashId,
                       Id = service.Id,
                       Description = service.Description,
                       Duration = service.Duration,
                       Price = service.Price,
                       Name = service.Name
                   };
        }
      
        public async Task<IEnumerable<PeriodDTO>> GetPeriods()
        {
            var user = await _userManager.FindByEmailAsync(_user.Email);
            var doctors = await _repository.SearchAsync(x => x.Email == user.Email);
            var doctor = doctors.FirstOrDefault();

            return from day in doctor.Schedules
                   select new PeriodDTO()
                   {
                       DayOfWeek = day.DayOfWeek,
                       MorningEndHour = day.MorningEndHour,
                       MorningStartHour = day.MorningStartHour,
                       AfternoonEndHour = day.AfternoonEndHour,
                       AfternoonStartHour = day.AfternoonStartHour
                   };
        }
        public async Task<ServiceDTO> GetServiceByIdAsync(int id)
        {
            var service = await _repository.GetServiceByIdAsync(id);
            return new ServiceDTO()
            {
                Id = service.Id,
                Description = service.Description,
                Duration = service.Duration,
                Price = service.Price,
                Name = service.Name
            };

        }
        public async Task<IEnumerable<ServiceDTO>> GetServicesByIdAsync(int carWashId)
        {
            var services = await _repository.GetServicesByIdAsync(carWashId);
            return from service in services
                   select new ServiceDTO()
                   {
                       CarWashId = carWashId,
                       Id = service.Id,
                       Description = service.Description,
                       Duration = service.Duration,
                       Price = service.Price,
                       Name = service.Name
                   };
        }
    }
    public class DoctorDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
    public class ServiceDTO
    {
        public int Id { get; set; }
        public int CarWashId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TimeSpan Duration { get; set; }
        public decimal Price { get; set; }
    }
    public class PeriodDTO
    {
        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan MorningStartHour { get; set; }
        public TimeSpan MorningEndHour { get; set; }
        public TimeSpan AfternoonStartHour { get; set; }
        public TimeSpan AfternoonEndHour { get; set; }
    }
   
}
