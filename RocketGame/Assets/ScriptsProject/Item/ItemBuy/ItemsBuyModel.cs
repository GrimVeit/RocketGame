using System;

public class ItemsBuyModel
{
    public event Action<ItemGroup> OnSetItemGroup;

    private ItemGroup _currentItemGroup;

    private IMoneyProvider _moneyProvider;
    private IStoreOpenItems _storeOpenItems;

    public ItemsBuyModel(IStoreOpenItems storeOpenItems, IMoneyProvider moneyProvider)
    {
        _storeOpenItems = storeOpenItems;
        _moneyProvider = moneyProvider;
    }

    public void Initialize()
    {
        _storeOpenItems.OnSelectItemGroupForBuyItemGroup += SetItemGroup;
    }

    public void Dispose()
    {
        _storeOpenItems.OnSelectItemGroupForBuyItemGroup -= SetItemGroup;
    }

    public void BuyItems()
    {
        if (!_moneyProvider.CanAfford(_currentItemGroup.Price)) return;

        _moneyProvider.SendMoney(-_currentItemGroup.Price);
        _storeOpenItems.OpenItemGroup(_currentItemGroup.ID);
    }

    private void SetItemGroup(ItemGroup itemGroup)
    {
        _currentItemGroup = itemGroup;
        OnSetItemGroup?.Invoke(_currentItemGroup);
    }
}
