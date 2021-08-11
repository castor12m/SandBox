using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Input;

namespace SandBox
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region 맴버변수
        //private BackgroundWorker bgWorker;

        private Thread bgWorker;

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
            //bgWorker = new BackgroundWorker();
            //bgWorker.WorkerSupportsCancellation = true;
            //bgWorker.DoWork += BgWorker_DoWork;

            bgWorker = new Thread(Loop);
            bgWorker.IsBackground = true;
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
        public ICommand CmdBindingTest0 => new SampleCommand(x => true, o => DoTest());
        private void DoTest()
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

        private void ForceStop()
        {
            if(bgWorkRunRequest)
            {
                Test_PlayBYTimeSeriesForHogaPlay.SharedInstance.Stop();
                bgWorkRunRequest = false;
                //bgWorker.CancelAsync();
                bgWorker.Abort();
            }
        }

        private void ForceStart()
        {
            Test_PlayBYTimeSeriesForHogaPlay.SharedInstance.Reset();

            bgWorkRunRequest = true;
            //bgWorker.RunWorkerAsync();
            bgWorker.Start();
            Test_PlayBYTimeSeriesForHogaPlay.SharedInstance.Start();
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
