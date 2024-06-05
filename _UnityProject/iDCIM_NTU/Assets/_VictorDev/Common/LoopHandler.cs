using System;

namespace VictorDev.Common
{
    /// <summary>
    /// 迴圈處理器
    /// </summary>
    public abstract class LoopHandler
    {
        /// <summary>
        /// 執行For迴圈，精簡程式碼行數
        ///<code>
        ///    for (int i = startValue; i＜endValue; i += interval)
        ///    {
        ///        action.Invoke(i);
        ///    }
        ///</code>
        /// </summary>
        public static void ForLoop(int startValue, int endValue, Action<int> action, int interval = 1)
        {
            for (int i = startValue; i < endValue; i += interval)
            {
                action.Invoke(i);
            }
        }
    }
}
