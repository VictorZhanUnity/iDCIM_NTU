using TMPro;
using UMP;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using VictorDev.UI;
using Debug = VictorDev.Common.DebugHandler;

namespace VictorDev.Net.RTSP.UMPPlugin
{
    /// <summary>
    /// rtsp 視訊串流播放畫面組件
    /// <para> + 可將需要渲染的RawImage與SpriteRenderer，放在mediaPlayer.RenderingObjects裡進行渲染</para>
    /// </summary>
    public class RTSPPlayer : MonoBehaviour
    {
        [Header(">>> rtsp 網址Uri")]
        [SerializeField] private string uri;
        public string Uri => uri;

        [Header(">>> 渲染畫面的RawImage")]
        [SerializeField] private RawImage rawImg;

        /// <summary>
        /// 影片播放Texture
        /// </summary>
        public Texture RendereringTexture => rawImg.mainTexture;

        [Header(">>> 是否一開始就自動播放")]
        public bool isAutoPlay = true;

        [Header(">>> 監聽取得串流TextureObject(一次)")]
        public UnityEvent<Texture> onGetTextureObject = new UnityEvent<Texture>();

        [Header(">>> 當影片讀取緩衝時，Invoke進度")]
        public UnityEvent<float> onVideoBuffering = new UnityEvent<float>();

        [Header(">>> 當正在播放影片時")]
        public UnityEvent<RTSPPlayer> onRtspPlaying = new UnityEvent<RTSPPlayer>();

        [Header(">>> 當讀取影片失敗時")]
        public UnityEvent onError = new UnityEvent();

        [Header(">>> 其它組件")]
        [SerializeField] private UniversalMediaPlayer mediaPlayer;
        [SerializeField] private TextMeshProUGUI txtPercent;
        [SerializeField] private Button btnRefresh;
        [SerializeField] private Image imgBlack;

        private void Start()
        {
            //監聽 UMP的事件
            mediaPlayer.AddBufferingEvent(OnVideoBuffering);
            mediaPlayer.AddPlayingEvent(OnPlaying);
            mediaPlayer.AddEncounteredErrorEvent(OnError);

            if (isAutoPlay && string.IsNullOrEmpty(uri) == false)
            {
                Debug.Log($">>> [RTSP Player] AutoPlay is On");
                Play(uri);
            }

            btnRefresh.gameObject.SetActive(false);
            btnRefresh.onClick.AddListener(() => Play(uri));
        }

        /// <summary>
        /// 觸發發送串流的Texture物件(一次)
        /// </summary>
        public void InvokeTextureOnce() => onGetTextureObject?.Invoke(RendereringTexture);

        /// <summary>
        /// 正在讀取緩衝時
        /// </summary>
        private void OnVideoBuffering(float percent)
        {
            Debug.Log($"\t[OnVideoBuffering] : {percent}");

            int progress = Mathf.RoundToInt(percent);
            txtPercent.SetText($"{progress}{HtlmTagHandler.ToSetSize("%", txtPercent.fontSize * 0.5f)}");
            txtPercent.gameObject.SetActive(progress != 100);
            imgBlack.gameObject.SetActive(progress != 100);

            onVideoBuffering.Invoke(percent);
        }

        /// <summary>
        /// 正在播放時
        /// </summary>
        private void OnPlaying()
        {
            Debug.Log($"\t[OnPlaying]");
            imgBlack.gameObject.SetActive(false);
            onRtspPlaying?.Invoke(this);
        }
        /// <summary>
        /// 當發生錯誤時
        /// </summary>
        private void OnError()
        {
            Debug.LogWarning($"\t[OnError]");
            Stop();
            txtPercent.gameObject.SetActive(false);
            btnRefresh.gameObject.SetActive(true);
            onError?.Invoke();
        }

        public void Play() => Play(uri);
        /// <summary>
        /// 播放
        /// </summary>
        public void Play(string uri)
        {
            if (uri != null) this.uri = uri;
            btnRefresh.gameObject.SetActive(false);

            Debug.Log($"\t[Play] {uri}");

            mediaPlayer.Path = this.uri;
            mediaPlayer.Play();
        }

        /// <summary>
        /// 播放(直接設置RawImage)
        /// </summary>
        public void Play(Texture texture)
        {
            OnVideoBuffering(100);
            rawImg.texture = texture;
        }

        /// <summary>
        /// 暫停
        /// </summary>
        public void Pause() => mediaPlayer.Pause();

        /// <summary>
        /// 停止
        /// </summary>
        public void Stop()
        {
            Debug.Log($">>> [RTSP Stop]: {uri}");
            mediaPlayer.Stop();
        }

        private void OnDisable() => Stop();

        private void OnValidate()
        {
            mediaPlayer ??= transform.Find("UniversalMediaPlayer").GetComponent<UniversalMediaPlayer>();
            rawImg ??= transform.Find("RawImage").GetComponent<RawImage>();
            txtPercent ??= transform.Find("txtPercent").GetComponent<TextMeshProUGUI>();
            btnRefresh ??= transform.Find("btnRefresh").GetComponent<Button>();
            imgBlack ??= transform.Find("imgBlack").GetComponent<Image>();
        }
    }
}
