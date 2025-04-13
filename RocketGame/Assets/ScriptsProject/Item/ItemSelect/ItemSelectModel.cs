using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSelectModel
{
    public event Action<int, int> OnChooseItemForSelect;

    public event Action<ItemGroup> OnSetItemData;
    public event Action<int> OnSelectItem;
    public event Action<int> OnDeselectItem;

    private int _currentGroupId;

    public void SetItemGroup(ItemGroup itemGroup)
    {
        _currentGroupId = itemGroup.ID;

        OnSetItemData?.Invoke(itemGroup);
    }

    public void Select(ItemGroup itemGroup, Item item)
    {
        if (_currentGroupId != itemGroup.ID) return;

        OnSelectItem?.Invoke(item.ID);

    }

    public void Deselect(ItemGroup itemGroup, Item item)
    {
        if (_currentGroupId != itemGroup.ID) return;

        OnDeselectItem?.Invoke(item.ID);
    }

    public void ChooseItemForSelect(int indextem)
    {
        OnChooseItemForSelect?.Invoke(_currentGroupId, indextem);
    }
}
