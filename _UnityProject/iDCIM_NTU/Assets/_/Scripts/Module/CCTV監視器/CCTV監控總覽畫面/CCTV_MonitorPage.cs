using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CCTV_MonitorPage : MonoBehaviour
{
    [Header(">>> 主播放器")]
    [SerializeField] private CCTV_Player mainPlayer;
    [SerializeField] private CCTV_Player itemPrefab;
    [SerializeField] private ScrollRect scrollRect;

    private List<CCTV_Player> playerList { get; set; } = new List<CCTV_Player>();

    private void OnEnable()
    {
        scrollRect.verticalNormalizedPosition = -1;
    }

    private void OnDisable()
    {
        playerList.ForEach(player => player.onClickScaleButton.RemoveAllListeners());
        playerList.Clear();
    }

    public void SetDataList(List<SO_CCTV> list)
    {
        ObjectPoolManager.PushToPool<CCTV_Player>(scrollRect.content);

        gameObject.SetActive(true);
        IEnumerator CreateCCTVPlayer(SO_CCTV soData)
        {
            yield return new WaitForEndOfFrame();
            CCTV_Player player = ObjectPoolManager.GetInstanceFromQueuePool<CCTV_Player>(itemPrefab, scrollRect.content);
            player.soData = soData;
            player.onClickScaleButton.AddListener((soData) => mainPlayer.Play(soData));
            playerList.Add(player);
            //itemPrefab.Play();
        }
        list.ForEach(soData =>
        {
            StartCoroutine(CreateCCTVPlayer(soData));
        });



    }
}
