using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace VictorUtilties.UI
{  
    /// <summary>
    /// 直接掛載到ScrollView組件上就好
    /// </summary>
    public class ScrollBarAutoHide : MonoBehaviour
    {
        [SerializeField] private float fadeDuration = 0.3f;
        [SerializeField] private bool isScrolling;

        [SerializeField] private ScrollRect scrollRect;
        [SerializeField] private RectTransform viewport;
        [SerializeField] private CanvasGroup canvasGroup;

        private float lastScrollPosition;

        // 滑動停止的時間閾值，可根據需求調整
        public float scrollStopThreshold = 0.0001f;
        private float currentScrollPosition;

        private void Start()
        {
            canvasGroup.alpha = 0;
            scrollRect.onValueChanged.AddListener(OnScrollValueChanged);
        }

        private void Update()
        {
            scrollRect.movementType = (viewport.sizeDelta.x == 0) ? ScrollRect.MovementType.Clamped : ScrollRect.MovementType.Elastic;

            // 如果正在滑動，檢查是否停止滑動
            if (isScrolling)
            {
                currentScrollPosition = scrollRect.verticalNormalizedPosition;
                if (Mathf.Abs(currentScrollPosition - lastScrollPosition) < scrollStopThreshold)
                {
                    // 滑動停止
                    if (isScrolling == false) return;
                    isScrolling = false;
                    HideScrollBar();
                }

                lastScrollPosition = currentScrollPosition;
            }
        }

        private void OnScrollValueChanged(Vector2 value)
        {
            if (isScrolling) return;
            isScrolling = true;
            ShowScrollBar();
        }

        private void ShowScrollBar()
        {
            DOTween.Kill(canvasGroup);
            canvasGroup.DOFade(1, fadeDuration);
        }
        private void HideScrollBar()
        {
            DOTween.Kill(canvasGroup);
            canvasGroup.DOFade(0, fadeDuration);
        }

        private void OnValidate()
        {
            scrollRect = GetComponent<ScrollRect>();
            viewport = transform.Find("Viewport").GetComponent<RectTransform>();
            canvasGroup = transform.Find("Scrollbar Vertical").gameObject.GetComponent<CanvasGroup>();
            if (canvasGroup == null) canvasGroup = transform.Find("Scrollbar Vertical").gameObject.AddComponent<CanvasGroup>();
            canvasGroup.interactable = false;
        }
    }
}
