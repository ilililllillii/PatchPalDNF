using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using PatchPalDNF.Views;
using PatchPalDNF.Command;
using PatchPalDNF.Models;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;
using PatchPalDNF.Server;

namespace PatchPalDNF.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ICommand OpenWindowCommand { get; set; }

        // 将窗口声明为类级别成员变量
        private AddNewPatchBrief newWindow;

        private ObservableCollection<PatchModel> _patchBriefs;

        public ObservableCollection<PatchModel> PatchBriefs
        {
            get { return _patchBriefs; }
            set
            {
                _patchBriefs = value;
                OnPropertyChanged(nameof(PatchBriefs));
            }
        }

        public MainViewModel()
        {
            OpenWindowCommand = new RelayCommand(OpenWindow);
            PatchBriefs = new ObservableCollection<PatchModel>(new DataServer().LoadPatches());
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
                newWindow.DataContext = new AddNewPatchBriefViewModel(newWindow, PatchBriefs);
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

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
