using System.Collections.Generic;
using UnityEngine;
using VictorDev.Parser;

/// <summary>
/// SO COBie資訊
/// </summary>
public class SO_COBie : ScriptableObject
{
    [SerializeField] private string _elementId;
    public string elementId => _elementId;

    /// <summary>
    /// 設備名稱 [elementId]
    /// </summary>
    public string deviceName => $"{prop["08設備名稱"]} [{elementId}]";

    /// <summary>
    /// prop下的所有COBie資訊
    /// </summary>
    public Dictionary<string, string> prop { get; private set; }

    private Dictionary<string, string> _prop_NoNull { get; set; }
    /// <summary>
    /// prop下的所有COBie資訊(不包含空值)
    /// </summary>
    public Dictionary<string, string> prop_NoNull
    {
        get
        {
            _prop_NoNull ??= new Dictionary<string, string>();
            foreach (string key in prop.Keys)
            {
                if (string.IsNullOrEmpty(prop[key]) == false)
                {
                    _prop_NoNull[key] = prop[key];
                }
            }
            return _prop_NoNull;
        }
    }

    /// <summary>
    /// 解析JSON
    /// </summary>
    public void Parse(string jsonString)
    {
        Dictionary<string, string> keyValuePairs = JsonUtils.ParseJsonArray(jsonString)[0];
        _elementId = keyValuePairs["elementId"];
        prop = JsonUtils.ParseJson(keyValuePairs["prop"]);
    }
}
