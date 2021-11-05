using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SandBox
{
    public class Test_XX_VM : ViewModelBase
    {
        #region 맴버변수

        private string _bindingTest0 = "Function 0";
        public string BindingTest0
        {
            get { return _bindingTest0; }
            set { _bindingTest0 = value; OnPropertyChanged(); }
        }

        private string _bindingTest1 = "Function 1";
        public string BindingTest1
        {
            get { return _bindingTest1; }
            set { _bindingTest1 = value; OnPropertyChanged(); }
        }

        private string _bindingTest2 = "Function 2";
        public string BindingTest2
        {
            get { return _bindingTest2; }
            set { _bindingTest2 = value; OnPropertyChanged(); }
        }

        private string _bindingTest3 = "Function 3";
        public string BindingTest3
        {
            get { return _bindingTest3; }
            set { _bindingTest3 = value; OnPropertyChanged(); }
        }

        private string _bindingTest4 = "Function 4";
        public string BindingTest4
        {
            get { return _bindingTest4; }
            set { _bindingTest4 = value; OnPropertyChanged(); }
        }

        private string _bindingTest5 = "Function 5";
        public string BindingTest5
        {
            get { return _bindingTest5; }
            set { _bindingTest5 = value; OnPropertyChanged(); }
        }

        private string _loggingText = "Function 6";
        public string LoggingText
        {
            get { return _loggingText; }
            set { _loggingText = value; OnPropertyChanged(); }
        }

        #endregion


        #region 생성자
        public Test_XX_VM()
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

        private void DoTest0()
        {
            try
            {
                Test_XX_SpeedTest_Split_Contain.DoSomething(0);

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
                Test_XX_SpeedTest_Split_Contain.DoSomething(1);
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
                Test_XX_SpeedTest_Split_Contain.DoSomething(2);
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
                Test_XX_SpeedTest_Split_Contain.DoSomething(3);
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
                Test_XX_SpeedTest_Split_Contain.DoSomething(4);
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
                Test_XX_SpeedTest_Split_Contain.DoSomething(5);
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
