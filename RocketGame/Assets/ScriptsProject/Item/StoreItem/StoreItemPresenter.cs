using System;

public class StoreItemPresenter : IStoreOpenItems
{
    private readonly StoreItemModel _model;

    public StoreItemPresenter(StoreItemModel model)
    {
        _model = model;
    }

    public void Initialize()
    {
        _model.Initialize();
    }

    public void Dispose()
    {
        _model.Dispose();
    }

    #region Input

    public void SelectItemGroupForBuyItemGroup(int indexGroup)
    {
        _model.SelectItemGroupForBuyItemGroup(indexGroup);
    }

    public void SelectItemGroupForSelectItem(int indexGroup)
    {
        _model.SelectItemGroupForSelectItem(indexGroup);
    }

    public void OpenItemGroup(int id)
    {
        _model.OpenItemGroup(id);
    }

    public void SelectItem(int groubId, int itemId)
    {
        _model.SelectItem(groubId, itemId);
    }

    #endregion

    #region Output

    public event Action OnOpenAllItems
    {
        add => _model.OnOpenAllItems += value;
        remove => _model.OnOpenAllItems -= value;
    }

    public event Action OnOpenItems_None
    {
        add => _model.OnOpenItems_None += value;
        remove => _model.OnOpenItems_None -= value;
    }

    public event Action OnSelectItem_None
    {
        add => _model.OnSelectItem_None += value;
        remove => _model.OnSelectItem_None -= value;
    }




    public event Action<ItemGroup> OnOpenItems
    {
        add => _model.OnOpenItems += value;
        remove => _model.OnOpenItems -= value;
    }

    public event Action<ItemGroup> OnCloseItems
    {
        add => _model.OnCloseItems += value;
        remove => _model.OnCloseItems -= value;
    }

    public event Action<ItemGroup, Item> OnSelectItem
    {
        add => _model.OnSelectItem += value;
        remove => _model.OnSelectItem -= value;
    }

    public event Action<ItemGroup, Item> OnDeselectItem
    {
        add => _model.OnDeselectItem += value;
        remove => _model.OnDeselectItem -= value;
    }



    public event Action<ItemGroup> OnSelectItemGroupForBuyItemGroup
    {
        add => _model.OnSelectItemGroupForBuyItemGroup += value;
        remove => _model.OnSelectItemGroupForBuyItemGroup -= value;
    }

    public event Action<ItemGroup> OnSelectItemGroupForSelectItem
    {
        add => _model.OnSelectItemGroupForSelectItem += value;
        remove => _model.OnSelectItemGroupForSelectItem -= value;
    }
    #endregion
}

public interface IStoreOpenItems
{
    public event Action<ItemGroup> OnSelectItemGroupForBuyItemGroup;
    void OpenItemGroup(int id);
}
