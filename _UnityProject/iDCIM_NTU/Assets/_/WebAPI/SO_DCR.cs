using System.Collections.Generic;
using UnityEngine;
using VictorDev.Parser;

/// <summary>
/// [設備DCR] 機櫃
/// </summary>
public class SO_DCR : SO_Device
{
    [Header("========================================")]
    [SerializeField] private string _elementId;
    [SerializeField] private string _desc;
    [SerializeField] private string _modelnumber;
    [SerializeField] private string _needToInsp;

    [Header(">>> information欄位")]
    [SerializeField] private float _length;
    [SerializeField] private float _width;
    [SerializeField] private float _height;
    [SerializeField] private int _heightU;
    [SerializeField] private int _watt;
    [SerializeField] private int _weight;

    [Header(">>> 機櫃底下的DCS列表")]
    [SerializeField] private List<SO_DCSDCN> dcsList;

    #region [>>> Getter]
    public string elementId => _elementId;
    public string desc => _desc;
    public string modelnumber => _modelnumber;
    public string needToInsp => _needToInsp;
    public float length => _length;
    public float width => _width;
    public float height => _height;
    public int heightU => _heightU;
    public int watt => _watt;
    public int weight => _weight;

    /// <summary>
    /// 機櫃底下的DCS列表
    /// </summary>
    public List<SO_DCSDCN> DCS_List => dcsList;
    #endregion

    public override void SetSourceDataDict(Dictionary<string, string> dataDict)
    {
        base.SetSourceDataDict(dataDict);

        _elementId = CheckKeyExist("elementId");
        _desc = CheckKeyExist("desc");
        _modelnumber = CheckKeyExist("modelnumber");
        _needToInsp = CheckKeyExist("needToInsp");

        if (sourceDataDict.ContainsKey("information"))
        {
            Dictionary<string, string> informationSet = JsonUtils.ParseJson(sourceDataDict["information"]);
            _length = float.Parse(CheckKeyExist("length", informationSet));
            _width = float.Parse(CheckKeyExist("width", informationSet));
            _height = float.Parse(CheckKeyExist("height", informationSet));
            _heightU = int.Parse(CheckKeyExist("heightU", informationSet));
            _watt = int.Parse(CheckKeyExist("watt", informationSet)) + Random.Range(10000, 30000);
            _weight = int.Parse(CheckKeyExist("weight", informationSet)) + Random.Range(1000, 2000);
        }

        //讀取底下的DCS列表
        if (sourceDataDict.ContainsKey("contains"))
        {
            List<Dictionary<string, string>> dcsSet = JsonUtils.ParseJsonArray(sourceDataDict["contains"]);
            dcsList = new List<SO_DCSDCN>();
            dcsSet.ForEach(dataSet =>
            {
                SO_DCSDCN soDCS = ScriptableObject.CreateInstance<SO_DCSDCN>();
                soDCS.SetSourceDataDict(dataSet);
                dcsList.Add(soDCS);
            });
        }
    }
}
