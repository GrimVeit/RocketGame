using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMovePresenter
{
    private readonly RocketMoveModel _model;
    private readonly RocketMoveView _view;

    public RocketMovePresenter(RocketMoveModel model, RocketMoveView view)
    {
        _model = model;
        _view = view;
    }

    public void Initialize()
    {
        ActivateEvents();

        _view.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        _view.Dispose();
    }

    private void ActivateEvents()
    {
        _model.OnMoveToRight += _view.MoveRight;
        _model.OnMoveToLeft += _view.MoveLeft;
        _model.OnMoveToWinLeft += _view.MoveToWinLeft;
        _model.OnMoveToWinRight += _view.MoveToWinRight;

        _model.OnMoveToBase += _view.MoveToBase;
    }

    private void DeactivateEvents()
    {
        _model.OnMoveToRight -= _view.MoveRight;
        _model.OnMoveToLeft -= _view.MoveLeft;
        _model.OnMoveToWinLeft -= _view.MoveToWinLeft;
        _model.OnMoveToWinRight -= _view.MoveToWinRight;

        _model.OnMoveToBase -= _view.MoveToBase;
    }

    #region Input

    public void MoveLeft()
    {
        _model.MoveLeft();
    }

    public void MoveRight()
    {
        _model.MoveRight();
    }

    public void MoveToBase()
    {
        _model.MoveToBase();
    }

    #endregion

    #region Output

    public event Action OnEndMoveToBase
    {
        add => _view.OnEndMoveToBase += value;
        remove => _view.OnEndMoveToBase -= value;
    }

    #endregion
}
