using PatchPalDNF.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;

namespace PatchPalDNF.Views
{
    /// <summary>
    /// AddNewPatchBrief.xaml 的交互逻辑
    /// </summary>
    public partial class AddNewPatchBrief : Window
    {
        public AddNewPatchBrief()
        {
            InitializeComponent();
        }

        private void Grid_Drop(object sender, DragEventArgs e)
        {
            // 获取拖拽的文件路径
            string[] files = (string[])e.Data.GetData(System.Windows.DataFormats.FileDrop);

            if (files == null) return;

            List<string> fileList = new List<string>();
            List<string> fileUINameList = new List<string>();

            foreach (string file in files) 
            {
                // 判断文件名或文件类型
                string fileName = System.IO.Path.GetFileName(file);
                string fileExtension = System.IO.Path.GetExtension(file).ToLower();

                // 判断是否为npk文件
                if (fileExtension == ".npk")
                {
                    fileList.Add(file);
                    fileUINameList.Add(fileName);
                }
                else
                {
                    MessageBox.Show("只能拖拽 .npk 文件", "文件类型错误", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }

            //处理界面
            fileUINameList?.ForEach(name => { stackPanel.Children.Add(new Button { Content = name }); });

            //数据传给vm
            var viewModel = (AddNewPatchBriefViewModel)this.DataContext;
            if (viewModel.DropCommand.CanExecute(fileList))
            {
                viewModel.DropCommand.Execute(fileList);
            }
        }

        private void Image_Drop(object sender, DragEventArgs e)
        {
            // 获取拖拽的文件
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files != null && files.Length > 0 && IsImageFile(files[0]))
                {
                    // 更新 ViewModel 中的图片路径
                    var viewModel = (AddNewPatchBriefViewModel)DataContext;
                    viewModel.NpkImage = files[0];
                }
            }
        }

        // 判断是否为图片文件
        private bool IsImageFile(string filePath)
        {
            string[] imageExtensions = { ".jpg", ".jpeg", ".png", ".bmp", ".gif" };
            string fileExtension = System.IO.Path.GetExtension(filePath)?.ToLower();
            return imageExtensions.Contains(fileExtension);
        }
    }
}
