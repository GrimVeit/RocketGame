using System;
using UnityEngine;
using UnityEngine.UI;

public class ItemPreview : MonoBehaviour
{
    [SerializeField] private int id;
    [SerializeField] private Button buttonItem;
    [SerializeField] private GameObject objectPreview;

    public void Initialize()
    {
        buttonItem.onClick.AddListener(() => OnChooseItemGroup?.Invoke(id));
    }

    public void Dispose()
    {
        buttonItem.onClick.RemoveListener(() => OnChooseItemGroup?.Invoke(id));
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

    public event Action<int> OnChooseItemGroup;

    #endregion
}
