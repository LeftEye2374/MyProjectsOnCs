namespace NetWatch.Model
{
    public class NetworkDevice : BaseEntity
    {
        public string IpAddress { get; set; } = default!;
        public string? MACAddress { get; set; }
        public string? HostName { get; set; }
        public string? Vendor { get; set; }
        public DeviceType DeviceType { get; set; } = DeviceType.Unknown;
        public NetStatus Status { get; set; } = default!;
        public DateTime FirstSeen { get; set; } = default!;
        public DateTime LastSeen { get; set; } = default!;
        public bool IsTrusted { get; set; } = default!;
        public string? Notes { get; set; }
        public string? OpenPorts { get; set; } // JSON serialized List<int>
        public virtual ICollection<DeviceScanHistory> ScanHistory { get; set; } = [];
        public virtual ICollection<Alert> Alerts { get; set; } = [];
    }
}
