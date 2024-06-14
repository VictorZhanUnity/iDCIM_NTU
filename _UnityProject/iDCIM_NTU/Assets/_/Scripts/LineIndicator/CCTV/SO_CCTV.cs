using UnityEngine;

public class SO_CCTV : ScriptableObject
{
    [SerializeField] private string urlRTSP;

    public string URL_RTSP => urlRTSP;
}
