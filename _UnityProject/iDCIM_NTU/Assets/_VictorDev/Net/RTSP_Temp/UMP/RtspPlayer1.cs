using TMPro;
using UMP;
using UnityEngine;
using UnityEngine.UI;
using VictorDev.UI;
using Debug = VictorDev.Common.DebugHandler;

namespace VictorDev.Net.RTSP.UMPPlugin
{
    /// <summary>
    /// rtsp 視訊串流 Player
    /// <para> + 使用RawImage，需放在Canvas下</para>
    /// </summary>
    public class RtspPlayer1 : MonoBehaviour
    {
        [Header(">>> rtsp 網址Uri")]
        public string uri;

        [Header(">>> 要渲染畫面的GameObject對像")]
        public GameObject[] renderingGameObjects;

        [Header(">>> 是否一開始就自動播放")]
        public bool isAutoPlay = true;

        [Header(">>> UI組件")]
        [SerializeField] private TextMeshProUGUI txtPercent, txtTitle;
        [SerializeField] private Button btnClose;

        private UniversalMediaPlayer mediaPlayer;


        private void OnEnable()
        {
            mediaPlayer = GetComponentInChildren<UniversalMediaPlayer>();
            mediaPlayer.RenderingObjects = renderingGameObjects;

            mediaPlayer.AddBufferingEvent(OnBuffering);
            mediaPlayer.AddPlayingEvent(OnPlaying);

            if (isAutoPlay && string.IsNullOrEmpty(uri) == false)
            {
                Debug.Log($">>> RtspPlayer AutoPlay On");
                Play(uri);
            }

            btnClose.onClick.AddListener(() =>
            {
                mediaPlayer.Stop();
                GameObject.Destroy(this);
            });
        }

        private void OnBuffering(float percent)
        {
            Debug.Log($"[ OnBuffering ] percent: {percent}");
            txtPercent.text = $"{percent.ToString("F0")}{HtlmTagHandler.ToSetSize("%", txtPercent.fontSize * 0.5f)}";
        }

        private void OnPlaying()
        {
            Debug.Log($"[ OnPlaying ]");
            SetProgressBarVisible(false);
        }

        public void SetTitle(string title)
        {
            txtTitle.transform.parent.gameObject.SetActive(title != null);
            txtTitle.text = title;
        }

        /// <summary>
        /// 開啟
        /// </summary>
        public void Play(string uri = null, string title = null)
        {
            SetProgressBarVisible(true);
            SetTitle(title);

            if (uri != null)
            {
                this.uri = uri;
                mediaPlayer.Path = this.uri;
            }
            mediaPlayer.Play();
            LogMsg($"Play uri: {this.uri}");
        }


        public void Pause()
        {
            mediaPlayer.Pause();
            LogMsg($"Pause");
        }

        public void Stop()
        {
            mediaPlayer.Stop();
            LogMsg($"Close");
        }

        /// <summary>
        /// 設定本身RawImage的尺吋
        /// </summary>
        public void SetScreenWidth(int width)
        {
            GetComponent<RectTransform>().sizeDelta = new Vector2(width, 480 * width / 720);
        }
        public void SetScreenHeight(int height)
        {
            GetComponent<RectTransform>().sizeDelta = new Vector2(720 * height / 480, height);
        }

        private void LogMsg(string msg) => Debug.Log($"[rtsp player] >>> {msg}");
        private void SetProgressBarVisible(bool isVisible)
        {
            txtPercent.transform.parent.gameObject.SetActive(isVisible);
        }
    }
}
