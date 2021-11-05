using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SandBox
{
    public class Test_XX_ListStringInputRefectoring
    {
        public static void DoSomething()
        {
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Reset();
            stopwatch.Start();

            List<string> temp = new List<string>() { "temp" , "test"};

            string result = DataModel.SetSatelliteModel(temp);

            stopwatch.Stop();
            Console.WriteLine("elpase {0}", stopwatch.ElapsedMilliseconds);

        }

        public class DataModel
        {
            //static private string Message = "";

            //static public string SetData(DataVisualPacketID packetId, List<string> list)
            //{
            //    string result = string.Empty;

            //    switch (packetId)
            //    {
            //        case DataVisualPacketID.SetSatellite:
            //            result = SetSatelliteModel(list);
            //            break;
            //        default:
            //            break;
            //    }

            //    return result;
            //}


            static public string SetSatelliteModel(List<string> list)
            {
                try
                {
                    //DataVisualService.SharedInstance.CubesatName = cubesatName;

                    return string.Format("0,{0}", string.Concat(list));
                }
                catch (Exception ex)
                {
                    //LogManager.SharedInstance.Fatal(string.Empty, ex);
                    return string.Empty;
                }
            }

            static public string SetGroundStationPosition(string gsName, string longitude, string latitude, string altitude)
            {
                try
                {
                    List<string> temp = new List<string>();

                    

                    return string.Format("1,{0},{1},{2},{3}", gsName, longitude, latitude, altitude);
                    //Type = 1;
                }
                catch (Exception ex)
                {
                    //LogManager.SharedInstance.Fatal(string.Empty, ex);
                    return string.Empty;
                }
            }

            static public string SetStationPositionAndAtitude
                (string longitude, string latitude, string altitude,
                string heading = "0", string pitch = "0", string roll = "0",
                string gscont = "None", string missionStatus = "None",
                string footPrintA0 = "None", string footPrintA1 = "None", string footPrintA2 = "None", string footPrintA3 = "None",
                string footPrintB0 = "None", string footPrintB1 = "None", string footPrintB2 = "None", string footPrintB3 = "None",
                string detumble = "1", string cubesatName = "")
            {
                try
                {
                    string name = cubesatName;

                    return string.Format("2,{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17}",
                        name,
                        longitude, latitude, altitude, heading, pitch, roll,
                        gscont, missionStatus,
                        footPrintA0, footPrintA1, footPrintA2, footPrintA3, footPrintB0, footPrintB1, footPrintB2, footPrintB3,
                        detumble);
                    
                }
                catch (Exception ex)
                {
                    //LogManager.SharedInstance.Fatal(string.Empty, ex);
                    return string.Empty;
                }
            }

        }


        public enum DataVisualPacketID
        {
            SetSatellite,
        }

    }


}


