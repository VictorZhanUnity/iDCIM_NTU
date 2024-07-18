using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using VictorDev.Net.MQTT;
using VictorDev.Parser;

namespace VictorDev.IAQ
{
    /// <summary>
    /// IAQ資料解析處理
    /// </summary>
    public class IAQ_DataManager : MonoBehaviour
    {
        /// <summary>
        /// 當IAQ資料有更新時
        /// </summary>
        [Header(">>> 當IAQ資料有更新時")]
        public UnityEvent<List<SO_IAQ_Topic>> OnReceiveData;

        [Header(">>> IAQ歷史資料集 (主題名稱，資料集)")]
        [SerializeField] private List<SO_IAQ_Topic> iaqTopicList;

        [SerializeField] private MQTT_Connecter mqttConnecter;

        private void Awake() => mqttConnecter?.onReceivedMessage.AddListener(SetData);

        /// <summary>
        /// 解析IAQ資訊
        /// </summary>
        /// <param name="topic">訂閱主題名</param>
        public void SetData(string topic, string jsonString)
        {
            List<Dictionary<string, string>> jsonDataList = JsonUtils.ParseJsonArray(jsonString);

            //假如Topic資料已存在，直接給值
            for (int i = 0; i < iaqTopicList.Count; i++)
            {
                if (iaqTopicList[i].Topic == topic)
                {
                    iaqTopicList[i].AddIAQDataSet(jsonDataList);
                    //發送事件：資料更新
                    OnReceiveData.Invoke(iaqTopicList);
                    return;
                };
            }

            //假如Topic資料不存在，則生成新的Topic資料項
            SO_IAQ_Topic soTopicData = ScriptableObject.CreateInstance<SO_IAQ_Topic>();
            soTopicData.SetTopicData(topic, jsonDataList);
            iaqTopicList.Add(soTopicData);

            //發送事件：資料更新
            OnReceiveData.Invoke(iaqTopicList);
        }

        private void OnValidate() => mqttConnecter ??= GetComponent<MQTT_Connecter>();
    }
}

