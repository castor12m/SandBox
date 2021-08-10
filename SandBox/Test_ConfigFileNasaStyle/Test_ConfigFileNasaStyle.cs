using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SandBox
{
    public class Test_ConfigFileNasaStyle
    {
        public static Test_ConfigFileNasaStyle instance = new Test_ConfigFileNasaStyle();

        private NasaFileTreeModel[] dicTreeSim = new NasaFileTreeModel[]
        {
            new NasaFileTreeModel(sIndex : 0, values : new string[] { "42: The Mostly Harmless Simulator" }),

            new NasaFileTreeModel(sIndex : 1, values : new string[] { "Simulation Control" }),
            new NasaFileTreeModel(sIndex : 1, pIndex : 1, desription: "Time Mode (FAST, REAL, or EXTERNAL)", types: new Type[]{ typeof(string) }),
            new NasaFileTreeModel(sIndex : 1, pIndex : 2, desription: "Sim Duration, Step Size [sec]", types: new Type[]{ typeof(double), typeof(double) }),
            new NasaFileTreeModel(sIndex : 1, pIndex : 3, desription: "File Output Interval [sec]", types: new Type[]{ typeof(double) }),
            new NasaFileTreeModel(sIndex : 1, pIndex : 4, desription: "Graphics Front End?", types: new Type[]{ typeof(bool) }),
            new NasaFileTreeModel(sIndex : 1, pIndex : 5, desription: "Command Script File Name", types: new Type[]{ typeof(string) }),

            new NasaFileTreeModel(sIndex : 2, values : new string[] { "Reference Orbits" }),
            new NasaFileTreeModel(sIndex : 2, pIndex : 1, desription: "Number of Reference Orbits", types: new Type[]{ typeof(uint) }),
            new NasaFileTreeModel(sIndex : 2, pIndex : 2, desription: "Input file name for Orb 0", types: new Type[]{ typeof(bool), typeof(string) }),

            new NasaFileTreeModel(sIndex : 3, values : new string[] { "Spacecraft" }),
            new NasaFileTreeModel(sIndex : 3, pIndex : 1, desription: "Number of Spacecraft", types: new Type[]{ typeof(uint) }),
            new NasaFileTreeModel(sIndex : 3, pIndex : 2, desription: "Existence, RefOrb, Input file for SC 0", types: new Type[]{ typeof(bool), typeof(uint), typeof(string) }),

            new NasaFileTreeModel(sIndex : 4, values : new string[] { "Environment" }),
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

            new NasaFileTreeModel(sIndex : 5, values : new string[] { "Celestial Bodies of Interest" }),
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

            new NasaFileTreeModel(sIndex : 6, values : new string[] { "Lagrange Point Systems of Interest" }),
            new NasaFileTreeModel(sIndex : 6, pIndex : 1, desription: "Earth-Moon", types: new Type[]{ typeof(bool) }),
            new NasaFileTreeModel(sIndex : 6, pIndex : 2, desription: "Sun-Earth", types: new Type[]{ typeof(bool) }),
            new NasaFileTreeModel(sIndex : 6, pIndex : 3, desription: "Sun-Jupiter", types: new Type[]{ typeof(bool) }),

            new NasaFileTreeModel(sIndex : 7, values : new string[] { "Ground Stations" }),
            new NasaFileTreeModel(sIndex : 7, pIndex : 1, desription: "Number of Ground Stations", types: new Type[]{ typeof(uint) }),
            new NasaFileTreeModel(sIndex : 7, pIndex : 2, desription: "Exists, World, Lng, Lat, Label", types: new Type[]{ typeof(bool), typeof(string), typeof(double), typeof(string), typeof(string) }),

        };

        public List<NasaFileTreeModel> LoadFile(string filePath)
        {
            List<NasaFileTreeModel> dicPairs = new List<NasaFileTreeModel>();

            try
            {
                string[] lines = System.IO.File.ReadAllLines(filePath);

                string textTitle = string.Empty;
                
                int sIndex = 0;
                int pIndex = 0;

                List<NasaFileTreeModel> treeModels = null;

                foreach (string line in lines)
                {
                    if(line.Contains("<<<"))
                    {
                        string textTitleBuf = GetTiTleOfSectionInfo(line, new string[] { "<", ">" });

                        if(!string.IsNullOrEmpty(textTitleBuf))
                        {
                            treeModels = GetNasaFileTreeModel(textTitleBuf).ToList();
                        }
                    }
                    else
                    {
                        if(treeModels != null)
                        {
                            if (line.Contains("***"))
                            {
                                string textSectionBuf = GetTiTleOfSectionInfo(line, new string[] { "*" });

                                if(!string.IsNullOrEmpty(textSectionBuf))
                                {
                                    sIndex++;

                                    string name = treeModels.Find(x => x.SIndex == sIndex && x.PIndex == -1).Values[0];

                                    if (textSectionBuf.Equals(name))
                                    {
                                        pIndex = 0;
                                    }
                                    else
                                    {
                                        Console.WriteLine(string.Format("LoadFile section not matched * {0} != {1}", textSectionBuf, name));
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

                                    string name = treeModels.Find(x => x.SIndex == sIndex && x.PIndex == -1).Values[0];

                                    if (textSectionBuf.Equals(name))
                                    {
                                        pIndex = 0;
                                    }
                                    else
                                    {
                                        Console.WriteLine(string.Format("LoadFile section not matched * {0} != {1}", textSectionBuf, name));
                                        break;
                                    }
                                }
                            }
                            else if(line.Contains("!"))
                            {
                                string[] textPropertyBuf = line.Split('!');

                                if(textPropertyBuf.Length == 2)
                                {
                                    pIndex++;

                                    var propertyInfo = treeModels.Find(x => x.SIndex == sIndex && x.PIndex == pIndex);

                                    if (textPropertyBuf[1].Trim().Equals(propertyInfo.Desription))
                                    {
                                        string[] values = textPropertyBuf[0].Split(' ');

                                        List<string> valueList = new List<string>();

                                        foreach (var item in values)
                                        {
                                            if(string.IsNullOrEmpty(item) || item.Equals(" "))
                                            {
                                                continue;
                                            }
                                            else
                                            {
                                                valueList.Add(item);
                                            }
                                        }

                                        dicPairs.Add(new NasaFileTreeModel(sIndex, pIndex, valueList.ToArray(), propertyInfo.Types, propertyInfo.Desription));
                                    }
                                    else
                                    {
                                        Console.WriteLine(string.Format("LoadFile property not matched {0} != {1}", textPropertyBuf, propertyInfo.Desription));
                                        break;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine(string.Format("LoadFile property format error {0}", line));
                                }
                                
                            }
                        }
                        else
                        {
                            Console.WriteLine("LoadFile treeModels null");
                        }
                        
                    }
                }

                return dicPairs;
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}", ex.Message);

                return null;
            }
        }

        private string GetTiTleOfSectionInfo(string targetString, string[] replaceStringArray)
        {
            string result = string.Empty;

            try
            {
                if(!string.IsNullOrEmpty(targetString))
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
                    Console.WriteLine("GetTiTleOfSectionInfo targetString empty");
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}", ex.Message);
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
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}", ex.Message);
            }

            return null;
        }

        //public static List<Component> LoadSaveFile(string resource_data)
        //{
        //    List<Component> components = new List<Component>();

        //    string[] SCFile = resource_data.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
        //    int CurrentLine = 0;

        //    if (SCFile[CurrentLine].IndexOf("<<<") != -1)
        //    {
        //        string subsystemName = SCFile[CurrentLine].Replace("<", "").Trim();
        //        CurrentLine++;
        //        while (CurrentLine < SCFile.Length)
        //        {
        //            if (SCFile[CurrentLine].IndexOf("***") != -1)
        //            {
        //                components.Add(LoadData(SCFile, ref CurrentLine));
        //            }
        //            else
        //            {
        //                CurrentLine++;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine("Wrong File");
        //    }

        //    return components;
        //}

        //private static Component LoadData(string[] SCdata, ref int CurrentLine)
        //{
        //    int componentCount = -1;
        //    bool isSettingData = true;

        //    int dataCount = 1;
        //    Component_t component_t = new Component_t();
        //    component_t.CompName = new string[100];
        //    component_t.CompData = new double[100];
        //    component_t.CompUnit = new string[100];
        //    component_t.CompDisplay = new double[100];
        //    component_t.CompTooltip = new string[100];

        //    component_t.SetName = new string[100];
        //    component_t.SetData = new double[100];
        //    component_t.SetUnit = new string[100];
        //    component_t.SetTooltip = new string[100];

        //    string subsystemName = SCdata[CurrentLine++].Replace("*", "").Trim();
        //    Component components = new Component(subsystemName);

        //    string[] num = SCdata[CurrentLine].Split(new[] { "!" }, StringSplitOptions.RemoveEmptyEntries);
        //    components.components_t = new Component_t[int.Parse(num[0].Trim())];
        //    CurrentLine++;

        //    if (components.components_t.Length != 0)
        //    {
        //        for (; CurrentLine < SCdata.Length; CurrentLine++)
        //        {
        //            if (SCdata[CurrentLine].IndexOf("===") != -1)
        //            {
        //                if (componentCount != -1)
        //                {
        //                    setData(component_t, isSettingData, componentCount, dataCount, ref components);
        //                }
        //                componentCount++;
        //                isSettingData = true;
        //                dataCount = 0;

        //                components.components_t[componentCount] = new Component_t();
        //                components.components_t[componentCount].Name = SCdata[CurrentLine].Replace("=", "").Trim();
        //            }
        //            else if (SCdata[CurrentLine].IndexOf("###") != -1)
        //            {
        //                setData(component_t, isSettingData, componentCount, dataCount, ref components);
        //                isSettingData = false;
        //                dataCount = 0;
        //            }
        //            else if (SCdata[CurrentLine].IndexOf("***") != -1)
        //            {
        //                setData(component_t, isSettingData, componentCount, dataCount, ref components);
        //                break;
        //            }
        //            else
        //            {
        //                string[] dataSplited = SCdata[CurrentLine].Split(new[] { "!" }, StringSplitOptions.RemoveEmptyEntries);
        //                if (isSettingData)
        //                {
        //                    component_t.SetData[dataCount] = double.Parse(dataSplited[0].Trim());
        //                    component_t.SetTooltip[dataCount] = dataSplited[1].Trim();
        //                    component_t.SetName[dataCount] = dataSplited[2].Trim();
        //                    if (dataSplited[3].Trim().Equals("-"))
        //                    {
        //                        component_t.SetUnit[dataCount] = "";
        //                    }
        //                    else
        //                    {
        //                        component_t.SetUnit[dataCount] = dataSplited[3].Trim();
        //                    }
        //                }
        //                else
        //                {
        //                    component_t.CompData[dataCount] = double.Parse(dataSplited[0].Trim());
        //                    component_t.CompTooltip[dataCount] = dataSplited[1].Trim();
        //                    component_t.CompName[dataCount] = dataSplited[2].Trim();
        //                    if (dataSplited[3].Trim().Equals("-"))
        //                    {
        //                        component_t.CompUnit[dataCount] = "";
        //                    }
        //                    else
        //                    {
        //                        component_t.CompUnit[dataCount] = dataSplited[3].Trim();
        //                    }
        //                    component_t.CompDisplay[dataCount] = double.Parse(dataSplited[4].Trim());
        //                }
        //                dataCount++;
        //            }
        //        }
        //        if (CurrentLine == SCdata.Length)
        //        {
        //            setData(component_t, isSettingData, componentCount, dataCount, ref components);
        //        }
        //    }
        //    return components;
        //}

        //static private void setData(Component_t component_t, bool isSettingData,
        //                            int componentCount, int dataCount,
        //                            ref Component components)
        //{
        //    if (isSettingData)
        //    {
        //        components.components_t[componentCount].SetName = new string[dataCount];
        //        components.components_t[componentCount].SetData = new double[dataCount];
        //        components.components_t[componentCount].SetUnit = new string[dataCount];
        //        components.components_t[componentCount].SetTooltip = new string[dataCount];

        //        Array.Copy(component_t.SetName, components.components_t[componentCount].SetName, dataCount);
        //        Array.Copy(component_t.SetData, components.components_t[componentCount].SetData, dataCount);
        //        Array.Copy(component_t.SetUnit, components.components_t[componentCount].SetUnit, dataCount);
        //        Array.Copy(component_t.SetTooltip, components.components_t[componentCount].SetTooltip, dataCount);
        //    }
        //    else
        //    {
        //        components.components_t[componentCount].CompName = new string[dataCount];
        //        components.components_t[componentCount].CompData = new double[dataCount];
        //        components.components_t[componentCount].CompUnit = new string[dataCount];
        //        components.components_t[componentCount].CompDisplay = new double[dataCount];
        //        components.components_t[componentCount].CompTooltip = new string[dataCount];

        //        Array.Copy(component_t.CompName, components.components_t[componentCount].CompName, dataCount);
        //        Array.Copy(component_t.CompData, components.components_t[componentCount].CompData, dataCount);
        //        Array.Copy(component_t.CompUnit, components.components_t[componentCount].CompUnit, dataCount);
        //        Array.Copy(component_t.CompDisplay, components.components_t[componentCount].CompDisplay, dataCount);
        //        Array.Copy(component_t.CompTooltip, components.components_t[componentCount].CompTooltip, dataCount);
        //    }
        //}
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
