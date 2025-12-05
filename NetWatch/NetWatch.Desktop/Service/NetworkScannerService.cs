using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using NetWatch.Model.Entities;
using NetWatch.Model.Enums;
using NetWatch.Model.Interfaces;

namespace NetWatch.Desktop.Services
{
    public class NetworkScannerService : INetworkScanner
    {
        private readonly int _pingTimeout = 1000;
        private readonly SemaphoreSlim _throttler = new(50, 50);
        private readonly Dictionary<string, string> _macVendors = new()
        {
            {"001A2B", "Cisco Systems"},
            {"00E04C", "Realtek Semiconductor"},
            {"080027", "PCS Systemtechnik GmbH"},
            {"0050C2", "Dell Inc."},
            {"001C14", "Apple"},
            {"000C29", "VMware"},
            {"000D3A", "NETGEAR"},
            {"001B21", "TP-LINK"},
            {"0016E6", "Buffalo"},
            {"001DE1", "Samsung"}
        };

        public event EventHandler<NetworkDevice>? DeviceDiscovered;
        public event EventHandler<string>? ScanStatusChanged;
        public event EventHandler<double>? ScanProgressChanged;

        public async Task<List<NetworkDevice>> ScanNetworkAsync(string ipRange)
        {
            var devices = new List<NetworkDevice>();

            try
            {
                ScanStatusChanged?.Invoke(this, "Подготовка к сканированию...");
                var ipAddresses = GenerateIpAddresses(ipRange);
                var total = ipAddresses.Count;

                if (total == 0)
                {
                    ScanStatusChanged?.Invoke(this, "Нет IP-адресов для сканирования");
                    return devices;
                }

                ScanStatusChanged?.Invoke(this, $"Начинаем сканирование {total} адресов...");

                var tasks = new List<Task<NetworkDevice?>>();
                int completed = 0;

                foreach (var ip in ipAddresses)
                {
                    tasks.Add(ScanSingleIpAsync(ip));
                }

                await Task.WhenAll(tasks);

                foreach (var task in tasks)
                {
                    var device = await task;
                    if (device != null)
                    {
                        devices.Add(device);
                        DeviceDiscovered?.Invoke(this, device);
                    }

                    completed++;
                    var progress = (double)completed / total * 100;
                    ScanProgressChanged?.Invoke(this, progress);
                }

                ScanStatusChanged?.Invoke(this, $"Сканирование завершено. Найдено устройств: {devices.Count}");
            }
            catch (Exception ex)
            {
                ScanStatusChanged?.Invoke(this, $"Ошибка сканирования: {ex.Message}");
            }

            return devices;
        }

        private async Task<NetworkDevice?> ScanSingleIpAsync(IPAddress ip)
        {
            try
            {
                await _throttler.WaitAsync();

                var device = new NetworkDevice
                {
                    IpAddress = ip.ToString(),
                    Status = NetStatus.Offline,
                    FirstSeen = DateTime.Now,
                    LastSeen = DateTime.Now,
                    DeviceType = DeviceType.Unknown,
                    IsTrusted = false
                };

                // Ping проверка
                if (await PingAsync(ip))
                {
                    device.Status = NetStatus.Online;
                    ScanStatusChanged?.Invoke(this, $"Устройство {ip} онлайн");

                    await GetHostNameAsync(device);

                    await GetMacAddressAsync(device);

                    DetermineDeviceType(device);

                    return device;
                }
            }
            catch (Exception ex)
            {
                ScanStatusChanged?.Invoke(this, $"Ошибка при сканировании {ip}: {ex.Message}");
            }
            finally
            {
                _throttler.Release();
            }

            return null;
        }

        private async Task<bool> PingAsync(IPAddress ip)
        {
            try
            {
                using var ping = new Ping();
                var reply = await ping.SendPingAsync(ip, _pingTimeout);
                return reply.Status == IPStatus.Success;
            }
            catch
            {
                return false;
            }
        }

        private async Task GetHostNameAsync(NetworkDevice device)
        {
            try
            {
                var hostEntry = await Dns.GetHostEntryAsync(device.IpAddress);
                device.HostName = hostEntry.HostName;
            }
            catch 
            {
                throw new Exception($"HostNameNotFaundException! The host owns {device.ToString} is not found ");
            }
        }

        private async Task GetMacAddressAsync(NetworkDevice device)
        {
            try
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    device.MACAddress = await GetMacAddressWindowsAsync(device.IpAddress);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ||
                         RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    device.MACAddress = await GetMacAddressUnixAsync(device.IpAddress);
                }
            }
            catch { }

            if (!string.IsNullOrEmpty(device.MACAddress))
            {
                var oui = device.MACAddress.Replace(":", "").Replace("-", "").Substring(0, 6).ToUpper();
                device.Vendor = _macVendors.TryGetValue(oui, out var vendor) ? vendor : "Неизвестно";
            }
        }

        private async Task<string?> GetMacAddressWindowsAsync(string ipAddress)
        {
            try
            {
                var process = new System.Diagnostics.Process
                {
                    StartInfo = new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = "arp",
                        Arguments = $"-a {ipAddress}",
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }
                };

                process.Start();
                var output = await process.StandardOutput.ReadToEndAsync();
                process.WaitForExit();

                var match = Regex.Match(output, @"([0-9A-Fa-f]{2}[:-]){5}([0-9A-Fa-f]{2})");
                return match.Success ? match.Value : null;
            }
            catch
            {
                return null;
            }
        }

        private async Task<string?> GetMacAddressUnixAsync(string ipAddress)
        {
            try
            {
                var process = new System.Diagnostics.Process
                {
                    StartInfo = new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = "arp",
                        Arguments = $"-n {ipAddress}",
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }
                };

                process.Start();
                var output = await process.StandardOutput.ReadToEndAsync();
                process.WaitForExit();

                var match = Regex.Match(output, @"([0-9A-Fa-f]{2}[:-]){5}([0-9A-Fa-f]{2})");
                return match.Success ? match.Value : null;
            }
            catch
            {
                return null;
            }
        }

        private void DetermineDeviceType(NetworkDevice device)
        {
            if (string.IsNullOrEmpty(device.HostName) && string.IsNullOrEmpty(device.Vendor))
            {
                device.DeviceType = DeviceType.Unknown;
                return;
            }

            var hostName = device.HostName?.ToLower() ?? "";
            var vendor = device.Vendor?.ToLower() ?? "";

            if (hostName.Contains("router") || hostName.Contains("gateway") || hostName.Contains("mikrotik"))
                device.DeviceType = DeviceType.Router;
            else if (hostName.Contains("printer") || vendor.Contains("hp") || vendor.Contains("canon"))
                device.DeviceType = DeviceType.Printer;
            else if (hostName.Contains("server") || hostName.Contains("srv"))
                device.DeviceType = DeviceType.Server;
            else if (hostName.Contains("switch") || hostName.Contains("sw"))
                device.DeviceType = DeviceType.Switch;
            else if (vendor.Contains("apple") || hostName.Contains("iphone") || hostName.Contains("ipad"))
                device.DeviceType = DeviceType.Phone;
            else if (vendor.Contains("raspberry") || hostName.Contains("pi"))
                device.DeviceType = DeviceType.IoT;
            else
                device.DeviceType = DeviceType.Computer;
        }

        private List<IPAddress> GenerateIpAddresses(string ipRange)
        {
            var addresses = new List<IPAddress>();

            try
            {
                if (ipRange.Contains("/"))
                {
                    // CIDR нотация
                    var parts = ipRange.Split('/');
                    if (parts.Length == 2 && IPAddress.TryParse(parts[0], out var networkIp))
                    {
                        var prefixLength = int.Parse(parts[1]);
                        addresses = GenerateIpRangeFromCidr(networkIp, prefixLength);
                    }
                }
                else if (ipRange.Contains("-"))
                {
                    // Диапазон
                    var parts = ipRange.Split('.');
                    if (parts.Length == 4)
                    {
                        var rangeParts = parts[3].Split('-');
                        if (rangeParts.Length == 2)
                        {
                            var baseIp = $"{parts[0]}.{parts[1]}.{parts[2]}.";
                            var start = int.Parse(rangeParts[0]);
                            var end = int.Parse(rangeParts[1]);

                            for (int i = start; i <= end; i++)
                            {
                                if (IPAddress.TryParse(baseIp + i, out var ip))
                                    addresses.Add(ip);
                            }
                        }
                    }
                }
                else
                {
                    // Один IP
                    if (IPAddress.TryParse(ipRange, out var ip))
                        addresses.Add(ip);
                }
            }
            catch
            {
                // Дефолтный диапазон
                addresses = GenerateDefaultIpRange();
            }

            return addresses;
        }

        private List<IPAddress> GenerateIpRangeFromCidr(IPAddress networkIp, int prefixLength)
        {
            var addresses = new List<IPAddress>();

            if (networkIp.AddressFamily != AddressFamily.InterNetwork)
                return addresses;

            var ipBytes = networkIp.GetAddressBytes();
            var maskBytes = GetMaskBytes(prefixLength);

            // Вычисляем начальный и конечный адреса
            var startBytes = new byte[4];
            var endBytes = new byte[4];

            for (int i = 0; i < 4; i++)
            {
                startBytes[i] = (byte)(ipBytes[i] & maskBytes[i]);
                endBytes[i] = (byte)(startBytes[i] | ~maskBytes[i]);
            }

            // Генерируем адреса в диапазоне
            for (int i = 1; i < (BitConverter.ToInt32(endBytes, 0) - BitConverter.ToInt32(startBytes, 0)); i++)
            {
                var ipBytesTemp = BitConverter.GetBytes(BitConverter.ToInt32(startBytes, 0) + i);
                if (ipBytesTemp[3] != 0 && ipBytesTemp[3] != 255) // Пропускаем сетевой и широковещательный
                {
                    addresses.Add(new IPAddress(ipBytesTemp));
                }
            }

            return addresses;
        }

        private byte[] GetMaskBytes(int prefixLength)
        {
            var maskBytes = new byte[4];
            for (int i = 0; i < 4; i++)
            {
                if (prefixLength >= 8)
                {
                    maskBytes[i] = 255;
                    prefixLength -= 8;
                }
                else
                {
                    maskBytes[i] = (byte)(255 << (8 - prefixLength));
                    prefixLength = 0;
                }
            }
            return maskBytes;
        }

        private List<IPAddress> GenerateDefaultIpRange()
        {
            var addresses = new List<IPAddress>();
            var baseIp = "192.168.1.";

            for (int i = 1; i <= 254; i++)
            {
                if (IPAddress.TryParse(baseIp + i, out var ip))
                    addresses.Add(ip);
            }

            return addresses;
        }

        public Task<NetworkDevice?> ScanSingleIpAsync(string ipAddress)
        {
            if (IPAddress.TryParse(ipAddress, out var ip))
                return ScanSingleIpAsync(ip);

            return Task.FromResult<NetworkDevice?>(null);
        }
    }
}