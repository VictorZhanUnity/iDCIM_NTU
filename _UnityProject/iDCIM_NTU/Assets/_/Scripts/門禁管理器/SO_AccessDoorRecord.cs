using System;
using UnityEngine;

[CreateAssetMenu(fileName = "門禁記錄資料", menuName = ">>VictorDev<</ScriptableObject/門禁管理/門禁記錄資料SO")]
/// <summary>
/// 門禁記錄資料
/// </summary>
public class SO_AccessDoorRecord : ScriptableObject
{
    [Header(">>> 門禁編號")]
    [SerializeField] private string _id;
    [Header(">>> 刷卡人員")]
    [SerializeField] private SO_Account _user;
    [Header(">>> 記錄時間")]
    [SerializeField] private DateTime _timeStamp;
    [Header(">>> 狀態")]
    [SerializeField] private string _isEntering;
    [Header(">>> 門口資料")]
    [SerializeField] private SO_AccessDoor _accessDoor;

    #region [ >>> Getter]
    /// <summary>
    /// 門禁編號
    /// </summary>
    public string id => _id;
    /// <summary>
    /// 刷卡人員
    /// </summary>
    public SO_Account user => _user;
    /// <summary>
    /// 狀態
    /// </summary>
    public string isEntering => _isEntering;
    /// <summary>
    /// 記錄時間
    /// </summary>
    public DateTime timeStamp => _timeStamp;
    /// <summary>
    /// 門口資料
    /// </summary>
    public SO_AccessDoor accessDoor => _accessDoor;
    #endregion
}
