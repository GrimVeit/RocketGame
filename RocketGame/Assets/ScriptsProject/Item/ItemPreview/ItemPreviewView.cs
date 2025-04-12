using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemPreviewView : View
{
    [SerializeField] private List<ItemPreview> itemPreviews = new();

    public void Initialize()
    {
        itemPreviews.ForEach(itemPreview => 
        {
            itemPreview.OnChooseBuyItemGroup += HandleChooseBuyItemGroup;
            itemPreview.Initialize();
        });
    }

    public void Dispose()
    {
        itemPreviews.ForEach(itemPreview =>
        {
            itemPreview.OnChooseBuyItemGroup -= HandleChooseBuyItemGroup;
            itemPreview.Dispose();
        });
    }

    public void Activate(int index)
    {
        var itemPreview = GetItemPreviewById(index);

        if(itemPreview == null)
        {
            Debug.LogError($"Not found item preview by id - {index}");
            return;
        }

        itemPreview.Activate();
    }

    public void Deactivate(int index)
    {
        var itemPreview = GetItemPreviewById(index);

        if (itemPreview == null)
        {
            Debug.LogError($"Not found item preview by id - {index}");
            return;
        }

        itemPreview.Deactivate();
    }

    private ItemPreview GetItemPreviewById(int id)
    {
        return itemPreviews.FirstOrDefault(ip => ip.ID == id);
    }

    #region Output

    public event Action<int> OnChooseBuyItemGroup;

    private void HandleChooseBuyItemGroup(int index)
    {
        OnChooseBuyItemGroup?.Invoke(index);
    }

    #endregion
}
