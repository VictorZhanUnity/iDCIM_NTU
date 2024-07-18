using Newtonsoft.Json;
using System;
using System.Text;

namespace VictorDev.Common
{
    public abstract class StringHandler
    {
        /// <summary>
        /// 設置文字大小(HTML)
        /// </summary>
        public static string SetFontSizeString(string str, int fontSize) => $"<size='{fontSize}'>{str}</size>";

        /// <summary>
        /// 解碼Base64 byte[] 轉成UTF8字串
        /// </summary>
        public static string Base64ToString(byte[] data)
        {
            string base64String = JsonConvert.SerializeObject(data).Trim('\"');
            byte[] byteArray = Convert.FromBase64String(base64String);
            // 將 byte[] 解碼為字符串
            return Encoding.UTF8.GetString(byteArray);
        }

        private static StringBuilder sb = new StringBuilder();
        /// <summary>
        /// 將多個字串組在一起
        /// <para> + 使用StringBuilder更有效率</para>
        /// <para> + 直接用原始值，故不進行Trim()</para>
        /// </summary>
        public static string StringBuilderAppend(params string[] strValues)
        {
            sb.Clear();
            foreach (string strValue in strValues)
            {
                sb.Append(strValue);
            }
            return sb.ToString();
        }
    }
}
