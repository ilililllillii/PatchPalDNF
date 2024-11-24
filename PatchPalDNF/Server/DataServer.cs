using Microsoft.Win32;
using Newtonsoft.Json;
using PatchPalDNF.Models;
using PatchPalDNF.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PatchPalDNF.Server
{
    public class DataServer
    {

        /// <summary>
        /// 项目运行初始化
        /// </summary>
        public void initProject()
        {

            string programDataPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            string patchDirectory = Path.Combine(programDataPath, "PatchPalDNF");
            if (!Directory.Exists(patchDirectory))
            {
                Directory.CreateDirectory(patchDirectory);
            }

            MainViewModel.JsonFilePath = Path.Combine(patchDirectory, "PatchPalDNF_Data.json");
            MainViewModel.DnfBackupFilePath = Path.Combine(patchDirectory, "Backup");
            if (!Directory.Exists(MainViewModel.DnfBackupFilePath))
            {
                Directory.CreateDirectory(MainViewModel.DnfBackupFilePath);
            }

            MainViewModel.DnfFilePath = Path.Combine(GetDNFInstallPath(), "ImagePacks2");
        }

        //获取玩家DNF安装路径
        public static string GetDNFInstallPath()
        {

            // 检查注册表中的路径
            string registryKey = @"SOFTWARE\Tencent\DNF";
            string path = null;

            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(registryKey))
            {
                if (key != null)
                {
                    path = key.GetValue("InstallPath") as string;
                }
            }

            // 如果路径为空，尝试查找 64 位注册表项
            if (string.IsNullOrEmpty(path))
            {
                registryKey = @"SOFTWARE\WOW6432Node\Tencent\DNF";
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(registryKey))
                {
                    if (key != null)
                    {
                        path = key.GetValue("InstallPath") as string;
                    }
                }
            }

            return path;
        }

        /// <summary>
        /// 从文件加载数据
        /// </summary>
        public List<PatchModel> LoadPatches()
        {
            if (File.Exists(MainViewModel.JsonFilePath))
            {
                string json = File.ReadAllText(MainViewModel.JsonFilePath);
                return JsonConvert.DeserializeObject<List<PatchModel>>(json) ?? new List<PatchModel>();
                 
            }
            return new List<PatchModel>();
        }

        /// <summary>
        /// 将当前数据保存到文件
        /// </summary>
        public void SavePatches(List<PatchModel> PatchList)
        {
            string json = JsonConvert.SerializeObject(PatchList, Formatting.Indented);
            File.WriteAllText(MainViewModel.JsonFilePath, json);
        }

        /// <summary>
        /// 启用补丁
        /// </summary>
        public void EnableNPK(List<string> fileName)
        {
            foreach (var item in fileName)
            {
                try
                {
                    string destinationPath = Path.Combine(MainViewModel.DnfFilePath, item);
                    // 如果目标文件已存在，删除它
                    if (File.Exists(destinationPath))
                    {
                        File.Delete(destinationPath);
                    }

                    // 复制补丁文件到 ImagePacks2
                    File.Copy(Path.Combine(MainViewModel.DnfBackupFilePath, item), destinationPath);
                    Console.WriteLine("补丁已启用");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("启用补丁时出错: " + ex.Message);
                }
            }
        }
        /// <summary>
        /// 禁用补丁
        /// </summary>
        public void DisableNPK(List<string> fileName) 
        {
            foreach (var item in fileName)
            {
                try
                {
                    string destinationPath = Path.Combine(MainViewModel.DnfFilePath, item);
                    // 如果目标文件已存在，删除它
                    if (File.Exists(destinationPath))
                    {
                        File.Delete(destinationPath);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("禁用补丁时出错: " + ex.Message);
                }
            }
        }
    }
}
