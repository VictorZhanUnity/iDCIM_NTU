using System.Collections.Generic;
using UnityEngine;
using VictorDev.RevitUtils;

/// <summary>
/// SO資產設備共同欄位，做為父類別
/// </summary>
public abstract class SO_Device : ScriptableObject
{
    [Header(">>> 設備共同欄位")]
    [SerializeField] private string _buildingCode;
    [SerializeField] private string _deviceCode;
    [SerializeField] private string _code;
    [SerializeField] private string _area;
    [SerializeField] private string _floor;
    /// <summary>
    /// 設備類型：DCR / DCS / DCN / DCE / DCP
    /// </summary>
    [SerializeField] protected string _system;
    [SerializeField] private string _type;
    [SerializeField] private string _description;
    [SerializeField] private string _deviceId;
    [SerializeField] private string _manufacturer;
    [SerializeField] private string _useType;
    [SerializeField] private string _assetCode;
    [SerializeField] private string _mode;

    public string buildingCode => _buildingCode;
    /// <summary>
    /// 用於連接MQTT訂閱主題
    /// </summary>
    public string deviceCode => _deviceCode;
    public string code => _code;
    public string area => _area;
    public string floor => _floor;
    /// <summary>
    /// 設備類型：DCR / DCS / DCN / DCE / DCP
    /// </summary>
    public string system => _system;
    public string type => _type;
    public string description => _description;
    public string deviceId => _deviceId;
    public string manufacturer => _manufacturer;
    public string useType => _useType;
    public string assetCode => _assetCode;
    public string mode => _mode;

    /// <summary>
    /// 原始解析好的JSON字典資料
    /// </summary>
    protected Dictionary<string, string> sourceDataDict { get; set; }

    public virtual void SetSourceDataDict(Dictionary<string, string> dataDict)
    {
        sourceDataDict = dataDict;
        _buildingCode = CheckKeyExist("buildingCode");
        _deviceCode = CheckKeyExist("deviceCode");
        _code = CheckKeyExist("code");
        _area = CheckKeyExist("area");
        _floor = CheckKeyExist("floor");
        _system = CheckKeyExist("system");
        _type = CheckKeyExist("type");
        _description = CheckKeyExist("description");
        _deviceId = CheckKeyExist("deviceId");
        _manufacturer = CheckKeyExist("manufacturer");
        _useType = CheckKeyExist("useType");
        _assetCode = CheckKeyExist("assetCode");
        _mode = CheckKeyExist("mode");

        if (string.IsNullOrEmpty(_system)) _system = RevitHandler.GetSystemTypeFromDeviceID(deviceId);

        name = $"[{system}] {deviceId}";
    }

    protected string CheckKeyExist(string key, Dictionary<string, string> dataDict = null)
    {
        if (dataDict == null) dataDict = sourceDataDict;
        if (dataDict.ContainsKey(key)) return dataDict[key];
        else return "";
    }
}
