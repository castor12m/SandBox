using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SandBox._1_MVVM.Text_MV_NasaConfigEdit
{
    ///////////////////////////////////////////
    class NasaDataType_CMD
    {
        public string[] Commands = null;
    }

    ///////////////////////////////////////////
    class NasaDataType_IPC
    {
        public SocketParam[] Socket_Parameters = null;
    }

    class SocketParam
    {
        public string[] IPC_Mode = null;
        public string[] ACS_mode = null;
        public string[] File_name = null;
        public string[] Socket_Role = null;
        public string[] Server_Host = null;
        public string[] Allow_Blocking = null;
        public string[] Echo_to_stdout = null;

        public PrefixParam[] Prefixes_Parameters = null;
    }

    class PrefixParam
    {
        public string[] Prefix = null;
    }


    ///////////////////////////////////////////
    class NasaDataType_ORB
    {
        public string[] Description = null;
        public string[] Orbit_Type = null;

        public string[] World = null;
        public string[] Use_Polyhedron_Gravity_if_ZERO = null;

        public string[] Region_Number = null;
        public string[] Use_Polyhedron_Gravity_if_FLIGHT = null;

        public string[] Orbit_Center = null;
        public string[] Secular_Orbit_Drift = null;
        public string[] Use_Keplerian_elements = null;
        public string[] Use_Peri_Apoapsis = null;
        public string[] Periapsis_Apoapsis_Altitude = null;
        public string[] Min_Altitude_Eccentricity = null;
        public string[] Inclination = null;
        public string[] RAAN = null;
        public string[] Argument_of_Periapsis = null;
        public string[] True_Anomaly = null;
        public string[] RV_Initial_Position = null;
        public string[] RV_Initial_Velocity = null;
        public string[] TLE_or_TRV_format_Label_to_find_in_file_if_CENTRAL = null;
        public string[] File_name_if_CENTRAL = null;

        public string[] Lagrange_system = null;
        public string[] Propagate = null;
        public string[] Initialize = null;
        public string[] Libration_point = null;
        public string[] XY_Semi_major_axis = null;
        public string[] Initial_XY_Phase = null;
        public string[] Sense_CW_CCW_viewed_from = null;
        public string[] Second_XY_Mode_Semi_major_Axis_L4_L5_Only = null;
        public string[] Second_XY_Mode_Initial_Phase_L4_L5_Only = null;
        public string[] Sense_CW_CCW_viewed_from_L4_L5_Only = null;
        public string[] Z_Semi_axis = null;
        public string[] Initial_Z_Phase = null;
        public string[] Initial_X_Y_Z = null;
        public string[] Initial_Xdot_Ydot_Zdot = null;
        public string[] TLE_or_TRV_format_Label_to_find_in_file_if_THREE_BODY = null;
        public string[] File_name_if_THREE_BODY = null;

        public string[] Formation_Frame_Fixed = null;
        public string[] Euler_Angles = null;
        public string[] Formation_Origin_expressed = null;
        public string[] Formation_Origin_wrt = null;

    }

    ///////////////////////////////////////////

    class NasaDataType_SC
    {
        public string[] Decription = null;
        public string[] Label = null;
        public string[] Sprite_File_Name = null;
        public string[] Flight_Software_Identifier = null;
        public string[] FSW_Sample_Time = null;

        public string[] Orbit_Prop = null;
        public string[] Pos = null;
        public string[] Pos_wrt_Formation = null;
        public string[] Vel_wrt_Formation = null;

        public string[] Ang_Vel_wrt = null;
        public string[] Ang_Vel = null;
        public string[] Quaternion = new string[4];
        public string[] Angles_Euler_Sequence = new string[4];

        public string[] Rotation = null;
        public string[] Passive_Joint_Enable = null;
        public string[] Compute_Constraint_Forces = null;
        public string[] Mass_Props_referenced = null;
        public string[] Flex_Active = null;
        public string[] Include_2nd_Order = null;
        public string[] Drag_Coefficient = null;

        public BodyParam[] Body_Parameters = null;
        public JointParam[] Joint_Parameters = null;
        public WheelParam[] Wheel_Parameters = null;
        public MTBParam[] MTB_Parameters = null;
        public ThrusterParam[] Thruster_Parameters = null;
        public GyroParam[] Gyro_Parameters = null;
        public MagnetometerParam[] Magnetometer_Parameters = null;
        public CSSParam[] CSS_Parameters = null;
        public FSSParam[] FSS_Parameters = null;
        public StarTrackerParam[] StarTracker_Parameters = null;

        public GPSParam[] GPS_Parameters = null;
        public AccelerometerParam[] Accelerometer_Parameters = null;

    }

    class BodyParam
    {
        public string[] Mass = null;
        public string[] Moments_of_Inertia = null;
        public string[] Products_of_Inertia = null;
        public string[] Location_of_mass_center = null;
        public string[] Constant_Embedded_Momentum = null;
        public string[] Geometry_Input_File_Name = null;
        public string[] Flex_File_Name = null;
    }

    class JointParam
    {
        public string[] Inner_outer_body_indices = null;
        public string[] RotDOF_Seq = null;
        public string[] TrnDOF_Seq = null;
        public string[] RotDOF_Locked = null;
        public string[] TrnDOF_Locked = null;
        public string[] Initial_Angles = null;
        public string[] Initial_Rates = null;
        public string[] Initial_Displacements = null;
        public string[] Initial_Displacements_Rates = null;
        public string[] Bi_to_Gi_Static_Angles = null;
        public string[] Go_to_Bo_Static_Angles = null;
        public string[] Position_wrt_inner_body_origin = null;
        public string[] Position_wrt_outer_body_origin = null;
        public string[] Rot_Passive_Spring_Coefficients = null;
        public string[] Rot_Passive_Damping_Coefficients = null;
        public string[] Trn_Passive_Spring_Coefficients = null;
        public string[] Trn_Passive_Damping_Coefficients = null;
    }

    class WheelParam
    {
        public string[] Initial_Momentum = null;
        public string[] Wheel_Axis_Components = null;
        public string[] Max_Torque = null;
        public string[] Wheel_Rotor_Inertia = null;
        public string[] Static_Imbalance = null;
        public string[] Dynamic_Imbalance = null;
        public string[] Flex_Node_Index = null;
    }

    class MTBParam
    {
        public string[] Saturation = null;
        public string[] MTB_Axis_Components = null;
        public string[] Flex_Node_Index = null;
    }

    class ThrusterParam
    {
        public string[] Thrust_Force = null;
        public string[] Body = null;
        public string[] Location = null;
        public string[] Flex_Node_Index = null;
    }

    class GyroParam
    {
        public string[] Sample_Time = null;
        public string[] Axis_expressed_in_Body_Frame = null;
        public string[] Max_Rate = null;
        public string[] Scale_Factor_Error = null;
        public string[] Quantization = null;
        public string[] Angle_Random_Walk = null;
        public string[] Bias_Stability = null;
        public string[] Angle_Noise = null;
        public string[] Initial_Bias = null;
        public string[] Flex_Node_Index = null;
    }

    class MagnetometerParam
    {
        public string[] Sample_Time = null;
        public string[] Axis_expressed_in_Body_Frame = null;
        public string[] Saturation = null;
        public string[] Scale_Factor_Error = null;
        public string[] Quantization = null;
        public string[] Noise = null;
        public string[] Flex_Node_Index = null;
    }

    class CSSParam
    {
        public string[] Sample_Time = null;
        public string[] Axis_expressed_in_Body_Frame = null;
        public string[] Half_cone_Angle = null;
        public string[] Scale_Factor = null;
        public string[] Quantization = null;
        public string[] Flex_Node_Index = null;
    }

    class FSSParam
    {
        public string[] Sample_Time = null;
        public string[] Mounting_Angles = null;
        public string[] FOV_Size = null;
        public string[] Noise_Equivalent_Angle = null;
        public string[] Quantization = null;
        public string[] Flex_Node_Index = null;
    }

    class StarTrackerParam
    {
        public string[] Sample_Time = null;
        public string[] Mounting_Angles = null;
        public string[] FOV_Size = null;
        public string[] Exclusion_Angles = null;
        public string[] Noise_Equivalent_Angle = null;
        public string[] Flex_Node_Index = null;
    }

    class GPSParam
    {
        public string[] Sample_Time = null;
        public string[] Position_Noise = null;
        public string[] Velocity_Noise = null;
        public string[] Time_Noise = null;
        public string[] Flex_Node_Index = null;
    }

    class AccelerometerParam
    {
        public string[] Sample_Time = null;
        public string[] Position = null;
        public string[] Axis_expressed_in_Body_Frame = null;
        public string[] Max_Acceleration = null;
        public string[] Scale_Factor_Error = null;
        public string[] Quantization = null;
        public string[] DV_Random_Walk = null;
        public string[] Bias_Stability = null;
        public string[] DV_Noise = null;
        public string[] Initial_Bias = null;
        public string[] Flex_Node_Index = null;
    }

    ///////////////////////////////////////////
    class NasaDataType_SIM
    {
        public string[] Time_Mode = null;
        public string[] Sim_Duration_Step_Size = null;
        public string[] File_Output_Interval = null;
        public string[] Graphics_Front_End = null;
        public string[] Command_Script_File_Name = null;

        public string[] Number_of_Reference_Orbits = null;
        public string[] Existence_Input_file_name_for_Orb_0 = null;

        public string[] Number_of_Spacecraft = null;
        public string[] Existence_Input_file_name_for_SC_0 = null;

        public string[] Date = null;
        public string[] Time = null;
        public string[] Leap_Seconds = null;
        public string[] F10_7_Ap = null;
        public string[] enter_desired_F10_7_value = null;
        public string[] enter_desired_AP_value = null;
        public string[] Magfield = null;
        public string[] IGRF_Degree_and_Order = null;
        public string[] Earth_Gravity = null;
        public string[] Mars_Gravity = null;
        public string[] Luna_Gravity = null;
        public string[] Aerodynamic_Forces = null;
        public string[] Gravity_Gradient_Torques = null;
        public string[] Solar_Pressure_Forces = null;
        public string[] Gravity_Perturbation_Forces = null;
        public string[] Passive_Joint_Forces = null;
        public string[] Thruster_Plume_Forces = null;
        public string[] RWA_Imbalance_Forces = null;
        public string[] Contact_Forces = null;
        public string[] CFD_Slosh_Forces = null;
        public string[] Output_Environmental_Torques = null;

        public string[] Ephem_Option = null;
        public string[] Mercury = null;
        public string[] Venus = null;
        public string[] Earth_and_Luna = null;
        public string[] Mars_and_its_moons = null;
        public string[] Jupiter_and_its_moons = null;
        public string[] Saturn_and_its_moons = null;
        public string[] Uranus_and_its_moons = null;
        public string[] Neptune_and_its_moons = null;
        public string[] Pluto_and_its_moons = null;
        public string[] Asteroids_and_Comets = null;

        public string[] Earth_Moon = null;
        public string[] Sun_Earth = null;
        public string[] Sun_Jupiter = null;

        public GroundStationParam[] GroundStation_Parameters = null;

    }

    class GroundStationParam
    {
        public string[] Exists_World_Lng_Lat_Label = null;
    }

}

