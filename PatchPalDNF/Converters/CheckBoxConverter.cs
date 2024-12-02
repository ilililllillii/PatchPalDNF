using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PatchPalDNF.Converters
{
    public class CheckBoxConverter : IValueConverter
    {
        // Convert method - 从文件路径提取文件名
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
            {
                return "启用"; 
            }
            return "禁用"; 
        }

        // ConvertBack 方法 - 不需要实现反向转换
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value; // 不需要反向转换
        }
    }
}
