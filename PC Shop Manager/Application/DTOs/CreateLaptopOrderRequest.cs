namespace Application.DTOs
{
    public class CreateLaptopOrderRequest
    {
        public string CustomerName { get; set; } = string.Empty;
        public string OrderType { get; set; } = string.Empty;
        public string? CustomCPU { get; set; }
        public string? CustomRAM { get; set; }
        public string? CustomStorage { get; set; }
        public string? CustomScreenSize { get; set; }
        public string? CustomBattery { get; set; }
        public string? CustomWeight { get; set; }
    }
}
