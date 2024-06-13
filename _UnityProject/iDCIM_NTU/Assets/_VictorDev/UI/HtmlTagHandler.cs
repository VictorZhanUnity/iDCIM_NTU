namespace VictorDev.UI
{
    public abstract class HtmlTagHandler
    {
        public static string ToSetSize(string target, float size)
        {
            return $"<size={size}>{target}</size>";
        }
    }
}
