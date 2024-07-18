using UnityEngine.Events;

using UnityEngine;
using static XCharts.Runtime.RadarCoord;

/// <summary>
/// CCTV資料與控件
/// <para>繼承：DataHandler_withToggle</para>
/// <para>點擊模型或Toggle時，皆由改變Toggle狀態來Invoke事件到外部</para>
/// </summary>
public class CCTV_DataHandler : DataHandler_withToggle<CCTV_LineIndicator, SO_CCTV>
{
    public string RTSP_URL => soDataInfo.URL;
}
