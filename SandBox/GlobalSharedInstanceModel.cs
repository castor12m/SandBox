using System;

namespace SandBox
{
    public class GlobalSharedInstanceModel
    {
        #region 접근자
        private static GlobalSharedInstanceModel _instance = null;

        private static readonly object padlock = new object();
        public static GlobalSharedInstanceModel SharedInstance
        {
            get
            {
                lock (padlock)
                {
                    if (_instance == null)
                    {
                        _instance = new GlobalSharedInstanceModel();
                    }

                    return _instance;
                }
            }
        }
        #endregion

        #region 맴버변수
        public DateTime CurrentTimePosition { get; set; } = new DateTime(2000, 1, 1, 9, 0, 0);
        #endregion

    }
}
