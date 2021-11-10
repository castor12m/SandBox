using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SandBox
{
    class LogManager
    {
        #region 접근자
        static private LogManager _instance = null;
        static readonly object padlock = new object();
        static public LogManager SharedInstance
        {
            get
            {
                lock (padlock)
                {
                    if (_instance == null)
                    {
                        _instance = new LogManager();
                    }
                }

                return _instance;
            }
        }
        #endregion

        #region 메소드
 
        public void Info(object message)
        {
            Console.WriteLine(message.ToString());
        }

        public void Wran(object message)
        {
            Console.WriteLine(message.ToString());
        }

        public void Fatal(object message)
        {
            Console.WriteLine(message.ToString());
        }

        public void Fatal(object message, Exception ex)
        {
            Console.WriteLine(message.ToString() + " " + ex.Message);
        }

        public void FatalOnlyFileWrite(object message, Exception ex)
        {
            Console.WriteLine(message.ToString() + " " + ex.Message);
        }
        #endregion
    }
}
