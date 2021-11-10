using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SandBox._1_MVVM.Text_MV_NasaConfigEdit
{
    class Test_MV_NasaConfigEdit
    {
    }

    public class NasaConfigManager
    {
        #region 접근자
        private static NasaConfigManager _instance = null;
        public static NasaConfigManager SharedInstance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new NasaConfigManager();
                }

                return _instance;
            }
        }
        #endregion

        #region 맴버변수
        private NasaFileTreeModel[] dicTreeSim = new NasaFileTreeModel[]
        {
            new NasaFileTreeModel(sIndex : 0, values : new string[] { "42: The Mostly Harmless Simulator", "<", ">" }),

            new NasaFileTreeModel(sIndex : 1, values : new string[] { "Simulation Control", "*" }),
            new NasaFileTreeModel(sIndex : 1, pIndex : 1, desription: "Time Mode (FAST, REAL, or EXTERNAL)", types: new Type[]{ typeof(string) }),
            new NasaFileTreeModel(sIndex : 1, pIndex : 2, desription: "Sim Duration, Step Size [sec]", types: new Type[]{ typeof(double), typeof(double) }),
            new NasaFileTreeModel(sIndex : 1, pIndex : 3, desription: "File Output Interval [sec]", types: new Type[]{ typeof(double) }),
            new NasaFileTreeModel(sIndex : 1, pIndex : 4, desription: "Graphics Front End?", types: new Type[]{ typeof(bool) }),
            new NasaFileTreeModel(sIndex : 1, pIndex : 5, desription: "Command Script File Name", types: new Type[]{ typeof(string) }),

            new NasaFileTreeModel(sIndex : 2, values : new string[] { "Reference Orbits", "*"  }),
            new NasaFileTreeModel(sIndex : 2, pIndex : 1, desription: "Number of Reference Orbits", types: new Type[]{ typeof(uint) }),
            new NasaFileTreeModel(sIndex : 2, pIndex : 2, desription: "Input file name for Orb 0", types: new Type[]{ typeof(bool), typeof(string) }),

            new NasaFileTreeModel(sIndex : 3, values : new string[] { "Spacecraft", "*"  }),
            new NasaFileTreeModel(sIndex : 3, pIndex : 1, desription: "Number of Spacecraft", types: new Type[]{ typeof(uint) }),
            new NasaFileTreeModel(sIndex : 3, pIndex : 2, desription: "Existence, RefOrb, Input file for SC 0", types: new Type[]{ typeof(bool), typeof(uint), typeof(string) }),

            new NasaFileTreeModel(sIndex : 4, values : new string[] { "Environment", "*"  }),
            new NasaFileTreeModel(sIndex : 4, pIndex : 1, desription: "Date (UTC) (Month, Day, Year)", types: new Type[]{ typeof(uint), typeof(uint), typeof(uint)}),
            new NasaFileTreeModel(sIndex : 4, pIndex : 2, desription: "Time (UTC) (Hr,Min,Sec)", types: new Type[]{ typeof(uint), typeof(uint), typeof(double) }),
            new NasaFileTreeModel(sIndex : 4, pIndex : 3, desription: "Leap Seconds (sec)", types: new Type[]{ typeof(double) }),
            new NasaFileTreeModel(sIndex : 4, pIndex : 4, desription: "F10.7, Ap (USER, NOMINAL or TWOSIGMA)ex", types: new Type[]{ typeof(string) }),
            new NasaFileTreeModel(sIndex : 4, pIndex : 5, desription: "If USER_DEFINED, enter desired F10.7 value", types: new Type[]{ typeof(double) }),
            new NasaFileTreeModel(sIndex : 4, pIndex : 6, desription: "If USER_DEFINED, enter desired AP value", types: new Type[]{ typeof(double) }),
            new NasaFileTreeModel(sIndex : 4, pIndex : 7, desription: "Magfield (NONE,DIPOLE,IGRF)", types: new Type[]{ typeof(string) }),
            new NasaFileTreeModel(sIndex : 4, pIndex : 8, desription: "IGRF Degree and Order (<=10)", types: new Type[]{ typeof(uint), typeof(uint) }),
            new NasaFileTreeModel(sIndex : 4, pIndex : 9, desription: "Earth Gravity Model N and M (<=18)", types: new Type[]{ typeof(uint), typeof(uint) }),
            new NasaFileTreeModel(sIndex : 4, pIndex : 10, desription: "Mars Gravity Model N and M (<=18)", types: new Type[]{ typeof(uint), typeof(uint) }),
            new NasaFileTreeModel(sIndex : 4, pIndex : 11, desription: "Luna Gravity Model N and M (<=18)", types: new Type[]{ typeof(uint), typeof(uint) }),
            new NasaFileTreeModel(sIndex : 4, pIndex : 12, desription: "Aerodynamic Forces & Torques (Shadows)", types: new Type[]{ typeof(bool), typeof(bool) }),
            new NasaFileTreeModel(sIndex : 4, pIndex : 13, desription: "Gravity Gradient Torques", types: new Type[]{ typeof(bool) }),
            new NasaFileTreeModel(sIndex : 4, pIndex : 14, desription: "Solar Pressure Forces & Torques (Shadows)", types: new Type[]{ typeof(bool), typeof(bool) }),
            new NasaFileTreeModel(sIndex : 4, pIndex : 15, desription: "Gravity Perturbation Forces", types: new Type[]{ typeof(bool) }),
            new NasaFileTreeModel(sIndex : 4, pIndex : 16, desription: "Passive Joint Forces & Torques", types: new Type[]{ typeof(bool) }),
            new NasaFileTreeModel(sIndex : 4, pIndex : 17, desription: "Thruster Plume Forces & Torques", types: new Type[]{ typeof(bool) }),
            new NasaFileTreeModel(sIndex : 4, pIndex : 18, desription: "RWA Imbalance Forces and Torques", types: new Type[]{ typeof(bool) }),
            new NasaFileTreeModel(sIndex : 4, pIndex : 19, desription: "Contact Forces and Torques", types: new Type[]{ typeof(bool) }),
            new NasaFileTreeModel(sIndex : 4, pIndex : 20, desription: "CFD Slosh Forces and Torques", types: new Type[]{ typeof(bool) }),
            new NasaFileTreeModel(sIndex : 4, pIndex : 21, desription: "Output Environmental Torques to Files", types: new Type[]{ typeof(bool) }),

            new NasaFileTreeModel(sIndex : 5, values : new string[] { "Celestial Bodies of Interest", "*"  }),
            new NasaFileTreeModel(sIndex : 5, pIndex : 1, desription: "Ephem Option (MEAN or DE430)", types: new Type[]{ typeof(string) }),
            new NasaFileTreeModel(sIndex : 5, pIndex : 2, desription: "Mercury", types: new Type[]{ typeof(bool) }),
            new NasaFileTreeModel(sIndex : 5, pIndex : 3, desription: "Venus", types: new Type[]{ typeof(bool) }),
            new NasaFileTreeModel(sIndex : 5, pIndex : 4, desription: "Earth and Luna", types: new Type[]{ typeof(bool) }),
            new NasaFileTreeModel(sIndex : 5, pIndex : 5, desription: "Mars and its moons", types: new Type[]{ typeof(bool) }),
            new NasaFileTreeModel(sIndex : 5, pIndex : 6, desription: "Jupiter and its moons", types: new Type[]{ typeof(bool) }),
            new NasaFileTreeModel(sIndex : 5, pIndex : 7, desription: "Saturn and its moons", types: new Type[]{ typeof(bool) }),
            new NasaFileTreeModel(sIndex : 5, pIndex : 8, desription: "Uranus and its moons", types: new Type[]{ typeof(bool) }),
            new NasaFileTreeModel(sIndex : 5, pIndex : 9, desription: "Neptune and its moons", types: new Type[]{ typeof(bool) }),
            new NasaFileTreeModel(sIndex : 5, pIndex : 10, desription: "Pluto and its moons", types: new Type[]{ typeof(bool) }),
            new NasaFileTreeModel(sIndex : 5, pIndex : 11, desription: "Asteroids and Comets", types: new Type[]{ typeof(bool) }),

            new NasaFileTreeModel(sIndex : 6, values : new string[] { "Lagrange Point Systems of Interest", "*"  }),
            new NasaFileTreeModel(sIndex : 6, pIndex : 1, desription: "Earth-Moon", types: new Type[]{ typeof(bool) }),
            new NasaFileTreeModel(sIndex : 6, pIndex : 2, desription: "Sun-Earth", types: new Type[]{ typeof(bool) }),
            new NasaFileTreeModel(sIndex : 6, pIndex : 3, desription: "Sun-Jupiter", types: new Type[]{ typeof(bool) }),

            new NasaFileTreeModel(sIndex : 7, values : new string[] { "Ground Stations", "*"  }),
            new NasaFileTreeModel(sIndex : 7, pIndex : 1, desription: "Number of Ground Stations", types: new Type[]{ typeof(uint) }),
            new NasaFileTreeModel(sIndex : 7, pIndex : 2, desription: "Exists, World, Lng, Lat, Label", types: new Type[]{ typeof(bool), typeof(string), typeof(double), typeof(string), typeof(string) }),

        };

        private NasaFileTreeModel[] dicTreeOrb = new NasaFileTreeModel[]
        {
            new NasaFileTreeModel(sIndex : 0, values : new string[] { "42: Orbit Description File", "<", ">" }),
            new NasaFileTreeModel(sIndex : 0, pIndex : 1, desription: "Description", types: new Type[]{ typeof(string) }),
            new NasaFileTreeModel(sIndex : 0, pIndex : 2, desription: "Orbit Type (ZERO, FLIGHT, CENTRAL, THREE_BODY)", types: new Type[]{ typeof(string) }),

            new NasaFileTreeModel(sIndex : 1, values : new string[] { "Use these lines if ZERO", "*"  }),
            new NasaFileTreeModel(sIndex : 1, pIndex : 1, desription: "World", types: new Type[]{ typeof(string) }),
            new NasaFileTreeModel(sIndex : 1, pIndex : 2, desription: "Use Polyhedron Gravity", types: new Type[]{ typeof(bool) }),

            new NasaFileTreeModel(sIndex : 2, values : new string[] { "Use these lines if FLIGHT", "*"  }),
            new NasaFileTreeModel(sIndex : 2, pIndex : 1, desription: "Region Number", types: new Type[]{ typeof(uint) }),
            new NasaFileTreeModel(sIndex : 2, pIndex : 2, desription: "Use Polyhedron Gravity", types: new Type[]{ typeof(bool) }),

            new NasaFileTreeModel(sIndex : 3, values : new string[] { "Use these lines if CENTRAL", "*"  }),
            new NasaFileTreeModel(sIndex : 3, pIndex : 1, desription: "Orbit Center", types: new Type[]{ typeof(string) }),
            new NasaFileTreeModel(sIndex : 3, pIndex : 2, desription: "Secular Orbit Drift Due to J2", types: new Type[]{ typeof(bool) }),
            new NasaFileTreeModel(sIndex : 3, pIndex : 3, desription: "Use Keplerian elements (KEP) or (RV) or FILE", types: new Type[]{ typeof(string) }),
            new NasaFileTreeModel(sIndex : 3, pIndex : 4, desription: "Use Peri/Apoapsis (PA) or min alt/ecc (AE)", types: new Type[]{ typeof(string) }),
            new NasaFileTreeModel(sIndex : 3, pIndex : 5, desription: "Periapsis & Apoapsis Altitude, km", types: new Type[]{ typeof(double), typeof(double) }),
            new NasaFileTreeModel(sIndex : 3, pIndex : 6, desription: "Min Altitude (km), Eccentricity", types: new Type[]{ typeof(double), typeof(double) }),
            new NasaFileTreeModel(sIndex : 3, pIndex : 7, desription: "Inclination (deg)", types: new Type[]{ typeof(double) }),
            new NasaFileTreeModel(sIndex : 3, pIndex : 8, desription: "Right Ascension of Ascending Node (deg)", types: new Type[]{ typeof(double) }),
            new NasaFileTreeModel(sIndex : 3, pIndex : 9, desription: "Argument of Periapsis (deg)", types: new Type[]{ typeof(double) }),
            new NasaFileTreeModel(sIndex : 3, pIndex : 10, desription: "True Anomaly (deg)", types: new Type[]{ typeof(double) }),
            new NasaFileTreeModel(sIndex : 3, pIndex : 11, desription: "RV Initial Position (km)", types: new Type[]{ typeof(double), typeof(double), typeof(double) }),
            new NasaFileTreeModel(sIndex : 3, pIndex : 12, desription: "RV Initial Velocity (km/sec)", types: new Type[]{ typeof(double), typeof(double), typeof(double) }),
            new NasaFileTreeModel(sIndex : 3, pIndex : 13, desription: "TLE or TRV format, Label to find in file", types: new Type[]{ typeof(string), typeof(string)}),
            new NasaFileTreeModel(sIndex : 3, pIndex : 14, desription: "File name", types: new Type[]{ typeof(string) }),


            new NasaFileTreeModel(sIndex : 4, values : new string[] { "Use these lines if THREE_BODY", "*"  }),
            new NasaFileTreeModel(sIndex : 4, pIndex : 1, desription: "Lagrange system", types: new Type[]{ typeof(string)}),
            new NasaFileTreeModel(sIndex : 4, pIndex : 2, desription: "Propagate using LAGDOF_MODES or LAGDOF_COWELL or LAGDOF_SPLINE", types: new Type[]{ typeof(string) }),
            new NasaFileTreeModel(sIndex : 4, pIndex : 3, desription: "Initialize with MODES or XYZ or FILE", types: new Type[]{ typeof(string)}),
            new NasaFileTreeModel(sIndex : 4, pIndex : 4, desription: "Libration point (L1, L2, L3, L4, L5)", types: new Type[]{ typeof(string)}),
            new NasaFileTreeModel(sIndex : 4, pIndex : 5, desription: "XY Semi-major axis, km", types: new Type[]{ typeof(double)}),
            new NasaFileTreeModel(sIndex : 4, pIndex : 6, desription: "Initial XY Phase, deg  (CCW from -Y)", types: new Type[]{ typeof(double)}),
            new NasaFileTreeModel(sIndex : 4, pIndex : 7, desription: "Sense (CW, CCW), viewed from +Z", types: new Type[]{ typeof(string)}),
            new NasaFileTreeModel(sIndex : 4, pIndex : 8, desription: "Second XY Mode Semi-major Axis, km (L4, L5 only)", types: new Type[]{ typeof(double)}),
            new NasaFileTreeModel(sIndex : 4, pIndex : 9, desription: "Second XY Mode Initial Phase, deg (L4, L5 only)", types: new Type[]{ typeof(double)}),
            new NasaFileTreeModel(sIndex : 4, pIndex : 10, desription: "Sense (CW, CCW), viewed from +Z (L4, L5 only)", types: new Type[]{ typeof(string)}),
            new NasaFileTreeModel(sIndex : 4, pIndex : 11, desription: "Z Semi-axis, km", types: new Type[]{ typeof(double)}),
            new NasaFileTreeModel(sIndex : 4, pIndex : 12, desription: "Initial Z Phase, deg", types: new Type[]{ typeof(double)}),
            new NasaFileTreeModel(sIndex : 4, pIndex : 13, desription: "Initial X, Y, Z (Non-dimensional)", types: new Type[]{ typeof(double), typeof(double), typeof(double) }),
            new NasaFileTreeModel(sIndex : 4, pIndex : 14, desription: "Initial Xdot, Ydot, Zdot (Non-dimensional)", types: new Type[]{ typeof(double), typeof(double), typeof(double)}),
            new NasaFileTreeModel(sIndex : 4, pIndex : 15, desription: "TLE, TRV or SPLINE format, Label to find in file", types: new Type[]{ typeof(string), typeof(string)}),
            new NasaFileTreeModel(sIndex : 4, pIndex : 16, desription: "File name", types: new Type[]{ typeof(string)}),

            new NasaFileTreeModel(sIndex : 5, values : new string[] { "Formation Frame Parameters", "*"  }),
            new NasaFileTreeModel(sIndex : 5, pIndex : 1, desription: "Formation Frame Fixed in [NL]", types: new Type[]{ typeof(string) }),
            new NasaFileTreeModel(sIndex : 5, pIndex : 2, desription: "Euler Angles (deg) and Sequence", types: new Type[]{ typeof(double), typeof(double), typeof(double), typeof(double) }),
            new NasaFileTreeModel(sIndex : 5, pIndex : 3, desription: "Formation Origin expressed in [NL]", types: new Type[]{ typeof(string) }),
            new NasaFileTreeModel(sIndex : 5, pIndex : 4, desription: "Formation Origin wrt Ref Orbit (m)", types: new Type[]{ typeof(double), typeof(double), typeof(double) }),

        };

        private NasaFileTreeModel[] dicTreeSC = new NasaFileTreeModel[]
        {
            new NasaFileTreeModel(sIndex : 0, values : new string[] { "42: Spacecraft Description File", "<", ">" }),
            new NasaFileTreeModel(sIndex : 0, pIndex : 1, desription: "Description", types: new Type[]{ typeof(string) }),
            new NasaFileTreeModel(sIndex : 0, pIndex : 2, desription: "Label", types: new Type[]{ typeof(string) }),
            new NasaFileTreeModel(sIndex : 0, pIndex : 3, desription: "Sprite File Name", types: new Type[]{ typeof(string) }),
            new NasaFileTreeModel(sIndex : 0, pIndex : 4, desription: "Flight Software Identifier", types: new Type[]{ typeof(string) }),
            new NasaFileTreeModel(sIndex : 0, pIndex : 5, desription: "FSW Sample Time, sec", types: new Type[]{ typeof(double) }),

            new NasaFileTreeModel(sIndex : 1, values : new string[] { "Orbit Parameters", "*" }),
            new NasaFileTreeModel(sIndex : 1, pIndex : 1, desription: "Orbit Prop FIXED, EULER_HILL, ENCKE, or COWELL", types: new Type[]{ typeof(string) }),
            new NasaFileTreeModel(sIndex : 1, pIndex : 2, desription: "Pos of CM or ORIGIN, wrt F", types: new Type[]{ typeof(string) }),
            new NasaFileTreeModel(sIndex : 1, pIndex : 3, desription: "Pos wrt Formation (m), expressed in F", types: new Type[]{ typeof(double), typeof(double), typeof(double) }),
            new NasaFileTreeModel(sIndex : 1, pIndex : 4, desription: "Vel wrt Formation (m/s), expressed in F", types: new Type[]{ typeof(double), typeof(double), typeof(double) }),

            new NasaFileTreeModel(sIndex : 2, values : new string[] { "Initial Attitude", "*" }),
            new NasaFileTreeModel(sIndex : 2, pIndex : 1, desription: "Ang Vel wrt [NL], Att [QA] wrt [NLF]", types: new Type[]{ typeof(string) }),
            new NasaFileTreeModel(sIndex : 2, pIndex : 2, desription: "Ang Vel (deg/sec)", types: new Type[]{ typeof(double), typeof(double), typeof(double)}),
            new NasaFileTreeModel(sIndex : 2, pIndex : 3, desription: "Quaternion", types: new Type[]{ typeof(double), typeof(double), typeof(double), typeof(double)}),
            new NasaFileTreeModel(sIndex : 2, pIndex : 4, desription: "Angles (deg) & Euler Sequence", types: new Type[]{ typeof(double), typeof(double), typeof(double), typeof(double)}),

            new NasaFileTreeModel(sIndex : 3, values : new string[] { "Dynamics Flags", "*" }),
            new NasaFileTreeModel(sIndex : 3, pIndex : 1, desription: "Rotation STEADY, KIN_JOINT, or DYN_JOINT", types: new Type[]{ typeof(string) }),
            new NasaFileTreeModel(sIndex : 3, pIndex : 2, desription: "Passive Joint Forces and Torques Enabled", types: new Type[]{ typeof(bool) }),
            new NasaFileTreeModel(sIndex : 3, pIndex : 3, desription: "Compute Constraint Forces and Torques", types: new Type[]{ typeof(bool) }),
            new NasaFileTreeModel(sIndex : 3, pIndex : 4, desription: "Mass Props referenced to REFPT_CM or REFPT_JOINT", types: new Type[]{ typeof(string) }),
            new NasaFileTreeModel(sIndex : 3, pIndex : 5, desription: "Flex Active", types: new Type[]{ typeof(bool) }),
            new NasaFileTreeModel(sIndex : 3, pIndex : 6, desription: "Include 2nd Order Flex Terms", types: new Type[]{ typeof(bool) }),
            new NasaFileTreeModel(sIndex : 3, pIndex : 7, desription: "Drag Coefficient", types: new Type[]{ typeof(double) }),

            new NasaFileTreeModel(sIndex : 4, values : new string[] { "Body Parameters", "*"}),
            new NasaFileTreeModel(sIndex : 4, pIndex : 1, desription: "Number of Bodies", types: new Type[]{ typeof(uint) }),

            new NasaFileTreeModel(sIndex : 5, values : new string[] { "Body 0", "="  }),
            new NasaFileTreeModel(sIndex : 5, pIndex : 1, desription: "Mass", types: new Type[]{ typeof(double)}),
            new NasaFileTreeModel(sIndex : 5, pIndex : 2, desription: "Moments of Inertia (kg-m^2)", types: new Type[]{ typeof(double), typeof(double), typeof(double) }),
            new NasaFileTreeModel(sIndex : 5, pIndex : 3, desription: "Products of Inertia (xy,xz,yz)./42", types: new Type[]{ typeof(double), typeof(double), typeof(double) }),
            new NasaFileTreeModel(sIndex : 5, pIndex : 4, desription: "Location of mass center, m", types: new Type[]{ typeof(double), typeof(double), typeof(double) }),
            new NasaFileTreeModel(sIndex : 5, pIndex : 5, desription: "Constant Embedded Momentum (Nms)", types: new Type[]{ typeof(double), typeof(double), typeof(double) }),
            new NasaFileTreeModel(sIndex : 5, pIndex : 6, desription: "Geometry Input File Name", types: new Type[]{ typeof(string) }),
            new NasaFileTreeModel(sIndex : 5, pIndex : 7, desription: "Flex File Name", types: new Type[]{ typeof(string) }),

            new NasaFileTreeModel(sIndex : 6, values : new string[] { "SP 1", "="  }),
            new NasaFileTreeModel(sIndex : 6, pIndex : 1, desription: "Mass", types: new Type[]{ typeof(double)}),
            new NasaFileTreeModel(sIndex : 6, pIndex : 2, desription: "Moments of Inertia (kg-m^2)", types: new Type[]{ typeof(double), typeof(double), typeof(double) }),
            new NasaFileTreeModel(sIndex : 6, pIndex : 3, desription: "Products of Inertia (xy,xz,yz)", types: new Type[]{ typeof(double), typeof(double), typeof(double) }),
            new NasaFileTreeModel(sIndex : 6, pIndex : 4, desription: "Location of mass center, m", types: new Type[]{ typeof(double), typeof(double), typeof(double) }),
            new NasaFileTreeModel(sIndex : 6, pIndex : 5, desription: "Constant Embedded Momentum (Nms)", types: new Type[]{ typeof(double), typeof(double), typeof(double) }),
            new NasaFileTreeModel(sIndex : 6, pIndex : 6, desription: "Geometry Input File Name", types: new Type[]{ typeof(string) }),
            new NasaFileTreeModel(sIndex : 6, pIndex : 7, desription: "Flex File Name", types: new Type[]{ typeof(string) }),

            new NasaFileTreeModel(sIndex : 7, values : new string[] { "SP 2", "="  }),
            new NasaFileTreeModel(sIndex : 7, pIndex : 1, desription: "Mass", types: new Type[]{ typeof(double)}),
            new NasaFileTreeModel(sIndex : 7, pIndex : 2, desription: "Moments of Inertia (kg-m^2)", types: new Type[]{ typeof(double), typeof(double), typeof(double) }),
            new NasaFileTreeModel(sIndex : 7, pIndex : 3, desription: "Products of Inertia (xy,xz,yz)", types: new Type[]{ typeof(double), typeof(double), typeof(double) }),
            new NasaFileTreeModel(sIndex : 7, pIndex : 4, desription: "Location of mass center, m", types: new Type[]{ typeof(double), typeof(double), typeof(double) }),
            new NasaFileTreeModel(sIndex : 7, pIndex : 5, desription: "Constant Embedded Momentum (Nms)", types: new Type[]{ typeof(double), typeof(double), typeof(double) }),
            new NasaFileTreeModel(sIndex : 7, pIndex : 6, desription: "Geometry Input File Name", types: new Type[]{ typeof(string) }),
            new NasaFileTreeModel(sIndex : 7, pIndex : 7, desription: "Flex File Name", types: new Type[]{ typeof(string) }),

            new NasaFileTreeModel(sIndex : 8, values : new string[] { "SP 3", "="  }),
            new NasaFileTreeModel(sIndex : 8, pIndex : 1, desription: "Mass", types: new Type[]{ typeof(double)}),
            new NasaFileTreeModel(sIndex : 8, pIndex : 2, desription: "Moments of Inertia (kg-m^2)", types: new Type[]{ typeof(double), typeof(double), typeof(double) }),
            new NasaFileTreeModel(sIndex : 8, pIndex : 3, desription: "Products of Inertia (xy,xz,yz)", types: new Type[]{ typeof(double), typeof(double), typeof(double) }),
            new NasaFileTreeModel(sIndex : 8, pIndex : 4, desription: "Location of mass center, m", types: new Type[]{ typeof(double), typeof(double), typeof(double) }),
            new NasaFileTreeModel(sIndex : 8, pIndex : 5, desription: "Constant Embedded Momentum (Nms)", types: new Type[]{ typeof(double), typeof(double), typeof(double) }),
            new NasaFileTreeModel(sIndex : 8, pIndex : 6, desription: "Geometry Input File Name", types: new Type[]{ typeof(string) }),
            new NasaFileTreeModel(sIndex : 8, pIndex : 7, desription: "Flex File Name", types: new Type[]{ typeof(string) }),

            new NasaFileTreeModel(sIndex : 9, values : new string[] { "SP 4", "="  }),
            new NasaFileTreeModel(sIndex : 9, pIndex : 1, desription: "Mass", types: new Type[]{ typeof(double)}),
            new NasaFileTreeModel(sIndex : 9, pIndex : 2, desription: "Moments of Inertia (kg-m^2)", types: new Type[]{ typeof(double), typeof(double), typeof(double) }),
            new NasaFileTreeModel(sIndex : 9, pIndex : 3, desription: "Products of Inertia (xy,xz,yz)", types: new Type[]{ typeof(double), typeof(double), typeof(double) }),
            new NasaFileTreeModel(sIndex : 9, pIndex : 4, desription: "Location of mass center, m", types: new Type[]{ typeof(double), typeof(double), typeof(double) }),
            new NasaFileTreeModel(sIndex : 9, pIndex : 5, desription: "Constant Embedded Momentum (Nms)", types: new Type[]{ typeof(double), typeof(double), typeof(double) }),
            new NasaFileTreeModel(sIndex : 9, pIndex : 6, desription: "Geometry Input File Name", types: new Type[]{ typeof(string) }),
            new NasaFileTreeModel(sIndex : 9, pIndex : 7, desription: "Flex File Name", types: new Type[]{ typeof(string) }),

            new NasaFileTreeModel(sIndex : 10, values : new string[] { "Joint Parameters", "*" }),

            new NasaFileTreeModel(sIndex : 11, values : new string[] { "Joint SP (+x)", "="  }),
            new NasaFileTreeModel(sIndex : 11, pIndex : 1, desription: "Inner, outer body indices", types: new Type[]{ typeof(uint), typeof(uint) }),
            new NasaFileTreeModel(sIndex : 11, pIndex : 2, desription: "RotDOF, Seq, GIMBAL or SPHERICAL", types: new Type[]{ typeof(uint), typeof(uint), typeof(string) }),
            new NasaFileTreeModel(sIndex : 11, pIndex : 3, desription: "TrnDOF, Seq", types: new Type[]{ typeof(uint), typeof(uint) }),
            new NasaFileTreeModel(sIndex : 11, pIndex : 4, desription: "RotDOF Locked", types: new Type[]{ typeof(bool), typeof(bool),typeof(bool) }),
            new NasaFileTreeModel(sIndex : 11, pIndex : 5, desription: "TrnDOF Locked", types: new Type[]{ typeof(bool), typeof(bool),typeof(bool) }),
            new NasaFileTreeModel(sIndex : 11, pIndex : 6, desription: "Initial Angles [deg]", types: new Type[]{ typeof(double), typeof(double),typeof(double) }),
            new NasaFileTreeModel(sIndex : 11, pIndex : 7, desription: "Initial Rates, deg/sec", types: new Type[]{ typeof(double), typeof(double),typeof(double) }),
            new NasaFileTreeModel(sIndex : 11, pIndex : 8, desription: "Initial Displacements [m]", types: new Type[]{ typeof(double), typeof(double),typeof(double) }),
            new NasaFileTreeModel(sIndex : 11, pIndex : 9, desription: "Initial Displacement Rates, m/sec", types: new Type[]{ typeof(double), typeof(double),typeof(double) }),
            new NasaFileTreeModel(sIndex : 11, pIndex : 10, desription: "Bi to Gi Static Angles [deg] & Seq", types: new Type[]{ typeof(double), typeof(double),typeof(double),typeof(double) }),
            new NasaFileTreeModel(sIndex : 11, pIndex : 11, desription: "Go to Bo Static Angles [deg] & Seq", types: new Type[]{typeof(double), typeof(double),typeof(double),typeof(double) }),
            new NasaFileTreeModel(sIndex : 11, pIndex : 12, desription: "Position wrt inner body origin, m", types: new Type[]{ typeof(double), typeof(double),typeof(double) }),
            new NasaFileTreeModel(sIndex : 11, pIndex : 13, desription: "Position wrt outer body origin, m", types: new Type[]{ typeof(double), typeof(double),typeof(double) }),
            new NasaFileTreeModel(sIndex : 11, pIndex : 14, desription: "Rot Passive Spring Coefficients (Nm/rad)", types: new Type[]{ typeof(double), typeof(double),typeof(double) }),
            new NasaFileTreeModel(sIndex : 11, pIndex : 15, desription: "Rot Passive Damping Coefficients (Nms/rad)", types: new Type[]{ typeof(double), typeof(double),typeof(double)}),
            new NasaFileTreeModel(sIndex : 11, pIndex : 16, desription: "Trn Passive Spring Coefficients (N/m)", types: new Type[]{ typeof(double), typeof(double),typeof(double) }),
            new NasaFileTreeModel(sIndex : 11, pIndex : 17, desription: "Trn Passive Damping Coefficients (Ns/m)", types: new Type[]{ typeof(double), typeof(double),typeof(double) }),

            new NasaFileTreeModel(sIndex : 12, values : new string[] { "Joint SP (+y)", "="  }),
            new NasaFileTreeModel(sIndex : 12, pIndex : 1, desription: "Inner, outer body indices", types: new Type[]{ typeof(uint), typeof(uint) }),
            new NasaFileTreeModel(sIndex : 12, pIndex : 2, desription: "RotDOF, Seq, GIMBAL or SPHERICAL", types: new Type[]{ typeof(uint), typeof(uint), typeof(string) }),
            new NasaFileTreeModel(sIndex : 12, pIndex : 3, desription: "TrnDOF, Seq", types: new Type[]{ typeof(uint), typeof(uint) }),
            new NasaFileTreeModel(sIndex : 12, pIndex : 4, desription: "RotDOF Locked", types: new Type[]{ typeof(bool), typeof(bool),typeof(bool) }),
            new NasaFileTreeModel(sIndex : 12, pIndex : 5, desription: "TrnDOF Locked", types: new Type[]{ typeof(bool), typeof(bool),typeof(bool) }),
            new NasaFileTreeModel(sIndex : 12, pIndex : 6, desription: "Initial Angles [deg]", types: new Type[]{ typeof(double), typeof(double),typeof(double) }),
            new NasaFileTreeModel(sIndex : 12, pIndex : 7, desription: "Initial Rates, deg/sec", types: new Type[]{ typeof(double), typeof(double),typeof(double) }),
            new NasaFileTreeModel(sIndex : 12, pIndex : 8, desription: "Initial Displacements [m]", types: new Type[]{ typeof(double), typeof(double),typeof(double) }),
            new NasaFileTreeModel(sIndex : 12, pIndex : 9, desription: "Initial Displacement Rates, m/sec", types: new Type[]{ typeof(double), typeof(double),typeof(double) }),
            new NasaFileTreeModel(sIndex : 12, pIndex : 10, desription: "Bi to Gi Static Angles [deg] & Seq", types: new Type[]{ typeof(double), typeof(double),typeof(double),typeof(double) }),
            new NasaFileTreeModel(sIndex : 12, pIndex : 11, desription: "Go to Bo Static Angles [deg] & Seq", types: new Type[]{typeof(double), typeof(double),typeof(double),typeof(double) }),
            new NasaFileTreeModel(sIndex : 12, pIndex : 12, desription: "Position wrt inner body origin, m", types: new Type[]{ typeof(double), typeof(double),typeof(double) }),
            new NasaFileTreeModel(sIndex : 12, pIndex : 13, desription: "Position wrt outer body origin, m", types: new Type[]{ typeof(double), typeof(double),typeof(double) }),
            new NasaFileTreeModel(sIndex : 12, pIndex : 14, desription: "Rot Passive Spring Coefficients (Nm/rad)", types: new Type[]{ typeof(double), typeof(double),typeof(double) }),
            new NasaFileTreeModel(sIndex : 12, pIndex : 15, desription: "Rot Passive Damping Coefficients (Nms/rad)", types: new Type[]{ typeof(double), typeof(double),typeof(double)}),
            new NasaFileTreeModel(sIndex : 12, pIndex : 16, desription: "Trn Passive Spring Coefficients (N/m)", types: new Type[]{ typeof(double), typeof(double),typeof(double) }),
            new NasaFileTreeModel(sIndex : 12, pIndex : 17, desription: "Trn Passive Damping Coefficients (Ns/m)", types: new Type[]{ typeof(double), typeof(double),typeof(double) }),

            new NasaFileTreeModel(sIndex : 13, values : new string[] { "Joint SP (-x)", "="  }),
            new NasaFileTreeModel(sIndex : 13, pIndex : 1, desription: "Inner, outer body indices", types: new Type[]{ typeof(uint), typeof(uint) }),
            new NasaFileTreeModel(sIndex : 13, pIndex : 2, desription: "RotDOF, Seq, GIMBAL or SPHERICAL", types: new Type[]{ typeof(uint), typeof(uint), typeof(string) }),
            new NasaFileTreeModel(sIndex : 13, pIndex : 3, desription: "TrnDOF, Seq", types: new Type[]{ typeof(uint), typeof(uint) }),
            new NasaFileTreeModel(sIndex : 13, pIndex : 4, desription: "RotDOF Locked", types: new Type[]{ typeof(bool), typeof(bool),typeof(bool) }),
            new NasaFileTreeModel(sIndex : 13, pIndex : 5, desription: "TrnDOF Locked", types: new Type[]{ typeof(bool), typeof(bool),typeof(bool) }),
            new NasaFileTreeModel(sIndex : 13, pIndex : 6, desription: "Initial Angles [deg]", types: new Type[]{ typeof(double), typeof(double),typeof(double) }),
            new NasaFileTreeModel(sIndex : 13, pIndex : 7, desription: "Initial Rates, deg/sec", types: new Type[]{ typeof(double), typeof(double),typeof(double) }),
            new NasaFileTreeModel(sIndex : 13, pIndex : 8, desription: "Initial Displacements [m]", types: new Type[]{ typeof(double), typeof(double),typeof(double) }),
            new NasaFileTreeModel(sIndex : 13, pIndex : 9, desription: "Initial Displacement Rates, m/sec", types: new Type[]{ typeof(double), typeof(double),typeof(double) }),
            new NasaFileTreeModel(sIndex : 13, pIndex : 10, desription: "Bi to Gi Static Angles [deg] & Seq", types: new Type[]{ typeof(double), typeof(double),typeof(double),typeof(double) }),
            new NasaFileTreeModel(sIndex : 13, pIndex : 11, desription: "Go to Bo Static Angles [deg] & Seq", types: new Type[]{typeof(double), typeof(double),typeof(double),typeof(double) }),
            new NasaFileTreeModel(sIndex : 13, pIndex : 12, desription: "Position wrt inner body origin, m", types: new Type[]{ typeof(double), typeof(double),typeof(double) }),
            new NasaFileTreeModel(sIndex : 13, pIndex : 13, desription: "Position wrt outer body origin, m", types: new Type[]{ typeof(double), typeof(double),typeof(double) }),
            new NasaFileTreeModel(sIndex : 13, pIndex : 14, desription: "Rot Passive Spring Coefficients (Nm/rad)", types: new Type[]{ typeof(double), typeof(double),typeof(double) }),
            new NasaFileTreeModel(sIndex : 13, pIndex : 15, desription: "Rot Passive Damping Coefficients (Nms/rad)", types: new Type[]{ typeof(double), typeof(double),typeof(double)}),
            new NasaFileTreeModel(sIndex : 13, pIndex : 16, desription: "Trn Passive Spring Coefficients (N/m)", types: new Type[]{ typeof(double), typeof(double),typeof(double) }),
            new NasaFileTreeModel(sIndex : 13, pIndex : 17, desription: "Trn Passive Damping Coefficients (Ns/m)", types: new Type[]{ typeof(double), typeof(double),typeof(double) }),

            new NasaFileTreeModel(sIndex : 14, values : new string[] { "Joint SP (-y)", "="  }),
            new NasaFileTreeModel(sIndex : 14, pIndex : 1, desription: "Inner, outer body indices", types: new Type[]{ typeof(uint), typeof(uint) }),
            new NasaFileTreeModel(sIndex : 14, pIndex : 2, desription: "RotDOF, Seq, GIMBAL or SPHERICAL", types: new Type[]{ typeof(uint), typeof(uint), typeof(string) }),
            new NasaFileTreeModel(sIndex : 14, pIndex : 3, desription: "TrnDOF, Seq", types: new Type[]{ typeof(uint), typeof(uint) }),
            new NasaFileTreeModel(sIndex : 14, pIndex : 4, desription: "RotDOF Locked", types: new Type[]{ typeof(bool), typeof(bool),typeof(bool) }),
            new NasaFileTreeModel(sIndex : 14, pIndex : 5, desription: "TrnDOF Locked", types: new Type[]{ typeof(bool), typeof(bool),typeof(bool) }),
            new NasaFileTreeModel(sIndex : 14, pIndex : 6, desription: "Initial Angles [deg]", types: new Type[]{ typeof(double), typeof(double),typeof(double) }),
            new NasaFileTreeModel(sIndex : 14, pIndex : 7, desription: "Initial Rates, deg/sec", types: new Type[]{ typeof(double), typeof(double),typeof(double) }),
            new NasaFileTreeModel(sIndex : 14, pIndex : 8, desription: "Initial Displacements [m]", types: new Type[]{ typeof(double), typeof(double),typeof(double) }),
            new NasaFileTreeModel(sIndex : 14, pIndex : 9, desription: "Initial Displacement Rates, m/sec", types: new Type[]{ typeof(double), typeof(double),typeof(double) }),
            new NasaFileTreeModel(sIndex : 14, pIndex : 10, desription: "Bi to Gi Static Angles [deg] & Seq", types: new Type[]{ typeof(double), typeof(double),typeof(double),typeof(double) }),
            new NasaFileTreeModel(sIndex : 14, pIndex : 11, desription: "Go to Bo Static Angles [deg] & Seq", types: new Type[]{typeof(double), typeof(double),typeof(double),typeof(double) }),
            new NasaFileTreeModel(sIndex : 14, pIndex : 12, desription: "Position wrt inner body origin, m", types: new Type[]{ typeof(double), typeof(double),typeof(double) }),
            new NasaFileTreeModel(sIndex : 14, pIndex : 13, desription: "Position wrt outer body origin, m", types: new Type[]{ typeof(double), typeof(double),typeof(double) }),
            new NasaFileTreeModel(sIndex : 14, pIndex : 14, desription: "Rot Passive Spring Coefficients (Nm/rad)", types: new Type[]{ typeof(double), typeof(double),typeof(double) }),
            new NasaFileTreeModel(sIndex : 14, pIndex : 15, desription: "Rot Passive Damping Coefficients (Nms/rad)", types: new Type[]{ typeof(double), typeof(double),typeof(double)}),
            new NasaFileTreeModel(sIndex : 14, pIndex : 16, desription: "Trn Passive Spring Coefficients (N/m)", types: new Type[]{ typeof(double), typeof(double),typeof(double) }),
            new NasaFileTreeModel(sIndex : 14, pIndex : 17, desription: "Trn Passive Damping Coefficients (Ns/m)", types: new Type[]{ typeof(double), typeof(double),typeof(double) }),

            new NasaFileTreeModel(sIndex : 15, values : new string[] { "Wheel Parameters", "*" }),
            new NasaFileTreeModel(sIndex : 15, pIndex : 1, desription: "Number of wheels", types: new Type[]{ typeof(uint) }),

            new NasaFileTreeModel(sIndex : 16, values : new string[] { "Wheel 0", "="  }),
            new NasaFileTreeModel(sIndex : 16, pIndex : 1, desription: "Initial Momentum, N-m-sec", types: new Type[]{ typeof(double) }),
            new NasaFileTreeModel(sIndex : 16, pIndex : 2, desription: "Wheel Axis Components, [X, Y, Z]", types: new Type[]{ typeof(double), typeof(double), typeof(double) }),
            new NasaFileTreeModel(sIndex : 16, pIndex : 3, desription: "Max Torque (N-m), Momentum (N-m-sec)", types: new Type[]{ typeof(double), typeof(double) }),
            new NasaFileTreeModel(sIndex : 16, pIndex : 4, desription: "Wheel Rotor Inertia, kg-m^2", types: new Type[]{ typeof(double) }),
            new NasaFileTreeModel(sIndex : 16, pIndex : 5, desription: "Static Imbalance, g-cm", types: new Type[]{ typeof(double) }),
            new NasaFileTreeModel(sIndex : 16, pIndex : 6, desription: "Dynamic Imbalance, g-cm^2", types: new Type[]{ typeof(double) }),
            new NasaFileTreeModel(sIndex : 16, pIndex : 7, desription: "Flex Node Index", types: new Type[]{ typeof(uint) }),

            new NasaFileTreeModel(sIndex : 17, values : new string[] { "Wheel 1", "="  }),
            new NasaFileTreeModel(sIndex : 17, pIndex : 1, desription: "Initial Momentum, N-m-sec", types: new Type[]{ typeof(double) }),
            new NasaFileTreeModel(sIndex : 17, pIndex : 2, desription: "Wheel Axis Components, [X, Y, Z]", types: new Type[]{ typeof(double), typeof(double), typeof(double) }),
            new NasaFileTreeModel(sIndex : 17, pIndex : 3, desription: "Max Torque (N-m), Momentum (N-m-sec)", types: new Type[]{ typeof(double), typeof(double) }),
            new NasaFileTreeModel(sIndex : 17, pIndex : 4, desription: "Wheel Rotor Inertia, kg-m^2", types: new Type[]{ typeof(double) }),
            new NasaFileTreeModel(sIndex : 17, pIndex : 5, desription: "Static Imbalance, g-cm", types: new Type[]{ typeof(double) }),
            new NasaFileTreeModel(sIndex : 17, pIndex : 6, desription: "Dynamic Imbalance, g-cm^2", types: new Type[]{ typeof(double) }),
            new NasaFileTreeModel(sIndex : 17, pIndex : 7, desription: "Flex Node Index", types: new Type[]{ typeof(uint) }),

            new NasaFileTreeModel(sIndex : 18, values : new string[] { "Wheel 2", "="  }),
            new NasaFileTreeModel(sIndex : 18, pIndex : 1, desription: "Initial Momentum, N-m-sec", types: new Type[]{ typeof(double) }),
            new NasaFileTreeModel(sIndex : 18, pIndex : 2, desription: "Wheel Axis Components, [X, Y, Z]", types: new Type[]{ typeof(double), typeof(double), typeof(double) }),
            new NasaFileTreeModel(sIndex : 18, pIndex : 3, desription: "Max Torque (N-m), Momentum (N-m-sec)", types: new Type[]{ typeof(double), typeof(double) }),
            new NasaFileTreeModel(sIndex : 18, pIndex : 4, desription: "Wheel Rotor Inertia, kg-m^2", types: new Type[]{ typeof(double) }),
            new NasaFileTreeModel(sIndex : 18, pIndex : 5, desription: "Static Imbalance, g-cm", types: new Type[]{ typeof(double) }),
            new NasaFileTreeModel(sIndex : 18, pIndex : 6, desription: "Dynamic Imbalance, g-cm^2", types: new Type[]{ typeof(double) }),
            new NasaFileTreeModel(sIndex : 18, pIndex : 7, desription: "Flex Node Index", types: new Type[]{ typeof(uint) }),

            new NasaFileTreeModel(sIndex : 19, values : new string[] { "MTB Parameters", "*" }),
            new NasaFileTreeModel(sIndex : 19, pIndex : 1, desription: "Number of MTBs", types: new Type[]{ typeof(uint) }),

            new NasaFileTreeModel(sIndex : 20, values : new string[] { "MTB 0", "="  }),
            new NasaFileTreeModel(sIndex : 20, pIndex : 1, desription: "Saturation (A-m^2)", types: new Type[]{ typeof(double) }),
            new NasaFileTreeModel(sIndex : 20, pIndex : 2, desription: "MTB Axis Components, [X, Y, Z]", types: new Type[]{ typeof(double), typeof(double), typeof(double) }),
            new NasaFileTreeModel(sIndex : 20, pIndex : 3, desription: "Flex Node Index", types: new Type[]{ typeof(uint) }),

            new NasaFileTreeModel(sIndex : 21, values : new string[] { "MTB 1", "="  }),
            new NasaFileTreeModel(sIndex : 21, pIndex : 1, desription: "Saturation (A-m^2)", types: new Type[]{ typeof(double) }),
            new NasaFileTreeModel(sIndex : 21, pIndex : 2, desription: "MTB Axis Components, [X, Y, Z]", types: new Type[]{ typeof(double), typeof(double), typeof(double) }),
            new NasaFileTreeModel(sIndex : 21, pIndex : 3, desription: "Flex Node Index", types: new Type[]{ typeof(uint) }),

            new NasaFileTreeModel(sIndex : 22, values : new string[] { "MTB 2", "="  }),
            new NasaFileTreeModel(sIndex : 22, pIndex : 1, desription: "Saturation (A-m^2)", types: new Type[]{ typeof(double) }),
            new NasaFileTreeModel(sIndex : 22, pIndex : 2, desription: "MTB Axis Components, [X, Y, Z]", types: new Type[]{ typeof(double), typeof(double), typeof(double) }),
            new NasaFileTreeModel(sIndex : 22, pIndex : 3, desription: "Flex Node Index", types: new Type[]{ typeof(uint) }),

            new NasaFileTreeModel(sIndex : 23, values : new string[] { "Thruster Parameters", "*" }),
            new NasaFileTreeModel(sIndex : 23, pIndex : 1, desription: "Number of Thrusters", types: new Type[]{ typeof(uint) }),

            new NasaFileTreeModel(sIndex : 24, values : new string[] { "Thr 0", "="  }),
            new NasaFileTreeModel(sIndex : 24, pIndex : 1, desription: "Thrust Force (N)", types: new Type[]{ typeof(double) }),
            new NasaFileTreeModel(sIndex : 24, pIndex : 2, desription: "Body, Thrust Axis", types: new Type[]{ typeof(double), typeof(double), typeof(double), typeof(double) }),
            new NasaFileTreeModel(sIndex : 24, pIndex : 3, desription: "Location in B0, m", types: new Type[]{ typeof(double), typeof(double), typeof(double) }),
            new NasaFileTreeModel(sIndex : 24, pIndex : 4, desription: "Flex Node Index", types: new Type[]{ typeof(uint) }),

            new NasaFileTreeModel(sIndex : 25, values : new string[] { "Gyro", "*" }),
            new NasaFileTreeModel(sIndex : 25, pIndex : 1, desription: "Number of Gyro Axes", types: new Type[]{ typeof(uint) }),

            new NasaFileTreeModel(sIndex : 26, values : new string[] { "Axis 0", "="  }),
            new NasaFileTreeModel(sIndex : 26, pIndex : 1, desription: "Sample Time,sec", types: new Type[]{ typeof(double) }),
            new NasaFileTreeModel(sIndex : 26, pIndex : 2, desription: "Axis expressed in Body Frame", types: new Type[]{ typeof(double), typeof(double), typeof(double) }),
            new NasaFileTreeModel(sIndex : 26, pIndex : 3, desription: "Max Rate, deg/sec", types: new Type[]{ typeof(double) }),
            new NasaFileTreeModel(sIndex : 26, pIndex : 4, desription: "Scale Factor Error, ppm", types: new Type[]{ typeof(double) }),
            new NasaFileTreeModel(sIndex : 26, pIndex : 5, desription: "Quantization, arcsec ", types: new Type[]{ typeof(double) }),
            new NasaFileTreeModel(sIndex : 26, pIndex : 6, desription: "Angle Random Walk (deg/rt-hr)", types: new Type[]{ typeof(double) }),
            new NasaFileTreeModel(sIndex : 26, pIndex : 7, desription: "Bias Stability (deg/hr) over timespan (hr)", types: new Type[]{ typeof(double), typeof(double) }),
            new NasaFileTreeModel(sIndex : 26, pIndex : 8, desription: "Angle Noise, arcsec RMS", types: new Type[]{ typeof(double) }),
            new NasaFileTreeModel(sIndex : 26, pIndex : 9, desription: "Initial Bias (deg/hr)", types: new Type[]{ typeof(double) }),
            new NasaFileTreeModel(sIndex : 26, pIndex : 10, desription: "Flex Node Index", types: new Type[]{ typeof(uint) }),

            new NasaFileTreeModel(sIndex : 27, values : new string[] { "Magnetometer", "*" }),
            new NasaFileTreeModel(sIndex : 27, pIndex : 1, desription: "Number of Magnetometer Axes", types: new Type[]{ typeof(uint) }),

            new NasaFileTreeModel(sIndex : 28, values : new string[] { "Axis 0 ", "="  }),
            new NasaFileTreeModel(sIndex : 28, pIndex : 1, desription: "Sample Time,sec", types: new Type[]{ typeof(double) }),
            new NasaFileTreeModel(sIndex : 28, pIndex : 2, desription: "Axis expressed in Body Frame", types: new Type[]{ typeof(double), typeof(double), typeof(double) }),
            new NasaFileTreeModel(sIndex : 28, pIndex : 3, desription: "Saturation, Tesla", types: new Type[]{ typeof(double) }),
            new NasaFileTreeModel(sIndex : 28, pIndex : 4, desription: "Scale Factor Error, ppm", types: new Type[]{ typeof(double) }),
            new NasaFileTreeModel(sIndex : 28, pIndex : 5, desription: "Quantization, Tesla ", types: new Type[]{ typeof(double) }),
            new NasaFileTreeModel(sIndex : 28, pIndex : 6, desription: "Noise, Tesla RMS", types: new Type[]{ typeof(double) }),
            new NasaFileTreeModel(sIndex : 28, pIndex : 7, desription: "Flex Node Index", types: new Type[]{ typeof(uint) }),

            new NasaFileTreeModel(sIndex : 29, values : new string[] { "Coarse Sun Sensor", "*" }),
            new NasaFileTreeModel(sIndex : 29, pIndex : 1, desription: "Number of Coarse Sun Sensors", types: new Type[]{ typeof(uint) }),

            new NasaFileTreeModel(sIndex : 30, values : new string[] { "CSS 0", "="  }),
            new NasaFileTreeModel(sIndex : 30, pIndex : 1, desription: "Sample Time,sec", types: new Type[]{ typeof(double) }),
            new NasaFileTreeModel(sIndex : 30, pIndex : 2, desription: "Axis expressed in Body Frame", types: new Type[]{ typeof(double), typeof(double), typeof(double), typeof(double) }),
            new NasaFileTreeModel(sIndex : 30, pIndex : 3, desription: "Half-cone Angle, deg", types: new Type[]{ typeof(double) }),
            new NasaFileTreeModel(sIndex : 30, pIndex : 4, desription: "Scale Factor", types: new Type[]{ typeof(double) }),
            new NasaFileTreeModel(sIndex : 30, pIndex : 5, desription: "Quantization", types: new Type[]{ typeof(double) }),
            new NasaFileTreeModel(sIndex : 30, pIndex : 6, desription: "Flex Node Index", types: new Type[]{ typeof(uint) }),

            new NasaFileTreeModel(sIndex : 31, values : new string[] { "Fine Sun Sensor", "*" }),
            new NasaFileTreeModel(sIndex : 31, pIndex : 1, desription: "Number of Fine Sun Sensors", types: new Type[]{ typeof(uint) }),

            new NasaFileTreeModel(sIndex : 32, values : new string[] { "FSS 0", "="  }),
            new NasaFileTreeModel(sIndex : 32, pIndex : 1, desription: "Sample Time,sec", types: new Type[]{ typeof(double) }),
            new NasaFileTreeModel(sIndex : 32, pIndex : 2, desription: "Mounting Angles (deg), Seq in Body", types: new Type[]{ typeof(double), typeof(double), typeof(double), typeof(double) }),
            new NasaFileTreeModel(sIndex : 32, pIndex : 3, desription: "X, Y FOV Size, deg", types: new Type[]{ typeof(double), typeof(double) }),
            new NasaFileTreeModel(sIndex : 32, pIndex : 4, desription: "Noise Equivalent Angle, deg RMS", types: new Type[]{ typeof(double) }),
            new NasaFileTreeModel(sIndex : 32, pIndex : 5, desription: "Quantization, deg", types: new Type[]{ typeof(double) }),
            new NasaFileTreeModel(sIndex : 32, pIndex : 6, desription: "Flex Node Index", types: new Type[]{ typeof(uint) }),

            new NasaFileTreeModel(sIndex : 33, values : new string[] { "Star Tracker", "*" }),
            new NasaFileTreeModel(sIndex : 33, pIndex : 1, desription: "Number of Star Trackers", types: new Type[]{ typeof(uint) }),

            new NasaFileTreeModel(sIndex : 34, values : new string[] { "ST 0", "="  }),
            new NasaFileTreeModel(sIndex : 34, pIndex : 1, desription: "Sample Time,sec", types: new Type[]{ typeof(double) }),
            new NasaFileTreeModel(sIndex : 34, pIndex : 2, desription: "Mounting Angles (deg), Seq in Body", types: new Type[]{ typeof(double), typeof(double), typeof(double), typeof(double) }),
            new NasaFileTreeModel(sIndex : 34, pIndex : 3, desription: "X, Y FOV Size, deg", types: new Type[]{ typeof(double), typeof(double) }),
            new NasaFileTreeModel(sIndex : 34, pIndex : 4, desription: "Sun, Earth, Moon Exclusion Angles, deg", types: new Type[]{ typeof(double), typeof(double), typeof(double) }),
            new NasaFileTreeModel(sIndex : 34, pIndex : 5, desription: "Noise Equivalent Angle, arcsec RMS", types: new Type[]{ typeof(double), typeof(double), typeof(double) }),
            new NasaFileTreeModel(sIndex : 34, pIndex : 6, desription: "Flex Node Index", types: new Type[]{ typeof(uint) }),

            new NasaFileTreeModel(sIndex : 35, values : new string[] { "GPS", "*" }),
            new NasaFileTreeModel(sIndex : 35, pIndex : 1, desription: "Number of GPS Receivers", types: new Type[]{ typeof(uint) }),

            new NasaFileTreeModel(sIndex : 36, values : new string[] { "GPSR 0", "="  }),
            new NasaFileTreeModel(sIndex : 36, pIndex : 1, desription: "Sample Time,sec", types: new Type[]{ typeof(double) }),
            new NasaFileTreeModel(sIndex : 36, pIndex : 2, desription: "Position Noise, m RMS", types: new Type[]{ typeof(double) }),
            new NasaFileTreeModel(sIndex : 36, pIndex : 3, desription: "Velocity Noise, m/sec RMS", types: new Type[]{ typeof(double) }),
            new NasaFileTreeModel(sIndex : 36, pIndex : 4, desription: "Time Noise, sec RMS", types: new Type[]{ typeof(double) }),
            new NasaFileTreeModel(sIndex : 36, pIndex : 5, desription: "Flex Node Index", types: new Type[]{ typeof(uint) }),

            new NasaFileTreeModel(sIndex : 37, values : new string[] { "Accelerometer", "*" }),
            new NasaFileTreeModel(sIndex : 37, pIndex : 1, desription: "Number of Accel Axes", types: new Type[]{ typeof(uint) }),

            new NasaFileTreeModel(sIndex : 38, values : new string[] { "Axis 0", "=" }),
            new NasaFileTreeModel(sIndex : 38, pIndex : 1, desription: "Sample Time,sec", types: new Type[]{ typeof(double) }),
            new NasaFileTreeModel(sIndex : 38, pIndex : 2, desription: "Position in B[0] (m)", types: new Type[]{typeof(double), typeof(double), typeof(double) }),
            new NasaFileTreeModel(sIndex : 38, pIndex : 3, desription: "Axis expressed in Body Frame", types: new Type[]{ typeof(double), typeof(double), typeof(double) }),
            new NasaFileTreeModel(sIndex : 38, pIndex : 4, desription: "Max Acceleration (m/s^2)", types: new Type[]{ typeof(double) }),
            new NasaFileTreeModel(sIndex : 38, pIndex : 5, desription: "Scale Factor Error, ppm", types: new Type[]{ typeof(double) }),
            new NasaFileTreeModel(sIndex : 38, pIndex : 6, desription: "Quantization, m/s^2", types: new Type[]{ typeof(double) }),
            new NasaFileTreeModel(sIndex : 38, pIndex : 7, desription: "DV Random Walk (m/s/rt-hr)", types: new Type[]{ typeof(double) }),
            new NasaFileTreeModel(sIndex : 38, pIndex : 8, desription: "Bias Stability (m/s^2) over timespan (hr)", types: new Type[]{ typeof(double), typeof(double) }),
            new NasaFileTreeModel(sIndex : 38, pIndex : 9, desription: "DV Noise, m/s", types: new Type[]{ typeof(double) }),
            new NasaFileTreeModel(sIndex : 38, pIndex : 10, desription: "Initial Bias (m/s^2)", types: new Type[]{ typeof(double) }),
            new NasaFileTreeModel(sIndex : 38, pIndex : 11, desription: "Flex Node Index", types: new Type[]{ typeof(uint) }),


        };


        #endregion

        #region 매소드
        public List<NasaFileTreeModel> LoadFile(string filePath)
        {
            List<NasaFileTreeModel> dicPairs = new List<NasaFileTreeModel>();

            try
            {
                string[] lines = File.ReadAllLines(filePath);

                string textTitle = string.Empty;

                int sIndex = 0;
                int pIndex = 0;

                List<NasaFileTreeModel> treeModels = null;

                foreach (string line in lines)
                {
                    if (line.Contains("<<<"))
                    {
                        string textTitleBuf = GetTiTleOfSectionInfo(line, new string[] { "<", ">" });

                        if (!string.IsNullOrEmpty(textTitleBuf))
                        {
                            treeModels = GetNasaFileTreeModel(textTitleBuf).ToList();

                            dicPairs.Add(treeModels[0]);
                        }
                    }
                    else
                    {
                        if (treeModels != null)
                        {
                            if (line.Contains("***"))
                            {
                                string textSectionBuf = GetTiTleOfSectionInfo(line, new string[] { "*" });

                                if (!string.IsNullOrEmpty(textSectionBuf))
                                {
                                    sIndex++;

                                    var propertyInfo = treeModels.Find(x => x.SIndex == sIndex && x.PIndex == -1);

                                    string name = propertyInfo.Values[0];

                                    if (textSectionBuf.Equals(name.Trim()))
                                    {
                                        dicPairs.Add(propertyInfo);
                                        pIndex = 0;
                                    }
                                    else
                                    {
                                        LogManager.SharedInstance.Fatal(string.Format("LoadFile section not matched * {0} != {1}", textSectionBuf, name));
                                        break;
                                    }
                                }

                            }
                            else if (line.Contains(":::"))
                            {
                                string textSectionBuf = GetTiTleOfSectionInfo(line, new string[] { ":" });

                                if (!string.IsNullOrEmpty(textSectionBuf))
                                {
                                    sIndex++;

                                    var propertyInfo = treeModels.Find(x => x.SIndex == sIndex && x.PIndex == -1);

                                    string name = propertyInfo.Values[0];

                                    if (textSectionBuf.Equals(name.Trim()))
                                    {
                                        dicPairs.Add(propertyInfo);
                                        pIndex = 0;
                                    }
                                    else
                                    {
                                        LogManager.SharedInstance.Fatal(string.Format("LoadFile section not matched : {0} != {1}", textSectionBuf, name));
                                        break;
                                    }
                                }
                            }
                            else if (line.Contains("==="))
                            {
                                string textSectionBuf = GetTiTleOfSectionInfo(line, new string[] { "=" });

                                if (!string.IsNullOrEmpty(textSectionBuf))
                                {
                                    sIndex++;

                                    var propertyInfo = treeModels.Find(x => x.SIndex == sIndex && x.PIndex == -1);

                                    string name = propertyInfo.Values[0];

                                    if (textSectionBuf.Equals(name.Trim()))
                                    {
                                        dicPairs.Add(propertyInfo);
                                        pIndex = 0;
                                    }
                                    else
                                    {
                                        LogManager.SharedInstance.Fatal(string.Format("LoadFile section not matched = {0} != {1}", textSectionBuf, name));
                                        break;
                                    }
                                }
                            }
                            else if (line.Contains("!"))
                            {
                                string[] textPropertyBuf = line.Split('!');

                                if (textPropertyBuf.Length == 2)
                                {
                                    pIndex++;

                                    var propertyInfo = treeModels.Find(x => x.SIndex == sIndex && x.PIndex == pIndex);

                                    if (textPropertyBuf[1].Trim().Equals(propertyInfo.Desription.Trim()))
                                    {
                                        List<string> valueList = new List<string>();

                                        if (propertyInfo.Types.Length > 1)
                                        {
                                            string[] values = textPropertyBuf[0].Split(' ');

                                            foreach (var item in values)
                                            {
                                                if (string.IsNullOrEmpty(item) || item.Equals(" "))
                                                {
                                                    continue;
                                                }
                                                else
                                                {
                                                    valueList.Add(item.Trim());
                                                }
                                            }

                                        }
                                        else if (propertyInfo.Types.Length == 1)
                                        {
                                            valueList.Add(textPropertyBuf[0].Trim());
                                        }
                                        else
                                        {
                                            LogManager.SharedInstance.Fatal(string.Format("LoadFile propertyInfo type length error {0}", propertyInfo.Types.Length));
                                        }

                                        dicPairs.Add(new NasaFileTreeModel(sIndex, pIndex, valueList.ToArray(), propertyInfo.Types, propertyInfo.Desription));
                                    }
                                    else
                                    {
                                        LogManager.SharedInstance.Fatal(string.Format("LoadFile property not matched {0} != {1}", textPropertyBuf, propertyInfo.Desription));
                                        break;
                                    }
                                }
                                else
                                {
                                    LogManager.SharedInstance.Fatal(string.Format("LoadFile property format error {0}", line));
                                }

                            }
                            else
                            {
                                LogManager.SharedInstance.Fatal("LoadFile not found any reserved character");
                            }
                        }
                        else
                        {
                            LogManager.SharedInstance.Fatal("LoadFile treeModels null");
                        }

                    }
                }

                return dicPairs;
            }
            catch (Exception ex)
            {
                LogManager.SharedInstance.Fatal(string.Empty, ex);

                return null;
            }
        }

        public void SaveFile(string filePath, List<NasaFileTreeModel> data)
        {
            try
            {
                List<string> outLines = new List<string>();

                foreach (var item in data)
                {
                    if (item.PIndex == -1)
                    {
                        if (item.Values.Length == 2)
                        {
                            outLines.Add(GetSectionLine(item.Values[0], item.Values[1], item.Values[1]));
                        }
                        else if (item.Values.Length == 3)
                        {
                            outLines.Add(GetSectionLine(item.Values[0], item.Values[1], item.Values[2]));
                        }
                        else
                        {
                            LogManager.SharedInstance.Fatal(string.Format("SaveFile item.Values.Length error {0},{1},{2}", item.SIndex, item.PIndex, item.Values.Length));
                        }
                    }
                    else if (item.PIndex >= 1)
                    {
                        string outLineValue = string.Join(" ", item.Values);

                        string outLine = string.Format("{0} \t\t ! {1}", outLineValue, item.Desription);

                        outLines.Add(outLine);
                    }
                    else
                    {
                        LogManager.SharedInstance.Fatal(string.Format("SaveFile PIndex error {0},{1},{2}", item.SIndex, item.PIndex, item.Desription));
                    }

                }

                using (StreamWriter outputFile = new StreamWriter(filePath))
                {
                    foreach (string line in outLines.ToArray())
                        outputFile.WriteLine(line);
                }
            }
            catch (Exception ex)
            {
                LogManager.SharedInstance.Fatal(string.Empty, ex);
            }
        }

        private string GetSectionLine(string sectionName, string preCharacter, string postCharacter)
        {
            try
            {
                string result = string.Empty;

                for (int i = 0; i < 10; i++)
                {
                    result += preCharacter;
                }

                result += " " + sectionName + " ";

                for (int i = 0; i < 10; i++)
                {
                    result += postCharacter;
                }

                return result;
            }
            catch (Exception ex)
            {
                LogManager.SharedInstance.Fatal(string.Empty, ex);

                return null;
            }
        }

        private string GetTiTleOfSectionInfo(string targetString, string[] replaceStringArray)
        {
            string result = string.Empty;

            try
            {
                if (!string.IsNullOrEmpty(targetString))
                {
                    result = targetString;

                    foreach (string item in replaceStringArray)
                    {
                        result = result.Replace(item, "");
                    }

                    result = result.Trim();
                }
                else
                {
                    LogManager.SharedInstance.Fatal("GetTiTleOfSectionInfo targetString empty");
                }

            }
            catch (Exception ex)
            {
                LogManager.SharedInstance.Fatal(string.Empty, ex);
            }

            return result;
        }

        private NasaFileTreeModel[] GetNasaFileTreeModel(string title)
        {
            try
            {
                if (dicTreeSim[0].Values[0].Equals(title))
                {
                    return dicTreeSim;
                }
                else if (dicTreeOrb[0].Values[0].Equals(title))
                {
                    return dicTreeOrb;
                }
                else if (dicTreeSC[0].Values[0].Equals(title))
                {
                    return dicTreeSC;
                }
            }
            catch (Exception ex)
            {
                LogManager.SharedInstance.Fatal(string.Empty, ex);
            }

            return null;
        }
        #endregion
    }

    public class NasaFileTreeModel
    {
        // Section Index 
        // -1       = None
        // 0        = Title
        // n > 0    = Section
        public int SIndex { get; protected set; }

        // Property Index
        // n <= 0   = Not Property
        // n > 0    = Property
        public int PIndex { get; protected set; }

        public string[] Values { get; set; } = null;
        public Type[] Types { get; protected set; } = null;
        public string Desription { get; protected set; }

        public NasaFileTreeModel(int sIndex = -1, int pIndex = -1, string[] values = null, Type[] types = null, string desription = "")
        {
            SIndex = sIndex;
            PIndex = pIndex;
            Values = values;
            Types = types;
            Desription = desription;
        }

    }

}
