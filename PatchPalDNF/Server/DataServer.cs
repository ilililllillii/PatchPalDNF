using Newtonsoft.Json;
using PatchPalDNF.Models;
using PatchPalDNF.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatchPalDNF.Server
{
    public class DataServer
    {
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
    }
}
