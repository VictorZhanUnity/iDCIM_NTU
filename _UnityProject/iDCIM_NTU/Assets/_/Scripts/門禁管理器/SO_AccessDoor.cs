using UnityEngine;

[CreateAssetMenu(fileName = "門口資料", menuName = ">>VictorDev<</ScriptableObject/門禁管理/門口資料SO")]
/// <summary>
/// 門禁SO
/// </summary>
public class SO_AccessDoor : ScriptableObject
{
    [Header(">>> 棟別")]
    [SerializeField] private string _building;
    [Header(">>> 樓層")]
    [SerializeField] private string _floor;
    [Header(">>> 設備編碼")]
    [SerializeField] private string _deviceCode;
    [Header(">>> 設備名稱")]
    [SerializeField] private string _deviceName;
    [Header(">>> 目前是否開啟")]
    [SerializeField] private bool _isOpen;

    #region [ >>> Getter]
    /// <summary>
    /// 棟別
    /// </summary>
    public string building => _building;
    /// <summary>
    /// 樓層
    /// </summary>
    public string floor => _floor;
    /// <summary>
    /// 設備編碼
    /// </summary>
    public string deviceCode => _deviceCode;
    /// <summary>
    /// 設備名稱
    /// </summary>
    public string deviceName => _deviceName;
    /// <summary>
    /// 目前是否開啟
    /// </summary>
    public bool isOpen => _isOpen;
    #endregion
}
