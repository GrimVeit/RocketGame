using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemVisualView : View, IIdentify
{
    public string GetID() => id;

    [SerializeField] private string id;
    [SerializeField] private List<ItemVisual> itemVisuals = new List<ItemVisual>();

    public void Initialize()
    {
        itemVisuals.ForEach(iv => iv.Deactivate());
    }

    public void Dispose()
    {

    }

    private ItemVisual GetItemVisualById(int id)
    {
        return itemVisuals.FirstOrDefault(iv => iv.ID == id);
    }

    #region Input

    public void SetVisual(int index, Item item)
    {
        var itemVisual = GetItemVisualById(index);

        if(itemVisual == null)
        {
            Debug.LogError($"Not found Item Visual by id - {index}");
            return;
        }

        itemVisual.SetData(item.SpriteItem);
        itemVisual.Activate();
    }

    #endregion
}
