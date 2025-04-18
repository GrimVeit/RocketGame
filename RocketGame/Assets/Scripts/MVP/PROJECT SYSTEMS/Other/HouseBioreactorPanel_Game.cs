using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HouseBioreactorPanel_Game : MovePanel
{
    [SerializeField] private Button buttonBedroom;
    [SerializeField] private Button buttonStorage;
    [SerializeField] private List<Button> buttonsBuyItems;
    [SerializeField] private List<Button> buttonsSelectItems;

    public override void Initialize()
    {
        base.Initialize();

        buttonBedroom.onClick.AddListener(() => OnClickToBedroom?.Invoke());
        buttonStorage.onClick.AddListener(() => OnClickToStorage?.Invoke());
        buttonsSelectItems.ForEach(b => b.onClick.AddListener(() => OnClickToSelectItems?.Invoke()));
        buttonsBuyItems.ForEach(b => b.onClick.AddListener(() => OnClickToBuyItems?.Invoke()));
    }

    public override void Dispose()
    {
        base.Dispose();

        buttonBedroom.onClick.RemoveListener(() => OnClickToBedroom?.Invoke());
        buttonStorage.onClick.RemoveListener(() => OnClickToStorage?.Invoke());
        buttonsSelectItems.ForEach(b => b.onClick.RemoveListener(() => OnClickToSelectItems?.Invoke()));
        buttonsBuyItems.ForEach(b => b.onClick.RemoveListener(() => OnClickToBuyItems?.Invoke()));
    }

    #region Input

    public event Action OnClickToBedroom;
    public event Action OnClickToStorage;
    public event Action OnClickToBuyItems;
    public event Action OnClickToSelectItems;

    #endregion
}
