using UnityEngine;

[CreateAssetMenu(fileName = "人員資料", menuName = ">>VictorDev<</ScriptableObject/門禁管理/人員資料SO")]
/// <summary>
/// 門禁SO
/// </summary>
public class SO_Account : ScriptableObject
{
    [Header(">>> 使用者名稱")]
    [SerializeField] private string _userName;

    #region [ >>> Getter]
    /// <summary>
    /// 使用者名稱
    /// </summary>
    public string userName => _userName;
    #endregion
}
