using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPreviewPresenter
{
    private readonly ItemPreviewModel _model;
    private readonly ItemPreviewView _view;

    public ItemPreviewPresenter(ItemPreviewModel model, ItemPreviewView view)
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

    public void Activate()
    {

    }

    public void Deactivate()
    {

    }

    #endregion
}
