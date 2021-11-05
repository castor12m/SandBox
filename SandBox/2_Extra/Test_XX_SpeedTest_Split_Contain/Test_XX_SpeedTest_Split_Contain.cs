using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SandBox
{
    class Test_XX_SpeedTest_Split_Contain
    {
        private const string TestString = "[SIMDT]TYPE=byp_2,Log=2.232440,Lat=0.696804,Alt=508774.785215,Heading=-1.775674,Pitch=-0.215998,Roll=0.069391,GSCONT=None,MStat=0,FootA0=0,FootA1=0,FootA2=0,FootA3=0,FootB0=0,FootB1=0,FootB2=0,FootB3=0,EFSOLAREA=0.088529,LEOP=1,MOIX=0.576,MOIY=0.576,MOIZ=0.350,POIX=0.000,POIY=-0.000,POIZ=-0.000,LOMCX=-0.000,LOMCY=-0.000,LOMCZ=-0.013,HNX=-0.004,HNY=0.004,HNZ=0.023,HBX=0.017,HBY=-0.002,HBZ=-0.017,MASS=24.504,EULERSEQ=123,ANGVELX=-0.001,ANGVELY=0.000,ANGVELZ=-0.000,WHL0_AXISX=1.000,WHL0_AXISY=0.000,WHL0_AXISZ=0.000,WHL0_MAXTORQ=0.007,WHL0_MOMENT=0.017,WHL0_WRI=0.000,WHL0_SI=0.000,WHL0_DI=0.000,WHL1_AXISX=0.000,WHL1_AXISY=1.000,WHL1_AXISZ=0.000,WHL1_MAXTORQ=0.007,WHL1_MOMENT=-0.014,WHL1_WRI=0.000,WHL1_SI=0.000,WHL1_DI=0.000,WHL2_AXISX=0.000,WHL2_AXISY=0.000,WHL2_AXISZ=1.000,WHL2_MAXTORQ=0.007,WHL2_MOMENT=-0.023,WHL2_WRI=0.000,WHL2_SI=0.000,WHL2_DI=0.000,WHL0_RPM=20828.242,WHL1_RPM=-16278.868,WHL2_RPM=-26891.741,MTB0_AXISX=1.000,MTB0_AXISY=0.000,MTB0_AXISZ=0.000,MTB0_SATUR=3.300,MTB1_AXISX=0.000,MTB1_AXISY=1.000,MTB1_AXISZ=0.000,MTB1_SATUR=3.300,MTB2_AXISX=0.000,MTB2_AXISY=0.000,MTB2_AXISZ=1.000,MTB2_SATUR=3.300,MTB0_MT0=0.000,MTB0_MT1=-0.000,MTB0_MT2=0.000,MTB0_MP=0.000,MTB1_MT0=0.000,MTB1_MT1=0.000,MTB1_MT2=-0.000,MTB1_MP=0.000,MTB2_MT0=-0.000,MTB2_MT1=0.000,MTB2_MT2=0.000,MTB2_MP=0.000,GYRO0_SAMPLERATE=1.000,GYRO0_MDX=1.000,GYRO0_MDY=0.000,GYRO0_MDZ=0.000,GYRO0_TMDX=1.000,GYRO0_TMDY=0.000,GYRO0_TMDZ=0.000,GYRO0_RRW=0.000,GYRO0_BIAS=0.000,GYRO1_SAMPLERATE=1.000,GYRO1_MDX=0.000,GYRO1_MDY=1.000,GYRO1_MDZ=0.000,GYRO1_TMDX=0.000,GYRO1_TMDY=1.000,GYRO1_TMDZ=0.000,GYRO1_RRW=0.000,GYRO1_BIAS=-0.000,GYRO2_SAMPLERATE=1.000,GYRO2_MDX=0.000,GYRO2_MDY=0.000,GYRO2_MDZ=1.000,GYRO2_TMDX=0.000,GYRO2_TMDY=0.000,GYRO2_TMDZ=1.000,GYRO2_RRW=0.000,GYRO2_BIAS=0.000,GYRO0_ANGVEL=-0.000,GYRO1_ANGVEL=0.021,GYRO2_ANGVEL=0.018,MAG0_SAMPLERATE,MAG0_AXISX=1.000,MAG0_AXISY=1.000,MAG0_AXISZ=0.000,MAG0_SATUR=0.000,MAG0_SCALE=0.000,MAG0_QUANT=1.000,MAG0_NOISE=0.000,MAG1_SAMPLERATE,MAG1_AXISX=0.000,MAG1_AXISY=1.000,MAG1_AXISZ=0.000,MAG1_SATUR=1.000,MAG1_SCALE=0.000,MAG1_QUANT=0.000,MAG1_NOISE=1.000,MAG2_SAMPLERATE,MAG2_AXISX=0.000,MAG2_AXISY=0.000,MAG2_AXISZ=1.000,MAG2_SATUR=0.000,MAG2_SCALE=0.000,MAG2_QUANT=1.000,MAG2_NOISE=0.000,MAG0_FIELD=1.000,MAG1_FIELD=0.000,MAG2_FIELD=0.000,FSS_SAMPLERATE=1.000,FSS_QB0=0.189,FSS_QB1=0.239,FSS_QB2=0.038,FSS_QB3=0.952,FSS_FOVX=0.559,FSS_FOVY=0.559,FSS_NEA=0.002,FSS_QUANT=0.009,FSS_SUNANG0=0.000,FSS_SUNANG1=0.000,ST_SAMPLERATE=1.000,ST_QB0=0.189,ST_QB1=0.239,ST_QB2=0.038,ST_QB3=0.952,ST_FOVX=0.140,,ST_FOVY=0.140,ST_SUNANG=0.524,ST_EARTHANG=0.175,ST_MOONANG=0.175,ST_QN0=0.072,ST_QN1=0.483,ST_QN2=0.640,ST_QN3=-0.593,GPS_SAMPLERATE=1.000,GPS_POSNOISE=4.000,GPS_VELNOISE=0.020,GPS_TIMENOISE=0.000,GPS_POXNX=-1767968.419,GPS_POXNY=-4984029.602,GPS_POXNZ=4398028.231,GPS_VELNX=402.160,GPS_VELNY=4949.181,GPS_VELNZ=5770.316,GPS_POSWX=-3249217.554,GPS_POSWY=4172391.243,GPS_POSWZ=4398028.231,GPS_VELWX=4259.149,GPS_VELWY=-2765.554,GPS_VELWZ=5770.316,ACCEL0_SAMPLERATE=1.000,ACCEL0_AXISX=1.000,ACCEL0_AXISY=0.000,ACCEL0_AXISZ=0.000,ACCEL0_TAXISX=1.000,ACCEL0_TAXISY=0.000,ACCEL0_TAXISZ=0.000,ACCEL0_BIAS=0.263,ACCEL0_SCALE=1.000,ACCEL1_SAMPLERATE=1.000,ACCEL1_AXISX=1.000,ACCEL1_AXISY=0.000,ACCEL1_AXISZ=0.000,ACCEL1_TAXISX=1.000,ACCEL1_TAXISY=0.000,ACCEL1_TAXISZ=0.000,ACCEL1_BIAS=0.263,ACCEL1_SCALE=1.000,ACCEL2_SAMPLERATE=1.000,ACCEL2_AXISX=1.000,ACCEL2_AXISY=0.000,ACCEL2_AXISZ=0.000,ACCEL2_TAXISX=1.000,ACCEL2_TAXISY=0.000,ACCEL2_TAXISZ=0.000,ACCEL2_BIAS=0.263,ACCEL2_SCALE=1.000,ACCEL0_ACC=0.250,ACCEL1_ACC=0.250,ACCEL2_ACC=0.300";
        private const string TestHeader = "[SIMDT]";

        private static int preNum = -1;
        private static int repeatCount = 0;

        public static void DoSomething(int mode)
        {
            for (int i = 0; i < 50; i++)
            {
                if (mode != preNum)
                {
                    history.Clear();
                    preNum = mode;
                    repeatCount = 0;
                }
                else
                {
                    repeatCount++;
                }

                switch (mode)
                {
                    case 0:
                        DoInstanceDictionary();
                        break;
                    case 1:
                        DoInstanceDictionary1();
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    default:
                        break;
                }
            }

            
        }

        private static List<double> history = new List<double>();

        private static void DoSplit()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Reset();
            stopwatch.Start();

            for (int i = 0; i < 1000; i++)
            {
                TestString.Split(new string[] { "[SIMDT]" }, StringSplitOptions.RemoveEmptyEntries);
            }

            stopwatch.Stop();
            history.Add(stopwatch.ElapsedTicks);
            Console.WriteLine("[{0:000}] elpase {1} ms, tick {2}, meantick {3:0}", repeatCount, stopwatch.ElapsedMilliseconds, stopwatch.ElapsedTicks, history.Average());
        }

        

        private static void DoContain()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Reset();
            stopwatch.Start();

            for (int i = 0; i < 1000; i++)
            {
                if(TestString.Contains("[SIMDT]"))
                {
                    string temp = TestString.Substring(7);
                }
            }

            stopwatch.Stop();
            history.Add(stopwatch.ElapsedTicks);
            Console.WriteLine("[{0:000}] elpase {1} ms, tick {2}, meantick {3:0}", repeatCount, stopwatch.ElapsedMilliseconds, stopwatch.ElapsedTicks, history.Average());
        }

        

        private static void DoSplit1()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Reset();
            stopwatch.Start();

            for (int i = 0; i < 1000; i++)
            {
                TestString.Split(new string[] { TestHeader }, StringSplitOptions.RemoveEmptyEntries);
            }

            stopwatch.Stop();
            history.Add(stopwatch.ElapsedTicks);
            Console.WriteLine("[{0:000}] elpase {1} ms, tick {2}, meantick {3:0}", repeatCount, stopwatch.ElapsedMilliseconds, stopwatch.ElapsedTicks, history.Average());
        }

        private static void DoContain1()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Reset();
            stopwatch.Start();

            for (int i = 0; i < 1000; i++)
            {
                if (TestString.Contains(TestHeader))
                {
                    string temp = TestString.Substring(7, TestString.Length - 7);
                }
            }

            stopwatch.Stop();
            history.Add(stopwatch.ElapsedTicks);
            Console.WriteLine("[{0:000}] elpase {1} ms, tick {2}, meantick {3:0}", repeatCount, stopwatch.ElapsedMilliseconds, stopwatch.ElapsedTicks, history.Average());
        }

        private static void DoInstanceDictionary()
        {
            int temp = 0;

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Reset();
            stopwatch.Start();

            for (int i = 0; i < 1000; i++)
            {
                Dictionary<OrbitDataPacketKeyType, string> dataPacket = new Dictionary<OrbitDataPacketKeyType, string>();

                //stopwatch.Stop();
                for (int j = 0; j < 200; j++)
                    dataPacket.Add((OrbitDataPacketKeyType)temp++, "1");
                //stopwatch.Start();
            }

            stopwatch.Stop();
            history.Add(stopwatch.ElapsedTicks);
            Console.WriteLine("[{0:000}] elpase {1} ms, tick {2}, meantick {3:0}", repeatCount, stopwatch.ElapsedMilliseconds, stopwatch.ElapsedTicks, history.Average());
            
        }

        private static void DoInstanceDictionary1()
        {
            Dictionary<OrbitDataPacketKeyType, string> dataPacket = new Dictionary<OrbitDataPacketKeyType, string>();
            int temp = 0;

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Reset();
            stopwatch.Start();

            for (int i = 0; i < 1000; i++)
            {
                dataPacket.Clear();

                //stopwatch.Stop();
                for (int j = 0; j < 200; j++)
                    dataPacket.Add((OrbitDataPacketKeyType)temp++, "1");
                //stopwatch.Start();
            }

            stopwatch.Stop();
            history.Add(stopwatch.ElapsedTicks);
            Console.WriteLine("[{0:000}] elpase {1} ms, tick {2}, meantick {3:0}", repeatCount, stopwatch.ElapsedMilliseconds, stopwatch.ElapsedTicks, history.Average());

        }

        public enum OrbitDataPacketKeyType
        {
            // 무조건 소문자로...
            rawdata,
            time,
            efsolarea,
            leop,
            rocketstatus,

            type,
            log,
            lat,
            alt,
            heading,
            pitch,
            roll,
            foota0,
            foota1,
            foota2,
            foota3,
            footb0,
            footb1,
            footb2,
            footb3,
            gscont,
            mstat,
            moix,
            moiy,
            moiz,
            poix,
            poiy,
            poiz,
            lomcx,
            lomcy,
            lomcz,
            hnx,
            hny,
            hnz,
            hbx,
            hby,
            hbz,
            mass,
            eulerseq,
            angvelx,
            angvely,
            angvelz,
            whl0_axisx,
            whl0_axisy,
            whl0_axisz,
            whl0_maxtorq,
            whl0_moment,
            whl0_wri,
            whl0_si,
            whl0_di,
            whl1_axisx,
            whl1_axisy,
            whl1_axisz,
            whl1_maxtorq,
            whl1_moment,
            whl1_wri,
            whl1_si,
            whl1_di,
            whl2_axisx,
            whl2_axisy,
            whl2_axisz,
            whl2_maxtorq,
            whl2_moment,
            whl2_wri,
            whl2_si,
            whl2_di,
            whl0_rpm,
            whl1_rpm,
            whl2_rpm,
            mtb0_axisx,
            mtb0_axisy,
            mtb0_axisz,
            mtb0_satur,
            mtb1_axisx,
            mtb1_axisy,
            mtb1_axisz,
            mtb1_satur,
            mtb2_axisx,
            mtb2_axisy,
            mtb2_axisz,
            mtb2_satur,
            mtb0_mt0,
            mtb0_mt1,
            mtb0_mt2,
            mtb0_mp,
            mtb1_mt0,
            mtb1_mt1,
            mtb1_mt2,
            mtb1_mp,
            mtb2_mt0,
            mtb2_mt1,
            mtb2_mt2,
            mtb2_mp,
            gyro0_samplerate,
            gyro0_mdx,
            gyro0_mdy,
            gyro0_mdz,
            gyro0_tmdx,
            gyro0_tmdy,
            gyro0_tmdz,
            gyro0_maxvalue,
            gyro0_rrw,
            gyro0_bias,
            gyro0_initbias,
            gyro1_samplerate,
            gyro1_mdx,
            gyro1_mdy,
            gyro1_mdz,
            gyro1_tmdx,
            gyro1_tmdy,
            gyro1_tmdz,
            gyro1_maxvalue,
            gyro1_rrw,
            gyro1_bias,
            gyro1_initbias,
            gyro2_samplerate,
            gyro2_mdx,
            gyro2_mdy,
            gyro2_mdz,
            gyro2_tmdx,
            gyro2_tmdy,
            gyro2_tmdz,
            gyro2_maxvalue,
            gyro2_rrw,
            gyro2_bias,
            gyro2_initbias,
            gyro0_angvel,
            gyro1_angvel,
            gyro2_angvel,
            mag0_samplerate,
            mag0_axisx,
            mag0_axisy,
            mag0_axisz,
            mag0_satur,
            mag0_scale,
            mag0_quant,
            mag0_noise,
            mag1_samplerate,
            mag1_axisx,
            mag1_axisy,
            mag1_axisz,
            mag1_satur,
            mag1_scale,
            mag1_quant,
            mag1_noise,
            mag2_samplerate,
            mag2_axisx,
            mag2_axisy,
            mag2_axisz,
            mag2_satur,
            mag2_scale,
            mag2_quant,
            mag2_noise,
            mag0_field,
            mag1_field,
            mag2_field,
            fss_samplerate,
            fss_qb0,
            fss_qb1,
            fss_qb2,
            fss_qb3,
            fss_fovx,
            fss_fovy,
            fss_nea,
            fss_quant,
            fss_sunang0,
            fss_sunang1,
            st_samplerate,
            st_qb0,
            st_qb1,
            st_qb2,
            st_qb3,
            st_fovx,
            st_fovy,
            st_sunang,
            st_earthang,
            st_moonang,
            st_qn0,
            st_qn1,
            st_qn2,
            st_qn3,
            gps_samplerate,
            gps_posnoise,
            gps_velnoise,
            gps_timenoise,
            gps_poxnx,
            gps_poxny,
            gps_poxnz,
            gps_velnx,
            gps_velny,
            gps_velnz,
            gps_poswx,
            gps_poswy,
            gps_poswz,
            gps_velwx,
            gps_velwy,
            gps_velwz,
            accel0_samplerate,
            accel0_axisx,
            accel0_axisy,
            accel0_axisz,
            accel0_taxisx,
            accel0_taxisy,
            accel0_taxisz,
            accel0_maxvalue,
            accel0_bias,
            accel0_scale,
            accel1_samplerate,
            accel1_axisx,
            accel1_axisy,
            accel1_axisz,
            accel1_taxisx,
            accel1_taxisy,
            accel1_taxisz,
            accel1_maxvalue,
            accel1_bias,
            accel1_scale,
            accel2_samplerate,
            accel2_axisx,
            accel2_axisy,
            accel2_axisz,
            accel2_taxisx,
            accel2_taxisy,
            accel2_taxisz,
            accel2_maxvalue,
            accel2_bias,
            accel2_scale,
            accel0_acc,
            accel1_acc,
            accel2_acc,


            etc
        }

    }

}
