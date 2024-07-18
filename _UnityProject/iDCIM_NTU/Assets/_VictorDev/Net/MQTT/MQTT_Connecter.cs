using M2MqttUnity;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using VictorDev.Common;
using Debug = VictorDev.Common.DebugHandler;

namespace VictorDev.Net.MQTT
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(M2MqttUnityClient))]
    /// <summary>
    /// MQTT連線器
    /// </summary>
    public class MQTT_Connecter : MonoBehaviour
    {
        [Header(">>> 向MQTT註冊的主題")]
        [SerializeField] private List<string> topicsOfSubscribed;
        /// <summary>
        ///  目前已向MQTT註冊的主題
        /// </summary>
        public List<string> TopicsOfSubscribed => topicsOfSubscribed;

        [Header(">>> MQTT Broker IP")]
        [SerializeField] private string brokerIPAddress;
        [Header(">>> MQTT Broker Port")]
        [Range(1000, 9999)]
        [SerializeField] private int brokerPort;
        public string BrokerIPAddress => brokerIPAddress;
        public int BrokerPort => brokerPort;

        /// <summary>
        /// 當連接至MQTT Broker成功時
        /// </summary>
        [Header(">>> 當連接至MQTT Broker成功時")]
        public UnityEvent<string, int> onConnectSuccessed;
        /// <summary>
        /// 當進行斷線時
        /// </summary>
        [Header(">>> 當進行斷線時")]
        public UnityEvent<string, int> onDisconnected;
        /// <summary>
        /// 從MQTT Broker傳來主題的資料時
        /// <para>[主題，解碼後的JsonString]</para>
        /// </summary>
        [Header(">>> 從MQTT Broker傳來主題的資料時")]
        public UnityEvent<string, string> onReceivedMessage;
        /// <summary>
        /// 當連接至MQTT Broker失敗時
        /// </summary>
        [Header(">>> 當連接至MQTT Broker失敗時")]
        public UnityEvent<string, int> onConnectFailed;

        [Header(">>> MQTT套件")]
        [SerializeField] private M2MqttUnityClient m2MqttUnityClient;

        private string markerOfAllTopics => "/#";

        /// <summary>
        /// 目前是否已連線
        /// </summary>
        public bool isConnected { get; private set; } = false;

        private void Awake()
        {
            m2MqttUnityClient.ConnectionSucceeded += () =>
            {
                isConnected = true;
                onConnectSuccessed?.Invoke(brokerIPAddress, brokerPort);
            };
            m2MqttUnityClient.ConnectionFailed += () => onConnectFailed?.Invoke(brokerIPAddress, brokerPort);
            m2MqttUnityClient.onReceiveMessageEvent += OnReceivedMessage;
        }

        /// <summary>
        /// 接收到byte[] Base64資料，轉成UTF8發送出去
        /// </summary>
        /// <param name="topic">主題名稱</param>
        /// <param name="data">Base64資料</param>
        private void OnReceivedMessage(string topic, byte[] data) => onReceivedMessage.Invoke(topic, StringHandler.Base64ToString(data));

        /// <summary>
        /// 連線至MQTT Broker
        /// </summary>
        [ContextMenu("- Connect 進行連線至MQTT Borker")]
        public void Connect() => Connect(brokerIPAddress, brokerPort);
        /// <summary>
        /// 連線至MQTT Broker
        /// </summary>[]
        public void Connect(string ipAddress, int port)
        {
            m2MqttUnityClient.brokerAddress = brokerIPAddress = ipAddress;
            m2MqttUnityClient.brokerPort = brokerPort = port;
            m2MqttUnityClient.Connect();
        }

        /// <summary>
        /// 向MQTT訂閱主題
        ///<code>
        ///     单层通配符（+）：
        ///         + 可以用来匹配主题层级中的一个层级。
        ///             例如，订阅 home/+/temperature 将匹配 home/livingroom/temperature、home/kitchen/temperature 等。
        ///</code>
        ///<code>
        ///     多层通配符（#）：
        ///         # 用于匹配包含当前层级及所有子层级。
        ///             例如，订阅 home/# 将匹配 home/livingroom/temperature、home/kitchen/temperature、home/livingroom/humidity 等所有以 home/ 开头的主题。
        ///</code>
        /// </summary>
        /// <param name="topic">主題名稱</param>
        /// <param name="isIncludeAllChildTopic">是否包含所有子主題</param>
        /// <returns>回傳訂閱主題完整名稱</returns>
        public string SubscribeTopic(string topic, bool isIncludeAllChildTopic = true)
        {
            if (isIncludeAllChildTopic) topic += markerOfAllTopics;
            if (topicsOfSubscribed.Contains(topic))
            {
                Debug.LogWarning($">>> {topic} is already Subscribed!");
                return null;
            }
            else
            {
                m2MqttUnityClient.SubscribeTopics(topic);
                topicsOfSubscribed.Add(topic);
                Debug.Log($"[{GetType().Name}] >>> SubscribeTopic: {topic}");
                return topic;
            }
        }
        /// <summary>
        /// 向MQTT訂閱多個主題
        /// <param name="isIncludeAllChildTopic">是否包含所有子主題</param>
        /// </summary>
        public void SubscribeTopics(string[] topics, bool isIncludeAllChildTopic = true)
        {
            for (int i = 0; i < topics.Length; i++) SubscribeTopic(topics[i], isIncludeAllChildTopic);
        }

        /// <summary>
        /// (供Inspector時使用)向MQTT訂閱主題
        ///<code>
        ///     单层通配符（+）：
        ///         + 可以用来匹配主题层级中的一个层级。
        ///             例如，订阅 home/+/temperature 将匹配 home/livingroom/temperature、home/kitchen/temperature 等。
        ///</code>
        ///<code>
        ///     多层通配符（#）：
        ///         # 用于匹配包含当前层级及所有子层级。
        ///             例如，订阅 home/# 将匹配 home/livingroom/temperature、home/kitchen/temperature、home/livingroom/humidity 等所有以 home/ 开头的主题。
        ///</code>
        /// </summary>
        /// <param name="topic">主題名稱</param>
        /// <param name="isIncludeAllChildTopic">是否包含所有子主題</param>
        public void SubscribeTopic(string topic) => SubscribeTopic(topic, true);

        /// <summary>
        /// 取消訂閱主題
        /// <param name="isIncludeAllChildTopic">是否包含所有子主題</param>
        /// </summary>
        public void UnsubscribeTopic(string topic, bool isIncludeAllChildTopic = true)
        {
            if (isIncludeAllChildTopic) topic += markerOfAllTopics;

            if (topicsOfSubscribed.Contains(topic) == false) Debug.LogWarning($">>> 並無訂閱此主題：{topic} ");
            m2MqttUnityClient.UnsubscribeTopics(topic);
            topicsOfSubscribed.Remove(topic);
            Debug.Log($"[{GetType().Name}] >>> UnsubscribeTopics: {topic}");
        }

        /// <summary>
        /// 取消訂閱主題
        /// </summary>
        public void UnsubscribeTopic(string topic) => UnsubscribeTopic(topic, true);
        /// <summary>
        /// 與MQTT Broker進行斷線，並取消全部訂閱主題
        /// </summary>
        public void Disconnect()
        {
            for (int i = 0; i < topicsOfSubscribed.Count; i++)
            {
                UnsubscribeTopic(topicsOfSubscribed[i], false);
            }
            topicsOfSubscribed.Clear();
            m2MqttUnityClient.Disconnect();
            isConnected = false;
            onDisconnected?.Invoke(brokerIPAddress, brokerPort);
        }

        private void OnDestroy()
        {
            onConnectSuccessed.RemoveAllListeners();
            onDisconnected.RemoveAllListeners();
            onReceivedMessage.RemoveAllListeners();
            onConnectFailed.RemoveAllListeners();
            Disconnect();
        }

        private void OnValidate() => m2MqttUnityClient ??= GetComponent<M2MqttUnityClient>();
    }
}
