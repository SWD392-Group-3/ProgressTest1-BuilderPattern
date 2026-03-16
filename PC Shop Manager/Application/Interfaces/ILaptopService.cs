using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface ILaptopService
    {
        Task<List<LaptopResponse>> GetAllLaptopsAsync();
        Task<Laptop> CreateLaptopOrderAsync(CreateLaptopOrderRequest request);
    }
}
