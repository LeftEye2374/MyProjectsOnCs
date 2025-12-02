namespace NetWatch.Model
{
    public class NetworkDevice : BaseEntity
    {
        public required string IpAddress { get; set; }
        public string? MACAddress { get; set; }
        public string? HostName { get; set; }
        public string? Vendor { get; set; }
        public DeviceType DeviceType { get; set; } = DeviceType.Unknown;
        public required NetStatus Status { get; set; }
        public required DateTime FirstSeen { get; set; }
        public required DateTime LastSeen { get; set; }
        public required bool IsTrusted { get; set; }
        public string? Notes { get; set; }
        public string? OpenPorts { get; set; } // JSON serialized List<int>
        public virtual ICollection<DeviceScanHistory> ScanHistory { get; set; } = [];
        public virtual ICollection<Alert> Alerts { get; set; } = [];
    }
}
