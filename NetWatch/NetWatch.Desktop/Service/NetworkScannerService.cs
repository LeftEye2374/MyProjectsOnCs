using NetWatch.Model;
using NetWatch.Model.Interfaces;

namespace NetWatch.Desktop.Service
{
    internal class NetworkScannerService : INetworkScanner
    {
       

        public event EventHandler<NetworkDevice> DeviceDiscovered;
        public event EventHandler<string> ScanStatusChanged;
        public event EventHandler<double> ScanProgressChanged;

        public Task<List<NetworkDevice>> ScanNetworkAsync(string ipRange)
        {
            throw new NotImplementedException();
        }
    }
}
