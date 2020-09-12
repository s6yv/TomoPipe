using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace RocsoleSimulator
{
    class Program
    {
        //static string json = @"{'CurrentMeasurementNo':1,'TimeStamp':'21:00:00','normalized':{'size':3,'data':[-0.014461,-0.065124,-0.011636 ]},'ROCSOLE_raw':{'size':5,'data':[-1.014461,-1.065124,-1.011636,1.079892,1.13304]}";
        static string json = "{\"CurrentMeasurementNo\":1,\"TimeStamp\":\"21:00:00\",\"normalized\":{\"size\":3,\"data\":[-0.014461,-0.065124,-0.011636 ]},\"ROCSOLE_raw\":{\"size\":5,\"data\":[-1.014461,-1.065124,-1.011636,1.079892,1.13304]}}";
        static private TcpListener _server = null;
        static private IPEndPoint _endPoint = null;
        static private Thread _lisiner = null;

        static private int _port = 7;
        static private int _backlog = 10;

        static private bool _areClients = false;
        static private int _activeClients = 0;

        static void Main(string[] args)
        {
            Console.WriteLine("Thread server started");

            Console.Write("Default lisiner port is 7, would you like to change it? (yes/no): ");
            string choose = Console.ReadLine();

            if (choose.Equals("yes"))
            {
                Console.Write("Entry port: ");
                GetNewPort();
            }

            Console.WriteLine($"Server is working on port {_port}");

            GetEndPoint();
            CreateTcpListener();
            RunLisinerThread();

            if (_lisiner != null)
            {
                _lisiner.Join();
            }

            CloseApplication();
        }

        static private void LisinerThread()
        {
            try
            {
                while (true)
                {
                    if (_areClients)
                    {
                        GetNewClient();
                    }

                    IsClientsCheck();
                }
            }
            catch (ObjectDisposedException)
            {
                Console.WriteLine("The Socket has been closed.");
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("The listener has not been started with a call to Start() or thread was created using a ThreadStart delegate instead of a ParameterizedThreadStart delegate.");
            }
            catch (SocketException)
            {
                Console.WriteLine("An error occurred when attempting to access the socket.");
            }
            catch (ThreadStateException)
            {
                Console.WriteLine("The thread has already been started.");
            }
            catch (OutOfMemoryException)
            {
                Console.WriteLine("There is not enough memory available to start this thread.");
            }
        }

        static private void RunLisinerThread()
        {
            try
            {
                _lisiner = new Thread(LisinerThread);
                _lisiner.IsBackground = true;
                _lisiner.Start();
            }
            catch (ThreadStateException)
            {
                Console.WriteLine("The thread has already been started.");
                StopTcpLisiner();
                Environment.Exit(1);
            }
            catch (OutOfMemoryException)
            {
                Console.WriteLine("There is not enough memory available to start this thread.");
                StopTcpLisiner();
                Environment.Exit(1);
            }
        }

        static private void IsClientsCheck()
        {
            try
            {
                _areClients = _server.Pending();
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("The listener has not been started with a call to Start().");
            }
        }

        static private void StopTcpLisiner()
        {
            try
            {
                _server.Stop();
            }
            catch (SocketException)
            {
                Console.WriteLine("Failed to stop TcpLisiner.");
                Environment.Exit(1);
            }
        }

        static private void CloseApplication()
        {
            StopTcpLisiner();
            Environment.Exit(0);
        }

        static private void GetNewClient()
        {
            Socket newClient = _server.AcceptSocket();

            if (_activeClients == 3)
            {
                ClearSocketInput(newClient);
                newClient.Send(Encoding.ASCII.GetBytes("Server has the maximum number of users. Try again later."));
                newClient.Close();
            }
            else
            {
                _activeClients++;
                RunNewClientThread(newClient);
                Console.WriteLine($"Active clients: {_activeClients}\nAccepted connection request from: {((IPEndPoint)newClient.RemoteEndPoint).Address}, on port: {((IPEndPoint)newClient.RemoteEndPoint).Port}");
            }
        }

        static private void RunNewClientThread(Socket client)
        {
            Thread thread = new Thread(ClientCommunicationHandler);
            thread.IsBackground = true;
            thread.Start(client);
        }

        static private void ClearSocketInput(Socket client)
        {
            byte[] bytesReceived = new byte[256];
            client.Receive(bytesReceived);
        }

        static private void GetEndPoint()
        {
            try
            {
                _endPoint = new IPEndPoint(IPAddress.Any, _port);
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("Address is equal to null.");
                Environment.Exit(1);
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("Port is less than MinPort or greater than MaxPort. Address is less than 0 or greater than 0x00000000FFFFFFFF.");
                Environment.Exit(1);
            }
        }

        static private void CreateTcpListener()
        {
            try
            {
                _server = new TcpListener(_endPoint);
                _server.Start(_backlog);
            }
            catch (SocketException)
            {
                Console.WriteLine("An error occurred when attempting to access the socket.");
                Environment.Exit(1);
            }
        }

        static private void ClientCommunicationHandler(object client)
        {
            string address = "", port = "";

            try
            {
                Socket newClient = (Socket)client;
                address = ((IPEndPoint)newClient.RemoteEndPoint).Address.ToString();
                port = ((IPEndPoint)newClient.RemoteEndPoint).Port.ToString();

                while (true)
                {
                    if (!(newClient.Poll(1000, SelectMode.SelectRead) && (newClient.Available == 0)))
                    {
                        ClientCommunication(newClient, address, port);
                    }
                    else
                    {
                        _activeClients--;
                        Console.WriteLine($"Close connection with client: {address}, on port: {port}\nActive clients: {_activeClients}");
                        newClient.Close();
                        break;
                    }
                }
            }
            catch (SocketException)
            {
                _activeClients--;
                Console.WriteLine($"Client: {address}, on port: {port}. An error occurred when attempting to access the socket.\nActive clients: {_activeClients}");
            }
            catch (ObjectDisposedException)
            {
                _activeClients--;
                Console.WriteLine($"Client: {address}, on port: {port}. The Socket has been closed. Message can not be read.\nActive clients: {_activeClients}");
            }
        }

        static private void ClientCommunication(Socket client, String address, String port)
        {
            NetworkStream ns = new NetworkStream(client);
            StreamWriter sw = new StreamWriter(ns);
            while (true)
            {
                sw.WriteLine(json);
                sw.Flush();
                //int len = client.Send(Encoding.ASCII.GetBytes(json));
                Console.WriteLine($"Sent to client {address}, {port} ({json.Length}b):");
                Thread.Sleep(500);
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
