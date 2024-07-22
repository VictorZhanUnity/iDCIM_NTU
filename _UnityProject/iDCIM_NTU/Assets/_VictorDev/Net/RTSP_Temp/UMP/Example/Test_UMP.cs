using UnityEngine;
using UnityEngine.UI;
using VictorDev.Net.RTSP.UMPPlugin;

public class Test_UMP : MonoBehaviour
{
    public RTSPPlayer rtspPlayerPrefab;
    public Button btn;
    public Transform container;

    private string uri = "rtsp://admin:sks12345@61.219.246.16:554";
    //private string uri = "rtsp://root:TCIT5i2020@192.168.0";

    private int counter = 1;

    private void Awake()
    {
        btn.onClick.AddListener(() =>
        {
            RTSPPlayer player = Instantiate(rtspPlayerPrefab, container);
            player.Play($"{uri}/{counter++}/1");
            //player.Play($"{uri}.{counter++}/live1s2.sdp");
        });
    }
}
