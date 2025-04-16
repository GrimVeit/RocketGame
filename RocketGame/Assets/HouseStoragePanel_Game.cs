using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HouseStoragePanel_Game : MovePanel
{
    [SerializeField] private Button buttonBioreactor;
    [SerializeField] private List<Button> buttonsBuyItems;
    [SerializeField] private List<Button> buttonsSelectItems;

    public override void Initialize()
    {
        base.Initialize();

        buttonBioreactor.onClick.AddListener(() => OnClickToBioreactor?.Invoke());
        buttonsSelectItems.ForEach(b => b.onClick.AddListener(() => OnClickToSelectItems?.Invoke()));
        buttonsBuyItems.ForEach(b => b.onClick.AddListener(() => OnClickToBuyItems?.Invoke()));
    }

    public override void Dispose()
    {
        base.Dispose();

        buttonBioreactor.onClick.RemoveListener(() => OnClickToBioreactor?.Invoke());
        buttonsSelectItems.ForEach(b => b.onClick.RemoveListener(() => OnClickToSelectItems?.Invoke()));
        buttonsBuyItems.ForEach(b => b.onClick.RemoveListener(() => OnClickToBuyItems?.Invoke()));
    }

    #region Input

    public event Action OnClickToBioreactor;
    public event Action OnClickToBuyItems;
    public event Action OnClickToSelectItems;

    #endregion
}
