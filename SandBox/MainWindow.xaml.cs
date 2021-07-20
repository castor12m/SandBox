using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
