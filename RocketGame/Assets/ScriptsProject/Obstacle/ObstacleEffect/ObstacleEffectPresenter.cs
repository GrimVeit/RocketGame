using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleEffectPresenter
{
    private readonly ObstacleEffectModel _model;
    private readonly ObstacleEffectView _view;

    public ObstacleEffectPresenter(ObstacleEffectModel model, ObstacleEffectView view)
    {
        _model = model;
        _view = view;
    }

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

    #endregion
}
