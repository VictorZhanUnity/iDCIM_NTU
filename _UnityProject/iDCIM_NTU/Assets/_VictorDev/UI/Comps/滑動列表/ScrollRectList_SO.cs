using System;
using UnityEngine;

public abstract class ScrollRectList_SO : ScriptableObject
{
    /// <summary>
    /// 建構式
    /// </summary>
    /// <param name="createRandomData">是否自動產生隨機資料</param>
    public ScrollRectList_SO(bool createRandomData = true)
    {
        if (createRandomData) _SetupRandomData();
    }

    protected ScrollRectList_SO()
    {
        Debug.Log("Called");
    }

    private void OnValidate() => _SetupRandomData();
    public abstract void _SetupRandomData();
}