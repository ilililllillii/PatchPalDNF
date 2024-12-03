using PatchPalDNF.Server;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Resources;

namespace PatchPalDNF
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        // 在应用启动时设置光标
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            try
            {
                // 设置全局光标为自定义光标
                StreamResourceInfo streamInfo = GetResourceStream(new Uri("Resources/basic.cur", UriKind.Relative));
                Mouse.OverrideCursor = new Cursor(streamInfo.Stream);
            }
            catch (Exception ex)
            {
                // 捕获异常并输出错误信息
                MessageBox.Show($"错误: {ex.Message}");
            }
        }

        // 在应用程序退出时恢复默认光标（可选）
        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            // 恢复默认光标
            Mouse.OverrideCursor = null;
        }
    }
}
