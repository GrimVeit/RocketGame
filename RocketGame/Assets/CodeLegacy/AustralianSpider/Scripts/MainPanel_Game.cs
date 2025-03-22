using System;
using UnityEngine;
using UnityEngine.UI;

public class MainPanel_Game : MovePanel
{
    [SerializeField] private Button buttonExit;

    public override void Initialize()
    {
        base.Initialize();

        buttonExit.onClick.AddListener(HandleClickToExit);
    }

    public override void Dispose()
    {
        base.Dispose();

        buttonExit.onClick.RemoveListener(HandleClickToExit);
    }

    #region Input

    public event Action OnClickToExit;

    private void HandleClickToExit()
    {
        OnClickToExit?.Invoke();
    }

    #endregion
}
