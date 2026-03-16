using Domain.Entities;

namespace Domain.Builders
{
    public interface ILaptopBuilder : IDeviceBuilder<Laptop>
    {
        new ILaptopBuilder WithOrderName(string orderName);
        new ILaptopBuilder WithCPU(string cpu);
        new ILaptopBuilder WithRAM(string ram);
        new ILaptopBuilder WithStorage(string storage);
        ILaptopBuilder WithScreenSize(string screenSize);
        ILaptopBuilder WithBattery(string batteryCapacity);
        ILaptopBuilder WithWeight(string weight);
    }
}
