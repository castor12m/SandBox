using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SandBox._1_MVVM.Text_MV_NasaConfigEdit
{
    class NasaIndex
    {
        /// <summary>
        ///  Parameter Index
        /// </summary>
        public int pi = 0;

        /// <summary>
        ///  Sub Index
        /// </summary>
        public int si = 0;

        /// <summary>
        ///  number of Element
        /// </summary>
        public int en = 0;

        /// <summary>
        ///  Skip Index
        /// </summary>
        public int ki = 0;  
    }

    public enum NasaTextType
    { 
        cmd,
        ipc,
        orbit,
        sc,
        sim,
        none = 10
    }


    

    class NasaTextStyle
    {
        static private Dictionary<string, NasaIndex> decodeTable = new Dictionary<string, NasaIndex>();

        public NasaTextStyle()
        {
            //decodeTable.Add("42: Spacecraft Description File", new NasaIndex() { pi = 1, si = 1, en = 5 });
            //decodeTable.Add("Orbit Parameters", new NasaIndex() { pi = 1, si = 2, en = 5 });
            //decodeTable.Add("Initial Attitude", new NasaIndex() { pi = 1, si = 3, en = 5 });
            //decodeTable.Add("Dynamics Flags", (1, 4));

            //decodeTable.Add("Dynamics Flags", (1, 4));

            this.LoadFile("42_cmd.txt");
            //this.LoadFile("42_ipc.txt");
            //this.LoadFile("42_orb.txt");
            //this.LoadFile("42_sc.txt");
            //this.LoadFile("42_sim.txt");

        }

        #region 매소드
        public string GetName(string line)
        {
            string result = string.Empty;

            result = line.Replace("*", "");
            result = result.Replace("=", "");
            result = result.Replace("<", "");
            result = result.Replace(">", "");
            result = result.Replace(":", "");
            result = result.Trim();

            return result;
        }

        public (bool, string, string) IsValueLine(string line)
        {
            if(line.Contains("!"))
            {
                string[] parts = line.Split('!');

                string value = parts[0].Trim();

                if(parts.Length == 2)
                {
                    string label = parts[1].Trim();

                    return (true, value, label);

                }
                else
                {
                    return (false, value, string.Empty);
                }
            }
            else
            {
                return (false, string.Empty, string.Empty);
            }
        }

        public bool isJunkLine()
        {
            return true;
        }

        public NasaIndex DecodeString(string str)
        {
            //var result = 
            NasaIndex rtn = null;

            if (decodeTable.TryGetValue(str, out rtn))
            {
                return rtn;
            }
            else
            {
                return null;
            }

        }

        public NasaTextType DecodeFirstString(string str)
        {
            switch (str)
            {
                case "42 Command Script File":
                case "42  Command Script File":
                    return NasaTextType.cmd;
                case "42 InterProcess Comm Configuration File":
                    return NasaTextType.ipc;
                case "42 Orbit Description File":
                    return NasaTextType.orbit;
                case "42 Spacecraft Description File":
                    return NasaTextType.sc;
                case "42 The Mostly Harmless Simulator":
                    return NasaTextType.sim;
                default:
                    return NasaTextType.none;
            }
        }

        public void LoadFile(string filePath)
        {
            try
            {
                string[] lines = File.ReadAllLines(filePath);

                //string textTitle = string.Empty;

                //NasaIndex currIndex = new NasaIndex();
                //// 초기화시 제외 위 Index는 0이 될 수 없도록

                //int lineNumber = 0;     // 0 가능
                //int ki = 0;             // 0 가능

                // sc 파일
                NasaTextType textType = NasaTextType.none;

                if(lines.Length > 0)
                {
                    textType = DecodeFirstString(GetName(lines[0]));

                    if (textType.Equals(NasaTextType.none))
                    {
                        LogManager.SharedInstance.Fatal("LoadFile NasaTextType read error");
                    }
                    else
                    {
                        switch (textType)
                        {
                            case NasaTextType.cmd:
                                GetCMDText(lines);
                                break;
                            case NasaTextType.ipc:
                                GetIPCText(lines);
                                break;
                            case NasaTextType.orbit:
                                GetOrbitText(lines);
                                break;
                            case NasaTextType.sc:
                                GetSCText(lines);
                                break;
                            case NasaTextType.sim:
                                GetSimText(lines);
                                break;
                            case NasaTextType.none:
                            default:
                                LogManager.SharedInstance.Fatal("LoadFile NasaTextType.none error");
                                break;
                        }
                        
                    }
                }
                else
                {
                    LogManager.SharedInstance.Fatal("LoadFile lines.Length error");
                }
            }
            catch (Exception ex)
            {
                LogManager.SharedInstance.Fatal(string.Empty, ex);
            }
        }

        public readonly string[] ignoreList = { " ", "//", "#", "\n", "\r", "%" };
        public NasaDataType_CMD GetCMDText(string[] fs)
        {
            int lineNumber = 1;
            int dupNum = 0;     // duplication Number 

            NasaDataType_CMD nasaData = new NasaDataType_CMD();

            try
            {

                List<string> vs = new List<string>();

                for (; lineNumber < fs.Length; lineNumber++)
                {
                    if(fs[lineNumber].Contains("EOF"))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"------{fs[lineNumber]}");

                        if(ignoreList.Any(s => fs[lineNumber].IndexOf(s) == 0))
                        {
                            Console.WriteLine($"---- FF");
                        }

                        vs.Add(fs[lineNumber]);
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.SharedInstance.Fatal(string.Empty, ex);
            }

            return nasaData;
        }

        public NasaDataType_IPC GetIPCText(string[] fs)
        {
            int lineNumber = 1;
            int dupNum = 0;     // duplication Number 

            NasaDataType_IPC nasaData = new NasaDataType_IPC();

            try
            {
                dupNum = int.Parse(SafeSetValue(IsValueLine(fs[lineNumber++]))[0]);
                nasaData.Socket_Parameters = new SocketParam[dupNum];
                lineNumber++;

                for (int i = 0; i < dupNum; i++)
                {
                    nasaData.Socket_Parameters[i] = new SocketParam();

                    nasaData.Socket_Parameters[i].IPC_Mode = SafeSetValue(IsValueLine(fs[lineNumber++]));
                    nasaData.Socket_Parameters[i].ACS_mode = SafeSetValue(IsValueLine(fs[lineNumber++]));
                    nasaData.Socket_Parameters[i].File_name = SafeSetValue(IsValueLine(fs[lineNumber++]));
                    nasaData.Socket_Parameters[i].Socket_Role = SafeSetValue(IsValueLine(fs[lineNumber++]));
                    nasaData.Socket_Parameters[i].Server_Host = SafeSetValue(IsValueLine(fs[lineNumber++]));
                    nasaData.Socket_Parameters[i].Allow_Blocking = SafeSetValue(IsValueLine(fs[lineNumber++]));
                    nasaData.Socket_Parameters[i].Echo_to_stdout = SafeSetValue(IsValueLine(fs[lineNumber++]));

                    dupNum = int.Parse(SafeSetValue(IsValueLine(fs[lineNumber++]))[0]);
                    nasaData.Socket_Parameters[i].Prefixes_Parameters = new PrefixParam[dupNum];

                    for (int j = 0; j < dupNum; j++)
                    {
                        nasaData.Socket_Parameters[i].Prefixes_Parameters[j] = new PrefixParam();

                        nasaData.Socket_Parameters[i].Prefixes_Parameters[j].Prefix = SafeSetValue(IsValueLine(fs[lineNumber++]));
                    }

                    lineNumber++;
                }

            }
            catch (ArgumentException ex)
            {
                LogManager.SharedInstance.Fatal(string.Empty, ex);
            }
            catch (Exception ex)
            {
                LogManager.SharedInstance.Fatal(string.Empty, ex);
            }

            return nasaData;
        }


        public NasaDataType_ORB GetOrbitText(string[] fs)
        {
            int lineNumber = 1;
            int dupNum = 0;     // duplication Number 

            NasaDataType_ORB nasaData = new NasaDataType_ORB();

            try
            {
                nasaData.Description = SafeSetValue(IsValueLine(fs[lineNumber++]));
                nasaData.Orbit_Type = SafeSetValue(IsValueLine(fs[lineNumber++]));
                lineNumber++;

                nasaData.World = SafeSetValue(IsValueLine(fs[lineNumber++]));
                nasaData.Use_Polyhedron_Gravity_if_ZERO = SafeSetValue(IsValueLine(fs[lineNumber++]));
                lineNumber++;

                nasaData.Region_Number = SafeSetValue(IsValueLine(fs[lineNumber++]));
                nasaData.Use_Polyhedron_Gravity_if_FLIGHT = SafeSetValue(IsValueLine(fs[lineNumber++]));
                lineNumber++;

                nasaData.Orbit_Center = SafeSetValue(IsValueLine(fs[lineNumber++]));
                nasaData.Secular_Orbit_Drift = SafeSetValue(IsValueLine(fs[lineNumber++]));
                nasaData.Use_Keplerian_elements = SafeSetValue(IsValueLine(fs[lineNumber++]));
                nasaData.Use_Peri_Apoapsis = SafeSetValue(IsValueLine(fs[lineNumber++]));
                nasaData.Periapsis_Apoapsis_Altitude = SafeSetValue(IsValueLine(fs[lineNumber++]));
                nasaData.Min_Altitude_Eccentricity = SafeSetValue(IsValueLine(fs[lineNumber++]));
                nasaData.Inclination = SafeSetValue(IsValueLine(fs[lineNumber++]));
                nasaData.RAAN = SafeSetValue(IsValueLine(fs[lineNumber++]));
                nasaData.Argument_of_Periapsis = SafeSetValue(IsValueLine(fs[lineNumber++]));
                nasaData.True_Anomaly = SafeSetValue(IsValueLine(fs[lineNumber++]));
                nasaData.RV_Initial_Position = SafeSetValue(IsValueLine(fs[lineNumber++]));
                nasaData.RV_Initial_Velocity = SafeSetValue(IsValueLine(fs[lineNumber++]));
                nasaData.TLE_or_TRV_format_Label_to_find_in_file_if_CENTRAL = SafeSetValue(IsValueLine(fs[lineNumber++]));
                nasaData.File_name_if_CENTRAL = SafeSetValue(IsValueLine(fs[lineNumber++]));
                lineNumber++;

                nasaData.Lagrange_system = SafeSetValue(IsValueLine(fs[lineNumber++]));
                nasaData.Propagate = SafeSetValue(IsValueLine(fs[lineNumber++]));
                nasaData.Initialize = SafeSetValue(IsValueLine(fs[lineNumber++]));
                nasaData.Libration_point = SafeSetValue(IsValueLine(fs[lineNumber++]));
                nasaData.XY_Semi_major_axis = SafeSetValue(IsValueLine(fs[lineNumber++]));
                nasaData.Initial_XY_Phase = SafeSetValue(IsValueLine(fs[lineNumber++]));
                nasaData.Sense_CW_CCW_viewed_from = SafeSetValue(IsValueLine(fs[lineNumber++]));
                nasaData.Second_XY_Mode_Semi_major_Axis_L4_L5_Only = SafeSetValue(IsValueLine(fs[lineNumber++]));
                nasaData.Second_XY_Mode_Initial_Phase_L4_L5_Only = SafeSetValue(IsValueLine(fs[lineNumber++]));
                nasaData.Sense_CW_CCW_viewed_from_L4_L5_Only = SafeSetValue(IsValueLine(fs[lineNumber++]));
                nasaData.Z_Semi_axis = SafeSetValue(IsValueLine(fs[lineNumber++]));
                nasaData.Initial_Z_Phase = SafeSetValue(IsValueLine(fs[lineNumber++]));
                nasaData.Initial_X_Y_Z = SafeSetValue(IsValueLine(fs[lineNumber++]));
                nasaData.Initial_Xdot_Ydot_Zdot = SafeSetValue(IsValueLine(fs[lineNumber++]));
                nasaData.TLE_or_TRV_format_Label_to_find_in_file_if_THREE_BODY = SafeSetValue(IsValueLine(fs[lineNumber++]));
                nasaData.File_name_if_THREE_BODY = SafeSetValue(IsValueLine(fs[lineNumber++]));
                lineNumber++;

                nasaData.Formation_Frame_Fixed = SafeSetValue(IsValueLine(fs[lineNumber++]));
                nasaData.Euler_Angles = SafeSetValue(IsValueLine(fs[lineNumber++]));
                nasaData.Formation_Origin_expressed = SafeSetValue(IsValueLine(fs[lineNumber++]));
                nasaData.Formation_Origin_wrt = SafeSetValue(IsValueLine(fs[lineNumber++]));


            }
            catch (ArgumentException ex)
            {
                LogManager.SharedInstance.Fatal(string.Empty, ex);
            }
            catch (Exception ex)
            {
                LogManager.SharedInstance.Fatal(string.Empty, ex);
            }

            return nasaData;
        }


        public NasaDataType_SC GetSCText(string[] fs)
        {
            int lineNumber = 1;
            int dupNum = 0;     // duplication Number 

            NasaDataType_SC nasaData = new NasaDataType_SC();

            try
            {
                nasaData.Decription = SafeSetValue(IsValueLine(fs[lineNumber++]));
                nasaData.Label = SafeSetValue(IsValueLine(fs[lineNumber++]));
                nasaData.Sprite_File_Name = SafeSetValue(IsValueLine(fs[lineNumber++]));
                nasaData.Flight_Software_Identifier = SafeSetValue(IsValueLine(fs[lineNumber++]));
                nasaData.FSW_Sample_Time = SafeSetValue(IsValueLine(fs[lineNumber++]));
                lineNumber++;

                nasaData.Orbit_Prop = SafeSetValue(IsValueLine(fs[lineNumber++]));
                nasaData.Pos = SafeSetValue(IsValueLine(fs[lineNumber++]));
                nasaData.Pos_wrt_Formation = SafeSetValue(IsValueLine(fs[lineNumber++]));
                nasaData.Vel_wrt_Formation = SafeSetValue(IsValueLine(fs[lineNumber++]));
                lineNumber++;

                nasaData.Ang_Vel_wrt = SafeSetValue(IsValueLine(fs[lineNumber++]));
                nasaData.Ang_Vel = SafeSetValue(IsValueLine(fs[lineNumber++]));
                nasaData.Quaternion = SafeSetValue(IsValueLine(fs[lineNumber++]));
                nasaData.Angles_Euler_Sequence = SafeSetValue(IsValueLine(fs[lineNumber++]));
                lineNumber++;

                nasaData.Rotation = SafeSetValue(IsValueLine(fs[lineNumber++]));
                nasaData.Passive_Joint_Enable = SafeSetValue(IsValueLine(fs[lineNumber++]));
                nasaData.Compute_Constraint_Forces = SafeSetValue(IsValueLine(fs[lineNumber++]));
                nasaData.Mass_Props_referenced = SafeSetValue(IsValueLine(fs[lineNumber++]));
                nasaData.Flex_Active = SafeSetValue(IsValueLine(fs[lineNumber++]));
                nasaData.Include_2nd_Order = SafeSetValue(IsValueLine(fs[lineNumber++]));
                nasaData.Drag_Coefficient = SafeSetValue(IsValueLine(fs[lineNumber++]));
                lineNumber += 3;

                dupNum = int.Parse(SafeSetValue(IsValueLine(fs[lineNumber++]))[0]);
                nasaData.Body_Parameters = new BodyParam[dupNum];
                lineNumber++;

                if (dupNum <= 0)
                {
                    lineNumber += typeof(BodyParam).GetFields().Length + 1;
                }

                for (int i = 0; i < dupNum; i++)
                {
                    nasaData.Body_Parameters[i] = new BodyParam();

                    nasaData.Body_Parameters[i].Mass = SafeSetValue(IsValueLine(fs[lineNumber++]));
                    nasaData.Body_Parameters[i].Moments_of_Inertia = SafeSetValue(IsValueLine(fs[lineNumber++]));
                    nasaData.Body_Parameters[i].Products_of_Inertia = SafeSetValue(IsValueLine(fs[lineNumber++]));
                    nasaData.Body_Parameters[i].Location_of_mass_center = SafeSetValue(IsValueLine(fs[lineNumber++]));
                    nasaData.Body_Parameters[i].Constant_Embedded_Momentum = SafeSetValue(IsValueLine(fs[lineNumber++]));
                    nasaData.Body_Parameters[i].Geometry_Input_File_Name = SafeSetValue(IsValueLine(fs[lineNumber++]));
                    nasaData.Body_Parameters[i].Flex_File_Name = SafeSetValue(IsValueLine(fs[lineNumber++]));

                    lineNumber++;
                }

                dupNum = dupNum - 1;
                nasaData.Joint_Parameters = new JointParam[dupNum];
                lineNumber += 4;

                if (dupNum <= 0)
                {
                    lineNumber += typeof(JointParam).GetFields().Length + 1;
                }

                for (int i = 0; i < dupNum; i++)
                {
                    nasaData.Joint_Parameters[i] = new JointParam();

                    nasaData.Joint_Parameters[i].Inner_outer_body_indices = SafeSetValue(IsValueLine(fs[lineNumber++]));
                    nasaData.Joint_Parameters[i].RotDOF_Seq = SafeSetValue(IsValueLine(fs[lineNumber++]));
                    nasaData.Joint_Parameters[i].TrnDOF_Seq = SafeSetValue(IsValueLine(fs[lineNumber++]));
                    nasaData.Joint_Parameters[i].RotDOF_Locked = SafeSetValue(IsValueLine(fs[lineNumber++]));
                    nasaData.Joint_Parameters[i].TrnDOF_Locked = SafeSetValue(IsValueLine(fs[lineNumber++]));
                    nasaData.Joint_Parameters[i].Initial_Angles = SafeSetValue(IsValueLine(fs[lineNumber++]));
                    nasaData.Joint_Parameters[i].Initial_Rates = SafeSetValue(IsValueLine(fs[lineNumber++]));
                    nasaData.Joint_Parameters[i].Initial_Displacements = SafeSetValue(IsValueLine(fs[lineNumber++]));
                    nasaData.Joint_Parameters[i].Initial_Displacements_Rates = SafeSetValue(IsValueLine(fs[lineNumber++]));
                    nasaData.Joint_Parameters[i].Bi_to_Gi_Static_Angles = SafeSetValue(IsValueLine(fs[lineNumber++]));
                    nasaData.Joint_Parameters[i].Go_to_Bo_Static_Angles = SafeSetValue(IsValueLine(fs[lineNumber++]));
                    nasaData.Joint_Parameters[i].Position_wrt_inner_body_origin = SafeSetValue(IsValueLine(fs[lineNumber++]));
                    nasaData.Joint_Parameters[i].Position_wrt_outer_body_origin = SafeSetValue(IsValueLine(fs[lineNumber++]));
                    nasaData.Joint_Parameters[i].Rot_Passive_Spring_Coefficients = SafeSetValue(IsValueLine(fs[lineNumber++]));
                    nasaData.Joint_Parameters[i].Rot_Passive_Damping_Coefficients = SafeSetValue(IsValueLine(fs[lineNumber++]));
                    nasaData.Joint_Parameters[i].Trn_Passive_Spring_Coefficients = SafeSetValue(IsValueLine(fs[lineNumber++]));
                    nasaData.Joint_Parameters[i].Trn_Passive_Damping_Coefficients = SafeSetValue(IsValueLine(fs[lineNumber++]));

                    lineNumber++;
                }

                dupNum = int.Parse(SafeSetValue(IsValueLine(fs[lineNumber++]))[0]);
                nasaData.Wheel_Parameters = new WheelParam[dupNum];
                lineNumber++;

                if (dupNum <= 0)
                {
                    lineNumber += typeof(WheelParam).GetFields().Length + 1;
                }

                for (int i = 0; i < dupNum; i++)
                {
                    nasaData.Wheel_Parameters[i] = new WheelParam();

                    nasaData.Wheel_Parameters[i].Initial_Momentum = SafeSetValue(IsValueLine(fs[lineNumber++]));
                    nasaData.Wheel_Parameters[i].Wheel_Axis_Components = SafeSetValue(IsValueLine(fs[lineNumber++]));
                    nasaData.Wheel_Parameters[i].Max_Torque = SafeSetValue(IsValueLine(fs[lineNumber++]));
                    nasaData.Wheel_Parameters[i].Wheel_Rotor_Inertia = SafeSetValue(IsValueLine(fs[lineNumber++]));
                    nasaData.Wheel_Parameters[i].Static_Imbalance = SafeSetValue(IsValueLine(fs[lineNumber++]));
                    nasaData.Wheel_Parameters[i].Dynamic_Imbalance = SafeSetValue(IsValueLine(fs[lineNumber++]));
                    nasaData.Wheel_Parameters[i].Flex_Node_Index = SafeSetValue(IsValueLine(fs[lineNumber++]));

                    lineNumber++;
                }

                dupNum = int.Parse(SafeSetValue(IsValueLine(fs[lineNumber++]))[0]);
                nasaData.MTB_Parameters = new MTBParam[dupNum];
                lineNumber++;

                if (dupNum <= 0)
                {
                    lineNumber += typeof(MTBParam).GetFields().Length + 1;
                }

                for (int i = 0; i < dupNum; i++)
                {
                    nasaData.MTB_Parameters[i] = new MTBParam();

                    nasaData.MTB_Parameters[i].Saturation = SafeSetValue(IsValueLine(fs[lineNumber++]));
                    nasaData.MTB_Parameters[i].MTB_Axis_Components = SafeSetValue(IsValueLine(fs[lineNumber++]));
                    nasaData.MTB_Parameters[i].Flex_Node_Index = SafeSetValue(IsValueLine(fs[lineNumber++]));

                    lineNumber++;
                }

                dupNum = int.Parse(SafeSetValue(IsValueLine(fs[lineNumber++]))[0]);
                nasaData.Thruster_Parameters = new ThrusterParam[dupNum];
                lineNumber++;

                if (dupNum <= 0)
                {
                    lineNumber += typeof(ThrusterParam).GetFields().Length + 1;
                }

                for (int i = 0; i < dupNum; i++)
                {
                    nasaData.Thruster_Parameters[i] = new ThrusterParam();

                    nasaData.Thruster_Parameters[i].Thrust_Force = SafeSetValue(IsValueLine(fs[lineNumber++]));
                    nasaData.Thruster_Parameters[i].Body = SafeSetValue(IsValueLine(fs[lineNumber++]));
                    nasaData.Thruster_Parameters[i].Location = SafeSetValue(IsValueLine(fs[lineNumber++]));
                    nasaData.Thruster_Parameters[i].Flex_Node_Index = SafeSetValue(IsValueLine(fs[lineNumber++]));

                    lineNumber++;
                }

                dupNum = int.Parse(SafeSetValue(IsValueLine(fs[lineNumber++]))[0]);
                nasaData.Gyro_Parameters = new GyroParam[dupNum];
                lineNumber++;

                if (dupNum <= 0)
                {
                    lineNumber += typeof(GyroParam).GetFields().Length + 1;
                }

                for (int i = 0; i < dupNum; i++)
                {
                    nasaData.Gyro_Parameters[i] = new GyroParam();

                    nasaData.Gyro_Parameters[i].Sample_Time = SafeSetValue(IsValueLine(fs[lineNumber++]));
                    nasaData.Gyro_Parameters[i].Axis_expressed_in_Body_Frame = SafeSetValue(IsValueLine(fs[lineNumber++]));
                    nasaData.Gyro_Parameters[i].Max_Rate = SafeSetValue(IsValueLine(fs[lineNumber++]));
                    nasaData.Gyro_Parameters[i].Scale_Factor_Error = SafeSetValue(IsValueLine(fs[lineNumber++]));
                    nasaData.Gyro_Parameters[i].Quantization = SafeSetValue(IsValueLine(fs[lineNumber++]));
                    nasaData.Gyro_Parameters[i].Angle_Random_Walk = SafeSetValue(IsValueLine(fs[lineNumber++]));
                    nasaData.Gyro_Parameters[i].Bias_Stability = SafeSetValue(IsValueLine(fs[lineNumber++]));
                    nasaData.Gyro_Parameters[i].Angle_Noise = SafeSetValue(IsValueLine(fs[lineNumber++]));
                    nasaData.Gyro_Parameters[i].Initial_Bias = SafeSetValue(IsValueLine(fs[lineNumber++]));
                    nasaData.Gyro_Parameters[i].Flex_Node_Index = SafeSetValue(IsValueLine(fs[lineNumber++]));

                    lineNumber++;
                }

                dupNum = int.Parse(SafeSetValue(IsValueLine(fs[lineNumber++]))[0]);
                nasaData.Magnetometer_Parameters = new MagnetometerParam[dupNum];
                lineNumber++;

                if (dupNum <= 0)
                {
                    lineNumber += typeof(MagnetometerParam).GetFields().Length + 1;
                }

                for (int i = 0; i < dupNum; i++)
                {
                    nasaData.Magnetometer_Parameters[i] = new MagnetometerParam();

                    nasaData.Magnetometer_Parameters[i].Sample_Time = SafeSetValue(IsValueLine(fs[lineNumber++]));
                    nasaData.Magnetometer_Parameters[i].Axis_expressed_in_Body_Frame = SafeSetValue(IsValueLine(fs[lineNumber++]));
                    nasaData.Magnetometer_Parameters[i].Saturation = SafeSetValue(IsValueLine(fs[lineNumber++]));
                    nasaData.Magnetometer_Parameters[i].Scale_Factor_Error = SafeSetValue(IsValueLine(fs[lineNumber++]));
                    nasaData.Magnetometer_Parameters[i].Quantization = SafeSetValue(IsValueLine(fs[lineNumber++]));
                    nasaData.Magnetometer_Parameters[i].Noise = SafeSetValue(IsValueLine(fs[lineNumber++]));
                    nasaData.Magnetometer_Parameters[i].Flex_Node_Index = SafeSetValue(IsValueLine(fs[lineNumber++]));

                    lineNumber++;
                }

                dupNum = int.Parse(SafeSetValue(IsValueLine(fs[lineNumber++]))[0]);
                nasaData.CSS_Parameters = new CSSParam[dupNum];
                lineNumber++;

                if (dupNum <= 0)
                {
                    lineNumber += typeof(CSSParam).GetFields().Length + 1;
                }

                for (int i = 0; i < dupNum; i++)
                {
                    nasaData.CSS_Parameters[i] = new CSSParam();

                    nasaData.CSS_Parameters[i].Sample_Time = SafeSetValue(IsValueLine(fs[lineNumber++]));
                    nasaData.CSS_Parameters[i].Axis_expressed_in_Body_Frame = SafeSetValue(IsValueLine(fs[lineNumber++]));
                    nasaData.CSS_Parameters[i].Half_cone_Angle = SafeSetValue(IsValueLine(fs[lineNumber++]));
                    nasaData.CSS_Parameters[i].Scale_Factor = SafeSetValue(IsValueLine(fs[lineNumber++]));
                    nasaData.CSS_Parameters[i].Quantization = SafeSetValue(IsValueLine(fs[lineNumber++]));
                    nasaData.CSS_Parameters[i].Flex_Node_Index = SafeSetValue(IsValueLine(fs[lineNumber++]));

                    lineNumber++;
                }

                dupNum = int.Parse(SafeSetValue(IsValueLine(fs[lineNumber++]))[0]);
                nasaData.FSS_Parameters = new FSSParam[dupNum];
                lineNumber++;

                if (dupNum <= 0)
                {
                    lineNumber += typeof(FSSParam).GetFields().Length + 1;
                }

                for (int i = 0; i < dupNum; i++)
                {
                    nasaData.FSS_Parameters[i] = new FSSParam();

                    nasaData.FSS_Parameters[i].Sample_Time = SafeSetValue(IsValueLine(fs[lineNumber++]));
                    nasaData.FSS_Parameters[i].Mounting_Angles = SafeSetValue(IsValueLine(fs[lineNumber++]));
                    nasaData.FSS_Parameters[i].FOV_Size = SafeSetValue(IsValueLine(fs[lineNumber++]));
                    nasaData.FSS_Parameters[i].Noise_Equivalent_Angle = SafeSetValue(IsValueLine(fs[lineNumber++]));
                    nasaData.FSS_Parameters[i].Quantization = SafeSetValue(IsValueLine(fs[lineNumber++]));
                    nasaData.FSS_Parameters[i].Flex_Node_Index = SafeSetValue(IsValueLine(fs[lineNumber++]));

                    lineNumber++;
                }

                dupNum = int.Parse(SafeSetValue(IsValueLine(fs[lineNumber++]))[0]);
                nasaData.StarTracker_Parameters = new StarTrackerParam[dupNum];
                lineNumber++;

                if (dupNum <= 0)
                {
                    lineNumber += typeof(StarTrackerParam).GetFields().Length + 1;
                }

                for (int i = 0; i < dupNum; i++)
                {
                    nasaData.StarTracker_Parameters[i] = new StarTrackerParam();

                    nasaData.StarTracker_Parameters[i].Sample_Time = SafeSetValue(IsValueLine(fs[lineNumber++]));
                    nasaData.StarTracker_Parameters[i].Mounting_Angles = SafeSetValue(IsValueLine(fs[lineNumber++]));
                    nasaData.StarTracker_Parameters[i].FOV_Size = SafeSetValue(IsValueLine(fs[lineNumber++]));
                    nasaData.StarTracker_Parameters[i].Exclusion_Angles = SafeSetValue(IsValueLine(fs[lineNumber++]));
                    nasaData.StarTracker_Parameters[i].Noise_Equivalent_Angle = SafeSetValue(IsValueLine(fs[lineNumber++]));
                    nasaData.StarTracker_Parameters[i].Flex_Node_Index = SafeSetValue(IsValueLine(fs[lineNumber++]));

                    lineNumber++;
                }

                dupNum = int.Parse(SafeSetValue(IsValueLine(fs[lineNumber++]))[0]);
                nasaData.GPS_Parameters = new GPSParam[dupNum];
                lineNumber++;

                if (dupNum <= 0)
                {
                    lineNumber += typeof(GPSParam).GetFields().Length + 1;
                }

                for (int i = 0; i < dupNum; i++)
                {
                    nasaData.GPS_Parameters[i] = new GPSParam();

                    nasaData.GPS_Parameters[i].Sample_Time = SafeSetValue(IsValueLine(fs[lineNumber++]));
                    nasaData.GPS_Parameters[i].Position_Noise = SafeSetValue(IsValueLine(fs[lineNumber++]));
                    nasaData.GPS_Parameters[i].Velocity_Noise = SafeSetValue(IsValueLine(fs[lineNumber++]));
                    nasaData.GPS_Parameters[i].Time_Noise = SafeSetValue(IsValueLine(fs[lineNumber++]));
                    nasaData.GPS_Parameters[i].Flex_Node_Index = SafeSetValue(IsValueLine(fs[lineNumber++]));

                    lineNumber++;
                }

                dupNum = int.Parse(SafeSetValue(IsValueLine(fs[lineNumber++]))[0]);
                nasaData.Accelerometer_Parameters = new AccelerometerParam[dupNum];
                lineNumber++;

                for (int i = 0; i < dupNum; i++)
                {
                    nasaData.Accelerometer_Parameters[i] = new AccelerometerParam();

                    nasaData.Accelerometer_Parameters[i].Sample_Time = SafeSetValue(IsValueLine(fs[lineNumber++]));
                    nasaData.Accelerometer_Parameters[i].Position = SafeSetValue(IsValueLine(fs[lineNumber++]));
                    nasaData.Accelerometer_Parameters[i].Axis_expressed_in_Body_Frame = SafeSetValue(IsValueLine(fs[lineNumber++]));
                    nasaData.Accelerometer_Parameters[i].Max_Acceleration = SafeSetValue(IsValueLine(fs[lineNumber++]));
                    nasaData.Accelerometer_Parameters[i].Scale_Factor_Error = SafeSetValue(IsValueLine(fs[lineNumber++]));
                    nasaData.Accelerometer_Parameters[i].Quantization = SafeSetValue(IsValueLine(fs[lineNumber++]));
                    nasaData.Accelerometer_Parameters[i].DV_Random_Walk = SafeSetValue(IsValueLine(fs[lineNumber++]));
                    nasaData.Accelerometer_Parameters[i].Bias_Stability = SafeSetValue(IsValueLine(fs[lineNumber++]));
                    nasaData.Accelerometer_Parameters[i].DV_Noise = SafeSetValue(IsValueLine(fs[lineNumber++]));
                    nasaData.Accelerometer_Parameters[i].Initial_Bias = SafeSetValue(IsValueLine(fs[lineNumber++]));
                    nasaData.Accelerometer_Parameters[i].Flex_Node_Index = SafeSetValue(IsValueLine(fs[lineNumber++]));

                    lineNumber++;
                }

            }
            catch (ArgumentException ex)
            {
                LogManager.SharedInstance.Fatal(string.Empty, ex);
            }
            catch (Exception ex)
            {
                LogManager.SharedInstance.Fatal(string.Empty, ex);
            }

            return nasaData;
        }


        public NasaDataType_SIM GetSimText(string[] fs)
        {
            int lineNumber = 1;
            int dupNum = 0;     // duplication Number 

            NasaDataType_SIM nasaData = new NasaDataType_SIM();

            try
            {
                lineNumber++;

                nasaData.Time_Mode = SafeSetValue(IsValueLine(fs[lineNumber++]));
                nasaData.Sim_Duration_Step_Size = SafeSetValue(IsValueLine(fs[lineNumber++]));
                nasaData.File_Output_Interval = SafeSetValue(IsValueLine(fs[lineNumber++]));
                nasaData.Graphics_Front_End = SafeSetValue(IsValueLine(fs[lineNumber++]));
                nasaData.Command_Script_File_Name = SafeSetValue(IsValueLine(fs[lineNumber++]));
                lineNumber++;

                nasaData.Number_of_Reference_Orbits = SafeSetValue(IsValueLine(fs[lineNumber++]));
                nasaData.Existence_Input_file_name_for_Orb_0 = SafeSetValue(IsValueLine(fs[lineNumber++]));
                lineNumber++;

                nasaData.Number_of_Spacecraft = SafeSetValue(IsValueLine(fs[lineNumber++]));
                nasaData.Existence_Input_file_name_for_SC_0 = SafeSetValue(IsValueLine(fs[lineNumber++]));
                lineNumber++;

                nasaData.Date = SafeSetValue(IsValueLine(fs[lineNumber++]));
                nasaData.Time = SafeSetValue(IsValueLine(fs[lineNumber++]));
                nasaData.Leap_Seconds = SafeSetValue(IsValueLine(fs[lineNumber++]));
                nasaData.F10_7_Ap = SafeSetValue(IsValueLine(fs[lineNumber++]));
                nasaData.enter_desired_F10_7_value = SafeSetValue(IsValueLine(fs[lineNumber++]));
                nasaData.enter_desired_AP_value = SafeSetValue(IsValueLine(fs[lineNumber++]));
                nasaData.Magfield = SafeSetValue(IsValueLine(fs[lineNumber++]));
                nasaData.IGRF_Degree_and_Order = SafeSetValue(IsValueLine(fs[lineNumber++]));
                nasaData.Earth_Gravity = SafeSetValue(IsValueLine(fs[lineNumber++]));
                nasaData.Mars_Gravity = SafeSetValue(IsValueLine(fs[lineNumber++]));
                nasaData.Luna_Gravity = SafeSetValue(IsValueLine(fs[lineNumber++]));
                nasaData.Aerodynamic_Forces = SafeSetValue(IsValueLine(fs[lineNumber++]));
                nasaData.Gravity_Gradient_Torques = SafeSetValue(IsValueLine(fs[lineNumber++]));
                nasaData.Solar_Pressure_Forces = SafeSetValue(IsValueLine(fs[lineNumber++]));
                nasaData.Gravity_Perturbation_Forces = SafeSetValue(IsValueLine(fs[lineNumber++]));
                nasaData.Passive_Joint_Forces = SafeSetValue(IsValueLine(fs[lineNumber++]));
                nasaData.Thruster_Plume_Forces = SafeSetValue(IsValueLine(fs[lineNumber++]));
                nasaData.RWA_Imbalance_Forces = SafeSetValue(IsValueLine(fs[lineNumber++]));
                nasaData.Contact_Forces = SafeSetValue(IsValueLine(fs[lineNumber++]));
                nasaData.CFD_Slosh_Forces = SafeSetValue(IsValueLine(fs[lineNumber++]));
                nasaData.Output_Environmental_Torques = SafeSetValue(IsValueLine(fs[lineNumber++]));
                lineNumber++;

                nasaData.Ephem_Option = SafeSetValue(IsValueLine(fs[lineNumber++]));
                nasaData.Mercury = SafeSetValue(IsValueLine(fs[lineNumber++]));
                nasaData.Venus = SafeSetValue(IsValueLine(fs[lineNumber++]));
                nasaData.Earth_and_Luna = SafeSetValue(IsValueLine(fs[lineNumber++]));
                nasaData.Mars_and_its_moons = SafeSetValue(IsValueLine(fs[lineNumber++]));
                nasaData.Jupiter_and_its_moons = SafeSetValue(IsValueLine(fs[lineNumber++]));
                nasaData.Saturn_and_its_moons = SafeSetValue(IsValueLine(fs[lineNumber++]));
                nasaData.Uranus_and_its_moons = SafeSetValue(IsValueLine(fs[lineNumber++]));
                nasaData.Neptune_and_its_moons = SafeSetValue(IsValueLine(fs[lineNumber++]));
                nasaData.Pluto_and_its_moons = SafeSetValue(IsValueLine(fs[lineNumber++]));
                nasaData.Asteroids_and_Comets = SafeSetValue(IsValueLine(fs[lineNumber++]));
                lineNumber++;

                nasaData.Earth_Moon = SafeSetValue(IsValueLine(fs[lineNumber++]));
                nasaData.Sun_Earth = SafeSetValue(IsValueLine(fs[lineNumber++]));
                nasaData.Sun_Jupiter = SafeSetValue(IsValueLine(fs[lineNumber++]));
                lineNumber++;

                dupNum = int.Parse(SafeSetValue(IsValueLine(fs[lineNumber++]))[0]);
                nasaData.GroundStation_Parameters = new GroundStationParam[dupNum];

                for (int i = 0; i < dupNum; i++)
                {
                    nasaData.GroundStation_Parameters[i] = new GroundStationParam();

                    nasaData.GroundStation_Parameters[i].Exists_World_Lng_Lat_Label = SafeSetValue(IsValueLine(fs[lineNumber++]));

                }

            }
            catch (ArgumentException ex)
            {
                LogManager.SharedInstance.Fatal(string.Empty, ex);
            }
            catch (Exception ex)
            {
                LogManager.SharedInstance.Fatal(string.Empty, ex);
            }

            return nasaData;
        }


        public string[] SafeSetValue((bool, string, string)input)
        {
            if(input.Item1)
            {
                return SafeValueSplit(input.Item2);
            }
            else
            {
                throw new ArgumentException($"SafeSetValue {input.Item1}, {input.Item2}, {input.Item3}");
            }
        }

        private readonly char[] charSeparators = { ' ' };
        public string[] SafeValueSplit(string input)
        {
            try
            {
                string[] result = input.Split(charSeparators, StringSplitOptions.RemoveEmptyEntries);

                return result;
            }
            catch (Exception ex)
            {
                LogManager.SharedInstance.Fatal(string.Empty, ex);

                return null;
            }
        }
        #endregion
    }
}