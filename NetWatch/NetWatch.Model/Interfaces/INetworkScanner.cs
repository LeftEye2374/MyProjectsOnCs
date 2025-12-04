namespace NetWatch.Model.Interfaces
{
    public interface INetworkScanner
    {
        event EventHandler<NetworkDevice> DeviceDiscovered;
        event EventHandler<string> ScanStatusChanged;
        event EventHandler<double> ScanProgressChanged;
        Task<List<NetworkDevice>> ScanNetworkAsync(string ipRange);
    }
}
