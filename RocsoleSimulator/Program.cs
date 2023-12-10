using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;


namespace RocsoleSimulator
{
    class Program
    {
        static string json;// = "{\"CurrentMeasurementNo\":\"1\",\"TimeStamp\":\"0\",\"normalized\":{\"size\":\"120\",\"data\":[\"-0.0868714\",\"-0.160788\",\"-0.0786658\",\"0.093516\",\"0.213512\",\"0.264423\",\"0.264658\",\"0.275459\",\"0.238077\",\"0.256489\",\"0.165883\",\"0.119135\",\"-0.00789353\",\"-0.0954726\",\"-0.0532992\",\"-0.0511884\",\"-0.0751547\",\"-0.0341612\",\"0.00906915\",\"0.0451083\",\"0.0807309\",\"0.0939355\",\"0.105633\",\"0.123101\",\"0.104406\",\"0.0921619\",\"0.0562338\",\"0.0197726\",\"-0.0532301\",\"-0.0344766\",\"-0.0471866\",\"-0.0361116\",\"-0.0222727\",\"-0.00180389\",\"0.0209648\",\"0.0142165\",\"0.0478997\",\"0.0349065\",\"0.0536053\",\"0.0470982\",\"0.0300387\",\"-0.00430897\",\"-0.0195148\",\"-0.0315619\",\"-0.0278691\",\"-0.0168412\",\"-0.0124079\",\"-0.00987894\",\"0.00756509\",\"-0.00126089\",\"0.0311228\",\"0.0186039\",\"0.0206071\",\"-0.0050364\",\"-0.0182946\",\"-0.0286778\",\"-0.0285551\",\"-0.00714163\",\"-0.0236211\",\"0.0087763\",\"-0.00313654\",\"0.0344917\",\"0.0233998\",\"0.0369436\",\"0.0137409\",\"-0.0340859\",\"-0.0293405\",\"-0.0440744\",\"-0.0190355\",\"-0.0149572\",\"0.00904537\",\"0.0246038\",\"0.0245371\",\"0.0424495\",\"0.0325219\",\"-0.0304931\",\"-0.0487187\",\"-0.0421605\",\"-0.0126509\",\"0.0149491\",\"0.0403848\",\"0.0867888\",\"0.0990068\",\"0.106266\",\"-0.0406898\",\"-0.0620904\",\"-0.0634533\",\"-0.0143604\",\"0.0361466\",\"0.083682\",\"0.104361\",\"0.159636\",\"-0.0392327\",\"-0.0731164\",\"-0.0499135\",\"-0.0146226\",\"0.0606828\",\"0.0863826\",\"0.128612\",\"-0.0407293\",\"-0.0739873\",\"-0.0669327\",\"-0.0205887\",\"0.0112899\",\"0.0820113\",\"-0.0580557\",\"-0.102451\",\"-0.0856689\",\"-0.0496506\",\"0.0438909\",\"-0.0508597\",\"-0.107923\",\"-0.0973691\",\"0.00476659\",\"-0.0577436\",\"-0.132596\",\"-0.0576641\",\"-0.0873712\",\"-0.131399\",\"-0.0855933\"]},\"ROCSOLE_raw\":{\"size\":\"256\",\"data\":[\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\"]}};";  


        static private TcpListener _server = null;
        static private IPEndPoint _endPoint = null;
        static private Thread _lisiner = null;

        static private int _port = 7777;
        static private int _backlog = 10;

        static private bool _areClients = false;
        static private int _activeClients = 0;
        static private double[] decimalArray;

        static void Main()
        {
            using (Stream stream = File.Open(@"./rawData.txt", FileMode.Open)) //256 1800
            using (TextReader sr = new StreamReader(stream, Encoding.UTF8))
            {


                List<double> column0 = new List<double>();
                string line;
                while ((line = sr.ReadLine()) != null)
                {

                    string[] arr = line.Trim().Split('\t');
                    foreach (var item in arr)
                    {
                        column0.Add(Convert.ToDouble(item));
                    }

                }

                decimalArray = column0.ToArray();
                Console.WriteLine(decimalArray.Length);
            }

            Console.WriteLine("Thread server started, listening on port 7777");

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
        static private string CraftString(int cycle)
        {
            int k = cycle * 256;
            string holder = "{\"CurrentMeasurementNo\":\"1\",\"TimeStamp\":\"0\",\"normalized\":{\"size\":\"120\",\"data\":[\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\",\"0\"]},\"ROCSOLE_raw\":{\"size\":\"256\",\"data\":[";
            for (int i = k;i <(k+256);i++)
            {
                holder += $"\"{decimalArray[i]}\"";
                if (i<(k+255)) { holder += ","; }
            }
            holder += "]}};";
            return holder;
        }
        static private void ClientCommunication(Socket client, String address, String port)
        {
            NetworkStream ns = new NetworkStream(client);
            StreamWriter sw = new StreamWriter(ns);
            int index = 1;
            int cycle = 0;

            try
            {
                while (true)
                {
                    json = CraftString(cycle);
                    cycle++;
                    if (cycle == 1800) { cycle = 0; }
                    string a = json.Substring(0, 25);
                    string b = json.Substring(26);
                    json = json.Substring(0, 25) + index + json.Substring(26);
                    index++; if (index == 10) index = 1;
                    sw.WriteLine(json);
                    sw.Flush();
                    //int len = client.Send(Encoding.ASCII.GetBytes(json));
                    Console.WriteLine($"Sent to client {address}, {port} ({json.Length}b):");
                    Thread.Sleep(350);
                }
            }
            catch (Exception)
            {
                Console.WriteLine($"Client disconnected");
            }
        }
    }
}
