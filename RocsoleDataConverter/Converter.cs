using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

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
        private int _ElectrodesCount;
        private bool _ConsiderNormalizedData = true;
        private RocsoleFrame lastRocsoleFrame;
        internal int _currentRocsoleFrameIndex;
        internal string currentRocsoleFrameTimeStamp;
        private double lastAverage;
        //private int lastRocsoleFrameIndex;
        private double _factorA;
        private double _factorB;

        static private Socket _UDPSocket = null;
        static private EndPoint _UDPEndPoint = null;
        static private bool _UDPSocketInitialized = false;
        static private Socket _TCPClient = null;
        static private bool _TCPClientConnected = false;
        static private Thread _receiver = null;

        public string TomoKISStudioIP { get => _TomoKISStudioIP; set => _TomoKISStudioIP = value; }
        public int TomoKISStudioPort { get => _TomoKISStudioPort; set => _TomoKISStudioPort = value; }
        public bool ConsiderNormalizedData { get => _ConsiderNormalizedData; set => _ConsiderNormalizedData = value; }
        public int CurrentRocsoleFrameIndex { get => _currentRocsoleFrameIndex; /*set => _currentRocsoleFrameIndex = value;*/ }
        public string CurrentRocsoleFrameTimeStamp { get => currentRocsoleFrameTimeStamp; /*set => currentRocsoleFrameTimeStamp = value;*/ }
        public double FactorA { get => _factorA; set => _factorA = value; }
        public double FactorB { get => _factorB; set => _factorB = value; }
        public int ElectrodesCount { get => _ElectrodesCount; set => _ElectrodesCount = value; }

        public Converter() {
            lastRocsoleFrame = new RocsoleFrame();
            _ElectrodesCount = 16;
            _currentRocsoleFrameIndex = -1;
            currentRocsoleFrameTimeStamp = "";
            lastAverage = -1;
            //lastRocsoleFrameIndex = -1;
            _factorA = 20.4;
            _factorB = 16;
            AllocConsole();
            Console.WriteLine("Converter initialized:\n Using equation: y = "+ _factorA.ToString("0.##") + "x+" + _factorB.ToString("0.##"));
            InitializeUDPSocket("127.0.0.1", 7);
        }

        public bool Connect2TomoKISStudio(string address, int port)
        {
            if (CloseTomoKISStudioConnection() == false)
                return false;
            // https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/exceptions/exception-handling
            // https://docs.microsoft.com/en-us/dotnet/api/system.net.sockets.socket.connect?view=netframework-4.8#System_Net_Sockets_Socket_Connect_System_Net_IPAddress_System_Int32_
            try
            {
                _TCPClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            }
            catch (SocketException)
            {
                Console.WriteLine("Problem creating TCPSocket");
                return false;
            }

            // https://docs.microsoft.com/en-us/dotnet/api/system.net.sockets.socket.connect?view=netframework-4.8#System_Net_Sockets_Socket_Connect_System_Net_IPAddress_System_Int32_
            try
            {
                TomoKISStudioIP = address;
                TomoKISStudioPort = port;
                _TCPClient.Connect(TomoKISStudioIP, TomoKISStudioPort);
                _TCPClientConnected = true;
                Console.WriteLine("Connected to TomoKISStudio Rocsole module on "+address+":"+port);
                _receiver = new Thread(ReceiverThread);
                _receiver.IsBackground = true;
                _receiver.Start();
                return true;
            }
            catch (SocketException)
            {
                Console.WriteLine("Problem connecting to TomoKISStudio Rocsole module");
            }
            catch (ThreadStateException)
            {
                Console.WriteLine("The thread has already been started.");
            }
            catch (OutOfMemoryException)
            {
                Console.WriteLine("There is not enough memory available to start this thread.");
            }
            StopTcpReceiver();
            return false;
        }

        public bool CloseTomoKISStudioConnection()
        {
            try
            {
                if (_TCPClientConnected)
                {
                    _TCPClient.Close();
                    _TCPClientConnected = false;
                }
                return true;
            }
            catch (SocketException)
            {
                Console.WriteLine("Problem with disconnection the TomoKISStudio Rocsole module");
            }
            return true;
        }

        private void ReceiverThread()
        {
            if (!_TCPClientConnected)
                return;
            Console.WriteLine("Receiver thread started ....");
            byte[] _bytesReceived = new byte[100000];
            while (_TCPClientConnected)
            {
                try
                {
                    NetworkStream ns = new NetworkStream(_TCPClient);
                    StreamReader sr = new StreamReader(ns);
                    //int bt = _TCPClient.Receive(_bytesReceived);
                    //string newmsg = Encoding.ASCII.GetString(_bytesReceived, 0, bt);
                    string newmsg = sr.ReadLine();
                    //Console.WriteLine(newmsg);
                    lastRocsoleFrame.FilterFrame(newmsg, _ConsiderNormalizedData, _ElectrodesCount);
                   

                    //processing


                    Console.WriteLine("Average = " + lastRocsoleFrame.lastFilteredAverage);
                }
                catch (SocketException)
                {
                    Console.WriteLine("Problem receiving data from TomoKISStudio Rocsole module");
                    StopTcpReceiver();
                }
                catch (IOException)
                {
                    Console.WriteLine("Disconnected from TomoKISStudio Rocsole module");
                    StopTcpReceiver();
                }
            }
        }
        static private void StopTcpReceiver()
        {
            try
            {
                _TCPClientConnected = false;
                _TCPClient.Close();
            }
            catch (SocketException)
            {
                Console.WriteLine("Failed to stop TcpReceiver.");
            }
        }

        

        //The opposite electrode data from lastFilteredFrame are averaged to get a single current value x
        //then returns y = _factorA * x + _factorB
        public double ProcessNextFrame()
        {
            if (!_TCPClientConnected)
                Connect2TomoKISStudio(_TomoKISStudioIP, _TomoKISStudioPort);
            if (!_TCPClientConnected)
                return -1;

            if (_currentRocsoleFrameIndex != lastRocsoleFrame.CurrentMeasurementNo)
            {
                lastAverage = lastRocsoleFrame.lastFilteredAverage;
                _currentRocsoleFrameIndex = lastRocsoleFrame.CurrentMeasurementNo;
            }

            double y = _factorA * lastAverage + _factorB;
            Console.WriteLine("Y = " + y.ToString("0.##") + " for frame index = " + _currentRocsoleFrameIndex);


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
