using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;

namespace PatchPalDNF.Converters
{
    public class FileNameConverter : IValueConverter
    {
        // Convert method - 从文件路径提取文件名
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string filePath)
            {
                return Path.GetFileName(filePath); // 提取文件名
            }
            return value; // 如果不是文件路径，直接返回原值
        }

        // ConvertBack 方法 - 不需要实现反向转换
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value; // 不需要反向转换
        }
    }
}
