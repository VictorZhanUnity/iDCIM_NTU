using UnityEngine;
using UnityEngine.UI;
using VictorDev.UI;

/// <summary>
/// CCTV¦Cªí
/// </summary>
public class CCTVList : ScrollRectToggleList<CCTVListItem, SO_CCTV>
{
    [Header(">>> ·j´M¿é¤J®Ø")]
    [SerializeField] private SearchBar searchBar;

    private void Awake()
    {
        SetDataList(SoDataList);
        searchBar.onClickSearchButton.AddListener(OnSearchHandler);
    }


    private void OnSearchHandler(string searchString)
    {
        Debug.Log($"OnSearchHandler: {searchString}");
    }

    private void OnValidate()
    {
        searchBar ??= transform.Find("SearchBar").GetComponent<SearchBar>();
    }
}
