using Domain.Entities;

namespace Domain.Builders
{
    /// <summary>
    /// DeviceDirector controls the build algorithm for each preset.
    /// It knows WHICH steps to call and in WHAT order — but not HOW each step
    /// is implemented (that is the builder's responsibility).
    ///
    /// Key GoF point demonstrated here:
    ///   - Same director, different concrete builders → different products
    ///   - Office PC preset deliberately omits GPU/RGB/LiquidCooling steps
    /// </summary>
    public class DeviceDirector
    {
        // ── Computer presets ─────────────────────────────────────────────────

        /// <summary>Office PC: basic components only — no discrete GPU, no RGB, no liquid cooling.</summary>
        public void BuildOfficePC(IComputerBuilder builder)
        {
            builder
                .WithCPU("Intel i5-12400")
                .WithRAM("8GB DDR4")
                .WithStorage("256GB NVMe SSD")
                .WithPSU("500W");
            // Intentionally NOT calling: WithGPU, WithRGB, WithLiquidCooling
        }

        /// <summary>Gaming PC: full high-end build with all premium steps.</summary>
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

        // ── Laptop presets ───────────────────────────────────────────────────

        /// <summary>Office Laptop: lightweight, good battery, 15.6" screen.</summary>
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

        /// <summary>Gaming Laptop: high-performance CPU, large screen, big battery.</summary>
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
