namespace NetWatch.Model
{
    public class DeviceScanHistory : BaseEntity
    {
        public required Guid DeviceId { get; set; }
        public required Guid ScanSessionId { get; set; }
        public required DateTime ScanTimestamp { get; set; }
        public required NetStatus StatusAtScan { get; set; }
        public int? ResponseTimeMs { get; set; }
        public string? OpenPortsSnapshot { get; set; }
        public virtual NetworkDevice Device { get; set; } = null!;
        public virtual NetworkScanSession ScanSession { get; set; } = null!;
    }
}
