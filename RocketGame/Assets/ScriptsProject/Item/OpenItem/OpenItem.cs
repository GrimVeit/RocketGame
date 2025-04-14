using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OpenItem : MonoBehaviour
{
    public int ID => id;

    [SerializeField] private int id;
    [SerializeField] private Button buttonChooseSelect;
    [SerializeField] private Image imageOpenItem;
    [SerializeField] private Sprite spriteActive;
    [SerializeField] private Sprite spriteDeactive;
    [SerializeField] private TextMeshProUGUI textItem;
    [SerializeField] private Color colorOpen;
    [SerializeField] private Color colorClose;

    public void Initialize()
    {
        buttonChooseSelect.onClick.AddListener(() => OnChooseSelectItemGroup?.Invoke(id));
    }

    public void Dispose()
    {
        buttonChooseSelect.onClick.RemoveListener(() => OnChooseSelectItemGroup?.Invoke(id));
    }

    #region Input

    public void Activate()
    {
        imageOpenItem.sprite = spriteActive;
        textItem.color = colorOpen;
        buttonChooseSelect.enabled = true;
    }

    public void Deactivate()
    {
        imageOpenItem.sprite = spriteDeactive;
        textItem.color = colorClose;
        buttonChooseSelect.enabled = false;
    }

    #endregion

    #region Output

    public event Action<int> OnChooseSelectItemGroup;

    #endregion
}
