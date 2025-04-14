using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSelectView : View
{
    [SerializeField] private ItemBuySizes itemBuySizes;
    [SerializeField] private TextMeshProUGUI textTypeItem;
    [SerializeField] private ItemSelect itemSelectPrefab;
    [SerializeField] private Transform transformItems;
    [SerializeField] private GridLayoutGroup gridLayoutGroup;

    private readonly List<ItemSelect> itemSelects = new();

    public void Initialize()
    {

    }

    public void Dispose()
    {

    }

    private ItemSelect GetItemSelectById(int id)
    {
        if(itemSelects.Count == 0)
            return null;

        return itemSelects.FirstOrDefault(itemSelect => itemSelect.ID == id);
    }

    private void ClearItems()
    {
        if(itemSelects.Count > 0)
        {
            itemSelects.ForEach(itemSelect => 
            {
                itemSelect.OnSelectItem -= HandleSelectItem;
                itemSelect.Dispose();
            });
        }
        itemSelects.Clear();

        foreach (Transform item in transformItems)
        {
            Destroy(item.gameObject);
        }
    }

    #region Input

    public void SetItems(ItemGroup itemGroup)
    {
        ClearItems();

        textTypeItem.text = itemGroup.Title.ToUpper();

        gridLayoutGroup.cellSize = itemBuySizes.GetSizeById(itemGroup.ID);
        gridLayoutGroup.spacing = new Vector2(0, itemBuySizes.GetSpaceById(itemGroup.ID));

        for (int i = 0; i < itemGroup.items.Count; i++)
        {
            var itemBuy = Instantiate(itemSelectPrefab, transformItems);
            itemBuy.SetData(itemGroup.items[i]);
            itemBuy.OnSelectItem += HandleSelectItem;
            itemBuy.Initialize();

            if (itemGroup.items[i].ItemData.IsSelect)
            {
                itemBuy.Select();
            }
            else
            {
                itemBuy.Deselect();
            }

            itemSelects.Add(itemBuy);
        }
    }

    public void Select(int id)
    {
        var itemSelect = GetItemSelectById(id);

        if(itemSelect == null)
        {
            Debug.LogWarning($"Not found Item Select by id - {id}");
            return;
        }

        itemSelect.Select();
    }

    public void Deselect(int id)
    {
        var itemSelect = GetItemSelectById(id);

        if (itemSelect == null)
        {
            Debug.LogWarning($"Not found Item Select by id - {id}");
            return;
        }

        itemSelect.Deselect();
    }

    #endregion

    #region Output

    public event Action<int> OnSelectItem;

    private void HandleSelectItem(int id)
    {
        OnSelectItem?.Invoke(id);
    }

    #endregion
}
