using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CanvasGroup))]
public class MoveFadeController : MonoBehaviour
{
    [Header(">>> 延遲(秒)")]
    [SerializeField] private float delay = 0f;
    [Header(">>> 動畫耗時(秒)")]
    [SerializeField] private float duration = 0.7f;
    [SerializeField] private Ease ease = Ease.OutQuart;

    [Header(">>> Position偏移值")]
    [SerializeField] private Vector2 offsetPos;

    [Header(">>> 是否進行Fade效果")]
    [SerializeField] private bool isFading = true;

    [Header(">>> 是否依照ChildIndex值來設定Delay")]
    [SerializeField] private bool isDelayByChildIndex = false;

    [Header(">>> OnEnabled動畫開始時")]
    public UnityEvent OnTweenStart = new UnityEvent();
    [Header(">>> OnEnabled動畫結束時")]
    public UnityEvent OnEnabledComplete = new UnityEvent();

    [Header(">>> UI組件")]
    [SerializeField] private CanvasGroup cg;
    [SerializeField] private RectTransform rectTrans;

    private Vector2 originalPos { get; set; }
    private Vector2 formPos { get; set; }

    private float currentAlpha { get; set; }
    private float targetDealy { get; set; }

    private Tween fadeTween { get; set; }
    private Tween moveTween { get; set; }

    private void Awake()
    {
        originalPos = new Vector2(transform.localPosition.x, transform.localPosition.y);
        formPos = originalPos + offsetPos;
    }

    private void OnEnable() => Play();
    [ContextMenu("- Dotween: Play")]
    public void Play()
    {
        moveTween?.Kill();

        targetDealy = isDelayByChildIndex ? delay * transform.GetSiblingIndex() : delay;

        // Fade
        if (isFading)
        {
            currentAlpha = (cg.alpha == 1) ? 0 : cg.alpha;
            fadeTween = cg.DOFade(1, duration).From(currentAlpha).SetEase(ease).SetDelay(targetDealy);
        }

        // LocalMove
        if (offsetPos == Vector2.zero)
        {
            OnCompleteHandler();
            return;
        }

        if (offsetPos.y == 0 && offsetPos.x != 0)
            moveTween = rectTrans.DOLocalMoveX(originalPos.x, duration).From(formPos.x);
        else if (offsetPos.x == 0 && offsetPos.y != 0)
            moveTween = rectTrans.DOLocalMoveY(originalPos.y, duration).From(formPos.y);
        else
            moveTween = rectTrans.DOLocalMove(originalPos, duration).From(formPos);

        moveTween = moveTween.SetEase(ease).SetDelay(targetDealy).OnStart(OnStartHandler).OnComplete(OnCompleteHandler);
    }

    private void OnStartHandler()
    {
        cg.interactable = false;
        OnTweenStart?.Invoke();
    }

    private void OnCompleteHandler()
    {
        cg.interactable = true;
        OnEnabledComplete?.Invoke();
    }

    private void OnValidate()
    {
        cg ??= GetComponent<CanvasGroup>();
        rectTrans ??= GetComponent<RectTransform>();
    }

    private void OnDisable()
    {
        fadeTween.Kill();
        moveTween.Kill();
    }

}
