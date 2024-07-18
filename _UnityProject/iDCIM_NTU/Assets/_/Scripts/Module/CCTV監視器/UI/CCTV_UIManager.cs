using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CCTV_UIManager : MonoBehaviour
{
    [Header(">>> CCTV�C��")]
    [SerializeField] private CCTVList cctvList;

    [Header(">>> CCTV����Prefab")]
    [SerializeField] private CCTV_Player cctvPlayerPrefab;

    [Header(">>> ���W��CCTV���� - ���ФJ�f")]
    [SerializeField] private CCTV_Player cctvPlayer_Entrance;
    [Header(">>> �k�U��CCTV����")]
    [SerializeField] private CCTV_Player cctvPlayer;
    [Header(">>> CCTV���ù�����")]
    [SerializeField] private CCTV_Player fullScreenCCTVPlayer;

    [SerializeField] private Button btnCloseOfFullScreen;


    [Header(">>> ���񾹾�V�M��")]
    [SerializeField] private Transform playersContainer;

    [Header(">>> ��C����Toggle�ܤƮ�Invoke")]
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
    /// �I��M��W���خ�
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
    /// �إ�CCTV����
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
            Debug.Log($"�}��: {dataHandler.name} / RTSP: {dataHandler.RTSP_URL}");
            this.dataHandler = dataHandler;
        }
        else
        {
            Debug.Log($"����: {dataHandler.name}");
        }
    }
}
