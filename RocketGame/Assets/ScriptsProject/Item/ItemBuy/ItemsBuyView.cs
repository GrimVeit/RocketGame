using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemsBuyView : View
{
    [SerializeField] private TextMeshProUGUI textTypeItem;
    [SerializeField] private TextMeshProUGUI textPrice;
    [SerializeField] private Button buttonBuy;

    public void SetData(ItemGroup itemGroup)
    {
        textTypeItem.text = itemGroup.Title;
        textPrice.text = itemGroup.Price.ToString();
    }
}
