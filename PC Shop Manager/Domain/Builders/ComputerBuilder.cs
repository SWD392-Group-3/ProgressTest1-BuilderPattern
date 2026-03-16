using Domain.Entities;

namespace Domain.Builders
{
    public class ComputerBuilder : IComputerBuilder
    {
        private Computer _computer;

        public ComputerBuilder()
        {
            _computer = new Computer
            {
                Id = Guid.NewGuid(),
                EstimatedPrice = 100,
                CreatedAt = DateTime.Now
            };
        }

        IDeviceBuilder<Computer> IDeviceBuilder<Computer>.WithOrderName(string orderName) => WithOrderName(orderName);
        IDeviceBuilder<Computer> IDeviceBuilder<Computer>.WithCPU(string cpu) => WithCPU(cpu);
        IDeviceBuilder<Computer> IDeviceBuilder<Computer>.WithRAM(string ram) => WithRAM(ram);
        IDeviceBuilder<Computer> IDeviceBuilder<Computer>.WithStorage(string storage) => WithStorage(storage);

        public IComputerBuilder WithOrderName(string orderName)
        {
            _computer.OrderName = orderName;
            return this;
        }

        public IComputerBuilder WithCPU(string cpu)
        {
            _computer.CPU = cpu;
            decimal price = cpu.Contains("i9") ? 500 : (cpu.Contains("i7") ? 300 : 150);
            _computer.EstimatedPrice += price;
            return this;
        }

        public IComputerBuilder WithGPU(string gpu)
        {
            _computer.GPU = gpu;
            decimal price = gpu.Contains("4090") ? 1500 : (gpu.Contains("3060") ? 400 : 0);
            _computer.EstimatedPrice += price;
            return this;
        }

        public IComputerBuilder WithRAM(string ram)
        {
            _computer.RAM = ram;
            _computer.EstimatedPrice += 50;
            return this;
        }

        public IComputerBuilder WithStorage(string storage)
        {
            _computer.Storage = storage;
            _computer.EstimatedPrice += 100;
            return this;
        }

        public IComputerBuilder WithRGB(bool hasRGB)
        {
            _computer.HasRGB = hasRGB;
            if (hasRGB) _computer.EstimatedPrice += 30;
            return this;
        }

        public IComputerBuilder WithLiquidCooling(bool hasLiquidCooling)
        {
            _computer.HasLiquidCooling = hasLiquidCooling;
            if (hasLiquidCooling) _computer.EstimatedPrice += 120;
            return this;
        }

        public IComputerBuilder WithPSU(string psu)
        {
            _computer.PSU = psu;
            if (psu.Contains("1000W")) _computer.EstimatedPrice += 200;
            else if (psu.Contains("750W")) _computer.EstimatedPrice += 100;
            else _computer.EstimatedPrice += 50;
            return this;
        }

        public IComputerBuilder UsePresetPrice(decimal price)
        {
            _computer.EstimatedPrice = price;
            return this;
        }

        public Computer Build()
        {
            if (string.IsNullOrEmpty(_computer.CPU))
                throw new InvalidOperationException("A computer cannot function without a CPU!");

            if (string.IsNullOrEmpty(_computer.RAM))
                throw new InvalidOperationException("A computer cannot function without RAM!");

            if (string.IsNullOrEmpty(_computer.PSU))
                throw new InvalidOperationException("A computer cannot function without a PSU!");

            int neededWattage = CalculateComponentWattage();
            int psuWattage = GetPSUWattage(_computer.PSU);
            if (psuWattage < neededWattage)
                throw new InvalidOperationException(
                    $"Danger! PSU ({psuWattage}W) is too weak for this system (Needs ~{neededWattage}W). Boom!");

            if (_computer.GPU.Contains("4090") && !_computer.CPU.Contains("i9") && !_computer.CPU.Contains("i7"))
                throw new InvalidOperationException(
                    "Anti-Stupid Rule: You cannot pair an RTX 4090 with a weak CPU! Complete bottleneck.");

            if (_computer.EstimatedPrice > 2000 && !_computer.HasLiquidCooling)
                throw new InvalidOperationException(
                    "High-end PCs (over $2000) absolutely require liquid cooling!");

            _computer.PerformanceScore = CalculatePerformanceScore();

            var result = _computer;
            _computer = new Computer
            {
                Id = Guid.NewGuid(),
                EstimatedPrice = 100,
                CreatedAt = DateTime.Now
            };

            return result;
        }

        private int CalculateComponentWattage()
        {
            int total = 100; // base system
            if (_computer.CPU.Contains("i9")) total += 250;
            else if (_computer.CPU.Contains("i7")) total += 150;
            else total += 65;

            if (_computer.GPU.Contains("4090")) total += 450;
            else if (_computer.GPU.Contains("3060")) total += 170;
            else if (!string.IsNullOrEmpty(_computer.GPU) && !_computer.GPU.Contains("Integrated")) total += 200;

            return total;
        }

        private int GetPSUWattage(string psu)
        {
            var digits = new string(psu.Where(char.IsDigit).ToArray());
            return int.TryParse(digits, out int watts) ? watts : 0;
        }

        private int CalculatePerformanceScore()
        {
            int score = 0;

            if (_computer.CPU.Contains("i9")) score += 3000;
            else if (_computer.CPU.Contains("i7")) score += 2000;
            else if (_computer.CPU.Contains("i5")) score += 1000;
            else score += 500;

            if (_computer.GPU.Contains("4090")) score += 5000;
            else if (_computer.GPU.Contains("3060")) score += 2000;
            else if (_computer.GPU.Contains("UHD")) score += 200;
            else score += 1000;

            if (_computer.RAM.Contains("32GB")) score += 500;
            else if (_computer.RAM.Contains("16GB")) score += 300;
            else score += 100;

            if (_computer.HasLiquidCooling) score += 200;
            if (_computer.Storage.Contains("NVMe") || _computer.Storage.Contains("SSD")) score += 300;

            return score;
        }
    }
}
