﻿using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace RocsoleDataConverter
{
    /// <summary>
    /// This class is the main of the DLL.
    /// It consists of the methodes and fields used in LabView.
    /// </summary>
    /// <remarks>
    /// How to use DLL in LabView see:
    /// https://www.youtube.com/watch?v=hE0TVHqZjBI
	/// ------------
	/// To view this doc see https://github.com/EWSoftware/SHFB or https://www.helixoft.com/vsdocman/overview.html
    /// </remarks>
    public class Converter
    {
        [DllImport("kernel32", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        private String _TomoKISStudioIP = "127.0.0.1";
        private int _TomoKISStudioPort = 7777;
        private String _UDPIP = "127.0.0.1";
        private int _UDPPort = 777;
        private int _ElectrodesCount = 16;
        private bool _ConsiderNormalizedData = true;
        private RocsoleFrame lastRocsoleFrame;
        internal int _currentRocsoleFrameIndex;
        internal string currentRocsoleFrameTimeStamp;
        private double lastAverage;
        private double lastStdDev;
        private int timeInterval = 350;
        //private int lastRocsoleFrameIndex;
        private double _factorA = 20.4;
        private double _factorB = 16;
        private double _factorC = 0;
        private double _avgf = 1;
        bool initializing = true;

        static private Socket _UDPSocket = null;
        static private EndPoint _UDPEndPoint = null;
        static private bool _UDPSocketInitialized = false;
        static private Socket _TCPClient = null;
        static private bool _TCPClientConnected = false;
        static private Thread _receiver = null;

        /// <value>The IP address of TomoKISStudio module.</value>
        public string TomoKISStudioIP { get => _TomoKISStudioIP; set { _TomoKISStudioIP = value; Settings.Store(this); } }
        /// <value>The TCP port number the TomoKISStudio module is listening on.</value>
        public int TomoKISStudioPort { get => _TomoKISStudioPort; set { _TomoKISStudioPort = value; Settings.Store(this); } }
        /// <value>If true the normalized data are considered otherwise the RAW data retreived directly from Rocsole device.</value>
        public bool ConsiderNormalizedData { get => _ConsiderNormalizedData; set { _ConsiderNormalizedData = value; Settings.Store(this); } }
        /// <value>Gets the index of the current measurement frame.</value>
        public int CurrentRocsoleFrameIndex { get => _currentRocsoleFrameIndex; /*set => _currentRocsoleFrameIndex = value;*/ }
        /// <value>Gets the time stamp of the current measurement frame.</value>
        public string CurrentRocsoleFrameTimeStamp { get => currentRocsoleFrameTimeStamp; /*set => currentRocsoleFrameTimeStamp = value;*/ }
        /// <value>Sets the factor A of the transformation equation.</value>
        public double FactorA
        {
            get => _factorA;
            set
            {
                _factorA = value;
                Settings.Store(this);
                if (!initializing)
                    Console.WriteLine("Using equation: y = " + _factorC.ToString("0.##") + "*" + "*x^2+" + _factorA.ToString("0.##") + "*x+" + _factorB.ToString("0.##") + " and Avg Current " + _avgf.ToString("0.##"));
            }
        }
        /// <value>Sets the factor B of the transformation equation.</value>
        public double FactorB
        {
            get => _factorB;
            set
            {
                _factorB = value;
                Settings.Store(this);
                if (!initializing)
                    Console.WriteLine("Using equation: y = " + _factorC.ToString("0.##") + "*" + _factorC.ToString("0.##") + "*x+" + _factorA.ToString("0.##") + "*x+" + _factorB.ToString("0.##") + "and Avg Current " + _avgf.ToString("0.##"));
            }
        }
        /// <value>Sets the factor C of the transformation equation.</value>
        public double FactorC
        {
            get => _factorC;
            set
            {
                _factorC = value;
                Settings.Store(this);
                if (!initializing)
                    Console.WriteLine("Using equation: y = " + _factorC.ToString("0.##") + "*x^2+" + _factorA.ToString("0.##") + "*x+" + _factorB.ToString("0.##") + "*Avg Current " + _avgf.ToString("0.##") + "and Avg Current " + _avgf.ToString("0.##"));
            }
        }
        public double Avgf
        {
            get => _avgf;
            set
            {
                _avgf = value;
                Settings.Store(this);
                if (!initializing)
                    Console.WriteLine("Using equation: y = " + _factorC.ToString("0.##") + "*x^2+" + _factorA.ToString("0.##") + "*x+" + _factorB.ToString("0.##") + "and Avg Current " + _avgf.ToString("0.##"));
            }
        }
        /// <value>Sets the electrodes count of the sensor.</value>
        public int ElectrodesCount { get => _ElectrodesCount; set { _ElectrodesCount = value; Settings.Store(this); } }
        /// <value>The IP address of LabView module.</value>
        public string UDPIP { get => _UDPIP; set { _UDPIP = value; _UDPSocketInitialized = false; Settings.Store(this); } }
        /// <value>The UDP port number the LabViewmodule is listening on.</value>
        public int UDPPort { get => _UDPPort; set { _UDPPort = value; _UDPSocketInitialized = false; Settings.Store(this); } }
        public int TimeInterval { get => timeInterval; set { timeInterval = value; Settings.Store(this); } }

        public Converter() {
            AllocConsole();

            lastRocsoleFrame = new RocsoleFrame();

            Settings.Read(this);

            _currentRocsoleFrameIndex = -1;
            currentRocsoleFrameTimeStamp = "";
            lastAverage = -1;
            lastStdDev = 0;
            //lastRocsoleFrameIndex = -1;
            //Console.WriteLine("Converter initialized:\n Using equation: y = "+ _factorC.ToString("0.##")+"*"+ _factorC.ToString("0.##")+"*x+"+_factorA.ToString("0.##") + "*x+" + _factorB.ToString("0.##"));
            Console.WriteLine("Converter initialized.");
            //InitializeUDPSocket(_UDPIP, _UDPPort);
            initializing = false;
        }

        /// <summary>
        /// Connects to the TomoKISStudio module on IP <paramref name="address"/> and port <paramref name="port"/>.
        /// </summary>
        /// <returns>
        /// <b>True</b> if success.
        /// See the console windows to check reasons
        /// </returns>
        /// <example>
        /// <code>
        /// Connect2TomoKISStudio("127.0.0.1", 7);
        /// </code>
        /// </example>
        /// <param name="address">TomoKISStudio IP address.</param>
        /// <param name="port">TCP port number the TomoKISStudio module is listening on.</param>
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

        /// <summary>
        /// Disconnects from the TomoKISStudio module and closes the internal receiving thred/>.
        /// </summary>
        /// <returns>
        /// <b>True</b> if success.
        /// See the console windows to check reasons
        /// </returns>
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
            string msg4processing = "";
            string msg4future = "";
            string newmsg;
            int indexOfChar = 0;
            try
            {
                //NetworkStream ns = new NetworkStream(_TCPClient);
                //StreamReader sr = new StreamReader(ns);
                while (_TCPClientConnected)
                {
                    if (msg4future.IndexOf(";", 0, msg4future.Length) == -1)
                    {
                        msg4processing = msg4future;
                        int bt = _TCPClient.Receive(_bytesReceived);
                        newmsg = Encoding.ASCII.GetString(_bytesReceived, 0, bt);
                    }
                    else
                        newmsg = msg4future;
                                                                                            //Console.WriteLine("newmsg:" + newmsg);


                    indexOfChar = newmsg.IndexOf(";", 0, newmsg.Length);
                    if (indexOfChar == -1)
                    {
                        msg4future += newmsg;
                        continue;
                    }
                    msg4processing += newmsg.Substring(0, indexOfChar);
                                                                                            //Console.WriteLine("msg4processing:" + msg4processing);
                    //if (indexOfChar < newmsg.Length - 1)
                        msg4future = newmsg.Substring(indexOfChar + 1);
                    //else
                    //    msg4future = "";
                    //Console.WriteLine("msg4future:" + msg4future);




                    //                    string newmsg = sr.Read()
                    lastRocsoleFrame.FilterFrame(msg4processing, _ConsiderNormalizedData, _ElectrodesCount);
                    if (_ConsiderNormalizedData)
                        Console.WriteLine("Received CurrentMeasurementNo = #" + lastRocsoleFrame.CurrentMeasurementNo + " TimeStamp: " + lastRocsoleFrame.TimeStamp + "; Average normalized= " + lastRocsoleFrame.lastFilteredAverage + "; StdDev normalized= " + lastRocsoleFrame.lastFilteredStdDev);
                    else
                        Console.WriteLine("Received CurrentMeasurementNo = #" + lastRocsoleFrame.CurrentMeasurementNo + " TimeStamp: " + lastRocsoleFrame.TimeStamp + "; Average RAW = " + lastRocsoleFrame.lastFilteredAverage + "; StdDev normalized= " + lastRocsoleFrame.lastFilteredStdDev);

                    msg4processing = "";

                    //processing


                }
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
        private void StopTcpReceiver()
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


        /// <summary>
        /// Starts processing:
        /// <list type="bullet">
        /// <item>
        /// <term>Connection</term>
        /// <description>Connects the TomoKISStudio module if not connected yet. </description>
        /// </item>
        /// <item>
        /// <term>New measurement frame</term>
        /// <description>Retreives a new measurement frame. </description>
        /// </item>
        /// <item>
        /// <term>Filtering</term>
        /// <description>Filters the opposite electrodes. </description>
        /// </item>
        /// <item>
        /// <term>Averaging</term>
        /// <description>Calculate average of measurements values of the opposite electrodes to get a single current value (X). </description>
        /// </item>
        /// <item>
        /// <term>Calculation</term>
        /// <description>Makes calculation Y = factorA * X + factorB. </description>
        /// </item>
        /// <item>
        /// <term>UDP sending</term>
        /// <description>Sends the (Y) value on UDP to LabView module. </description>
        /// </item>
        /// </list>
        /// </summary>
        /// <returns>
        /// <b>Y value</b>. 
        /// If error returns -1.
        /// <list type="bullet">
        /// <item>
        /// <term>Connection</term>
        /// <description>Not connected to TomoKISStudio or connection fails. </description>
        /// </item>
        /// <item>
        /// <term>Measurement data format</term>
        /// <description>Wrong measurement data format. </description>
        /// </item>
        /// </list>
        /// See the console windows to check reasons
        /// </returns>
        public bool ProcessNextFrame(out double avg, out double std, out double Y)
        {
            avg = 0;
            std = 0;
            Y = -1;

            if (!_TCPClientConnected)
            {
                Connect2TomoKISStudio(_TomoKISStudioIP, _TomoKISStudioPort);
                Thread.Sleep(1000);
            }
            if (!_TCPClientConnected)
                return false;

            if (_currentRocsoleFrameIndex != lastRocsoleFrame.CurrentMeasurementNo)
            {
                lastAverage = lastRocsoleFrame.lastFilteredAverage;
                lastStdDev = lastRocsoleFrame.lastFilteredStdDev;
                _currentRocsoleFrameIndex = lastRocsoleFrame.CurrentMeasurementNo;
            }

            double x = lastAverage;
           // lastAverage = Math.Round(lastAverage, 5);
           // lastAverage = Math.Round(lastAverage, 5);
            avg = lastAverage;
            std = lastStdDev;
            //std.Normalize();

            lastAverage = (lastAverage /0.00255);

            Y = -78.587 * (lastAverage * lastAverage) + 135.3 * lastAverage + 4.33;

           // Y = -173933811479.26 * (x * x * x) + 313138821.74 * (x * x) - 233463.16 * x + 109.58;
           // Y = -171299570766.255 * (x * x * x) + 308678865.185535 * (x * x) - -240571.136698138 * x + 114.227030065516;
           // Y= Math.Round(Y, 2);
           // std = Math.Round(std, 6);
           // if (Y < 0 )

           // {
            //    Y = 0;
          //  }

            //if (Y > 81)

           // {
             //   Y = 81.4;
          //  }
            Console.WriteLine("Y = " + Y.ToString("0.##########") + " and STD=" + std.ToString("0.##########") + " for frame index = " + _currentRocsoleFrameIndex);

            SendValue(Y, std, "0.##########");

            return true;
        }

        /// <summary>
        /// Sends calucated double-type <paramref name="value"/> and StdDev to the LabView module with the <paramref name="precision"/>
        /// Default precision is 0.##
        /// </summary>
        /// <returns>
        /// <b>True</b> if success otherwise <b>false</b>.
        /// See the console windows to check reasons
        /// </returns>
        /// <example>
        /// <code>
        /// SendValue(10.12345, 1.234, "0.###") will send 10.123 1.234
        /// SendValue(10.12345, 1.234) will send 10.12 1.234
        /// </code>
        /// </example>
        public bool SendValue(double value1, double value2, string precision = "0.##")
        {
            if (!_UDPSocketInitialized)
            {
                //Console.WriteLine("First set the UDPIP and UDPPort properties to initialize.");
                //return false;
                InitializeUDPSocket(_UDPIP, _UDPPort);
            }
            // https://docs.microsoft.com/en-us/dotnet/api/system.net.sockets.socket.sendto?view=netframework-4.8#System_Net_Sockets_Socket_SendTo_System_Byte___System_Net_EndPoint_
            try
            {
                string message = value1.ToString(precision) + " " + value2.ToString(precision);
                // https://stackoverflow.com/questions/2637697/sending-udp-packet-in-c-sharp
                _UDPSocket.SendTo(Encoding.ASCII.GetBytes(message), _UDPEndPoint);
                Console.WriteLine($"Send to server ({message.Length}b): " + message);
                return true;
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
            return false;
        }

        private void InitializeUDPSocket(string address, int port)
        {
            // https://docs.microsoft.com/en-us/dotnet/api/system.net.sockets.socket.-ctor?view=netframework-4.8#System_Net_Sockets_Socket__ctor_System_Net_Sockets_AddressFamily_System_Net_Sockets_SocketType_System_Net_Sockets_ProtocolType
            // https://docs.microsoft.com/en-us/dotnet/api/system.net.ipendpoint.-ctor?view=netframework-4.8#System_Net_IPEndPoint__ctor_System_Net_IPAddress_System_Int32_
            try
            {
                if (_UDPSocketInitialized)
                    _UDPSocket.Close();
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
