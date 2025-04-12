using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsBuyPresenter
{
    private readonly ItemsBuyModel _model;
    private readonly ItemsBuyView _view;

    public ItemsBuyPresenter(ItemsBuyModel model, ItemsBuyView view)
    {
        _model = model;
        _view = view;
    }

    public void Initialize()
    {
        ActivateEvents();

        _model.Initialize();
        _view.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        _model.Dispose();
        _view.Dispose();
    }

    private void ActivateEvents()
    {
        _view.OnBuyItemGroup += _model.BuyItems;

        _model.OnSetItemGroup += _view.SetData;
    }

    private void DeactivateEvents()
    {
        _view.OnBuyItemGroup -= _model.BuyItems;

        _model.OnSetItemGroup -= _view.SetData;
    }
}
