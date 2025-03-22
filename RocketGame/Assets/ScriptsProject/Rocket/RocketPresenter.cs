using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketPresenter
{
    private readonly RocketModel model;
    private readonly RocketView view;

    public void Initialize()
    {
        ActivateEvents();
    }

    public void Dispose()
    {
        DeactivateEvents();
    }

    private void ActivateEvents()
    {

    }

    private void DeactivateEvents()
    {

    }

    #region Input

    public void MoveLeft()
    {
        model.MoveLeft();
    }

    public void MoveRight()
    {
        model.MoveRight();
    }

    #endregion
}
