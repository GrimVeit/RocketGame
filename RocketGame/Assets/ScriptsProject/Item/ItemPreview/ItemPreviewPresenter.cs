using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPreviewPresenter
{
    private readonly ItemPreviewView _view;

    public ItemPreviewPresenter(ItemPreviewView view)
    {
        _view = view;
    }

    public void Initialize()
    {
        _view.Initialize();
    }

    public void Dispose()
    {
        _view.Dispose();
    }

    #region Input

    public void Activate(int index)
    {
        _view.Activate(index);
    }

    public void Deactivate(int index)
    {
        _view.Deactivate(index);
    }

    #endregion

    #region Output

    public event Action<int> OnChooseBuyItemGroup
    {
        add => _view.OnChooseBuyItemGroup += value;
        remove => _view.OnChooseBuyItemGroup -= value;
    }

    #endregion
}
