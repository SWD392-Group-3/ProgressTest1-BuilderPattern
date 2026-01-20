using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IComputerService
    {
        Task<List<ComputerResponse>> GetAllComputersAsync();

        Task<Computer> CreateOrderAsync(CreateOrderRequest request);
    }
}
