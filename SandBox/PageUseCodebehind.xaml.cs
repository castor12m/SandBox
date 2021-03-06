#define Test_CallAppWithSSH
#define Test_ConfigFileNasaStyle
#define Test_PlayByTimeSeriesForHogaPlay

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SandBox
{
    /// <summary>
    /// PageUseCodebehind.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class PageUseCodebehind : Page
    {
        public PageUseCodebehind()
        {
            InitializeComponent();
        }

        private void My_Loaded(object sender, RoutedEventArgs e)
        {
            InitTreeView();

            InitButtonName();
        }

        private void InitButtonName()
        {

#if Test_SimModuleDevConcept
            btnText1.Content = "PowerOn";
            btnText2.Content = "Run";
            btnText3.Content = "PowerOff";
#endif
        }

        #region Test_TreeView

        private BindingList<MyClassT> rootObjects = new BindingList<MyClassT>();
        public BindingList<MyClassT> RootObjects
        {
            get { return this.rootObjects; }
        }

        private void InitTreeView()
        {
            for (int i = 0; i < 2; i++)
            {
                MyClassT item = new MyClassT();
                item.Name = "test root 0 tree view item " + i.ToString();
                item.Children = new BindingList<MyClassT>();
                this.rootObjects.Add(item);

                for (int j = 0; j < 5; j++)
                {
                    MyClassT item2 = new MyClassT();
                    item2.Name = "test tree view item " + i.ToString();
                    item.Children.Add(item2);
                }
            }
        }

        private void treeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            MyClassT myClass = e.NewValue as MyClassT;
            if (myClass.ParentData == null)
            {
                return;
            }

            Debug.WriteLine(myClass.ParentData.Name);
        }
        #endregion

        #region Test_CallMethod
        private void CallTypeTestMethod()
        {
            List<Test_XX_CallMethod> test = new List<Test_XX_CallMethod>();

            for (int i = 0; i < 700; i++)
            {
                Test_XX_CallMethod tempclass = new Test_XX_CallMethod() { Name = string.Format("Test_{0:D3}", i) };

                test.Add(tempclass);

                callMeEvent += tempclass.callMe;
            }

            string findStr = "Test_350";
            string findStrLow = findStr.ToLower();

            #region MyRegion
            Stopwatch st = new Stopwatch();
            st.Reset();
            st.Start();
            #endregion

            Queue<Test_XX_CallMethod> tempQueue = new Queue<Test_XX_CallMethod>();
            callMeEvent(tempQueue, findStr);

            #region test
            st.Stop();
            Console.Write("EventCall {0} [{1}]\t", st.ElapsedMilliseconds, st.ElapsedTicks);

            st.Reset();
            st.Start();
            #endregion

            //TestClass temp1 = test.Find(x => x.Name.Equals(findStr));
            Test_XX_CallMethod temp1 = test.Find(x => x.Name.Equals(findStrLow, StringComparison.CurrentCultureIgnoreCase));

            #region test
            st.Stop();
            Console.Write("Lamda {0} [{1}]\t", st.ElapsedMilliseconds, st.ElapsedTicks);

            st.Reset();
            st.Start();
            #endregion

            Test_XX_CallMethod temp2 = test.Find(x => x.Name.ToLower().Equals(findStrLow));

            #region test
            st.Stop();
            Console.Write("Lamda Lower {0} [{1}]\t", st.ElapsedMilliseconds, st.ElapsedTicks);

            st.Reset();
            st.Start();
            #endregion

            var temp3 =
                (from testitem in test
                 where testitem.Name == findStr
                 select testitem).FirstOrDefault();

            #region test
            st.Stop();
            Console.Write("LINQ {0} [{1}]\t", st.ElapsedMilliseconds, st.ElapsedTicks);

            st.Reset();
            st.Start();
            #endregion

            var temp4 =
               (from testitem in test
                where testitem.Name.ToLower() == findStrLow
                select testitem).FirstOrDefault();

            #region test
            st.Stop();
            Console.WriteLine("LINQ tolower {0} [{1}]", st.ElapsedMilliseconds, st.ElapsedTicks);
            #endregion

        }

        public delegate void CallMeDelegate(Queue<Test_XX_CallMethod> que, string name);
        public event CallMeDelegate callMeEvent;

        private void CallValueTypeCompareMethod()
        {
            List<Test_XX_CallMethod> test = new List<Test_XX_CallMethod>();

            for (int i = 0; i < 700; i++)
            {
                Test_XX_CallMethod tempclass = new Test_XX_CallMethod() { Name = string.Format("Test_{0:D3}", i), testValue1 = i.ToString(), testValue2 = i };

                test.Add(tempclass);
            }

            Stopwatch st = new Stopwatch();
            st.Reset();
            st.Start();

            double sum = 0.0;

            foreach (var item in test)
            {
                sum += double.Parse(item.testValue1);
            }

            st.Stop();
            Console.WriteLine("Test {0} [{1}]\t", st.ElapsedMilliseconds, st.ElapsedTicks);
            st.Reset();
            st.Start();

            double sum2 = 0.0;

            foreach (var item in test)
            {
                sum2 += item.testValue2;
            }

            st.Stop();
            Console.WriteLine("Test {0} [{1}]\t", st.ElapsedMilliseconds, st.ElapsedTicks);
        }
        #endregion


        private void Button_Click(object sender, RoutedEventArgs e)
        {

            #region MyRegion
#if Test_CallAppWithSSH

            Test_CB_CallAppWithSSH.SharedInstance.DoSomething();
#endif
            #endregion

            #region Test_ConfigFileNasaStyle
#if Test_ConfigFileNasaStyle
            //var temp = Test_ConfigFileNasaStyle.instance.LoadFile(@"C:\Users\Garfield\Desktop\ConfigFileNasaStyle\Sample_SC_xx.txt");
            //Test_ConfigFileNasaStyle.instance.SaveFile(@"C:\Users\Garfield\Desktop\temp.txt", temp);
            Stopwatch st = new Stopwatch();
            st.Reset();
            st.Start();
            var temp = Test_CB_ConfigFileNasaStyle.SharedInstance.LoadFile(@"C:\Users\Garfield\Desktop\temp.txt");
            st.Stop();
#endif
            #endregion

            #region Test_PlayByTimeSeriesForHogaPlay
#if Test_PlayByTimeSeriesForHogaPlay
            if (Test_CB_PlayBYTimeSeriesForHogaPlay.SharedInstance.bgWorkRunRequest)
            {
                Test_CB_PlayBYTimeSeriesForHogaPlay.SharedInstance.Stop();
            }
            else
            {
                Test_CB_PlayBYTimeSeriesForHogaPlay.SharedInstance.Start();
            }
#endif
            #endregion


        }
    }
}
