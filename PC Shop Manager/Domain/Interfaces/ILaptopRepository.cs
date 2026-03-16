using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ILaptopRepository
    {
        Task<List<Laptop>> GetAllAsync();
        Task AddAsync(Laptop laptop);
    }
}
