using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemBuy : MonoBehaviour
{
    [SerializeField] private Image imageItem;

    public void SetData(Sprite sprite)
    {
        imageItem.sprite = sprite;
    }
}
