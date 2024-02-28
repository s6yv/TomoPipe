using RocsoleDataConverter;
using System;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using Newtonsoft.Json;

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
            Console.WriteLine("Started? Please SAY YES");

            var broadcast = IPAddress.Parse(deviceIp);
            IPEndPoint endpoint = new(broadcast, port);
            Socket socket = new(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, new MulticastOption(broadcast));
            socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.MulticastTimeToLive, 2);

            server = new Thread(() =>
            {
                while (true)
                {
                    dataReadyToSend.WaitOne();
                    GasCore dataToSend;
                    lock (latestGasCore) dataToSend = new GasCore(latestGasCore);
                    lock (latestGasCore)
                    {
                        Console.WriteLine($"GasCore values: diameter={latestGasCore.diameter}, offsetAngle={latestGasCore.offsetAngle}, offsetDistance={latestGasCore.offsetDistance}");
                        var serialized = Newtonsoft.Json.JsonConvert.SerializeObject(latestGasCore);
                        Console.WriteLine($"Serialized JSON: {(string.IsNullOrEmpty(serialized) ? "Empty" : serialized)}");
                        byte[] sendbuf = Encoding.ASCII.GetBytes(serialized);
                        Console.WriteLine($"Byte array length: {sendbuf.Length}");
                        Console.WriteLine($"Byte array content (ASCII representation): {BitConverter.ToString(sendbuf)}");
                        Console.WriteLine($"Endpoint: {endpoint}");
                        socket.SendTo(sendbuf, endpoint);


                    }
                   

                }
            });

            server.Start();

        }
    }
}
