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
            using (var server = new ResponseSocket())
            {
                server.Bind("tcp://*:5555");
                while (true)
                {
                    var message = server.ReceiveFrameString();
                    Console.WriteLine("Received {0}", message);
                    // processing the request
                    Thread.Sleep(100);
                    Console.WriteLine("Sending World");
                    server.SendFrame("World from window");
                }
            }

        }

        public void DoClient()
        {
            using (var client = new RequestSocket())
            {
                client.Connect("tcp://localhost:5555");
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine("Sending Hello");
                    client.SendFrame("Hello from window");
                    var message = client.ReceiveFrameString();
                    Console.WriteLine("Received {0}", message);
                }
            }
        }
        #endregion

    }
}
