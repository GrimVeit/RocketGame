using System;

public class ItemsBuyModel
{
    public event Action<ItemGroup> OnSetItemGroup;

    private ItemGroup _currentItemGroup;

    private readonly ISoundProvider _soundProvider;
    private readonly IMoneyProvider _moneyProvider;
    private readonly IStoreOpenItems _storeOpenItems;

    public ItemsBuyModel(IStoreOpenItems storeOpenItems, IMoneyProvider moneyProvider, ISoundProvider soundProvider)
    {
        _storeOpenItems = storeOpenItems;
        _moneyProvider = moneyProvider;
        _soundProvider = soundProvider;
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

        _soundProvider.PlayOneShot("Click");
        _moneyProvider.SendMoney(-_currentItemGroup.Price);
        _storeOpenItems.OpenItemGroup(_currentItemGroup.ID);
    }

    private void SetItemGroup(ItemGroup itemGroup)
    {
        _currentItemGroup = itemGroup;
        OnSetItemGroup?.Invoke(_currentItemGroup);
    }
}
