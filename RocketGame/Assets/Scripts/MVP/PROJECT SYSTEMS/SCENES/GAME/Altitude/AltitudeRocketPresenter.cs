using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltitudeRocketPresenter
{
    private readonly AltitudeRocketModel _model;
    private readonly AltitudeRocketView _view;

    public AltitudeRocketPresenter(AltitudeRocketModel model, AltitudeRocketView view)
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
        _model.OnChangeAltitude += _view.SetAltitude;
    }

    private void DeactivateEvents()
    {
        _model.OnChangeAltitude -= _view.SetAltitude;
    }

    #region Input

    public void Clear()
    {
        _model.Clear();
    }

    public void ActivateAltitude()
    {
        _model.ActivateAltitude();
    }

    public void DeactivateAltitude()
    {
        _model.DeactivateAltitude();
    }

    #endregion
}
