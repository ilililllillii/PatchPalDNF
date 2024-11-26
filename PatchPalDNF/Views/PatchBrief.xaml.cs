using PatchPalDNF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PatchPalDNF.Views
{
    /// <summary>
    /// PatchBrief.xaml 的交互逻辑
    /// </summary>
    public partial class PatchBrief : UserControl
    {
        // 定义向外暴露的 Command 属性
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register(
                "Command",
                typeof(ICommand),
                typeof(PatchBrief),
                new PropertyMetadata(null)
            );

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly DependencyProperty PatchCommandProperty =
            DependencyProperty.Register(
                "PatchCommand",
                typeof(ICommand),
                typeof(PatchBrief),
                new PropertyMetadata(null)
            );

        public ICommand PatchCommand
        {
            get { return (ICommand)GetValue(PatchCommandProperty); }
            set { SetValue(PatchCommandProperty, value); }
        }
        public PatchBrief()
        {
            InitializeComponent();
        }
    }
}
