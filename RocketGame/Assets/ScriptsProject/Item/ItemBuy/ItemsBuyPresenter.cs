using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsBuyPresenter
{
    private ItemsBuyModel _model;
    private ItemsBuyView _view;

    public ItemsBuyPresenter(ItemsBuyModel model, ItemsBuyView view)
    {
        _model = model;
        _view = view;
    }

    public void Initialize()
    {
        ActivateEvents();

        _model.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        _model.Dispose();
    }

    private void ActivateEvents()
    {
        _model.OnSetItemGroup += _view.SetData;
    }

    private void DeactivateEvents()
    {
        _model.OnSetItemGroup -= _view.SetData;
    }
}
