using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;

namespace RocsoleDataConverter
{
    //How to use DLL in LabView see:
    //https://www.youtube.com/watch?v=hE0TVHqZjBI

    public class Converter
    {
        [DllImport("kernel32", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        private String _TomoKISStudioIP;
        private int _TomoKISStudioPort;
        private List<double> lastRocsoleFrame;
        private List<double> lastFilteredFrame;
        private double _factorA;
        private double _factorB;

        static private Socket _UDPSocket = null;
        static private EndPoint _UDPEndPoint = null;
        static private bool _UDPSocketInitialized = false;

        public string TomoKISStudioIP { get => _TomoKISStudioIP; set => _TomoKISStudioIP = value; }
        public int TomoKISStudioPort { get => _TomoKISStudioPort; set => _TomoKISStudioPort = value; }
        public double FactorA { get => _factorA; set => _factorA = value; }
        public double FactorB { get => _factorB; set => _factorB = value; }

        public Converter() {
            lastRocsoleFrame = new List<double>();
            lastFilteredFrame = new List<double>();
            _factorA = 20.4;
            _factorB = 16;
            AllocConsole();
            Console.WriteLine("Converter initialized:\n Used equation: y = "+ _factorA.ToString("0.##") + "x+" + _factorB.ToString("0.##"));
            InitializeUDPSocket("127.0.0.1", 7);
        }

        //connect to TomoKISStudio and get current measurement frame
        //store it to lastRocsoleFrame
        private void GetFrameFromRocsole()
        {

        }

        //filter out only the opposite electrodes measurements from lastRocsoleFrame
        //and store them to lastFilteredFrame
        private void FilterFrame()
        {

        }

        //The opposite electrode data from lastFilteredFrame are averaged to get a single current value x
        //then returns y = _factorA * x + _factorB
        public double ProcessNextFrame()
        {
            double x = 1;
            double y = _factorA * x + _factorB;
            Console.WriteLine("Y = " + y.ToString("0.##"));


            SendValue(y);

            return y;
        }

        //sends via UDP on IP:port the value
        //returns 0 if success otherwise it returns 1
        //default precision: "0.##"
        public int SendValue(double value, string precision = "0.##")
        {
            if (!_UDPSocketInitialized)
            {
                Console.WriteLine("First call InitializeUDPSocket(string address, int port) method");
                return 1;
            }
            // https://docs.microsoft.com/en-us/dotnet/api/system.net.sockets.socket.sendto?view=netframework-4.8#System_Net_Sockets_Socket_SendTo_System_Byte___System_Net_EndPoint_
            try
            {
                string message = value.ToString(precision);
                // https://stackoverflow.com/questions/2637697/sending-udp-packet-in-c-sharp
                _UDPSocket.SendTo(Encoding.ASCII.GetBytes(message), _UDPEndPoint);
                Console.WriteLine($"Send to server ({message.Length}b): " + message);
                return 0;
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("Buffer is null or remoteEP is null.");
            }
            catch (SocketException)
            {
                Console.WriteLine("An error occurred when attempting to access the socket.");
            }
            catch (ObjectDisposedException)
            {
                Console.WriteLine("The Socket has been closed.");
            }
            return 1;
        }

        public void InitializeUDPSocket(string address, int port)
        {
            // https://docs.microsoft.com/en-us/dotnet/api/system.net.sockets.socket.-ctor?view=netframework-4.8#System_Net_Sockets_Socket__ctor_System_Net_Sockets_AddressFamily_System_Net_Sockets_SocketType_System_Net_Sockets_ProtocolType
            // https://docs.microsoft.com/en-us/dotnet/api/system.net.ipendpoint.-ctor?view=netframework-4.8#System_Net_IPEndPoint__ctor_System_Net_IPAddress_System_Int32_
            try
            {
                _UDPSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                _UDPEndPoint = new IPEndPoint(IPAddress.Parse(address), port);
                _UDPSocketInitialized = true;
                Console.WriteLine("UDP socket initialized on "+address+":"+port);
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("Address is null.");
                Environment.Exit(1);
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("Port is less than minPort or is greater than maxPort address is less than 0 or greater than 0x00000000FFFFFFFF.");
                Environment.Exit(1);
            }
            catch (SocketException)
            {
                Console.WriteLine("The combination of addressFamily, socketType, and protocolType results in an invalid socket.");
                Environment.Exit(1);
            }
        }
    }
}
