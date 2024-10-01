using Application.Gateways;
using Application.Gateways.DataAccess;
using Identidade;
using Microsoft.AspNetCore.Identity;

namespace Application.Queries
{
    public interface IHolidayQueries
    {
        public Task<IEnumerable<HolidayDTO>> GetAllAsync();
        public Task<HolidayDTO> GetByIdAsync(int id);
    }

    public class HolidayQueries : IHolidayQueries
    {
        private readonly IRepositoryHoliday _repository;
        private readonly IRepositoryDoctor _repositoryDoctor;
        private readonly IUser _user;
        private readonly UserManager<User> _userManager;

        public HolidayQueries(IRepositoryHoliday repository, IRepositoryDoctor repositoryDoctor,
            IUser user, UserManager<User> userManager)
        {
            _repository = repository;
            _repositoryDoctor = repositoryDoctor;
            _user = user;
            _userManager = userManager;
        }

        public async Task<IEnumerable<HolidayDTO>> GetAllAsync()
        {
            var user = await _userManager.FindByEmailAsync(_user.Email);
            var carWashes = await _repositoryDoctor.SearchAsync(x => x.Email == user.Email);
            var carWashId = carWashes.FirstOrDefault().Id;


            var holidays = await _repository.SearchAsync(x => x.Doctor.Id == carWashId && x.Date.Date >= DateTime.Now.Date);
            return from holiday in holidays
                   select new HolidayDTO()
                   {
                       Id = holiday.Id,
                       Description = holiday.Description,
                       Date = holiday.Date
                   };

        }

        public async Task<HolidayDTO> GetByIdAsync(int id)
        {
            var holiday = await _repository.GetByIdAsync(id);
            return new HolidayDTO()
            {
                Id = holiday.Id,
                Description = holiday.Description,
                Date = holiday.Date
            };
        }
    }

    public class HolidayDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}
