namespace NetWatch.Model
{
    public class NetworkScanSession : BaseEntity
    {
        public required DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public required string IPRangeScanned { get; set; }
        public required int DevicesFoundCount { get; set; }
        public virtual ICollection<DeviceScanHistory> DeviceScanHistories { get; set; } = [];
    }
}
