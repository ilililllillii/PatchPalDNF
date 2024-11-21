using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatchPalDNF.Models
{
    public class PatchModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string NpkName { get; set; }
        /// <summary>
        /// npk源连接
        /// </summary>
        public string NpkUrl { get; set; }
        /// <summary>
        /// npk描述
        /// </summary>
        public string NpkDescribe { get; set; }
        /// <summary>
        /// npk效果图
        /// </summary>
        public string NpkImage { get; set; }
        /// <summary>
        /// npk本地路径
        /// </summary>
        public List<string> NpkLocalURL { get; set; }
    }
}
