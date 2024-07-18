using System.Collections.Generic;
using UnityEngine;
using VictorDev.Parser;
using VictorDev.RevitUtils;

/// <summary>
/// [DCS伺服器主機 / DCN網路設備]
/// </summary>
public class SO_DCSDCN : SO_Device
{
    [Header("========================================")]
    [SerializeField] private int _rackLocation;

    [Header(">>> information欄位")]
    [SerializeField] private float _length;
    [SerializeField] private float _width;
    [SerializeField] private float _height;
    [SerializeField] private float _heightU;
    [SerializeField] private int _watt;
    [SerializeField] private int _weight;

    #region [>>> Getter]
    /// <summary>
    /// 位於機櫃第幾U
    /// </summary>
    public int rackLocation => _rackLocation;
    /// <summary>
    /// 深度(公分)
    /// </summary>
    public float length => _length;
    /// <summary>
    /// 寬度(公分)
    /// </summary>
    public float width => _width;
    /// <summary>
    /// 高度(公分)
    /// </summary>
    public float height => _height;
    /// <summary>
    /// 高度(RU)
    /// </summary>
    public float heightU => _heightU;
    /// <summary>
    /// 電力
    /// </summary>
    public int watt => _watt;
    /// <summary>
    /// 重量
    /// </summary>
    public int weigh => _weight;
    #endregion

    public override void SetSourceDataDict(Dictionary<string, string> dataDict)
    {
        base.SetSourceDataDict(dataDict);

        _rackLocation = int.Parse(CheckKeyExist("rackLocation"));

        if (sourceDataDict.ContainsKey("information"))
        {
            Dictionary<string, string> informationSet = JsonUtils.ParseJson(sourceDataDict["information"]);
            _length = float.Parse(CheckKeyExist("length", informationSet));
            _width = float.Parse(CheckKeyExist("width", informationSet));
            _height = float.Parse(CheckKeyExist("height", informationSet));

            _heightU = RevitHandler.GetHightUFromDeviceID(deviceId);
        }
    }
}