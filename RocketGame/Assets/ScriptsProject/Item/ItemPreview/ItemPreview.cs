using System;
using UnityEngine;
using UnityEngine.UI;

public class ItemPreview : MonoBehaviour
{
    public int ID => id;

    [SerializeField] private int id;
    [SerializeField] private Button buttonItem;
    [SerializeField] private GameObject objectPreview;

    public void Initialize()
    {
        buttonItem.onClick.AddListener(() => OnChooseBuyItemGroup?.Invoke(id));
    }

    public void Dispose()
    {
        buttonItem.onClick.RemoveListener(() => OnChooseBuyItemGroup?.Invoke(id));
    }

    public void Activate()
    {
        objectPreview.SetActive(true);
        buttonItem.enabled = true;
    }

    public void Deactivate()
    {
        objectPreview.SetActive(false);
        buttonItem.enabled = false;
    }

    #region Input

    public event Action<int> OnChooseBuyItemGroup;

    #endregion
}
