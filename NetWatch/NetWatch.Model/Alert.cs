namespace NetWatch.Model
{
    public class Alert
    {
        public int? DeviceId { get; set; }
        public AlertType AlertType { get; set; } = default!;
        public string Message { get; set; } = default!;
        public AlertLevel AlertLevel { get; set; } = default!;
        public DateTime CreatedAt { get; set; } = default!;
        public bool IsAcknowledged { get; set; } = default!;
        public virtual NetworkDevice? Device { get; set; }
    }
}
