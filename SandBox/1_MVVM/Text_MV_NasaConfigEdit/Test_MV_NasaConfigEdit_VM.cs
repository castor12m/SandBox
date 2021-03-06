using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Text.Json;

namespace SandBox._1_MVVM.Text_MV_NasaConfigEdit
{
    public class Test_MV_NasaConfigEdit_VM : ViewModelBase
    {
        #region 맴버변수

        private string _bindingTest0 = "gen Sim";
        public string BindingTest0
        {
            get { return _bindingTest0; }
            set { _bindingTest0 = value; OnPropertyChanged(); }
        }

        private string _bindingTest1 = "gen Orb";
        public string BindingTest1
        {
            get { return _bindingTest1; }
            set { _bindingTest1 = value; OnPropertyChanged(); }
        }

        private string _bindingTest2 = "gen SC";
        public string BindingTest2
        {
            get { return _bindingTest2; }
            set { _bindingTest2 = value; OnPropertyChanged(); }
        }

        private string _bindingTest3 = "";
        public string BindingTest3
        {
            get { return _bindingTest3; }
            set { _bindingTest3 = value; OnPropertyChanged(); }
        }

        private string _bindingTest4 = "";
        public string BindingTest4
        {
            get { return _bindingTest4; }
            set { _bindingTest4 = value; OnPropertyChanged(); }
        }

        private string _bindingTest5 = "";
        public string BindingTest5
        {
            get { return _bindingTest5; }
            set { _bindingTest5 = value; OnPropertyChanged(); }
        }

        private string _loggingText = "";
        public string LoggingText
        {
            get { return _loggingText; }
            set { _loggingText = value; OnPropertyChanged(); }
        }

        #endregion

        #region 생성자
        public Test_MV_NasaConfigEdit_VM()
        {



        }
        #endregion

        #region 커맨드변수
        public ICommand CmdBindingTest0 => new SimpleCommand(x => true, o => DoTest0());
        public ICommand CmdBindingTest1 => new SimpleCommand(x => true, o => DoTest1());
        public ICommand CmdBindingTest2 => new SimpleCommand(x => true, o => DoTest2());
        public ICommand CmdBindingTest3 => new SimpleCommand(x => true, o => DoTest3());
        public ICommand CmdBindingTest4 => new SimpleCommand(x => true, o => DoTest4());
        public ICommand CmdBindingTest5 => new SimpleCommand(x => true, o => DoTest5());

        //NetMqClient netMqClient;

        Test_MV_SimModuleDevConcept ModuleDevConcept = new Test_MV_SimModuleDevConcept();

        private void DoTest0()
        {
            try
            {

            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}", ex.Message);
            }
        }

        private void DoTest1()
        {
            try
            {
                //List<NasaFileTreeModel> rtn = NasaConfigManager.SharedInstance.LoadFile(@"D:\Workspace\001_Test\Simulator\SampleFile\ConfigFileNasaStyle\Sample_Orb_xx.txt");
                //NasaConfigManager.SharedInstance.SaveFile(@"D:\Workspace\001_Test\Simulator\SampleFile\ConfigFileNasaStyle\Sample_Orb_xx2.txt", rtn);

                //Test_MV_NasaConfigNewList value = new Test_MV_NasaConfigNewList();
                //string jsonstring = JsonSerializer.Serialize(value);


                NasaTextStyle nasaStyle = new NasaTextStyle();


                Console.WriteLine("");
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}", ex.Message);
            }
        }

        private void DoTest2()
        {
            try
            {

            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}", ex.Message);
            }
        }

        private void DoTest3()
        {
            try
            {
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}", ex.Message);
            }
        }

        private void DoTest4()
        {
            try
            {
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}", ex.Message);
            }
        }

        private void DoTest5()
        {
            try
            {

            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}", ex.Message);
            }
        }


        #endregion

        #region 매소드




        #endregion
    }
}
