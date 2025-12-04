using NetWatch.Model.Enums;

namespace NetWatch.Model.Entities
{
    public class DeviceScanHistory : BaseEntity
    {
        public Guid DeviceId { get; set; } = default!;
        public Guid ScanSessionId { get; set; } = default!;
        public DateTime ScanTimestamp { get; set; } = default!;
        public NetStatus StatusAtScan { get; set; } = default!;
        public int? ResponseTimeMs { get; set; }
        public string? OpenPortsSnapshot { get; set; }
        public virtual NetworkDevice Device { get; set; } = null!;
        public virtual NetworkScanSession ScanSession { get; set; } = null!;
    }
}
