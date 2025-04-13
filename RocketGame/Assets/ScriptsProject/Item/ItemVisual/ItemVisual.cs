using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemVisual : MonoBehaviour
{
    public int ID => id;

    [SerializeField] private int id;
    [SerializeField] private GameObject objectItem;
    [SerializeField] private Image imageitem;

    public void SetData(Sprite sprite)
    {
        imageitem.sprite = sprite;
    }

    public void Activate()
    {
        objectItem.SetActive(true);
    }

    public void Deactivate()
    {
        objectItem.SetActive(false);
    }
}
