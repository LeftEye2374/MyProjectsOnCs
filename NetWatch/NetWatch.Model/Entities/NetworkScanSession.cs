namespace NetWatch.Model.Entities
{
    public class NetworkScanSession : BaseEntity
    {
        public DateTime StartTime { get; set; } = default!;
        public DateTime? EndTime { get; set; }
        public string IPRangeScanned { get; set; } = default!;
        public int DevicesFoundCount { get; set; } = default!;
        public virtual ICollection<DeviceScanHistory> DeviceScanHistories { get; set; } = [];
    }
}
