using System;
using UnityEngine;
using UnityEngine.UI;

public class ItemSelect : MonoBehaviour
{
    public int ID => id;

    [SerializeField] private Image imageItem;
    [SerializeField] private Image imageButton;
    [SerializeField] private Sprite spriteSelectButton;
    [SerializeField] private Sprite spriteDeselectButton;
    [SerializeField] private Button buttonSelect;
    private int id;

    public void Initialize()
    {
        buttonSelect.onClick.AddListener(() => OnSelectItem?.Invoke(id));
    }

    public void Dispose()
    {
        buttonSelect.onClick.RemoveListener(() => OnSelectItem?.Invoke(id));
    }

    public void SetData(Item item)
    {
        imageItem.sprite = item.SpritePreview;
        id = item.ID;
    }

    public void Select()
    {
        imageButton.sprite = spriteSelectButton;
        buttonSelect.enabled = false;
    }

    public void Deselect()
    {
        imageButton.sprite = spriteDeselectButton;
        buttonSelect.enabled = true;
    }

    #region Input

    public event Action<int> OnSelectItem;

    #endregion
}
