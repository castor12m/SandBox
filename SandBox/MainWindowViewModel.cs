//#define BgWorker_background_method
//#define BgWorker_thread_method
#define BgWorker_timer_method

using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SandBox
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region 맴버변수

#if BgWorker_background_method
        private BackgroundWorker bgWorker;
#endif
#if BgWorker_thread_method
        private Thread bgWorker;
#endif
#if BgWorker_timer_method
        private Timer bgWorker;
#endif


        public bool bgWorkRunRequest { get; private set; } = false;

        public int PlayHour
        {
            get { return GlobalSharedInstanceModel.SharedInstance.CurrentTimePosition.Hour; }
            set 
            {
                GlobalSharedInstanceModel.SharedInstance.CurrentTimePosition = GetDateTime(hour: value, min: -1);
                OnPropertyChanged();
                OnPropertyChanged("PlayTime");
                ForceStop();
            }
        }

        public int PlayMin
        {
            get { return GlobalSharedInstanceModel.SharedInstance.CurrentTimePosition.Minute; }
            set 
            {
                GlobalSharedInstanceModel.SharedInstance.CurrentTimePosition = GetDateTime(hour:-1, min: value);
                OnPropertyChanged();
                OnPropertyChanged("PlayTime");
                ForceStop();
            }
        }

        public int PlaySec
        {
            get { return GlobalSharedInstanceModel.SharedInstance.CurrentTimePosition.Second; }
            set 
            {
                GlobalSharedInstanceModel.SharedInstance.CurrentTimePosition = GetDateTime(hour:-1, min:-1, sec: value);
                OnPropertyChanged();
                OnPropertyChanged("PlayTime");
                ForceStop();
            }
        }

        public int PlayTime
        {
            get 
            {
                return GetTotalMin();
            }
            set 
            {
                GlobalSharedInstanceModel.SharedInstance.CurrentTimePosition = GetDateTime(value);

                OnPropertyChanged();
                OnPropertyChanged("PlayHour");
                OnPropertyChanged("PlayMin");
                OnPropertyChanged("PlaySec");
                ForceStop();

                PlaySpeed = 1;
            }
        }

        private double _playSpeed = 1;
        public double PlaySpeed
        {
            get 
            {
                return _playSpeed; 
            }
            set 
            { 
                _playSpeed = value;

                OnPropertyChanged();
                OnPropertyChanged("PlaySpeedDisplay");

                Test_PlayBYTimeSeriesForHogaPlay.SharedInstance.SetPlaySpeed(_playSpeed);
            }
        }

        public string PlaySpeedDisplay
        {
            get
            {
                if (PlaySpeed < 1)
                    return "0.5";
                else
                    return PlaySpeed.ToString();
            }
        }

        #endregion

        #region 생성자
        public MainWindowViewModel()
        {

#if BgWorker_background_method
            bgWorker = new BackgroundWorker();
            bgWorker.WorkerSupportsCancellation = true;
            bgWorker.DoWork += BgWorker_DoWork;
#endif
#if BgWorker_thread_method
            bgWorker = new Thread(Loop);
            //bgWorker.IsBackground = true;
#endif

        }

        private void bgWorker_CallBack(object state)
        {
            try
            {
                bgWorker_CallBack_Event();
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}", ex.Message);
            }
        }

        private async void bgWorker_CallBack_Event()
        {
            try
            {
                var task1 = Task.Run(() =>
                {
                    GlobalSharedInstanceModel.SharedInstance.CurrentTimePosition = Test_PlayBYTimeSeriesForHogaPlay.SharedInstance.GetPlayWatchTime();

                    OnPropertyChanged("PlayTime");
                    OnPropertyChanged("PlayHour");
                    OnPropertyChanged("PlayMin");
                    OnPropertyChanged("PlaySec");

                    return 1;
                });

                // task1이 끝나길 기다렸다가 끝나면 결과치를 sum에 할당
                int sum = await task1;
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}", ex.Message);
            }
        }

        private void BgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Loop();
        }

        private void Loop()
        {
            while (bgWorkRunRequest)
            {
                GlobalSharedInstanceModel.SharedInstance.CurrentTimePosition = Test_PlayBYTimeSeriesForHogaPlay.SharedInstance.GetPlayWatchTime();

                OnPropertyChanged("PlayTime");
                OnPropertyChanged("PlayHour");
                OnPropertyChanged("PlayMin");
                OnPropertyChanged("PlaySec");

                Thread.Sleep(1);
            }
        }
        #endregion

        #region 커맨드변수
        public ICommand CmdBindingTest0 => new SampleCommand(x => true, o => DoTest0());
        private void DoTest0()
        {
            try
            {
                #region Test_PlayByTimeSeriesForHogaPlay
#if true
                if (Test_PlayBYTimeSeriesForHogaPlay.SharedInstance.bgWorkRunRequest)
                {
                    ForceStop();
                }
                else
                {
                    ForceStart();
                }
#endif
                #endregion
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}", ex.Message);
            }
        }

        public ICommand CmdBindingTest1 => new SampleCommand(x => true, o => DoTest1());

        NetMqServer netMqServer;
        private void DoTest1()
        {
            try
            {
#if true
                netMqServer = new NetMqServer();
                netMqServer.Init("127.0.0.1", 5555);
                netMqServer.Start();

                //Test_NetMq.SharedInstance.DoSever();
#endif
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}", ex.Message);
            }
        }

        public ICommand CmdBindingTest2 => new SampleCommand(x => true, o => DoTest2());

        NetMqClient netMqClient;
        private void DoTest2()
        {
            try
            {
#if true
                netMqClient = new NetMqClient();
                netMqClient.Init("192.168.0.69", 5555);
                netMqClient.Start();

                //Test_NetMq.SharedInstance.DoClient();
#endif
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}", ex.Message);
            }
        }

        public ICommand CmdBindingTest3 => new SampleCommand(x => true, o => DoTest3());

        private void DoTest3()
        {
            try
            {
#if true
                netMqServer.Send("test from window");
#endif
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}", ex.Message);
            }
        }

        public ICommand CmdBindingTest4 => new SampleCommand(x => true, o => DoTest4());

        private void DoTest4()
        {
            try
            {
#if true
                netMqClient.Send("test from window");
#endif
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}", ex.Message);
            }
        }

        

        private void ForceStop()
        {
            try
            {
                if (bgWorkRunRequest)
                {
                    Test_PlayBYTimeSeriesForHogaPlay.SharedInstance.Stop();
                    bgWorkRunRequest = false;
                    
                    Thread.Sleep(1);

#if BgWorker_background_method
                    bgWorker.CancelAsync();
#endif
#if BgWorker_thread_method
                    bgWorker.Abort();
                    //bgWorker.Join();
#endif
#if BgWorker_timer_method
                    bgWorker?.Dispose();
#endif
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}", ex.Message);
            }
        }

        private void ForceStart()
        {
            try
            {
                Test_PlayBYTimeSeriesForHogaPlay.SharedInstance.Reset();

                bgWorkRunRequest = true;

#if BgWorker_background_method
                bgWorker.RunWorkerAsync();
#endif
#if BgWorker_thread_method
                bgWorker.Start();
#endif
#if BgWorker_timer_method
                bgWorker = new Timer(bgWorker_CallBack, null, 1, 1);
#endif

                Test_PlayBYTimeSeriesForHogaPlay.SharedInstance.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}", ex.Message);
            }
        }
        #endregion

        #region 매소드
        public DateTime GetDateTime(int hour = 9, int min = 0, int sec = 0)
        {
            DateTime dateTime = GlobalSharedInstanceModel.SharedInstance.CurrentTimePosition;

            try
            {
                int setHour = hour < 0 ? GlobalSharedInstanceModel.SharedInstance.CurrentTimePosition.Hour : hour;
                int setMin = min < 0 ? GlobalSharedInstanceModel.SharedInstance.CurrentTimePosition.Minute : min;
                int setSec = sec < 0 ? GlobalSharedInstanceModel.SharedInstance.CurrentTimePosition.Second : sec;

                dateTime = new DateTime(2000, 1, 1, setHour, setMin, setSec);

            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}", ex.Message);
            }

            return dateTime;
        }

        public DateTime GetDateTime(int totalMin = 0)
        {
            DateTime dateTime = new DateTime(2000, 1, 1, 9, 0, 0);

            try
            {
                dateTime = dateTime.AddMinutes(totalMin);
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}", ex.Message);
            }

            return dateTime;
        }

        public int GetTotalMin()
        {
            try
            {
                return ((GlobalSharedInstanceModel.SharedInstance.CurrentTimePosition.Hour - 9) * 60 + GlobalSharedInstanceModel.SharedInstance.CurrentTimePosition.Minute);
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}", ex.Message);
                return 0;
            }
        }
        #endregion
    }
}
