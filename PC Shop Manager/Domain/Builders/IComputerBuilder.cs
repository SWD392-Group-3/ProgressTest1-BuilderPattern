using Domain.Entities;

namespace Domain.Builders
{
    public interface IComputerBuilder
    {
        IComputerBuilder WithOrderName(string orderName);
        IComputerBuilder WithCPU(string cpu);
        IComputerBuilder WithGPU(string gpu);
        IComputerBuilder WithRAM(string ram);
        IComputerBuilder WithStorage(string storage);
        IComputerBuilder WithRGB(bool hasRGB);
        IComputerBuilder WithLiquidCooling(bool hasLiquidCooling);
        IComputerBuilder WithPSU(string psu);
        IComputerBuilder UsePresetPrice(decimal price);
        Computer Build();
    }
}
