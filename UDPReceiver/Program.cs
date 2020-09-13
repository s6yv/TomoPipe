using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UDPReceiver
{
    class Program
    {
        private static int _port = 777;
        private static UdpClient _server = null;
        private static IPEndPoint _groupEP = null;
        private static bool _runServer = true;

        static void Main(string[] args)
        {
            Console.WriteLine("UDP Server started");

            Console.Write("Default lisiner port is 777, would you like to change it? (yes/no): ");
            string choose = Console.ReadLine();

            if (choose.Equals("yes"))
            {
                Console.Write("Entry port: ");
                GetNewPort();
            }

            Console.WriteLine($"Server is working on port {_port}");

            InitializeServer();

            while (_runServer)
            {
                SetIPEndPoint(IPAddress.Any);
                Communication();
            }

            Console.WriteLine("Closing server.");
            CloseApplication();
        }

        static private void CloseApplication()
        {
            _server.Close();
            Environment.Exit(0);
        }

        static private void Communication()
        {
            // https://docs.microsoft.com/en-us/dotnet/api/system.net.sockets.udpclient.receive?view=netframework-4.8
            // https://docs.microsoft.com/en-us/dotnet/api/system.net.sockets.udpclient.send?view=netframework-4.8#System_Net_Sockets_UdpClient_Send_System_Byte___System_Int32_System_Net_IPEndPoint_
            try
            {
                byte[] bytesReceived = _server.Receive(ref _groupEP);
                string message = Encoding.ASCII.GetString(bytesReceived);

                Console.WriteLine($"Received from client ({bytesReceived.Length}b): {message}");

                byte[] bytesToSend = Encoding.ASCII.GetBytes(message);
                _server.Send(bytesToSend, bytesToSend.Length, _groupEP);
                Console.WriteLine($"Sent to client ({bytesToSend.Length}b): {message}");
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("Dgram is null.");
            }
            catch (ObjectDisposedException)
            {
                Console.WriteLine("The underlying Socket or UdpClient has been closed.");
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("UdpClient has already established a default remote host.");
            }
            catch (SocketException)
            {
                Console.WriteLine("An error occurred when accessing the socket.");
            }
        }

        static private void SetIPEndPoint(IPAddress address)
        {
            // https://docs.microsoft.com/en-us/dotnet/api/system.net.ipendpoint?view=netframework-4.8
            try
            {
                _groupEP = new IPEndPoint(address, _port);
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("Address is null.");
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("port is less than MinPort or port is greater than MaxPort or address is less than 0 or greater than 0x00000000FFFFFFFF.");
            }
        }

        static private void InitializeServer()
        {
            // https://docs.microsoft.com/en-us/dotnet/api/system.net.sockets.udpclient.send?view=netframework-4.8
            try
            {
                _server = new UdpClient(_port);
                SetIPEndPoint(IPAddress.Any);
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("The port parameter is greater than MaxPort or less than MinPort.");
                Environment.Exit(1);
            }
            catch (SocketException)
            {
                Console.WriteLine("An error occurred when accessing the socket.");
                Environment.Exit(1);
            }
        }

        static private void GetNewPort()
        {
            if (!int.TryParse(Console.ReadLine(), out _port))
            {
                Console.WriteLine("Wrong port - there are characters or it is not an integer.");
                Environment.Exit(1);
            }
        }
    }
}
