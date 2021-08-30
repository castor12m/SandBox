using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SandBox
{
    public class Test_CalculatePowerWithSampleRate
    {
        #region 접근자
        private static Test_CalculatePowerWithSampleRate _instance = null;

        private static readonly object padlock = new object();
        public static Test_CalculatePowerWithSampleRate SharedInstance
        {
            get
            {
                lock (padlock)
                {
                    if (_instance == null)
                    {
                        _instance = new Test_CalculatePowerWithSampleRate();
                    }

                    return _instance;
                }
            }
        }
        #endregion

        #region 맴버변수
        //
        #endregion

        #region 메소드
        public void DoSomething()
        {
            try
            {

                double stepTime = 0.1;

                double ratio = sampleTime / stepTime;

                numberofOccupied = (int)Math.Ceiling(ratio);     // 고유 기록

                restTime = numberofOccupied * stepTime - sampleTime;

                int tempCount = 0;

                while(true)
                {
                    #region 랜덤사용
                    if (restStep == 0)
                    {
                        Random random = new Random();
                        int arbitraryVal = random.Next(10);

                        if (arbitraryVal > 7)
                        {
                            restStep = numberofOccupied;
                        }
                    }
                    #endregion

                    tempCount++;

                    var temp = DoCalculate(stepTime);

                    Console.WriteLine("[{0:D3}] Energy = {1}", tempCount, temp);

                    Thread.Sleep(800);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        private double standbyPower = 1;
        private double drivingPower = 3;
        private double sampleTime = 0.375;
        private double restTime = 0;

        private int numberofOccupied = 0;
        private int restStep = 0;
        
        public double DoCalculate(double stepTime)
        {

            double energy = 0;

            try
            {

                if(restStep > 0)
                {
                    energy = restTime * standbyPower + sampleTime * drivingPower;

                    restStep--;
                }
                else
                {
                    energy = stepTime * standbyPower;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return energy;
        }
        #endregion

    }
}
