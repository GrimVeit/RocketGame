using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HouseBedroomBuyItemPanel_Game : Panel_Move
{
    [SerializeField] private Button buttonExit;

    public override void Initialize()
    {
        base.Initialize();

        buttonExit.onClick.AddListener(() => OnClickToExit?.Invoke());
    }

    public override void Dispose()
    {
        base.Dispose();

        buttonExit.onClick.RemoveListener(() => OnClickToExit?.Invoke());
    }

    #region Input

    public event Action OnClickToExit;

    #endregion
}
