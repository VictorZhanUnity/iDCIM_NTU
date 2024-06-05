using System;

namespace VictorDev.Common
{
    public abstract class DateTimeHandler
    {
        /// <summary>
        /// 轉換成日期時間格式：2024/01/02 下午 3:45
        /// </summary>
        public static string ToDateString(DateTime date) => date.ToString("yyyy/MM/dd tt h:mm");
        public static DateTime ToDateString(string unixTimeStampe) => TimeStampToDateTime(unixTimeStampe);

        /// <summary>
        /// 將Unix時間戳（以毫秒为单位）转换为 DateTime
        /// </summary>
        public static DateTime TimeStampToDateTime(string unixTimeStampe)
        {
            long timestamp = long.Parse(unixTimeStampe);
            return DateTimeOffset.FromUnixTimeMilliseconds(timestamp).DateTime;

        }
    }
}
