using UnityEngine;

/// <summary>
/// CCTV伺服器資訊
/// </summary>
[CreateAssetMenu(fileName = "SO_CCTV_ServerInfo", menuName = ">>VictorDev<</Net/RTSP/SO_CCTV_ServerInfo")]
public class SO_CCTV_ServerInfo : ScriptableObject
{
    [Header(">>> 帳號密碼")]
    [SerializeField] private string account = "admin";
    [SerializeField] private string password = "baopu168";
    [Header(">>> URL與埠號")]
    [SerializeField] private string url = "61.220.55.194";
    [SerializeField] private string port = "5540";

    public string URL => $"rtsp://{account}:{password}@{url}:{port}";
}
