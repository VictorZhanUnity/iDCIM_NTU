using System.Linq;
using UMP;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.UI;
using VictorDev.UI;
using Debug = VictorDev.Common.DebugHandler;

namespace VictorDev.Net.RTSP.UMPPlugin
{
    /// <summary>
    /// rtsp 視訊串流播放畫面組件
    /// <para> + 使用RawImage，需放在Canvas下</para>
    /// </summary>
    public class RtspScreen : MonoBehaviour
    {
        [Header(">>> rtsp 網址Uri")]
        [SerializeField] private string uri;
        public string Uri => uri;

        [Header(">>> 要渲染畫面的GameObject對像")]
        [SerializeField] private GameObject[] renderingGameObjects;

        [Header(">>> 渲染畫面的RawImage")]
        [SerializeField] private RawImage rawImg;
        public Texture RendereringTexture => rawImg.mainTexture;


        [Header(">>> 是否一開始就自動播放")]
        public bool isAutoPlay = true;

        [Header(">>> UMP組件")]
        [SerializeField] private UniversalMediaPlayer mediaPlayer;
        [Header(">>> 進度條")]
        [SerializeField] private GameObject progressBar;
        [Header(">>> 進度文字組件")]
        [SerializeField] private Text txtPercent;

        [Header(">>> 當正在播放影片時")]
        public UnityEvent<RtspScreen> OnRtspPlaying;

        [Header(">>> 當影片Buffering結束時")]
        public UnityEvent<float> OnVideoBuffering;

        private void Start()
        {
            mediaPlayer.RenderingObjects = renderingGameObjects;

            mediaPlayer.AddBufferingEvent(OnBuffering);
            mediaPlayer.AddPlayingEvent(OnPlaying);

            if (isAutoPlay && string.IsNullOrEmpty(uri) == false)
            {
                Debug.Log($">>> [RtspPlayer] AutoPlay is On");
                Play(uri);
            }
        }

        private void OnBuffering(float percent)
        {
            Debug.Log($"\t[OnBuffering] percent: {percent}");
            progressBar.SetActive((percent != 100));
            txtPercent.text = $"{percent.ToString("F0")}{HtlmTagHandler.ToSetSize("%", txtPercent.fontSize * 0.5f)}";

            OnVideoBuffering.Invoke(percent);
        }

        private void OnPlaying()
        {
            Debug.Log($"\t[OnPlaying]");
            OnRtspPlaying?.Invoke(this);
        }


        /// <summary>
        /// 播放
        /// </summary>
        public void Play(string uri = null)
        {
            if (uri != null)
            {
                this.uri = uri;
            }
            mediaPlayer.Path = this.uri;
            mediaPlayer.Play();
        }

        /// <summary>
        /// 播放(直接設置RawImage)
        /// </summary>
        public void Play(Texture texture)
        {
            progressBar.SetActive(false);
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
            mediaPlayer.Stop();
        }


        private void OnValidate()
        {
            mediaPlayer ??= transform.GetChild(0).GetComponent<UniversalMediaPlayer>();
            progressBar ??= transform.GetChild(1).Find("ProgressBar").gameObject;
            txtPercent ??= progressBar.transform.Find("Txt_Percent").GetComponent<Text>();
        }
    }
}
