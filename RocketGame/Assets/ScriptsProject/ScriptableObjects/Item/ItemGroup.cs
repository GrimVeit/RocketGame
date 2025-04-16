using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "ItemGroup", menuName = "Game/Item/New Group")]
public class ItemGroup : ScriptableObject
{
    [SerializeField] private int id;
    [SerializeField] private string title;
    [SerializeField] private int price;
    private ItemDatas itemDatas;

    public int ID => id;
    public List<Item> items = new List<Item>();
    public string Title => title;
    public int Price => price;
    public ItemDatas ItemDatas => itemDatas;

    public Item GetItemById(int id)
    {
        return items.FirstOrDefault(i => i.ID == id);
    }

    public Item GetRandomItem()
    {
        var index = Random.Range(0, items.Count);

        return items[index];
    }

    public void SetData(ItemDatas itemDatas)
    {
        this.itemDatas = itemDatas;
    }
}
