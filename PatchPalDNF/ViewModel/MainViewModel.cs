using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using PatchPalDNF.Views;
using PatchPalDNF.Command;

namespace PatchPalDNF.ViewModel
{
    public class MainViewModel
    {
        public ICommand OpenWindowCommand { get; set; }

        // 将窗口声明为类级别成员变量
        private AddNewPatchBrief newWindow;

        public MainViewModel()
        {
            OpenWindowCommand = new RelayCommand(OpenWindow);
        }

        /// <summary>
        /// 打开新的窗口
        /// </summary>
        /// <param name="parameter"></param>
        private void OpenWindow(object parameter)
        {
            // 如果窗口未打开或已关闭，重新创建窗口
            if (newWindow == null || !newWindow.IsLoaded)
            {
                newWindow = new AddNewPatchBrief();
                newWindow.DataContext = new AddNewPatchBriefViewModel(newWindow);
                newWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;

                // 设置窗口关闭时清理 newWindow 引用
                newWindow.Closed += (sender, e) => newWindow = null;

                newWindow.ShowDialog();
            }
            else
            {
                // 如果窗口已经打开，激活它
                newWindow.Activate();
            }
        }
    }
}
