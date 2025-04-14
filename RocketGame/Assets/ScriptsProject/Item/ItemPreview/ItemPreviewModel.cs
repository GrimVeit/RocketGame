using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPreviewModel
{
    public event Action<int> OnActivateItemGroup;
    public event Action<int> OnDeactivateItemGroup;

    public event Action<int> OnChooseBuyItemGroup;

    public void Activate(ItemGroup itemGroup)
    {
        OnActivateItemGroup?.Invoke(itemGroup.ID);
    }

    public void Deactivate(ItemGroup itemGroup)
    {
        OnDeactivateItemGroup?.Invoke(itemGroup.ID);
    }

    public void ChooseBuyItemGroup(int indexGroup)
    {
        OnChooseBuyItemGroup?.Invoke(indexGroup);
    }
}
