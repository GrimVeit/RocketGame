using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OpenItemView : View, IIdentify
{
    public string GetID() => id;

    [SerializeField] private string id;
    [SerializeField] private List<OpenItem> items = new List<OpenItem>();

    public void Initialize()
    {
        items.ForEach(i =>
        {
            i.OnChooseSelectItemGroup += HandleChooseSelectItemGroup;
            i.Initialize();
        });
    }

    public void Dispose()
    {
        items.ForEach(i =>
        {
            i.OnChooseSelectItemGroup -= HandleChooseSelectItemGroup;
            i.Dispose();
        });
    }

    private OpenItem GetOpenItemById(int id)
    {
        return items.FirstOrDefault(i => i.ID == id);
    }

    #region Input

    public void Activate(int index)
    {
        var item = GetOpenItemById(index);

        if(item == null)
        {
            Debug.LogError("Not found open item by id - " + index);
            return;
        }

        item.Activate();
    }

    public void Deactivate(int index)
    {
        var item = GetOpenItemById(index);

        if (item == null)
        {
            Debug.LogError("Not found open item by id - " + index);
            return;
        }

        item.Deactivate();
    }

    #endregion

    #region Input

    public event Action<int> OnChooseSelectItemGroup;

    private void HandleChooseSelectItemGroup(int index)
    {
        OnChooseSelectItemGroup?.Invoke(index);
    }

    #endregion
}
