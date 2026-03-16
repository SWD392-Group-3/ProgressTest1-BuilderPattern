namespace Domain.Entities
{
    public class Laptop : Device
    {
        public string ScreenSize { get; set; } = string.Empty;
        public string BatteryCapacity { get; set; } = string.Empty;
        public string Weight { get; set; } = string.Empty;
    }
}
