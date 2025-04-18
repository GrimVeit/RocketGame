using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemsBuyView : View, IIdentify
{
    public string GetID() => id;

    [SerializeField] private string id;
    [SerializeField] private ItemBuySizes itemBuySizes;
    [SerializeField] private TextMeshProUGUI textTypeItem;
    [SerializeField] private TextMeshProUGUI textPrice;
    [SerializeField] private Button buttonBuy;
    [SerializeField] private ItemBuy itemBuyPrefab;
    [SerializeField] private Transform transformItems;
    [SerializeField] private GridLayoutGroup gridLayoutGroup;

    public void Initialize()
    {
        buttonBuy.onClick.AddListener(() => OnBuyItemGroup?.Invoke());
    }

    public void Dispose()
    {
        buttonBuy.onClick.RemoveListener(() => OnBuyItemGroup?.Invoke());
    }

    #region Input

    public void SetData(ItemGroup itemGroup)
    {
        textTypeItem.text = itemGroup.Title.ToUpper();
        textPrice.text = itemGroup.Price.ToString();

        gridLayoutGroup.cellSize = itemBuySizes.GetSizeById(itemGroup.ID);
        gridLayoutGroup.spacing = new Vector2(0, itemBuySizes.GetSpaceById(itemGroup.ID));

        foreach (Transform item in transformItems)
        {
            Destroy(item.gameObject);
        }

        for (int i = 0; i < itemGroup.items.Count; i++)
        {
            var itemBuy = Instantiate(itemBuyPrefab, transformItems);
            itemBuy.SetData(itemGroup.items[i].SpritePreview);
        }
    }

    #endregion

    #region Output

    public event Action OnBuyItemGroup;

    #endregion
}

[System.Serializable]
public class ItemBuySizes
{
    [SerializeField] private List<ItemBuySize> sizes = new List<ItemBuySize>();

    public Vector2 GetSizeById(int id)
    {
        var sizeClass = sizes.FirstOrDefault(size => size.ID == id);

        if(sizeClass == null)
        {
            Debug.LogError("Not found size by id -" + id);
            return Vector2.zero;
        }

        return sizeClass.Size;
    }

    public float GetSpaceById(int id)
    {
        var sizeClass = sizes.FirstOrDefault(size => size.ID == id);

        if (sizeClass == null)
        {
            Debug.LogError("Not found size by id -" + id);
            return 0;
        }

        return sizeClass.Space;
    }
}

[System.Serializable]
public class ItemBuySize
{
    [SerializeField] private int id;
    [SerializeField] private Vector2 size;
    [SerializeField] private float space;

    public int ID => id;
    public Vector2 Size => size;
    public float Space => space;
}
