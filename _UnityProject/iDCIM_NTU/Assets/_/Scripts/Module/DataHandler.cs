using UnityEngine;
using UnityEngine.Events;

public abstract class DataHandler<Indicator, SO> : MonoBehaviour where Indicator : LineIndicator where SO : ScriptableObject
{
    [Header(">>> SO資料")]
    [SerializeField] private SO soData;

    [Header(">>> 閃爍Shader Material")]
    [SerializeField] protected Material shaderMaterial;

    [Header(">>> 圖標物件")]
    [SerializeField] protected Indicator indicator;

    /// <summary>
    /// SO資料
    /// </summary>
    public SO soDataInfo => soData;

    /// <summary>
    /// 是否為啟動中
    /// </summary>
    public bool IsActivated
    {
        get => indicator.gameObject.activeSelf;
        set
        {
            indicator.gameObject.SetActive(value);
            shaderMaterial.SetInt("_IsActivated", (value ? 1 : 0));
        }
    }


    private void Awake()
    {
        shaderMaterial = GetComponent<MeshRenderer>().material;
        OnAwake();
    }

    protected virtual void OnAwake() { }

    private void OnValidate() => indicator ??= transform.GetChild(0).GetComponent<Indicator>();
}
