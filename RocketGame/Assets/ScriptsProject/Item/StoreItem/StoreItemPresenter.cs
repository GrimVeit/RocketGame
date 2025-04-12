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

    public void OpenItems()
    {

    }

    public void SelectItemGroupForBuyItemGroup(int indexGroup)
    {
        _model.SelectItemGroupForBuyItemGroup(indexGroup);
    }

    public void SelectItemGroupForSelectItem(int indexGroup)
    {
        _model.SelectItemGroupForBuyItemGroup(indexGroup);
    }

    #endregion

    #region Output

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

    public event Action<ItemGroup> OnSelectItemGroupForBuyItemGroup
    {
        add => _model.OnSelectItemGroupForBuyItemGroup += value;
        remove => _model.OnSelectItemGroupForBuyItemGroup -= value;
    }

    public event Action<ItemGroup, int> OnSelectItem
    {
        add => _model.OnSelectItem += value;
        remove => _model.OnSelectItem -= value;
    }

    public event Action<ItemGroup, int> OnDeselectItem
    {
        add => _model.OnDeselectItem += value;
        remove => _model.OnDeselectItem -= value;
    }

    #endregion
}

public interface IStoreOpenItems
{
    public event Action<ItemGroup> OnSelectItemGroupForBuyItemGroup;
    void OpenItems();
}
