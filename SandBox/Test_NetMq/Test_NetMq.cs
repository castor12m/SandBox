using NetMQ;
using NetMQ.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SandBox
{
    public class Test_NetMq
    {
        #region 접근자
        private static Test_NetMq _instance = null;

        private static readonly object padlock = new object();
        public static Test_NetMq SharedInstance
        {
            get
            {
                lock (padlock)
                {
                    if (_instance == null)
                    {
                        _instance = new Test_NetMq();
                    }

                    return _instance;
                }
            }
        }
        #endregion

        #region 매소드
        public void DoSever()
        {
            try
            {
                using (var server = new ResponseSocket())
                {
                    server.Bind("tcp://*:5555");
                    while (true)
                    {
                        var message = server.ReceiveFrameString();
                        Console.WriteLine("Received {0}", message);
                        // processing the request
                        Thread.Sleep(1);
                        Console.WriteLine("Sending World");
                        server.SendFrame("World from window");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}", ex.Message);
            }
        }

        public void DoClient()
        {
            try
            {
                using (var client = new RequestSocket())
                {
                    //client.Connect("tcp://192.168.0.65:5555");
                    client.Connect("tcp://127.0.0.1:6000");
                    for (int i = 0; i < 10; i++)
                    {
                        Console.WriteLine("Sending Hello");
                        client.SendFrame("Hello from window");
                        var message = client.ReceiveFrameString();
                        Console.WriteLine("Received {0}", message);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}", ex.Message);
            }
        }
        #endregion

    }




    public class NetMqServer
    {
        #region 맴버변수
        private string TargetIP = "";
        private ushort TargetPort = 0;

        private Thread ThreadReceiving;

        public bool IsReceiving { get; private set; } = true;
        public bool IsReady { get; private set; } = false;
        #endregion

        #region 이벤트

        public delegate void DataReceiveDelegate(string Data);  // 이벤트 전달을 위해 핸들 선언
        public event DataReceiveDelegate DataReceiveEvent;      // 이벤트 선언
        #endregion

        #region 생성자
        public NetMqServer()
        {
            //
        }

        ~NetMqServer()
        {
            Stop();
        }
        #endregion

        #region 메소드
        //https://netmq.readthedocs.io/en/latest/introduction/#first-example
        //
        //public void DoSever()
        //{
        //    using (var server = new ResponseSocket())
        //    {
        //        server.Bind("tcp://*:5555");
        //        while (true)
        //        {
        //            var message = server.ReceiveFrameString();
        //            Console.WriteLine("Received {0}", message);
        //            // processing the request
        //            Thread.Sleep(100);
        //            Console.WriteLine("Sending World");
        //            server.SendFrame("World from window");
        //        }
        //    }
        //}
        //
        //public void DoClient()
        //{
        //    using (var client = new RequestSocket())
        //    {
        //        client.Connect("tcp://localhost:5555");
        //        for (int i = 0; i < 10; i++)
        //        {
        //            Console.WriteLine("Sending Hello");
        //            client.SendFrame("Hello from window");
        //            var message = client.ReceiveFrameString();
        //            Console.WriteLine("Received {0}", message);
        //        }
        //    }
        //}

        private ResponseSocket server;
        public void Init(string ipAddress, int portNumber)
        {
            try
            {
                TargetIP = ipAddress;

                if (portNumber > 0)
                {
                    TargetPort = (ushort)portNumber;

                    Console.WriteLine($"Init portNumber set = {TargetPort}");
                }
                else
                {
                    Console.WriteLine($"Init portNumber parse fail = {portNumber}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public bool Start()
        {
            try
            {
                if (TargetPort == 0)
                {
                }
                else
                {
                    if (!IsReady)
                    {
                        IsReady = true;

                        recieveThread();

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            IsReady = false;

            return false;
        }

        public void Stop()
        {
            try
            {
                IsReady = false;

                ThreadReceiving?.Abort();

                DataReceiveEvent = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
        }

        public void recieveThread()
        {
            ThreadReceiving = new Thread(new ThreadStart(ThreadReceive));

            ThreadReceiving.IsBackground = true;

            ThreadReceiving.Start();
        }

        public bool Send(byte[] byteData)
        {
            try
            {
                if (IsReady)
                {
                    byte[] msg = new byte[byteData.Length];

                    msg = byteData;

                    server.SendFrame(byteData);

                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return false;
        }

        public bool Send(string Command)
        {
            try
            {
                if (IsReady)
                {
                    byte[] byteData = null;

                    //if (ReferenceModel.SharedInstance.TxDataHexStringConvert)
                    //{
                    //    byteData = Functions.ConvertHexStringToByte(Command);
                    //}
                    //else
                    {
                        byteData = Encoding.UTF8.GetBytes(Command);
                    }

                    if (byteData != null)
                    {
                        server.SendFrame(byteData);
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return false;
        }

        private void ThreadReceive()
        {
            try
            {
                IsReceiving = true;

                using (server = new ResponseSocket())
                {
                    server.Bind(string.Format("tcp://*:{0}", TargetPort));

                    while (IsReceiving)
                    {
                        var message = server.ReceiveFrameString();
                        Console.WriteLine("Received {0}", message);

                        // processing the request
                        Thread.Sleep(1);
                    }
                }

                Stop();
            }
            catch
            {
                IsReceiving = IsReady = false;

                Stop();
            }
        }

        //public void ReceiveData(string stringData)
        //{
        //    DataReceiveEvent(stringData);
        //}
        #endregion
    }

    public class NetMqClient
    {
        #region 맴버변수
        private string TargetIP = "";
        private ushort TargetPort = 0;

        private Thread ThreadReceiving;

        public bool IsReceiving { get; private set; } = true;
        public bool IsReady { get; private set; } = false;
        #endregion

        #region 이벤트

        public delegate void DataReceiveDelegate(string Data);  // 이벤트 전달을 위해 핸들 선언
        public event DataReceiveDelegate DataReceiveEvent;      // 이벤트 선언
        #endregion

        #region 생성자
        public NetMqClient()
        {
            //
        }

        ~NetMqClient()
        {
            Stop();
        }
        #endregion

        #region 메소드
        //https://netmq.readthedocs.io/en/latest/introduction/#first-example
        //
        //public void DoSever()
        //{
        //    using (var server = new ResponseSocket())
        //    {
        //        server.Bind("tcp://*:5555");
        //        while (true)
        //        {
        //            var message = server.ReceiveFrameString();
        //            Console.WriteLine("Received {0}", message);
        //            // processing the request
        //            Thread.Sleep(100);
        //            Console.WriteLine("Sending World");
        //            server.SendFrame("World from window");
        //        }
        //    }
        //}
        //
        //public void DoClient()
        //{
        //    using (var client = new RequestSocket())
        //    {
        //        client.Connect("tcp://localhost:5555");
        //        for (int i = 0; i < 10; i++)
        //        {
        //            Console.WriteLine("Sending Hello");
        //            client.SendFrame("Hello from window");
        //            var message = client.ReceiveFrameString();
        //            Console.WriteLine("Received {0}", message);
        //        }
        //    }
        //}

        private RequestSocket client;
        public void Init(string ipAddress, int portNumber)
        {
            try
            {
                TargetIP = ipAddress;

                if (portNumber > 0)
                {
                    TargetPort = (ushort)portNumber;

                    Console.WriteLine($"Init portNumber set = {TargetPort}");
                }
                else
                {
                    Console.WriteLine($"Init portNumber parse fail = {portNumber}");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public bool Start()
        {
            try
            {
                if (TargetPort == 0)
                {
                    Console.WriteLine($"Init portNumber value eror = {TargetPort}");
                }
                else
                {
                    if (!IsReady)
                    {
                        IsReady = true;

                        recieveThread();

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            IsReady = false;

            return false;
        }

        public void Stop()
        {
            try
            {
                IsReady = false;

                ThreadReceiving?.Abort();

                DataReceiveEvent = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async void recieveThread()
        {

            //ThreadReceiving = new Thread(new ThreadStart(ThreadReceive));

            //ThreadReceiving.IsBackground = true;

            //ThreadReceiving.Start();

            IsReceiving = true;

            using (var client = new RequestSocket())
            {
                client.Connect(string.Format("tcp://{0}:{1}", TargetIP, TargetPort));

                while (IsReceiving)
                {

                    var message = client.ReceiveFrameString();
                    Console.WriteLine("Received {0}", message);

                    // processing the request
                    Thread.Sleep(1);
                }
            }
        }

        public bool Send(byte[] byteData)
        {
            try
            {
                if (IsReady)
                {
                    byte[] msg = new byte[byteData.Length];

                    msg = byteData;

                    client.SendFrame(byteData);

                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return false;
        }

        public bool Send(string Command)
        {
            try
            {
                if (IsReady)
                {
                    byte[] byteData = null;

                    //if (ReferenceModel.SharedInstance.TxDataHexStringConvert)
                    //{
                    //    byteData = Functions.ConvertHexStringToByte(Command);
                    //}
                    //else
                    {
                        byteData = Encoding.UTF8.GetBytes(Command);
                    }

                    if (byteData != null)
                    {
                        client.SendFrame(byteData);
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return false;
        }

        private void ThreadReceive()
        {
            try
            {
                IsReceiving = true;

                using (var client = new RequestSocket())
                {
                    client.Connect(string.Format("tcp://{0}:{1}", TargetIP, TargetPort));

                    while (IsReceiving)
                    {
                        var message = client.ReceiveFrameString();
                        Console.WriteLine("Received {0}", message);

                        // processing the request
                        Thread.Sleep(1);
                    }
                }

                Stop();
            }
            catch
            {
                IsReceiving = IsReady = false;

                Stop();
            }
        }

        //public void ReceiveData(string stringData)
        //{
        //    DataReceiveEvent(stringData);
        //}
        #endregion
    }
}
