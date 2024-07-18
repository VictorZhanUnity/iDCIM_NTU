using System.Collections.Generic;
using TMPro;
using UnityEngine;
using VictorDev.IAQ;
using VictorDev.Net.MQTT;
using VictorDev.UI.Comps;

public class MQTT_Test : MonoBehaviour
{
    [SerializeField] private MQTT_Connecter mqttConnecter;
    [SerializeField] private IAQ_DataManager iaqManager;
    [SerializeField] private TMP_InputField inputIP, inputPort;
    [SerializeField] private TMP_InputField inputSubscribe, inputUnsubscribe;

    private void Awake()
    {
        inputIP.onSubmit.AddListener((str) => Connect());

        mqttConnecter.onConnectSuccessed.AddListener((ip, port) => ConsoleCMD.PrintOut($">>> MQTT {ip}:{port} - Connected!!"));
        mqttConnecter.onDisconnected.AddListener((ip, port) => ConsoleCMD.PrintOut($">>> MQTT {ip}:{port} - Disconnected!!"));

        iaqManager.OnReceiveData.AddListener(OnUpdateDataHandler);
    }

    private void OnUpdateDataHandler(List<SO_IAQ_Topic> topicList)
    {
        for (int i = 0; i < topicList.Count; i++)
        {
            ConsoleCMD.PrintOut($">>> UpdateData: {topicList[i].Topic}");
            for (int j = 0; j < topicList[i].LatestData.DataSet.Count; j++)
            {
                ConsoleCMD.PrintOut($"\t\t{topicList[i].LatestData.DataSet[j].ColumnName} / {topicList[i].LatestData.DataSet[j].DisplayValue}");
            }
        }
    }

    public void Connect() => mqttConnecter.Connect(inputIP.text, int.Parse(inputPort.text));
    public void Subscribe() => mqttConnecter.SubscribeTopic(inputSubscribe.text);
    public void UnsubscribeTopic() => mqttConnecter.UnsubscribeTopic(inputUnsubscribe.text);

    private void OnDestroy()
    {
        mqttConnecter.Disconnect();
    }
}
