using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinPanel_Game : MovePanel
{
    [SerializeField] private Button buttonClose;

    public override void Initialize()
    {
        base.Initialize();

        //buttonClose.onClick.AddListener(HandleClickToClose);
    }

    public override void Dispose()
    {
        base.Dispose();

        //buttonClose.onClick.RemoveListener(HandleClickToClose);
    }

    #region Input

    public event Action OnClickToClose;

    private void HandleClickToClose()
    {
        OnClickToClose?.Invoke();
    }

    #endregion
}
