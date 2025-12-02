namespace NetWatch.Model
{
    public class Alert
    {
        public int? DeviceId { get; set; }
        public required AlertType AlertType { get; set; }
        public required string Message { get; set; }
        public required AlertLevel AlertLevel { get; set; }
        public required DateTime CreatedAt { get; set; }
        public required bool IsAcknowledged { get; set; }
        public virtual NetworkDevice? Device { get; set; }
    }
}
