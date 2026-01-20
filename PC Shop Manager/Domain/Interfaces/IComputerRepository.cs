using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IComputerRepository
    {
        Task<List<Computer>> GetAllAsync();

        Task AddAsync(Computer computer);
    }
}
