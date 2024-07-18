using UnityEngine;
using UnityEngine.Events;
//using VictorDev.Net.RTSP.UMPPlugin;

public class CCTV_Player : MonoBehaviour
{
    [SerializeField] private SO_CCTV _soData;

    [Header(">> 在初始化時播放")]
    [SerializeField] private bool _isPlayWithInit = true;

    [Header(">>>當初始化完畢時")]
    public UnityEvent onInitComplete;

    /// <summary>
    /// 當點擊放大按鈕時
    /// </summary>
    [Header(">>>當點擊放大按鈕時")]
    public UnityEvent<SO_CCTV> onClickScaleButton;

    [Header(">>> UI組件")]
    //[SerializeField] private RtspScreen rtspScreen;
    [SerializeField] private PanelController panelController;

    public bool isPlayWithInit { set => _isPlayWithInit = value; }
    private bool isInitComplete { get; set; } = false;

    public SO_CCTV soData
    {
        set
        {
            _soData = value;
            Refresh();
        }
    }

    private void Awake()
    {
        panelController.onClickScaleButton.AddListener(() => onClickScaleButton.Invoke(_soData));
    }
    private void Start()
    {
        Debug.Log($"onInitComplete : {_soData.URL}");
        onInitComplete?.Invoke();
        isInitComplete = true;
        if (_isPlayWithInit) Play();
    }

    private void OnEnable()
    {
        Debug.Log($"OnEnable: {_soData.URL}");
        if (isInitComplete && _soData != null) Play();
    }
    /// <summary>
    /// 播放RTSP
    /// </summary>
    public void Play() => Play(_soData.URL);
    public void Play(string url)
    {
        gameObject.SetActive(true);
        //rtspScreen.Play(url);
    }
    public void Play(SO_CCTV soData)
    {
        this.soData = soData;
        Play();
    }

   /* public void Pause() => rtspScreen.Pause();

    public void Stop() => rtspScreen.Stop();*/

    public void StopAndClose()
    {
        _soData.sourceToggle.isOn = false;
       // Stop();
        ObjectPoolManager.PushToPool<CCTV_Player>(this);
    }

    private void OnValidate()
    {
        panelController ??= GetComponent<PanelController>();
        Refresh();
    }

    private void Refresh()
    {
        if (_soData != null)
        {
            panelController.Title = $"{_soData.DeviceName}";
        }
    }
}
