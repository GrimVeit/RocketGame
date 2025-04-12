using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreItemPresenter : IStoreOpenItems
{
    private StoreItemModel _model;

    public StoreItemPresenter(StoreItemModel model)
    {
        _model = model;
    }

    public void Initialize()
    {

    }

    public void Dispose()
    {

    }

    #region Input

    public void OpenItems()
    {

    }

    public void SelectForBuyItemGroup(int index)
    {
        Debug.Log(index);

        _model.SelectForBuyItemGroup(index);
    }

    #endregion

    #region Output

    public event Action<ItemGroup> OnSelectOpenItems
    {
        add => _model.OnSelectOpenItems += value;
        remove => _model.OnSelectOpenItems -= value;
    }

    #endregion
}

public interface IStoreOpenItems
{
    public event Action<ItemGroup> OnSelectOpenItems;
    void OpenItems();
}
