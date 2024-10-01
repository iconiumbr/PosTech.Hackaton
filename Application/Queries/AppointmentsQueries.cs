using Application.Gateways;
using Application.Gateways.DataAccess;

namespace Application.Queries
{

    public interface IAppointmentsQueries
    {
        public Task<IEnumerable<AppointmentsDTO>> MyAppointments();
        public Task<IEnumerable<AppointmentsDTO>> DoctorAppointments();
        public Task<AppointmentsDTO> GetAppointment(int id);

    }
    public class AppointmentsQueries : IAppointmentsQueries
    {
        private readonly IUser _user;
        private readonly IRepositoryAppointment _repository;
        private readonly IRepositoryDoctor _repositoryDoctor;

        public AppointmentsQueries(IUser user, IRepositoryAppointment repository, IRepositoryDoctor repositoryDoctor)
        {
            _user = user;
            _repository = repository;
            _repositoryDoctor = repositoryDoctor;
        }

        public async Task<IEnumerable<AppointmentsDTO>> DoctorAppointments()
        {

            var doctors = await _repositoryDoctor.SearchAsync(x => x.Email == _user.Email);
            var doctor = doctors.FirstOrDefault();

            var appointments = await _repository.SearchAsync(x => x.DateTime >= DateTime.Today && x.Doctor.Id == doctor.Id);
            return from appointment in appointments
                   select new AppointmentsDTO
                   {
                       Id = appointment.Id,
                       Doctor = appointment.Doctor.Name,
                       Service = appointment.Service.Name,
                       Date = appointment.DateTime,
                       Status = (int)appointment.Status,
                       Contact = appointment.Contact
                   };
        }
        public async Task<AppointmentsDTO> GetAppointment(int id)
        {
            var appointment = await _repository.GetByIdAsync(id);
            return new AppointmentsDTO
            {
                Id = appointment.Id,
                Doctor = appointment.Doctor.Name,
                Service = appointment.Service.Name,
                Date = appointment.DateTime,
                Status = (int)appointment.Status,
                Address = $"{appointment.Doctor.Address.Street},{appointment.Doctor.Address.Number},{appointment.Doctor.Address.City},{appointment.Doctor.Address.State}",
                Price = appointment.Service.Price,
                Contact = appointment.Contact
            };
        }
        public async Task<IEnumerable<AppointmentsDTO>> MyAppointments()
        {
            var appointments = await _repository.SearchAsync(x => x.DateTime >= DateTime.Today && x.CreatedBy == _user.Id);
            return from appointment in appointments
                   select new AppointmentsDTO
                   {
                       Id = appointment.Id,
                       Doctor = appointment.Doctor.Name,
                       Service = appointment.Service.Name,
                       Date = appointment.DateTime,
                       Status = (int)appointment.Status,
                       Contact = appointment.Contact
                   };
        }
    }

    public class AppointmentsDTO
    {
        public int Id { get; set; }
        public string Doctor { get; set; }
        public string Contact { get; set; }
        public string Service { get; set; }
        public DateTime Date { get; set; }
        public int Status { get; set; }
        public string Address { get; set; }
        public decimal Price { get; set; }
    }
}
