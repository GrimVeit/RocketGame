using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemGroup", menuName = "Game/Item/New Group")]
public class ItemGroup : ScriptableObject
{
    public List<Item> items = new List<Item>();

    public Item GetItemById(int id)
    {
        return items.FirstOrDefault(i => i.ID == id);
    }
}
