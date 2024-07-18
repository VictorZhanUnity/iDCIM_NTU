using System;
using System.Collections.Generic;
using UnityEngine;
using XCharts.Runtime;

namespace VictorDev.Chart.XChart
{
    /// <summary>
    /// XChart控制器，掛在GameObject上即可
    /// <para>+ 再手動自行設定想要使用的圖表類別</para>
    /// <para>+ SimplifiedChart圖表可完整存儲呈現歷史資料</para>
    /// </summary>
    public class XChartController : MonoBehaviour
    {
        [Header(">>> 標題")]
        [SerializeField] private string title;

        [Header(">>> 圖表serie項目名稱")]
        [SerializeField] private List<string> serieNameList;

        [Header(">>> X軸最大顯示筆數")]
        [SerializeField] private int numOfMax = 10;

        [Header(">>> Y軸數值設定")]
        [SerializeField] private int maxValue = 100;
        [SerializeField] private int minValue = 0;
        [SerializeField] private int interval = 10;

        [Header(">>> 最多暫存筆數")]
        [SerializeField] private int numOfDataCache = 50;

        [Header(">>> 任一種圖表")]
        [SerializeField] private BaseChart chart;

        private void Awake()
        {
            chart.EnsureChartComponent<Tooltip>().show = true;
            // 清除數據
            chart.ClearData();
            OnValidate();
        }

        /// <summary>
        /// 根據serieName新增一筆數據
        /// <para>+ 若無serieName，則直接新增到serie[0]資料項</para>
        /// <para>+ 若無xAxisLabel，則直接以時間點作為X軸資料項</para>
        /// </summary>
        public void AddData(float data, string serieName = null, string xAxisLabel = null)
        {
            if (string.IsNullOrEmpty(xAxisLabel))
            {
                xAxisLabel = DateTime.Now.ToString("HH:mm:ss");
            }
            chart.AddXAxisData(xAxisLabel);

            if (string.IsNullOrEmpty(serieName)) chart.AddData(0, data);
            else chart.AddData(serieName, data);

            for (int i = 0; i < chart.series.Count; i++)
            {
                if (chart.series[i].dataCount > numOfDataCache) chart.series[i].data.RemoveAt(0);
            }
        }

        /// <summary>
        /// 直接使用Dictionary來新增數值
        /// <para>+ key值為serie名稱</para>
        /// <para>+ 若無xAxisLabel，則直接以時間點作為X軸資料項</para>
        /// </summary>
        public void AddData(Dictionary<string, float> serieData, string xAxisLabel = null)
        {
            string key;
            for (int i = 0; i < serieNameList.Count; i++)
            {
                key = serieNameList[i];
                if (serieData.ContainsKey(key))
                {
                    AddData(serieData[key], key, xAxisLabel);
                }
            }
        }

        private void OnValidate()
        {
            if (chart != null)
            {
                chart.EnsureChartComponent<Title>().text = title;

                XAxis xAxis = chart.EnsureChartComponent<XAxis>();
                xAxis.boundaryGap = true;
                xAxis.type = Axis.AxisType.Category;

                YAxis yAxis = chart.EnsureChartComponent<YAxis>();
                yAxis.type = Axis.AxisType.Value;
                yAxis.minMaxType = Axis.AxisMinMaxType.Custom;
                yAxis.min = minValue;
                yAxis.max = maxValue;
                yAxis.interval = interval;

                chart.EnsureChartComponent<DataZoom>().minShowNum = numOfMax;

                for (int i = 0; i < serieNameList.Count; i++)
                {
                    if (chart.series.Count < i)
                    {
                        chart.AddSerie<SimplifiedLine>();
                    }
                    chart.series[i].serieName = serieNameList[i];
                }
            }
        }
    }
}
