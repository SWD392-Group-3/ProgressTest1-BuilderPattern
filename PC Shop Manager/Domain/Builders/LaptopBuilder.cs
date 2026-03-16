using Domain.Entities;

namespace Domain.Builders
{
    public class LaptopBuilder : ILaptopBuilder
    {
        private Laptop _laptop;

        public LaptopBuilder()
        {
            _laptop = new Laptop
            {
                Id = Guid.NewGuid(),
                EstimatedPrice = 50,
                CreatedAt = DateTime.Now
            };
        }

        // ── IDeviceBuilder<Laptop> explicit implementations ──────────────────
        IDeviceBuilder<Laptop> IDeviceBuilder<Laptop>.WithOrderName(string orderName) => WithOrderName(orderName);
        IDeviceBuilder<Laptop> IDeviceBuilder<Laptop>.WithCPU(string cpu) => WithCPU(cpu);
        IDeviceBuilder<Laptop> IDeviceBuilder<Laptop>.WithRAM(string ram) => WithRAM(ram);
        IDeviceBuilder<Laptop> IDeviceBuilder<Laptop>.WithStorage(string storage) => WithStorage(storage);

        // ── ILaptopBuilder fluent methods ────────────────────────────────────
        public ILaptopBuilder WithOrderName(string orderName)
        {
            _laptop.OrderName = orderName;
            return this;
        }

        public ILaptopBuilder WithCPU(string cpu)
        {
            _laptop.CPU = cpu;
            decimal price = cpu.Contains("i9") ? 450 : (cpu.Contains("i7") ? 280 : 130);
            _laptop.EstimatedPrice += price;
            return this;
        }

        public ILaptopBuilder WithRAM(string ram)
        {
            _laptop.RAM = ram;
            _laptop.EstimatedPrice += 40;
            return this;
        }

        public ILaptopBuilder WithStorage(string storage)
        {
            _laptop.Storage = storage;
            _laptop.EstimatedPrice += 80;
            return this;
        }

        public ILaptopBuilder WithScreenSize(string screenSize)
        {
            _laptop.ScreenSize = screenSize;
            _laptop.EstimatedPrice += 50;
            return this;
        }

        public ILaptopBuilder WithBattery(string batteryCapacity)
        {
            _laptop.BatteryCapacity = batteryCapacity;
            _laptop.EstimatedPrice += 30;
            return this;
        }

        public ILaptopBuilder WithWeight(string weight)
        {
            _laptop.Weight = weight;
            return this;
        }

        public Laptop Build()
        {
            // ── Validation ──────────────────────────────────────────────────
            if (string.IsNullOrEmpty(_laptop.CPU))
                throw new InvalidOperationException("Laptop requires a CPU!");

            if (string.IsNullOrEmpty(_laptop.RAM))
                throw new InvalidOperationException("Laptop requires RAM!");

            if (string.IsNullOrEmpty(_laptop.ScreenSize))
                throw new InvalidOperationException("Laptop requires a screen size!");

            // ── Performance scoring ─────────────────────────────────────────
            _laptop.PerformanceScore = CalculatePerformanceScore();

            // ── State reset ─────────────────────────────────────────────────
            var result = _laptop;
            _laptop = new Laptop
            {
                Id = Guid.NewGuid(),
                EstimatedPrice = 50,
                CreatedAt = DateTime.Now
            };

            return result;
        }

        // ── Private helpers ──────────────────────────────────────────────────
        private int CalculatePerformanceScore()
        {
            int score = 0;

            if (_laptop.CPU.Contains("i9")) score += 2800;
            else if (_laptop.CPU.Contains("i7")) score += 1800;
            else if (_laptop.CPU.Contains("i5")) score += 900;
            else score += 400;

            if (_laptop.RAM.Contains("32GB")) score += 400;
            else if (_laptop.RAM.Contains("16GB")) score += 250;
            else score += 80;

            if (_laptop.Storage.Contains("NVMe") || _laptop.Storage.Contains("SSD")) score += 250;

            // Battery bonus for longevity
            if (_laptop.BatteryCapacity.Contains("8000")) score += 300;
            else if (_laptop.BatteryCapacity.Contains("5000")) score += 150;

            return score;
        }
    }
}
