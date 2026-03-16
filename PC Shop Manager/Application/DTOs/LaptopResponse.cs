namespace Application.DTOs
{
    public class LaptopResponse
    {
        public Guid Id { get; set; }
        public string OrderName { get; set; } = string.Empty;
        public string CPU { get; set; } = string.Empty;
        public string RAM { get; set; } = string.Empty;
        public string Storage { get; set; } = string.Empty;
        public string ScreenSize { get; set; } = string.Empty;
        public string BatteryCapacity { get; set; } = string.Empty;
        public string Weight { get; set; } = string.Empty;
        public decimal EstimatedPrice { get; set; }
        public int PerformanceScore { get; set; }
    }
}
