using Application.DTOs;
using Application.Interfaces;
using Domain.Builders;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class ComputerService : IComputerService
    {
        private readonly IComputerRepository _computerRepository;

        public ComputerService(IComputerRepository computerRepository)
        {
            _computerRepository = computerRepository;
        }

        public async Task<Computer> CreateOrderAsync(CreateOrderRequest request)
        {
            var builder = new ComputerBuilder();
            builder.WithOrderName(request.CustomerName); // set OrderName BEFORE director

            var director = new DeviceDirector();

            if (request.OrderType == "Office")
            {
                // Director sets component steps; OrderName is already on the builder
                director.BuildOfficePC(builder);
            }
            else if (request.OrderType == "Gaming")
            {
                director.BuildGamingPC(builder);
            }
            else
            {
                // Custom order — build incrementally from request
                if (!string.IsNullOrEmpty(request.CustomCPU))     builder.WithCPU(request.CustomCPU);
                if (!string.IsNullOrEmpty(request.CustomGPU))     builder.WithGPU(request.CustomGPU);
                if (!string.IsNullOrEmpty(request.CustomRAM))     builder.WithRAM(request.CustomRAM);
                if (!string.IsNullOrEmpty(request.CustomStorage)) builder.WithStorage(request.CustomStorage);
                if (!string.IsNullOrEmpty(request.CustomPSU))     builder.WithPSU(request.CustomPSU);
                if (request.IsLiquidCooling == true)              builder.WithLiquidCooling(true);
                if (request.IsRGBLighting == true)                builder.WithRGB(true);
            }

            var computer = builder.Build();
            await _computerRepository.AddAsync(computer);
            return computer;
        }

        public async Task<List<ComputerResponse>> GetAllComputersAsync()
        {
            var computers = await _computerRepository.GetAllAsync();
            return computers.Select(c => new ComputerResponse
            {
                Id               = c.Id,
                OrderName        = c.OrderName,
                CPU              = c.CPU,
                GPU              = c.GPU,
                RAM              = c.RAM,
                Storage          = c.Storage,
                HasRGB           = c.HasRGB,
                HasLiquidCooling = c.HasLiquidCooling,
                EstimatedPrice   = c.EstimatedPrice
            }).ToList();
        }
    }
}
