using System;
using UnityEngine;
using UnityEngine.UI;

public class HouseChoosePanel_Game : MovePanel
{
    [SerializeField] private Button buttonHouse;

    public override void Initialize()
    {
        base.Initialize();

        buttonHouse.onClick.AddListener(() => OnClickToHouse?.Invoke());
    }

    public override void Dispose()
    {
        base.Dispose();

        buttonHouse.onClick.RemoveListener(() => OnClickToHouse?.Invoke());
    }

    #region Output

    public event Action OnClickToHouse;

    #endregion
}
