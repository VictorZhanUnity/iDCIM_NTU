using UnityEngine;

namespace VictorDev.Common
{
    public static class ColorUtils
    {
        private static Color green => ConvertRgbToColor(30, 255, 30);
        private static Color yellow => ConvertRgbToColor(255, 255, 30);
        private static Color orange => ConvertRgbToColor(255, 180, 30);
        private static Color red => ConvertRgbToColor(255, 30, 30);

        /// <summary>
        /// 依百分比取得各等級Color
        /// <para>+ percentaget為0~1之float值</para>
        /// <para>+ 設置各顏色之Threshold值</para>
        /// </summary>
        public static Color GetColorLevelFromPercentage(float percentage)
        {
            // 确保百分比在0到1之间
            percentage = Mathf.Clamp01(percentage);

            // 定义不同颜色等级的阈值百分比
            float greenThreshold = 0.1f;  // 绿色的阈值，20%以下
            float yellowThreshold = 0.3f; // 黄色的阈值，40%以下
            float orangeThreshold = 0.5f; // 橙色的阈值，60%以下

            // 根据百分比返回对应的颜色
            if (percentage <= greenThreshold) return green;
            else if (percentage <= yellowThreshold) return Color.Lerp(green, yellow, (percentage - greenThreshold) / (yellowThreshold - greenThreshold));
            else if (percentage <= orangeThreshold) return Color.Lerp(yellow, orange, (percentage - yellowThreshold) / (orangeThreshold - yellowThreshold));
            else return Color.Lerp(orange, red, (percentage - orangeThreshold) / (1.0f - orangeThreshold));
        }


        /// <summary>
        /// 依百分比取得Color
        /// <para>+ percentaget為0~1之float值</para>
        /// <para>+ 顏色從綠色到紅色</para>
        /// </summary>
        public static Color GetColorFromPercentage(float percentage)
        {
            // 确保百分比在0到1之间
            percentage = Mathf.Clamp01(percentage);
            // 使用Color.Lerp进行线性插值
            return Color.Lerp(green, red, percentage);
        }

        public static Color ConvertRgbToColor(float r, float g, float b)
        {
            return new Color(r / 255f, g / 255f, b / 255f);
        }

        public static Color colorOrange => ConvertRgbToColor(255, 125, 0);
        public static Color colorRed => ConvertRgbToColor(255, 50, 0);
    }
}
