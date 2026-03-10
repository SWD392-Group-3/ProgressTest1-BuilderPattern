using Domain.Entities;

namespace Domain.Builders
{
    public class ComputerBuilder : IComputerBuilder
    {
        private Computer _computer = new Computer();

        public ComputerBuilder()
        {
            _computer.Id = Guid.NewGuid();
            _computer.EstimatedPrice = 100;
            _computer.CreatedAt = DateTime.Now;
        }

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
            // Simple price logic for PSU
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
                throw new InvalidOperationException("A computer cannot function without a CPU.!");

            if (string.IsNullOrEmpty(_computer.RAM))
                throw new InvalidOperationException("A computer cannot function without a RAM!");

            if (string.IsNullOrEmpty(_computer.PSU))
                throw new InvalidOperationException("A computer cannot function without a PSU!");

            // 1. Compatibility Check: PSU Wattage
            int neededWattage = CalculateComponentWattage();
            int psuWattage = GetPSUWattage(_computer.PSU);
            if (psuWattage < neededWattage)
            {
                throw new InvalidOperationException($"Danger! PSU ({psuWattage}W) is too weak for this system (Needs ~{neededWattage}W). Boom!");
            }

            // 2. Anti-Stupid Rule: Bottleneck Check
            // Example: High-end GPU (4090) with Low-end CPU (non-i7/i9) -> Exception or Warning
            if (_computer.GPU.Contains("4090") && !_computer.CPU.Contains("i9") && !_computer.CPU.Contains("i7"))
            {
                throw new InvalidOperationException("Anti-Stupid Rule Triggered: You cannot pair an RTX 4090 with a weak CPU! Complete bottleneck.");
            }

            // 3. High-end cooling rule
            if (_computer.EstimatedPrice > 2000 && !_computer.HasLiquidCooling)
            {
                throw new InvalidOperationException("High-end PCs (over $2000) absolutely require water cooling!");
            }

            // 4. Performance Scoring
            _computer.PerformanceScore = CalculatePerformanceScore();

            return _computer;
        }

        private int CalculateComponentWattage()
        {
            int total = 100; // Base system (Motherboard, Fans, SSD)
            
            // CPU
            if (_computer.CPU.Contains("i9")) total += 250;
            else if (_computer.CPU.Contains("i7")) total += 150;
            else total += 65;

            // GPU
            if (_computer.GPU.Contains("4090")) total += 450;
            else if (_computer.GPU.Contains("3060")) total += 170;
            else if (!string.IsNullOrEmpty(_computer.GPU) && !_computer.GPU.Contains("Integrated")) total += 200; // Generic GPU

            return total;
        }

        private int GetPSUWattage(string psu)
        {
            // Simple parsing: separate number from string "750W", "Corsair 1000W"
            var digits = new string(psu.Where(char.IsDigit).ToArray());
            if (int.TryParse(digits, out int watts))
            {
                return watts;
            }
            return 0; // Invalid PSU string
        }

        private int CalculatePerformanceScore()
        {
            int score = 0;

            // CPU Score
            if (_computer.CPU.Contains("i9")) score += 3000;
            else if (_computer.CPU.Contains("i7")) score += 2000;
            else if (_computer.CPU.Contains("i5")) score += 1000;
            else score += 500;

            // GPU Score
            if (_computer.GPU.Contains("4090")) score += 5000;
            else if (_computer.GPU.Contains("3060")) score += 2000;
            else if (_computer.GPU.Contains("UHD")) score += 200; // Integrated
            else score += 1000;

            // RAM Score
            if (_computer.RAM.Contains("32GB")) score += 500;
            else if (_computer.RAM.Contains("16GB")) score += 300;
            else score += 100;

            // Bonus
            if (_computer.HasLiquidCooling) score += 200;
            if (_computer.Storage.Contains("NVMe") || _computer.Storage.Contains("SSD")) score += 300;

            return score;
        }
    }
}