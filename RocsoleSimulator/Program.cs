﻿using System;
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
        //static string json = "{\"CurrentMeasurementNo\":1,\"TimeStamp\":\"21:00:00\",\"normalized\":{\"size\":3,\"data\":[-0.014461,-0.065124,-0.011636 ]},\"ROCSOLE_raw\":{\"size\":5,\"data\":[-1.014461,-1.065124,-1.011636,1.079892,1.13304]}}";
        static string json = "{\"CurrentMeasurementNo\":\"1\",\"TimeStamp\":\"0\",\"normalized\":{\"size\":\"120\",\"data\":[\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\"]},\"ROCSOLE_raw\":{\"size\":\"256\",\"data\":[\"2294.63379\",\"4.97505243e-06\",\"5.21903576e-06\",\"7.90097511e-06\",\"3.78336244e-06\",\"3.05786784e-06\",\"2.14532315e-06\",\"8.40917255e-06\",\"3.43474824e-07\",\"1.02298941e-06\",\"1.10327874e-05\",\"6.58919998e-06\",\"8.77204729e-06\",\"3.82586222e-06\",\"4.5847969e-06\",\"6.48505875e-06\",\"1.36680944e-06\",\"2313.94824\",\"7.75711123e-06\",\"4.30243699e-06\",\"8.3304883e-07\",\"4.83024678e-06\",\"9.95937626e-06\",\"6.59488478e-06\",\"3.58598231e-06\",\"3.70346174e-06\",\"6.73204477e-06\",\"5.3341148e-07\",\"2.11830411e-06\",\"6.55435269e-06\",\"4.4427361e-06\",\"8.66530968e-07\",\"7.25355449e-06\",\"9.18577462e-06\",\"2319.50391\",\"8.51219374e-06\",\"5.80522055e-06\",\"4.54756128e-06\",\"1.47646706e-05\",\"1.1496375e-05\",\"5.73510397e-06\",\"7.22498771e-06\",\"5.17564058e-06\",\"4.40886697e-06\",\"4.12051895e-06\",\"6.01470447e-06\",\"7.87034969e-06\",\"5.82896109e-06\",\"5.74308478e-06\",\"4.20206879e-06\",\"3.9878596e-06\",\"2318.53345\",\"1.19198776e-05\",\"4.71037356e-06\",\"4.71552721e-06\",\"2.91852552e-06\",\"8.15954172e-06\",\"8.2151455e-06\",\"6.94075152e-06\",\"3.13297232e-06\",\"4.06490244e-06\",\"5.67281177e-06\",\"7.61116735e-06\",\"5.84513009e-06\",\"4.10414987e-06\",\"1.07988317e-05\",\"5.80091546e-06\",\"9.09054415e-06\",\"2319.62988\",\"2.63485845e-06\",\"7.52220467e-06\",\"6.81146685e-06\",\"5.51828043e-06\",\"6.45319869e-06\",\"4.46346257e-06\",\"6.03900071e-06\",\"1.13525939e-05\",\"1.07125024e-05\",\"1.76637775e-06\",\"4.19512662e-06\",\"2.49914649e-07\",\"4.80864765e-06\",\"1.5266447e-05\",\"5.14025533e-06\",\"6.07303446e-06\",\"2321.15918\",\"6.300515e-06\",\"3.51582116e-06\",\"1.37843779e-06\",\"4.39862379e-06\",\"3.3481931e-06\",\"6.41480028e-06\",\"1.36680944e-06\",\"5.37468213e-06\",\"1.10762912e-05\",\"1.40518814e-05\",\"3.51147605e-06\",\"5.26866302e-07\",\"7.68078189e-06\",\"3.28563578e-06\",\"6.12276699e-06\",\"2.04936782e-06\",\"2318.37378\",\"7.90834929e-06\",\"3.53530845e-06\",\"8.12895178e-06\",\"6.61778495e-06\",\"7.34633795e-06\",\"1.16461297e-05\",\"4.9996811e-06\",\"1.16388571e-06\",\"7.53879249e-06\",\"3.74668275e-06\",\"3.7853797e-06\",\"5.98138331e-06\",\"6.00084331e-06\",\"3.63346817e-06\",\"7.24752454e-06\",\"7.59903105e-06\",\"2315.43481\",\"6.81024403e-06\",\"3.57648719e-06\",\"3.7717889e-06\",\"5.80318783e-06\",\"6.00951034e-06\",\"4.20239894e-06\",\"5.01561817e-06\",\"6.33916761e-06\",\"7.34236937e-06\",\"7.1469567e-06\",\"1.50659816e-05\",\"3.61834827e-06\",\"5.13255418e-06\",\"3.34321498e-06\",\"1.04893379e-05\",\"3.00683314e-06\",\"2315.70215\",\"4.56705266e-06\",\"8.16430293e-06\",\"5.66534436e-06\",\"5.80856704e-06\",\"3.354819e-06\",\"6.58667204e-06\",\"1.02889453e-05\",\"6.42312489e-06\",\"1.15827343e-05\",\"1.40585473e-06\",\"4.71611565e-06\",\"6.15442104e-06\",\"3.20934691e-06\",\"8.51064488e-06\",\"3.7717889e-06\",\"2.6826242e-06\",\"2315.65503\",\"8.06432581e-06\",\"4.34624781e-06\",\"8.61905664e-06\",\"3.54882695e-06\",\"1.36925455e-05\",\"5.38461518e-06\",\"2.03679997e-06\",\"5.03219417e-06\",\"5.65737673e-06\",\"1.79791925e-06\",\"4.78042284e-06\",\"6.80749235e-06\",\"1.26246341e-05\",\"1.18574189e-05\",\"7.45632178e-06\",\"9.98880751e-06\",\"2316.16284\",\"4.42144119e-06\",\"3.59371506e-06\",\"7.63220032e-06\",\"8.06122716e-06\",\"4.96625671e-06\",\"3.38037194e-06\",\"6.78870902e-06\",\"6.49222466e-06\",\"8.07498964e-06\",\"4.42144119e-06\",\"2.37762106e-06\",\"5.68552014e-06\",\"7.28572513e-06\",\"9.94989568e-06\",\"5.41737973e-06\",\"3.11720578e-06\",\"2319.04736\",\"1.00861535e-05\",\"3.16996693e-06\",\"7.83145606e-06\",\"9.32836156e-06\",\"5.42902444e-06\",\"3.58036596e-06\",\"5.72056524e-06\",\"5.98277529e-06\",\"8.36382696e-06\",\"1.28029603e-06\",\"1.09124303e-05\",\"8.32690512e-06\",\"3.86322745e-06\",\"3.69746067e-06\",\"4.86046702e-06\",\"5.75575859e-06\",\"2313.51196\",\"9.0176527e-06\",\"4.84444899e-06\",\"8.1246817e-06\",\"4.51540109e-06\",\"7.41750318e-06\",\"9.06799596e-06\",\"6.10824191e-06\",\"6.90225033e-06\",\"6.86434259e-06\",\"1.20769091e-05\",\"2.37089853e-06\",\"6.10267261e-06\",\"9.67427604e-06\",\"1.02165886e-05\",\"4.61196169e-06\",\"1.35456912e-06\",\"2318.25366\",\"1.13668893e-05\",\"7.85225257e-06\",\"6.95862582e-06\",\"4.47836373e-06\",\"1.24984535e-05\",\"6.04233219e-06\",\"3.6292638e-06\",\"4.75611796e-06\",\"1.04816609e-05\",\"6.7275073e-06\",\"9.16118915e-06\",\"3.75223522e-06\",\"2.40288023e-06\",\"8.43282396e-06\",\"5.71510327e-06\",\"5.85900443e-06\",\"2318.74341\",\"4.84315979e-06\",\"2.15339492e-06\",\"1.06751231e-05\",\"9.78022035e-06\",\"9.47554145e-06\",\"4.28838121e-06\",\"2.73260321e-06\",\"4.94875849e-06\",\"6.02208456e-06\",\"1.33547883e-06\",\"9.82644542e-06\",\"3.51463683e-07\",\"3.50910386e-06\",\"2.99179374e-06\",\"4.18867012e-06\",\"1.09983894e-05\",\"2310.9375\"]}};";


        static private TcpListener _server = null;
        static private IPEndPoint _endPoint = null;
        static private Thread _lisiner = null;

        static private int _port = 7777;
        static private int _backlog = 10;

        static private bool _areClients = false;
        static private int _activeClients = 0;

        static void Main(string[] args)
        {
            Console.WriteLine("Thread server started");

            Console.Write("Default lisiner port is 7777, would you like to change it? (yes/no): ");
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
            int index = 1;
            try
            {
                while (true)
                {
                    string a = json.Substring(0, 25);
                    string b = json.Substring(26);
                    json = json.Substring(0, 25) + index + json.Substring(26);
                    index++; if (index == 10) index = 1;
                    sw.WriteLine(json);
                    sw.Flush();
                    //int len = client.Send(Encoding.ASCII.GetBytes(json));
                    Console.WriteLine($"Sent to client {address}, {port} ({json.Length}b):");
                    Thread.Sleep(500);
                }
            }
            catch (Exception)
            {
                Console.WriteLine($"Client disconnected");
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