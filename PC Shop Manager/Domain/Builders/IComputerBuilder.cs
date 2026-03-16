using Domain.Entities;

namespace Domain.Builders
{
    public interface IComputerBuilder : IDeviceBuilder<Computer>
    {
        new IComputerBuilder WithOrderName(string orderName);
        new IComputerBuilder WithCPU(string cpu);
        new IComputerBuilder WithRAM(string ram);
        new IComputerBuilder WithStorage(string storage);
        IComputerBuilder WithGPU(string gpu);
        IComputerBuilder WithPSU(string psu);
        IComputerBuilder WithRGB(bool hasRGB);
        IComputerBuilder WithLiquidCooling(bool hasLiquidCooling);
        IComputerBuilder UsePresetPrice(decimal price);
    }
}
