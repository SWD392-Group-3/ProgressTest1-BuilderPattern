namespace Domain.Builders
{
    public class ComputerDirector
    {
        private IComputerBuilder _builder;

        public ComputerDirector(IComputerBuilder builder)
        {
            _builder = builder;
        }

        public void SetBuilder(IComputerBuilder builder)
        {
            _builder = builder;
        }

        public void BuildOfficePC()
        {
            _builder
                .WithCPU("Intel i5-12400")
                .WithGPU("Intel UHD 730")
                .WithRAM("8GB DDR4")
                .WithStorage("256GB NVMe SSD")
                .WithPSU("500W")
                .UsePresetPrice(700);
        }

        public void BuildGamingPC()
        {
            _builder
                .WithCPU("Intel Core i9-13900K")
                .WithGPU("NVIDIA RTX 4090")
                .WithRAM("32GB DDR5")
                .WithStorage("2TB Samsung 990 Pro")
                .WithRGB(true)
                .WithLiquidCooling(true)
                .WithPSU("1000W")
                .UsePresetPrice(3500);
        }
    }
}
