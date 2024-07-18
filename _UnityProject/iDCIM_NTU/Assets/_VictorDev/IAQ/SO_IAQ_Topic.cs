using System;
using System.Collections.Generic;
using UnityEngine;
using VictorDev.Common;

namespace VictorDev.IAQ
{
    /// <summary>
    /// IAQ空氣品質 - 訂閱主題資訊
    /// <para>+ 底下儲存IAQ歷史資料與最新一筆資料</para>
    /// </summary>
    [CreateAssetMenu(fileName = "IAQ訂閱主題資料", menuName = ">>VictorDev<</Net/MQTT/IAQ訂閱主題資料")]
    public class SO_IAQ_Topic : ScriptableObject
    {
        [Header(">>> 訂閱主題名稱")]
        [SerializeField] private string topic;

        [Header(">>>  最新一筆IAQ資料")]
        [SerializeField] private IAQ_DataSet latestData;

        [Header(">>> IAQ歷史資料List")]
        [SerializeField] private List<IAQ_DataSet> dataSetList = new List<IAQ_DataSet>();

        /// <summary>
        /// 訂閱主題名稱
        /// </summary>
        public string Topic => topic;
        /// <summary>
        /// 最新一筆IAQ資料
        /// </summary>
        public IAQ_DataSet LatestData => latestData;

        /// <summary>
        /// IAQ歷史資料List
        /// </summary>
        public List<IAQ_DataSet> DataSetList => dataSetList;

        public void SetTopicData(string topic, List<Dictionary<string, string>> jsonDataList)
        {
            this.topic = topic;
            AddIAQDataSet(jsonDataList);
        }

        /// <summary>
        /// 新增IAQ資料
        /// </summary>
        public void AddIAQDataSet(List<Dictionary<string, string>> jsonDataList)
        {
            latestData = new IAQ_DataSet(jsonDataList);
            dataSetList.Add(latestData);
        }
    }

    /// <summary>
    /// IAQ 欄位資料集 
    /// </summary>
    [Serializable]
    public class IAQ_DataSet
    {
        [Header(">>> Unix時間戳記")]
        [SerializeField] private string unixTimeStamp;
        [Header(">>> IAQ欄位資料清單")]
        [SerializeField] private List<IAQ_Data> dataSet = new List<IAQ_Data>();

        /// <summary>
        /// IAQ欄位資料清單，方便根據欄位名取值
        /// </summary>
        public Dictionary<string, float> DataSetDict { get; private set; } = new Dictionary<string, float>();
        /// <summary>
        /// IAQ欄位資料清單
        /// </summary>
        public List<IAQ_Data> DataSet => dataSet;

        /// <summary>
        /// 時間戳記
        /// </summary>
        public DateTime timeStamp { get; private set; }

        public IAQ_DataSet(List<Dictionary<string, string>> jsonDataList)
        {
            //timestamp值都一樣，所以取第一個就好
            unixTimeStamp = jsonDataList[0]["timestamp"];
            timeStamp = DateTimeHandler.ToDateString(unixTimeStamp);

            for (int i = 0; i < jsonDataList.Count; i++)
            {
                string column = jsonDataList[i]["name"];
                float value = float.Parse(jsonDataList[i]["value"]);

                IAQ_Data iaqData = new IAQ_Data(column, value);
                dataSet.Add(iaqData);

                //存於Dictionary
                DataSetDict[column] = value;

                //進行單位置的換算
                IAQ_DataConverter.ConvertToDisplayValue(ref iaqData);
            }
        }
    }

    /// <summary>
    /// IAQ 欄位資料 
    /// </summary>
    [Serializable]
    public class IAQ_Data
    {
        [Header(">>> 欄位名稱")]
        [SerializeField] private string column;

        [Header(">>> 顯示值與單位名稱")]
        [SerializeField] private float displayValue;
        [SerializeField] private string displayUnitName;

        [Header(">>> 原始值與單位名稱")]
        [SerializeField] private float sourceValue;
        [SerializeField] private string sourceUnitName;

        /// <summary>
        /// 欄位名稱
        /// </summary>
        public string ColumnName => column;
        /// <summary>
        /// 顯示值
        /// </summary>
        public float DisplayValue => displayValue;
        /// <summary>
        /// 顯示單位名稱
        /// </summary>
        public string DisplayUnitName => displayUnitName;
        /// <summary>
        ///  原始值
        /// </summary>
        public float SourceValue => sourceValue;
        /// <summary>
        /// 原始單位名稱
        /// </summary>
        public string SourceUnitName => sourceUnitName;

        public IAQ_Data(string columnName, float sourceValue, string sourceUnitName = "")
        {
            column = columnName;
            this.sourceValue = sourceValue;
            this.sourceUnitName = sourceUnitName;
        }

        /// <summary>
        /// 設定顯示值為單位
        /// </summary>
        public void SetDisplayData(float value, string unit)
        {
            this.displayValue = value;
            this.displayUnitName = unit;
        }
    }
}
