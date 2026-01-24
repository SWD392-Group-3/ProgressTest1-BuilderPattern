namespace Application.DTOs
{
    public class ComputerResponse
    {
        public Guid Id { get; set; }

        public string OrderName { get; set; } = string.Empty;

        public string CPU { get; set; } = string.Empty;

        public string GPU { get; set; } = string.Empty;

        public string RAM { get; set; } = string.Empty;

        public string Storage { get; set; } = string.Empty;

        public bool HasRGB { get; set; }

        public bool HasLiquidCooling { get; set; }

        public decimal EstimatedPrice { get; set; }
    }
}
