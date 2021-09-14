//#define BgWorker_background_method
//#define BgWorker_thread_method
#define BgWorker_timer_method

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace SandBox
{
    public class Test_CB_PlayBYTimeSeriesForHogaPlay
    {
        #region 접근자
        private static Test_CB_PlayBYTimeSeriesForHogaPlay _instance = null;

        private static readonly object padlock = new object();
        public static Test_CB_PlayBYTimeSeriesForHogaPlay SharedInstance
        {
            get
            {
                lock(padlock)
                {
                    if(_instance == null)
                    {
                        _instance = new Test_CB_PlayBYTimeSeriesForHogaPlay();
                    }

                    return _instance;
                }
            }
        }
        #endregion

        #region 맴버변수

        private List<DateTime> dateTimeList = null;
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

        private PlayWatch playWatch = new PlayWatch();

        private DateTime playTimeStandardStartTime = new DateTime(2000, 1, 1, 9, 0, 0);

        private DateTime playTimeStandardLastTime = new DateTime(2000, 1, 1, 15, 35, 0);

        private object dateTimeBuf = null;

        private int listIdexCount = 0;

        #endregion

        #region 생성자
        public Test_CB_PlayBYTimeSeriesForHogaPlay()
        {
            DateTime dateTime = playTimeStandardStartTime;

            dateTimeList = new List<DateTime>();

            //for (int i = 0; i < 390; i++)
            //{
            //    dateTimeList.Add(dateTime);
            //    dateTime = dateTime.AddMinutes(1);
            //}

            for (int i = 0; i < 1000; i++)
            {
                dateTimeList.Add(dateTime);
                dateTime = dateTime.AddMilliseconds(1);
            }

            Init();
        }
        #endregion

        #region 매소드
        public bool Init()
        {
            try
            {
#if BgWorker_background_method
                bgWorker = new BackgroundWorker();
                bgWorker.WorkerSupportsCancellation = true;
                bgWorker.DoWork += BgWorker_DoWork;
#endif
#if BgWorker_thread_method
                bgWorker = new Thread(Loop);
                bgWorker.IsBackground = true;
#endif
#if BgWorker_timer_method
                //
#endif

                playWatch.Reset();
               
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}", ex.Message);

                return false;
            }
        }

        public bool Start()
        {
            try
            {
                if(playTimeStandardStartTime.Equals(GlobalSharedInstanceModel.SharedInstance.CurrentTimePosition))
                {
                    playWatch.AddTime(0);

                    listIdexCount = 0;

                }
                else
                {
                    long addTime = GetElapsePlayTime(GlobalSharedInstanceModel.SharedInstance.CurrentTimePosition);

                    playWatch.AddTime(addTime);

                    listIdexCount = GetListIndex(addTime);
                }

                bgWorkRunRequest = true;

#if BgWorker_background_method
                bgWorker.RunWorkerAsync();
#endif
#if BgWorker_thread_method
                bgWorker.Start();
#endif
#if BgWorker_timer_method
                bgWorker = new Timer(bgWorker_CallBack, null, 1, 10);
#endif

                playWatch.Start();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}", ex.Message);

                return false;
            }
        }

        

        public bool Stop()
        {
            try
            {
                dateTimeBuf = null;

                playWatch.Stop();

                bgWorkRunRequest = false;

                Thread.Sleep(1);

#if BgWorker_background_method
                bgWorker.CancelAsync();
#endif
#if BgWorker_thread_method
                bgWorker.Abort();
#endif
#if BgWorker_timer_method
                bgWorker?.Dispose();
#endif

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}", ex.Message);

                return false;
            }
        }

        public bool Reset()
        {
            try
            {
                long elapse = GetElapsePlayTime(GlobalSharedInstanceModel.SharedInstance.CurrentTimePosition);

                playWatch.AddTime(elapse);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}", ex.Message);

                return false;
            }
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
                    FastReader();

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

        private static readonly object lockFastReader = new object();
        private async void FastReader()
        {
            try
            {
                lock(lockFastReader)
                {
                    while (true)
                    {
                        if (dateTimeBuf == null)
                        {
                            if (dateTimeList.Count > 0 && listIdexCount < dateTimeList.Count && listIdexCount >= 0)
                            {
                                dateTimeBuf = dateTimeList[listIdexCount];
                            }

                        }
                        else
                        {
                            long playWatchElapse = (long)(playWatch.GetElapse());

                            if (playWatchElapse >= GetElapsePlayTime((DateTime)dateTimeBuf))
                            {
                                Console.WriteLine("[{0:D3}] {1}, real {2}", listIdexCount++, ((DateTime)dateTimeBuf).ToString("HH:mm:ss.fff"), DateTime.Now.ToString("HH:mm:ss.fff"));

                                dateTimeBuf = null;

                            }

                            if (playWatchElapse >= GetElapsePlayTime(playTimeStandardLastTime))
                            {
                                Stop();
                                break;
                            }
                        }

                        if (dateTimeBuf == null)
                        {
                            break;
                        }
                        else
                        {
                            //Thread.Sleep(1);
                            continue;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}", ex.Message);
            }
        }

        private void BgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Loop();
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}", ex.Message);
            }
        }

        private void Loop()
        {
            try
            {
                while (bgWorkRunRequest)
                {
                    if (dateTimeBuf == null)
                    {
                        if (dateTimeList.Count > 0 && listIdexCount < dateTimeList.Count && listIdexCount >= 0)
                        {
                            dateTimeBuf = dateTimeList[listIdexCount];
                        }

                    }
                    else
                    {
                        long playWatchElapse = (long)(playWatch.GetElapse());

                        if (playWatchElapse >= GetElapsePlayTime((DateTime)dateTimeBuf))
                        {
                            Console.WriteLine("[{0:D3}] {1}, real {2}", listIdexCount++, ((DateTime)dateTimeBuf).ToString("HH:mm:ss.fff"), DateTime.Now.ToString("HH:mm:ss.fff"));

                            dateTimeBuf = null;

                        }

                        if (playWatchElapse >= GetElapsePlayTime(playTimeStandardLastTime))
                        {
                            Stop();
                        }
                    }

                    Thread.Sleep(1);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}", ex.Message);
            }
        }

        private int GetListIndex(long addTimeMillisecond)
        {
            try
            {
                int temp = dateTimeList.FindIndex(x => GetElapsePlayTime(x) >= addTimeMillisecond);

                return temp;
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}", ex.Message);

                return 0;
            }
            
        }

        private long GetElapsePlayTime(DateTime dateTime)
        {
            TimeSpan ts = dateTime - playTimeStandardStartTime;

            return (long)ts.TotalMilliseconds;
        }

        public DateTime GetPlayWatchTime()
        {
            return playTimeStandardStartTime.AddMilliseconds((double)playWatch.GetElapse());
        }

        public bool SetPlaySpeed(double val)
        {
            if(val < 1)
            {
                playWatch.SetSpeed(0.5);
            }
            else
            {
                playWatch.SetSpeed(val);
            }

            return true;
        }
        #endregion
    }

    public class PlayWatch : Stopwatch
    {
        public long AddMilisecond { get; private set; } = 0;
        public double PlaySpeed { get; private set; } = 0;

        private DateTime playTimeStandardStartTime = new DateTime(2000, 1, 1, 9, 0, 0);

        private DateTime playTimeStandardLastTime = new DateTime(2000, 1, 1, 15, 35, 0);

        private long playMaxminumElaspe = 100000;

        private long elapseBuf = 0;
        public PlayWatch()
        {
            TimeSpan ts = playTimeStandardLastTime - playTimeStandardStartTime;

            playMaxminumElaspe = (long)ts.TotalMilliseconds;
        }

        public void AddTime(long val)
        {
            base.Reset();

            AddMilisecond = val;
        }

        private long GetDiffElapse(long elapseCurrent)
        {
            long diff = elapseCurrent - elapseBuf;

            //Console.WriteLine("diff = {0}", diff);
            diff = 2;


            if (PlaySpeed < 1)
            {
                if (elapseBuf == 0)
                {
                    return 0;
                }
                else
                {
                    return - ((elapseCurrent - elapseBuf) * (long)PlaySpeed);
                }
            }
            else if(PlaySpeed == 1)
            {
                return 0;
            }
            else
            {
                if (elapseBuf == 0)
                {
                    return 0;
                }
                else
                {
                    return (elapseCurrent - elapseBuf) * (long)PlaySpeed;
                }
            }
        }

        public long GetElapse()
        {
            long elapseCurrent = base.ElapsedMilliseconds;

            AddMilisecond += GetDiffElapse(elapseCurrent);

            long result = elapseCurrent + AddMilisecond;

            elapseBuf = elapseCurrent;

            if (result >= playMaxminumElaspe)
            {
                return playMaxminumElaspe;
            }
            else
            {
                return result;
            }
        }

        public void SetSpeed(double playSpeed)
        {
            PlaySpeed = playSpeed;
        }
    }
}
