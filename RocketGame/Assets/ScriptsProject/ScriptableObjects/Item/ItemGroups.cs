using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemGroup", menuName = "Game/Item/New Groups")]
public class ItemGroups : ScriptableObject
{
    public List<ItemGroup> itemGroups = new();

    public ItemGroup GetItemGroupByType(ItemType itemType)
    {
        return itemGroups.FirstOrDefault(i => i.ItemType == itemType);
    }
}
