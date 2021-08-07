using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace SandBox
{
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
}
