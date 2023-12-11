using RocsoleDataConverter;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;

namespace TestDLL
{
    class ArAppConnection {
        private GasCore latestGasCore = new();
        private AutoResetEvent dataReadyToSend = new(false);
        private Thread server;

        public int port { get => 8080; }
        public string ipAddress { get => NetworkInterface.GetAllNetworkInterfaces()
                .ToArray()
                .SelectMany(adapter => adapter.GetIPProperties().UnicastAddresses)
                .Where(adr => adr.Address.AddressFamily == AddressFamily.InterNetwork && adr.IsDnsEligible)
                .Select(adr => adr.Address.ToString())
                .First();
        }

        public void updateGasCore(GasCore newData) {
            lock(latestGasCore) {
                latestGasCore = newData;
                dataReadyToSend.Set();
            }
        }

        public void startBroadcast(string deviceIp)
        {
            var broadcast = IPAddress.Parse(deviceIp);
            IPEndPoint endpoint = new(broadcast, port);
            Socket socket = new(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            server = new Thread(() =>
            {
                while (true)
                {
                    dataReadyToSend.WaitOne();
                    GasCore dataToSend;
                    lock (latestGasCore) dataToSend = new GasCore(latestGasCore);
                    var serialized = JsonSerializer.Serialize(dataToSend);
                    byte[] sendbuf = Encoding.ASCII.GetBytes(serialized);
                    socket.SendTo(sendbuf, endpoint);

                }
            });

            server.Start();

        }
    }
}
