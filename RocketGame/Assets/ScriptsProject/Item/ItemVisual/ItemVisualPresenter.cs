using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemVisualPresenter
{
    public readonly ItemVisualView _view;

    public ItemVisualPresenter(ItemVisualView view)
    {
        _view = view;
    }

    public void Initialize()
    {
        _view.Initialize();
    }

    public void Dispose()
    {
        _view.Dispose();
    }

    #region Input

    public void SetVisual(ItemGroup itemGroup, Item item)
    {
        _view.SetVisual(itemGroup.ID, item);
    }

    #endregion
}
