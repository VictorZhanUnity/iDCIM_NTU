using TMPro;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(TMP_InputField))]
public class TMP_InputFIeld_AdvancedEventDispatcher : MonoBehaviour
{
    [Header(">>> 當值改變時，Invoke目前是否為空值")]
    public UnityEvent<bool> onValueChanged = new UnityEvent<bool>();

    [Space(10)]
    [SerializeField] private TMP_InputField inputField;

    private void Awake()
    {
        inputField.onValueChanged.AddListener(Invoke_OnValueChanged);
    }

    private void Start()
    {
        Invoke_OnValueChanged(inputField.text);
    }


    private void Invoke_OnValueChanged(string inputTxt) => onValueChanged?.Invoke(string.IsNullOrEmpty(inputTxt.Trim()));

    private void OnValidate() => inputField ??= GetComponent<TMP_InputField>();
}
