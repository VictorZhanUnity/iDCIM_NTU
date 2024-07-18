using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VictorDev.Net.MQTT;
using Debug = VictorDev.Common.DebugHandler;

/// <summary>
/// 模擬MQTT Broker發送假資料進行測試
/// <para>+ 直接掛載在MQTT_Connecter底下的子物件就好</para>
/// </summary>  
public class IAQ_TempDataInvoker : MonoBehaviour
{
    [Header(">>> 每隔幾秒發送資料")]
    [SerializeField] private float timeInterval = 2;

    [Header(">>> 假資料供測試")]
    [SerializeField] private List<TempDataFormat> tempDataFormatList;
    [SerializeField] private MQTT_Connecter mqttConnecter;

    private int counter { get; set; } = -1;

    private void Start()
    {
        Debug.Log($"[IAQ_TempData] 假資料發送 運作中…");
        mqttConnecter?.onConnectSuccessed.AddListener((ip, port) => StartInovke());
        mqttConnecter?.onDisconnected.AddListener((ip, port) => StopInovke());
    }

    public void StartInovke() => StartCoroutine(InvokeData());

    public void StopInovke() => StopCoroutine("InvokeData");

    private IEnumerator InvokeData()
    {
        Action invokeAction = () =>
        {
            CounterJumper();

            //先比對目前是否已訂閱該主題
            if (mqttConnecter.TopicsOfSubscribed.Contains(tempDataFormatList[counter].Topic.Split("/")[0] + "/#"))
            {
                mqttConnecter.onReceivedMessage.Invoke(tempDataFormatList[counter].Topic, tempDataFormatList[counter].JsonString);
            }
        };

        while (true)
        {
            yield return new WaitForSeconds(timeInterval);
            invokeAction.Invoke();
            invokeAction.Invoke();
        }
    }

    private void OnDestroy() => StopInovke();

    private int CounterJumper()
    {
        if (++counter > tempDataFormatList.Count - 1) counter = 0;
        return counter;
    }

    private void OnValidate()
    {
        if (transform.parent != null)
        {
            mqttConnecter ??= transform.parent.GetComponent<MQTT_Connecter>();
        }
    }

    [Serializable]
    public class TempDataFormat
    {
        [SerializeField] private string topic;
        [TextArea(3, 5)]
        [SerializeField] private string jsonString;
        public string Topic => topic;
        public string JsonString => jsonString;
    }
}
