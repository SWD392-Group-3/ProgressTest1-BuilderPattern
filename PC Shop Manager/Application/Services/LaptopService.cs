using Application.DTOs;
using Application.Interfaces;
using Domain.Builders;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class LaptopService : ILaptopService
    {
        private readonly ILaptopRepository _laptopRepository;

        public LaptopService(ILaptopRepository laptopRepository)
        {
            _laptopRepository = laptopRepository;
        }

        public async Task<Laptop> CreateLaptopOrderAsync(CreateLaptopOrderRequest request)
        {
            var builder = new LaptopBuilder();
            builder.WithOrderName(request.CustomerName);

            var director = new DeviceDirector();

            if (request.OrderType == "Office")
            {
                director.BuildOfficeLaptop(builder);
            }
            else if (request.OrderType == "Gaming")
            {
                director.BuildGamingLaptop(builder);
            }
            else
            {
                if (!string.IsNullOrEmpty(request.CustomCPU)) builder.WithCPU(request.CustomCPU);
                if (!string.IsNullOrEmpty(request.CustomRAM)) builder.WithRAM(request.CustomRAM);
                if (!string.IsNullOrEmpty(request.CustomStorage)) builder.WithStorage(request.CustomStorage);
                if (!string.IsNullOrEmpty(request.CustomScreenSize)) builder.WithScreenSize(request.CustomScreenSize);
                if (!string.IsNullOrEmpty(request.CustomBattery)) builder.WithBattery(request.CustomBattery);
                if (!string.IsNullOrEmpty(request.CustomWeight)) builder.WithWeight(request.CustomWeight);
            }

            var laptop = builder.Build();
            await _laptopRepository.AddAsync(laptop);
            return laptop;
        }

        public async Task<List<LaptopResponse>> GetAllLaptopsAsync()
        {
            var laptops = await _laptopRepository.GetAllAsync();
            return laptops.Select(l => new LaptopResponse
            {
                Id = l.Id,
                OrderName = l.OrderName,
                CPU = l.CPU,
                RAM = l.RAM,
                Storage = l.Storage,
                ScreenSize = l.ScreenSize,
                BatteryCapacity = l.BatteryCapacity,
                Weight = l.Weight,
                EstimatedPrice = l.EstimatedPrice,
                PerformanceScore = l.PerformanceScore
            }).ToList();
        }
    }
}
