using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace SandBox
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private BindingList<MyClassT> rootObjects = new BindingList<MyClassT>();
        public BindingList<MyClassT> RootObjects
        {
            get { return this.rootObjects; }
        }

        private void My_Loaded(object sender, RoutedEventArgs e)
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


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CallValueTypeCompareMethod();

        }

        private void CallTypeTestMethod()
        {
            List<TestClass> test = new List<TestClass>();

            for (int i = 0; i < 700; i++)
            {
                TestClass tempclass = new TestClass() { Name = string.Format("Test_{0:D3}", i) };

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

            Queue<TestClass> tempQueue = new Queue<TestClass>();
            callMeEvent(tempQueue, findStr);

            #region test
            st.Stop();
            Console.Write("EventCall {0} [{1}]\t", st.ElapsedMilliseconds, st.ElapsedTicks);

            st.Reset();
            st.Start();
            #endregion

            TestClass temp1 = test.Find(x => x.Name.Equals(findStr));

            #region test
            st.Stop();
            Console.Write("Lamda {0} [{1}]\t", st.ElapsedMilliseconds, st.ElapsedTicks);

            st.Reset();
            st.Start();
            #endregion

            TestClass temp2 = test.Find(x => x.Name.ToLower().Equals(findStrLow));

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

        public delegate void CallMeDelegate(Queue<TestClass> que, string name);
        public event CallMeDelegate callMeEvent;

        private void CallValueTypeCompareMethod()
        {
            List<TestClass> test = new List<TestClass>();

            for (int i = 0; i < 700; i++)
            {
                TestClass tempclass = new TestClass() { Name = string.Format("Test_{0:D3}", i), testValue1 = i.ToString(), testValue2 = i };

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
    }

    public class TestClass
    {
        private string _name = "";
        public string Name 
        {
            get { return _name; }
            set
            {
                if(_name != value)
                {
                    _name = value;

                }
            }
        }

        public int value { get; set; } = 0;

        public string testValue1 { get; set; } = "0.0";

        public double testValue2 { get; set; } = 0.0;



        public void callMe(Queue<TestClass> que, string name)
        {
            if(Name.Equals(name))
            {
                que.Enqueue(this);

                value = 1;
            }
        }


    }

    public interface ITreeViewParentLink
    {
        TreeViewItem ParentItem { get; set; }
    }

    public class MyClassT : ViewModelBase, ITreeViewParentLink
    {
        TreeViewItem parentItem;
        public TreeViewItem ParentItem
        {
            get { return this.parentItem; }
            set { this.parentItem = value; }
        }

        public MyClassT ParentData
        {
            get
            {
                if (this.parentItem == null)
                {
                    return null;
                }

                return this.parentItem.DataContext as MyClassT;
            }
        }

        private BindingList<MyClassT> children;
        public BindingList<MyClassT> Children
        {
            get { return this.children; }
            set
            {
                this.children = value;
                OnPropertyChanged();
            }
        }

        /// <remarks/>
        private string name;
        public string Name
        {
            get { return this.name; }
            set { this.name = value; OnPropertyChanged(); }
        }
    }

    public class ParentChildTreeView : TreeView
    {
        public class ParentChildTreeViewItem : TreeViewItem
        {
            public ParentChildTreeViewItem()
                : base()
            {
            }

            protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
            {
                base.PrepareContainerForItemOverride(element, item);

                ITreeViewParentLink support = item as ITreeViewParentLink;
                if (support == null)
                {
                    return;
                }

                support.ParentItem = ItemsControl.ItemsControlFromItemContainer(element) as TreeViewItem;
            }

            protected override DependencyObject GetContainerForItemOverride()
            {
                return new ParentChildTreeViewItem();
            }
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new ParentChildTreeViewItem();
        }
    }

    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected bool Set<T>(ref T field, T newValue = default(T), string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(newValue))
            {
                return false;
            }
            field = newValue;
            OnPropertyChanged(propertyName);
            return true;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
