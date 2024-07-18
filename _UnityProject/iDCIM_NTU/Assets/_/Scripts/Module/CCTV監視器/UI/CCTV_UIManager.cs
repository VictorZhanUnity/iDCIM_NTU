using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CCTV_UIManager : MonoBehaviour
{
    [Header(">>> CCTV列表")]
    [SerializeField] private CCTVList cctvList;

    [Header(">>> CCTV播放器Prefab")]
    [SerializeField] private CCTV_Player cctvPlayerPrefab;

    [Header(">>> 左上角CCTV播放器 - 機房入口")]
    [SerializeField] private CCTV_Player cctvPlayer_Entrance;
    [Header(">>> 右下角CCTV播放器")]
    [SerializeField] private CCTV_Player cctvPlayer;
    [Header(">>> CCTV全螢幕播放器")]
    [SerializeField] private CCTV_Player fullScreenCCTVPlayer;

    [SerializeField] private Button btnCloseOfFullScreen;


    [Header(">>> 播放器橫向清單")]
    [SerializeField] private Transform playersContainer;

    [Header(">>> 當列表項目Toggle變化時Invoke")]
    public UnityEvent<CCTVListItem> onToggleChanged;

    private CCTV_DataHandler dataHandler;

    private Dictionary<SO_CCTV, CCTV_Player> playerDictionary { get; set; } = new Dictionary<SO_CCTV, CCTV_Player>();

    private void Awake()
    {
        cctvList.onToggleChanged.AddListener(onToggleChanged.Invoke);
        cctvList.onToggleChanged.AddListener(onToggleChangedHandler);

        cctvPlayer_Entrance.onClickScaleButton.AddListener(PlayFullScreenCCTV);
        cctvPlayer.onClickScaleButton.AddListener(PlayFullScreenCCTV);

        btnCloseOfFullScreen.onClick.AddListener(() =>
        {
            playerDictionary.Values.ToList().ForEach(player =>
            {
                player.Play();
            });
        });
    }

    /// <summary>
    /// 點選清單上項目時
    /// </summary>
    private void onToggleChangedHandler(CCTVListItem listItem)
    {
        if (playerDictionary.ContainsKey(listItem.soData))
        {
            if (listItem.isOn == false) playerDictionary[listItem.soData].StopAndClose();
            playerDictionary.Remove(listItem.soData);
        }
        else
        {
            StartCoroutine(CreatePlayer(listItem.soData));
        }
    }

    /// <summary>
    /// 建立CCTV播放器
    /// </summary>
    private IEnumerator CreatePlayer(SO_CCTV soData)
    {
        yield return new WaitForEndOfFrame();
        CCTV_Player player = ObjectPoolManager.GetInstanceFromQueuePool<CCTV_Player>(cctvPlayerPrefab, playersContainer);
        player.soData = soData;
        player.onClickScaleButton.AddListener(PlayFullScreenCCTV);
        playerDictionary[soData] = player;
    }

    public void PlayFullScreenCCTV(SO_CCTV soData)
    {
      //  playerDictionary.Values.ToList().ForEach(player => player.Pause());

        fullScreenCCTVPlayer.soData = soData;
        fullScreenCCTVPlayer.transform.parent.gameObject.SetActive(true);
        fullScreenCCTVPlayer.gameObject.SetActive(true);
    }

    public void SetDataHandler(CCTV_DataHandler dataHandler)
    {
        if (dataHandler.IsSelected)
        {
            Debug.Log($"開啟: {dataHandler.name} / RTSP: {dataHandler.RTSP_URL}");
            this.dataHandler = dataHandler;
        }
        else
        {
            Debug.Log($"關閉: {dataHandler.name}");
        }
    }
}
