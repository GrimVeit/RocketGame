using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Game/Item/New Item")]
public class Item : ScriptableObject
{
    [SerializeField] private int id;
    [SerializeField] private string description;
    [SerializeField] private Sprite spriteItem;
    private ItemData itemData;

    public int ID => id;
    public string Description => description;
    public Sprite Sprite => spriteItem;
    public ItemData ItemData => itemData;

    public void SetData(ItemData data)
    {
        itemData = data;
    }
}
