using PatchPalDNF.Command;
using PatchPalDNF.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PatchPalDNF.ViewModel
{
    public class AddNewPatchBriefViewModel
    {
        private readonly AddNewPatchBrief _addNewPatchBrief;

        // 取消命令
        public ICommand CancelCommand { get; }

        // 确定命令
        public ICommand SureCommand { get; }

        public AddNewPatchBriefViewModel(AddNewPatchBrief addNewPatchBrief)
        {
            _addNewPatchBrief = addNewPatchBrief;

            // 初始化命令
            CancelCommand = new RelayCommand(IsCancel);
            SureCommand = new RelayCommand(IsSure);
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
            // 在这里处理确定逻辑
            // 例如，可以执行保存操作，关闭窗口等
        }
    }
}
