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
using System.Windows.Data;
using Microsoft.Win32;

namespace PatchPalDNF.ViewModel
{

    public class MainViewModel : INotifyPropertyChanged
    {
        //项目json文件路径
        public static string JsonFilePath;
        //玩家DNF文件路径
        public static string DnfFilePath;
        //玩家DNF备用文件路径
        public static string DnfBackupFilePath;


        //打开新窗体命令
        public ICommand OpenWindowCommand { get; set; }
        //检索命令
        public ICommand SearchCommand { get; set; }

        public ICommand NpkStatusClickCommand { get; set; }

        // 将窗口声明为类级别成员变量
        private AddNewPatchBrief newWindow;

        // 数据操作类
        private DataServer dataServer;

        // 用于绑定过滤后的数据
        private ICollectionView _patchBriefsView;  

        private ObservableCollection<PatchModel> _patchBriefs;

        public ObservableCollection<PatchModel> PatchBriefs
        {
            get { return _patchBriefs; }
            set
            {
                _patchBriefs = value;
                OnPropertyChanged(nameof(PatchBriefs));
                _patchBriefsView = CollectionViewSource.GetDefaultView(_patchBriefs);  // 包装成 ICollectionView
            }
        }


        public string QueryText { get; set; }

        public ICollectionView PatchBriefsView
        {
            get { return _patchBriefsView; }
        }

        public MainViewModel()
        {
            dataServer = new DataServer();
            dataServer.initProject();
            OpenWindowCommand = new RelayCommand(OpenWindow);
            SearchCommand = new RelayCommand(Search);
            NpkStatusClickCommand = new RelayCommand(NpkStatusClick);
            PatchBriefs = new ObservableCollection<PatchModel>(dataServer.LoadPatches());
        }

        /// <summary>
        /// 启用禁用命令
        /// </summary>
        /// <param name="parameter"></param>
        private void NpkStatusClick(object parameter)
        {
            var patch = parameter as PatchModel;
            var fileName = patch.NpkLocalURL.Select(x => System.IO.Path.GetFileName(x)).ToList();
            if (patch.NpkStatus)
            {
                dataServer.EnableNPK(fileName); 
            }
            else
            {
                dataServer.DisableNPK(fileName);
            }
        }

        // 检索功能
        private void Search(object parameter)
        {

            _patchBriefsView.Filter = obj =>
            {
                var patch = obj as PatchModel;
                if (QueryText == null)
                {
                    QueryText = "";
                }
                return patch != null && patch.NpkName.Contains(QueryText);
            };
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
