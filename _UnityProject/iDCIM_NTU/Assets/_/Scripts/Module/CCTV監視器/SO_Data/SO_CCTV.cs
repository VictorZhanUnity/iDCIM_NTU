using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// CCTV鏈結網址
/// </summary>
[CreateAssetMenu(fileName = "SO_CCTV", menuName = ">>VictorDev<</Net/RTSP/SO_CCTV")]
public class SO_CCTV : ScriptableObject
{
    [Header(">>> CCTV監視器 - 編號")]
    [SerializeField] private string txtNo = "235123";

    [Header(">>> CCTV監視器 - 名稱")]
    [SerializeField] private string txtName = "監視器 - A";

    [Header(">>> CCTV鏈結網址(可填寫完整路徑)")]
    [SerializeField] private string url = "live/video/record/ch1";

    [Header(">>> CCTV伺服器資訊(留空的話，則使用完整路徑)")]
    [SerializeField] private SO_CCTV_ServerInfo cctvServerInfo;

    [Header(">>> 來源Toggle物件")]
    public Toggle sourceToggle;

    /// <summary>
    /// CCTV監視器 - 編號
    /// </summary>
    public string NoNumber => txtNo;
    /// <summary>
    /// CCTV監視器 - 名稱
    /// </summary>
    public string DeviceName => txtName;

    /// <summary>
    /// CCTV完整鏈結網址
    /// <para> + 若cctvServerInfo為null，則使用完整URL網址</para>
    /// </summary>
    public string URL => (cctvServerInfo != null) ? $"{cctvServerInfo.URL}/{url}" : url;
}
