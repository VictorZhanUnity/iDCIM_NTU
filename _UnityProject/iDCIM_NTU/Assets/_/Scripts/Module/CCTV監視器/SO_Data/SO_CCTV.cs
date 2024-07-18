using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// CCTV�쵲���}
/// </summary>
[CreateAssetMenu(fileName = "SO_CCTV", menuName = ">>VictorDev<</Net/RTSP/SO_CCTV")]
public class SO_CCTV : ScriptableObject
{
    [Header(">>> CCTV�ʵ��� - �s��")]
    [SerializeField] private string txtNo = "235123";

    [Header(">>> CCTV�ʵ��� - �W��")]
    [SerializeField] private string txtName = "�ʵ��� - A";

    [Header(">>> CCTV�쵲���}")]
    [SerializeField] private string url = "live/video/record/ch1";

    [Header(">>> CCTV���A����T")]
    [SerializeField] private SO_CCTV_ServerInfo cctvServerInfo;

    [Header(">>> �ӷ�Toggle����")]
    public Toggle sourceToggle;

    /// <summary>
    /// CCTV�ʵ��� - �s��
    /// </summary>
    public string NoNumber => txtNo;
    /// <summary>
    /// CCTV�ʵ��� - �W��
    /// </summary>
    public string DeviceName => txtName;

    /// <summary>
    /// CCTV�����쵲���}
    /// </summary>
    public string URL => $"{cctvServerInfo.URL}/{url}";
}
