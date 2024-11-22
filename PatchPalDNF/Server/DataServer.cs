using Newtonsoft.Json;
using PatchPalDNF.Models;
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
        private string filePath = @"E:\scjTest\patches.json"; // 数据文件路径

        /// <summary>
        /// 从文件加载数据
        /// </summary>
        public List<PatchModel> LoadPatches()
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
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
            File.WriteAllText(filePath, json);
        }
    }
}
