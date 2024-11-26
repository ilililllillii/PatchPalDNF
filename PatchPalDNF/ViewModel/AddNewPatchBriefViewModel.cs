﻿using PatchPalDNF.Command;
using PatchPalDNF.Models;
using PatchPalDNF.Server;
using PatchPalDNF.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static System.Net.WebRequestMethods;

namespace PatchPalDNF.ViewModel
{
    public class AddNewPatchBriefViewModel : INotifyPropertyChanged
    {
        private readonly AddNewPatchBrief _addNewPatchBrief;

        private ObservableCollection<PatchModel> _patchBriefs;

        private PatchModel PatchModel = new PatchModel();

        private bool isUpDataPatchBrief = false;
        /// <summary>
        /// 名称
        /// </summary>
        public string NpkName
        {
            get => PatchModel.NpkName;
            set
            {
                PatchModel.NpkName = value;
                OnPropertyChanged(nameof(NpkName));
            }
        }
        /// <summary>
        /// npk源连接
        /// </summary>
        public string NpkUrl
        {
            get => PatchModel.NpkUrl;
            set
            {
                PatchModel.NpkUrl = value;
                OnPropertyChanged(nameof(NpkUrl));
            }
        }
        /// <summary>
        /// npk描述
        /// </summary>
        public string NpkDescribe
        {
            get => PatchModel.NpkDescribe;
            set
            {
                PatchModel.NpkDescribe = value;
                OnPropertyChanged(nameof(NpkDescribe));
            }
        }
        /// <summary>
        /// npk效果图
        /// </summary>
        public string NpkImage
        {
            get => PatchModel.NpkImage;
            set
            {
                PatchModel.NpkImage = value;
                OnPropertyChanged(nameof(NpkImage));
            }
        }

        /// <summary>
        /// 本地路径
        /// </summary>
        public List<string> NpkLocalURL
        {
            get => PatchModel.NpkLocalURL;
            set
            {
                PatchModel.NpkLocalURL = value;
                OnPropertyChanged(nameof(NpkLocalURL));
            }
        }

        /// <summary>
        /// 本地路径
        /// </summary>
        public bool NpkStatus
        {
            get => PatchModel.NpkStatus;
            set
            {
                PatchModel.NpkStatus = value;
                OnPropertyChanged(nameof(NpkStatus));
            }
        }

        // 取消命令
        public ICommand CancelCommand { get; }

        // 确定命令
        public ICommand SureCommand { get; }

        // 命令处理 Drop 事件
        public ICommand DropCommand { get; }

        public AddNewPatchBriefViewModel(AddNewPatchBrief addNewPatchBrief, ObservableCollection<PatchModel> patchModels, PatchModel patchModel = null)
        {
            _addNewPatchBrief = addNewPatchBrief;
            _patchBriefs = patchModels;
            if (patchModel != null)
            {
                PatchModel = patchModel;
                isUpDataPatchBrief = true;
            }
            else
            {
                isUpDataPatchBrief = false;
            }
            CancelCommand = new RelayCommand(IsCancel);
            SureCommand = new RelayCommand(IsSure);
            DropCommand = new RelayCommand(OnDrop);
        }

        /// <summary>
        /// 取消操作
        /// </summary>
        private void IsCancel(object parameter)
        {
            // 关闭当前窗口
            _addNewPatchBrief.Close();
        }

        
        /// <summary>
        /// 确定操作
        /// </summary>
        private void IsSure(object parameter)
        {
            if (string.IsNullOrWhiteSpace(MainViewModel.DnfFilePath))
            {
                MessageBox.Show($"你没装DNF你搞什么补丁？");
                return;
            }
            if (isUpDataPatchBrief)
            {
                UpData();
            }
            else
            {
                AddData();
            }
        }

        /// <summary>
        /// 编辑数据
        /// </summary>

        private void UpData()
        {
            //foreach (var file in NpkLocalURL)
            //{
            //    try
            //    {
            //        string fileName = Path.GetFileName(file);
            //        string targetPath = Path.Combine(MainViewModel.DnfFilePath, fileName);
            //        System.IO.File.Copy(file, targetPath, true);
            //        //复制备用文件
            //        System.IO.File.Copy(file, Path.Combine(MainViewModel.DnfBackupFilePath, fileName), true);
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show($"文件复制失败: {ex.Message}");
            //    }
            //}
            //保存数据
            new DataServer().SavePatches(new List<PatchModel>(_patchBriefs));

            _addNewPatchBrief.Close();
        }

        /// <summary>
        /// 新增数据
        /// </summary>
        private void AddData()
        {
            foreach (var file in List)
            {
                try
                {
                    string fileName = Path.GetFileName(file);
                    string targetPath = Path.Combine(MainViewModel.DnfFilePath, fileName);
                    System.IO.File.Copy(file, targetPath, true);
                    //复制备用文件
                    System.IO.File.Copy(file, Path.Combine(MainViewModel.DnfBackupFilePath, fileName), true);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"文件复制失败: {ex.Message}");
                }
            }

            //生成卡片逻辑
            _patchBriefs.Add(new PatchModel
            {
                NpkName = PatchModel.NpkName,
                NpkUrl = PatchModel.NpkUrl,
                NpkDescribe = PatchModel.NpkDescribe,
                NpkImage = PatchModel.NpkImage,
                NpkLocalURL = List,
                NpkStatus = NpkStatus
            });

            //保存数据
            new DataServer().SavePatches(new List<PatchModel>(_patchBriefs));

            _addNewPatchBrief.Close();
        }

        List<string> List = new List<string>();

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Drop 事件的处理方法
        private void OnDrop(object parameter)
        {
            List = (List<string>)parameter;
        }
    }
}
