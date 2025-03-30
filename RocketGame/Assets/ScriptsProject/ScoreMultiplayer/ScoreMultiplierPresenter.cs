using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreMultiplierPresenter
{
    private readonly ScoreMultiplierModel _model;
    private readonly ScoreMultiplierView _view;

    public ScoreMultiplierPresenter(ScoreMultiplierModel model, ScoreMultiplierView view)
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
}
