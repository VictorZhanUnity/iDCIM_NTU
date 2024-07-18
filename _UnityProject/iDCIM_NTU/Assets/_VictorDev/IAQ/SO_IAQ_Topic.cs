using System;
using System.Collections.Generic;
using UnityEngine;
using VictorDev.Common;

namespace VictorDev.IAQ
{
    /// <summary>
    /// IAQ�Ů�~�� - �q�\�D�D��T
    /// <para>+ ���U�x�sIAQ���v��ƻP�̷s�@�����</para>
    /// </summary>
    [CreateAssetMenu(fileName = "IAQ�q�\�D�D���", menuName = ">>VictorDev<</Net/MQTT/IAQ�q�\�D�D���")]
    public class SO_IAQ_Topic : ScriptableObject
    {
        [Header(">>> �q�\�D�D�W��")]
        [SerializeField] private string topic;

        [Header(">>>  �̷s�@��IAQ���")]
        [SerializeField] private IAQ_DataSet latestData;

        [Header(">>> IAQ���v���List")]
        [SerializeField] private List<IAQ_DataSet> dataSetList = new List<IAQ_DataSet>();

        /// <summary>
        /// �q�\�D�D�W��
        /// </summary>
        public string Topic => topic;
        /// <summary>
        /// �̷s�@��IAQ���
        /// </summary>
        public IAQ_DataSet LatestData => latestData;

        /// <summary>
        /// IAQ���v���List
        /// </summary>
        public List<IAQ_DataSet> DataSetList => dataSetList;

        public void SetTopicData(string topic, List<Dictionary<string, string>> jsonDataList)
        {
            this.topic = topic;
            AddIAQDataSet(jsonDataList);
        }

        /// <summary>
        /// �s�WIAQ���
        /// </summary>
        public void AddIAQDataSet(List<Dictionary<string, string>> jsonDataList)
        {
            latestData = new IAQ_DataSet(jsonDataList);
            dataSetList.Add(latestData);
        }
    }

    /// <summary>
    /// IAQ ����ƶ� 
    /// </summary>
    [Serializable]
    public class IAQ_DataSet
    {
        [Header(">>> Unix�ɶ��W�O")]
        [SerializeField] private string unixTimeStamp;
        [Header(">>> IAQ����ƲM��")]
        [SerializeField] private List<IAQ_Data> dataSet = new List<IAQ_Data>();

        /// <summary>
        /// IAQ����ƲM��A��K�ھ����W����
        /// </summary>
        public Dictionary<string, float> DataSetDict { get; private set; } = new Dictionary<string, float>();
        /// <summary>
        /// IAQ����ƲM��
        /// </summary>
        public List<IAQ_Data> DataSet => dataSet;

        /// <summary>
        /// �ɶ��W�O
        /// </summary>
        public DateTime timeStamp { get; private set; }

        public IAQ_DataSet(List<Dictionary<string, string>> jsonDataList)
        {
            //timestamp�ȳ��@�ˡA�ҥH���Ĥ@�ӴN�n
            unixTimeStamp = jsonDataList[0]["timestamp"];
            timeStamp = DateTimeHandler.ToDateString(unixTimeStamp);

            for (int i = 0; i < jsonDataList.Count; i++)
            {
                string column = jsonDataList[i]["name"];
                float value = float.Parse(jsonDataList[i]["value"]);

                IAQ_Data iaqData = new IAQ_Data(column, value);
                dataSet.Add(iaqData);

                //�s��Dictionary
                DataSetDict[column] = value;

                //�i����m������
                IAQ_DataConverter.ConvertToDisplayValue(ref iaqData);
            }
        }
    }

    /// <summary>
    /// IAQ ����� 
    /// </summary>
    [Serializable]
    public class IAQ_Data
    {
        [Header(">>> ���W��")]
        [SerializeField] private string column;

        [Header(">>> ��ܭȻP���W��")]
        [SerializeField] private float displayValue;
        [SerializeField] private string displayUnitName;

        [Header(">>> ��l�ȻP���W��")]
        [SerializeField] private float sourceValue;
        [SerializeField] private string sourceUnitName;

        /// <summary>
        /// ���W��
        /// </summary>
        public string ColumnName => column;
        /// <summary>
        /// ��ܭ�
        /// </summary>
        public float DisplayValue => displayValue;
        /// <summary>
        /// ��ܳ��W��
        /// </summary>
        public string DisplayUnitName => displayUnitName;
        /// <summary>
        ///  ��l��
        /// </summary>
        public float SourceValue => sourceValue;
        /// <summary>
        /// ��l���W��
        /// </summary>
        public string SourceUnitName => sourceUnitName;

        public IAQ_Data(string columnName, float sourceValue, string sourceUnitName = "")
        {
            column = columnName;
            this.sourceValue = sourceValue;
            this.sourceUnitName = sourceUnitName;
        }

        /// <summary>
        /// �]�w��ܭȬ����
        /// </summary>
        public void SetDisplayData(float value, string unit)
        {
            this.displayValue = value;
            this.displayUnitName = unit;
        }
    }
}
