namespace Domain.Entities
{
    public class Computer : Device
    {
        public string GPU { get; set; } = string.Empty;
        public string PSU { get; set; } = string.Empty;
        public bool HasRGB { get; set; }
        public bool HasLiquidCooling { get; set; }
    }
}
