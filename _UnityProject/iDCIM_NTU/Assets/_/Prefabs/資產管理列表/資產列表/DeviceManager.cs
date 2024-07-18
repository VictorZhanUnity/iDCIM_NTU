using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using VictorDev.Parser;

public class DeviceManager : MonoBehaviour
{
    [Header(">>> DCS/DCN模型Prefab")]
    [SerializeField] private Transform dcsPrefab;
    [Header(">>> DCS/DCN材質庫")]
    [SerializeField] private List<Texture> dcsTextureList;
    private Dictionary<string, Texture> dcsTextureDictionary { get; set; } = new Dictionary<string, Texture>();

    [Header(">>> 場景上模型 - DCR")]
    [SerializeField] private List<DeviceModel_DCR> modelDCRList;

    [Header(">>> 從WebAPI取得的DCR列表")]
    [SerializeField] private List<SO_DCR> soDCRList;

    [Header(">>> 右上角DCR基本資訊列表")]
    [SerializeField] private Panel_DeviceInfo panel_DeviceInfo;
    [Header(">>> DCR面板 - RU佈局排列")]
    [SerializeField] private Panel_DCR_RU panel_DCR_RUInfo;
    [Header(">>>COBie面板")]
    [SerializeField] private Panel_COBie panelCOBie;

    [Header(">>> 點擊設備模型時Invoke deviceId")]
    public UnityEvent<string> onClickDevice = new UnityEvent<string>();

    /// <summary>
    /// 以字典按elementId儲存各個DCR模型
    /// </summary>
    private Dictionary<string, DeviceModel_DCR> modelDcrDict = new Dictionary<string, DeviceModel_DCR>();

    /// <summary>
    /// 上一次點選的設備模型
    /// <para>+ 父類別有T泛型，所以不能把子類別指派給父類別</para>
    /// <para>+ 可以用Interface來進行指派</para>
    /// <para>+ 將父類別物件轉型成子類別物件，即可抓取其子類別參數</para>
    /// </summary>
    private IDeviceModel currentSelectedModel = null;

    private void Awake()
    {
        //設定DCS 材質 Dictionary
        dcsTextureList.ForEach(texture => dcsTextureDictionary[texture.name] = texture);

        //設定DCS點擊後，傳遞soData給資訊面板 
        modelDCRList.ForEach(deviceModelDCR =>
        {
            //以字典儲存各個DCR模型
            modelDcrDict[deviceModelDCR.elementId] = deviceModelDCR;

            deviceModelDCR.onToggleChanged.AddListener(OnModelClicked);
        });

        // 當RU列表項目被點擊時
        //panel_DCR_RUInfo.onToggledEvent.AddListener(OnModelClicked);
    }

    /// <summary>
    /// 當DCR/DCS/DCN模型被點擊時
    /// </summary>
    private void OnModelClicked(IDeviceModel deviceModel)
    {
        if (deviceModel.isSelected == false) return;

        if (deviceModel is RU_DCSListItem item)
        {
            if (currentSelectedModel == (deviceModel as RU_DCSListItem).modelDCSDCN) return;
            currentSelectedModel = (deviceModel as RU_DCSListItem).modelDCSDCN;
            currentSelectedModel.isSelected = true;
            return;
        }
        else
        {
            if (currentSelectedModel == deviceModel) return;
            //取消上一個模型的選取狀態
            if (currentSelectedModel != null) currentSelectedModel.isSelected = false;
            currentSelectedModel = deviceModel;
        }

        bool isDCR = currentSelectedModel.system == "DCR";

        // 設置soDCR給相關面板並顯示
        panel_DeviceInfo.gameObject.SetActive(true);
        panel_DCR_RUInfo.gameObject.SetActive(true);

        if (isDCR)
        {
            panel_DeviceInfo.soDCR = (currentSelectedModel as DeviceModel_DCR).soData;
            panel_DCR_RUInfo.SetModelDCR(currentSelectedModel as DeviceModel_DCR);
        }
        else if (currentSelectedModel.system == "DCS" || currentSelectedModel.system == "DCN")
        {
            onClickDevice?.Invoke((currentSelectedModel as DeviceModel_DCSDCN).soData.deviceId);
        }
    }

    /// <summary>
    /// 取得WebAP資料後，設置給每一台DCR，並動態建立其DCS/DCN
    /// </summary>
    public void Parse_AllDCRInfo(string jsonString)
    {
        // 解析JSON
        List<Dictionary<string, string>> dataSet_DCR = JsonUtils.ParseJsonArray(jsonString);

        //建立DCR資料List
        soDCRList = new List<SO_DCR>();
        dataSet_DCR.ForEach(dataSet =>
        {
            SO_DCR soDCR = ScriptableObject.CreateInstance<SO_DCR>();
            soDCR.SetSourceDataDict(dataSet);
            soDCRList.Add(soDCR);
        });

        //資料儲存給每個一DCR模型
        soDCRList.ForEach(soDCR =>
        {
            modelDcrDict[soDCR.elementId].soData = soDCR;
            modelDcrDict[soDCR.elementId].CreateDeviceDCSfromDict(dcsTextureDictionary, dcsPrefab);
        });
    }

    /// <summary>
    /// 取得WebAP資料後，解析COBie後給其面板資訊
    /// </summary>
    internal void Parse_COBie(string jsonString)
    {
        SO_COBie soCOBie = ScriptableObject.CreateInstance<SO_COBie>();
        soCOBie.Parse(jsonString);

        panelCOBie.soData = soCOBie;
    }
}