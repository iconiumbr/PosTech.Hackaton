using Domain.Entities;

namespace Application.Gateways.DataAccess
{
    public interface IRepositoryDoctor : IRepository<Doctor>
    {
        bool Exists(string cnpj);
        Task RemoveSchedules();
        Task<IEnumerable<DoctorService>> GetServicesByIdAsync(int doctorId);
        Task<DoctorService> GetServiceByIdAsync(int id);
        Task UpdateServiceAsync(DoctorService service);
        Task<Doctor> GetWithAllRelations(int id);
       
    }
}
