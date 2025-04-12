using System;
using System.Collections.Generic;

public class ItemsBuyModel
{
    public event Action<ItemGroup> OnSetItemGroup;

    public List<IStoreOpenItems> _storeOpenItems;

    private IStoreOpenItems _currentStore;
    private ItemGroup _currentItemGroup;

    private IMoneyProvider _moneyProvider;

    private int maxCountBuy => _storeOpenItems.Count;
    private int currentCountBuy;

    public ItemsBuyModel(List<IStoreOpenItems> storeOpenItems, IMoneyProvider moneyProvider)
    {
        _storeOpenItems = storeOpenItems;
        _moneyProvider = moneyProvider;
    }

    public void Initialize()
    {
        _storeOpenItems.ForEach(sot => sot.OnSelectOpenItems += SetItems);
    }

    public void Dispose()
    {
        _storeOpenItems.ForEach(sot => sot.OnSelectOpenItems -= SetItems);
    }

    private void SetItems(IStoreOpenItems storeOpenItems, ItemGroup itemGroup)
    {
        _currentStore = storeOpenItems;
        _currentItemGroup = itemGroup;
    }

    public void OpenItems()
    {
        if (!_moneyProvider.CanAfford(_currentItemGroup.Price)) return;

        _moneyProvider.SendMoney(-_currentItemGroup.Price);
        _currentStore.OpenItems();
    }
}
