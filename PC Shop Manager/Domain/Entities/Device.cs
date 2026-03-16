namespace Domain.Entities
{
    public abstract class Device
    {
        public Guid Id { get; set; }
        public string OrderName { get; set; } = string.Empty;
        public string CPU { get; set; } = string.Empty;
        public string RAM { get; set; } = string.Empty;
        public string Storage { get; set; } = string.Empty;
        public decimal EstimatedPrice { get; set; }
        public DateTime CreatedAt { get; set; }
        public int PerformanceScore { get; set; }
    }
}
