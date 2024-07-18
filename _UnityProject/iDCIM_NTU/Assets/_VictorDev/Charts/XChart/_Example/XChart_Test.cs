using System;
using System.Collections;
using UnityEngine;
using XCharts.Runtime;
using Random = UnityEngine.Random;

public class XChart_Test : MonoBehaviour
{
    private int counter = 0;
    private LineChart chart;

    void Start()
    {
        chart = transform.GetChild(0).GetComponent<LineChart>();
        /*chart = gameObject.AddComponent<LineChart>();
        chart.Init();

        var title = chart.EnsureChartComponent<Title>();
        title.text = "MQTT";

        var tooltip = chart.EnsureChartComponent<Tooltip>();
        tooltip.show = true;

        // 圖例
        var legend = chart.EnsureChartComponent<Legend>();
        legend.show = true;

        var xAxis = chart.EnsureChartComponent<XAxis>();
        xAxis.splitNumber = 10;
        xAxis.boundaryGap = true;
        xAxis.type = Axis.AxisType.Category;

        var yAxis = chart.EnsureChartComponent<YAxis>();
        yAxis.type = Axis.AxisType.Value;
*/

       // chart.RemoveData();
        //chart.AddSerie<Line>("IAQ");

        // 動態畫曲線圖
        StartCoroutine(AddValue());
    }

    private IEnumerator AddValue()
    {
        while (true)
        {
            chart.UpdateYAxisData(counter, DateTime.Now.ToString("mm:ss"));
            chart.UpdateData("IAQ", counter, Random.Range(10, 100));
            /*     chart.AddXAxisData(DateTime.Now.ToString("mm:ss"));
                 chart.AddData(0, Random.Range(10, 100));*/
            if (++counter >= 5) counter = 0;
             
            yield return new WaitForSeconds(2);
        }
    }
}
