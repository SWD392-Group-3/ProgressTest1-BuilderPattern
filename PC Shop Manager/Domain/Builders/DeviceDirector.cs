namespace Domain.Builders
{
    public class DeviceDirector
    {
        public void BuildOfficePC(IComputerBuilder builder)
        {
            builder
                .WithCPU("Intel i5-12400")
                .WithRAM("8GB DDR4")
                .WithStorage("256GB NVMe SSD")
                .WithPSU("500W");
        }

        public void BuildGamingPC(IComputerBuilder builder)
        {
            builder
                .WithCPU("Intel Core i9-13900K")
                .WithGPU("NVIDIA RTX 4090")
                .WithRAM("32GB DDR5")
                .WithStorage("2TB Samsung 990 Pro NVMe")
                .WithRGB(true)
                .WithLiquidCooling(true)
                .WithPSU("1000W");
        }

        public void BuildOfficeLaptop(ILaptopBuilder builder)
        {
            builder
                .WithCPU("Intel i5-1235U")
                .WithRAM("8GB DDR4")
                .WithStorage("256GB NVMe SSD")
                .WithScreenSize("15.6 inch FHD")
                .WithBattery("5000mAh")
                .WithWeight("1.8kg");
        }

        public void BuildGamingLaptop(ILaptopBuilder builder)
        {
            builder
                .WithCPU("Intel Core i9-13900H")
                .WithRAM("32GB DDR5")
                .WithStorage("1TB NVMe SSD")
                .WithScreenSize("17.3 inch QHD 165Hz")
                .WithBattery("8000mAh")
                .WithWeight("2.5kg");
        }
    }
}
