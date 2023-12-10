using RocsoleDataConverter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestDLL
{
    class ArAppConnection {
        private GasCore? _gasCore = null;
        private AutoResetEvent dataReadyToSend = new AutoResetEvent(false);

        public int port { get => 8080; }
        public string ipAddress { get => NetworkInterface.GetAllNetworkInterfaces()
                .ToArray()
                .SelectMany(adapter => adapter.GetIPProperties().UnicastAddresses)
                .Where(adr => adr.Address.AddressFamily == AddressFamily.InterNetwork && adr.IsDnsEligible)
                .Select(adr => adr.Address.ToString())
                .First();
        }
    }
}
