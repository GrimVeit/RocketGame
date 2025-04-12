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

    public event Action<IStoreOpenItems, ItemGroup> OnSelectOpenItems
    {
        add => _model.OnSelectOpenItems += value;
        remove => _model.OnSelectOpenItems -= value;
    }

    #endregion

    #region Output

    public void OpenItems()
    {
        _model.OpenItems();
    }

    #endregion
}

public interface IStoreOpenItems
{
    public event Action<IStoreOpenItems, ItemGroup> OnSelectOpenItems;
    void OpenItems();
}
