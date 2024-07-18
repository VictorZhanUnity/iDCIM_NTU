using TMPro;
using UnityEngine;

public class COBieListItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtKey, txtValue;
    public string key { set => txtKey.SetText(value); }
    public string value { set => txtValue.SetText(value); }

    private void OnValidate()
    {
        txtKey ??= transform.Find("txtKey").GetComponent<TextMeshProUGUI>();
        txtValue ??= transform.Find("txtValue").GetComponent<TextMeshProUGUI>();
    }
}
