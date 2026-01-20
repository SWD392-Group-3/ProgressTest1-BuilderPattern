namespace Application.DTOs
{
    public class CreateOrderRequest
    {
        public string CustomerName { get; set; } = string.Empty;

        public string OrderType { get; set; } = string.Empty;

        public string? CustomCPU { get; set; }

        public string? CustomGPU { get; set; }

        public string? CustomRAM { get; set; }

        public string? CustomStorage { get; set; }

        public bool? IsLiquidCooling { get; set; }

        public bool? IsRGBLighting { get; set; }
    }
}
