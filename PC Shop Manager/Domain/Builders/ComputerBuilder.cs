using Domain.Entities;

namespace Domain.Builders
{
    public class ComputerBuilder
    {
        private Computer _computer = new Computer();

        public ComputerBuilder()
        {
            _computer.Id = Guid.NewGuid();
            _computer.EstimatedPrice = 100;
            _computer.CreatedAt = DateTime.Now;
        }

        public ComputerBuilder WithOrderName(string orderName)
        {
            _computer.OrderName = orderName;
            return this;
        }

        public ComputerBuilder WithCPU(string cpu)
        {
            _computer.CPU = cpu;
            decimal price = cpu.Contains("i9") ? 500 : (cpu.Contains("i7") ? 300 : 150);
            _computer.EstimatedPrice += price;
            return this;
        }

        public ComputerBuilder WithGPU(string gpu)
        {
            _computer.GPU = gpu;
            decimal price = gpu.Contains("4090") ? 1500 : (gpu.Contains("3060") ? 400 : 0);
            _computer.EstimatedPrice += price;
            return this;
        }

        public ComputerBuilder WithRAM(string ram)
        {
            _computer.RAM = ram;
            _computer.EstimatedPrice += 50;
            return this;
        }

        public ComputerBuilder WithStorage(string storage)
        {
            _computer.Storage = storage;
            _computer.EstimatedPrice += 100;
            return this;
        }

        public ComputerBuilder WithRGB(bool hasRGB)
        {
            _computer.HasRGB = hasRGB;
            if (hasRGB) _computer.EstimatedPrice += 30;
            return this;
        }

        public ComputerBuilder WithLiquidCooling(bool hasLiquidCooling)
        {
            _computer.HasLiquidCooling = hasLiquidCooling;
            if (hasLiquidCooling) _computer.EstimatedPrice += 120;
            return this;
        }

        public ComputerBuilder AsOfficePreset()
        {
            _computer.CPU = "Intel i5-12400";
            _computer.GPU = "Intel UHD 730";
            _computer.RAM = "8GB DDR4";
            _computer.Storage = "256GB NVMe SSD";
            _computer.HasRGB = false;
            _computer.HasLiquidCooling = false;
            _computer.EstimatedPrice = 700;
            return this;
        }

        public ComputerBuilder AsGamingPreset()
        {
            _computer.CPU = "Intel Core i9-13900K";
            _computer.GPU = "NVIDIA RTX 4090";
            _computer.RAM = "32GB DDR5";
            _computer.Storage = "2TB Samsung 990 Pro";
            _computer.HasRGB = true;
            _computer.HasLiquidCooling = true;
            _computer.EstimatedPrice = 3500;
            return this;
        }

        public Computer Build()
        {
            if (string.IsNullOrEmpty(_computer.CPU))
                throw new InvalidOperationException("A computer cannot function without a CPU.!");

            if (string.IsNullOrEmpty(_computer.RAM))
                throw new InvalidOperationException("A computer cannot function without a RAM!");

            if (_computer.EstimatedPrice > 2000 && !_computer.HasLiquidCooling)
            {
                throw new InvalidOperationException("High-end PCs (over $2000) absolutely require water cooling!");
            }

            return _computer;
        }
    }
}