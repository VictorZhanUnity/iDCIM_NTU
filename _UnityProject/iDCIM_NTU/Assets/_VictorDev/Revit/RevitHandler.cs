using System.Text.RegularExpressions;

namespace VictorDev.RevitUtils
{
    /// <summary>
    /// Revit相關處理
    /// </summary>
    public abstract class RevitHandler
    {
        public static string GetRevitObjectID(string value)
        {
            // 创建正则表达式来匹配方括号内的内容
            Regex regex = new Regex(@"\[(.*?)\]");

            // 通过正则表达式匹配
            Match match = regex.Match(value);

            // 提取匹配到的内容，match.Groups[0] = "[值]"
            if (match.Success) return match.Groups[1].Value;
            else return null;
        }
    }
}
