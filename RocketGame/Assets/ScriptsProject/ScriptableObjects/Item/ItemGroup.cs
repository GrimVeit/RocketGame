using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemGroup", menuName = "Game/Item/New Group")]
public class ItemGroup : ScriptableObject
{
    [SerializeField] private int id;
    [SerializeField] private string title;
    [SerializeField] private int price;
    [SerializeField] private ItemType itemType;

    public int ID => id;
    public List<Item> items = new List<Item>();
    public string Title => title;
    public int Price => price;
    public ItemType ItemType => itemType;

    public Item GetItemById(int id)
    {
        return items.FirstOrDefault(i => i.ID == id);
    }
}

public enum ItemType
{
    Bed, Rug, Monitor, Lighting, Nightstand
}
