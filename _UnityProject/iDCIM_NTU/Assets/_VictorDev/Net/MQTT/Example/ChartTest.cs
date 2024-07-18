using System.Collections.Generic;
using TMPro;
using UnityEngine;
using VictorDev.Chart.XChart;
using VictorDev.IAQ;
using XCharts.Runtime;

public class ChartTest : MonoBehaviour
{
    public LineChart chartCO2;
    public BarChart chartPM25;
    public int numOfMax = 5;
    public string serieName = "CO2";

    public TextMeshProUGUI txtCO2, txtPM25;

    public XChartController xChart;


    public void OnRecivedData(List<SO_IAQ_Topic> topics)
    {
        for (int i = 0; i < topics.Count; i++)
        {
            if (topics[i].Topic.Contains("209"))
            {
                xChart.AddData(topics[i].LatestData.DataSetDict);
            }
        }
    }
}
