using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnerPresenter
{
    private readonly ItemSpawnerModel _model;
    private readonly ItemSpawnerView _view;

    public ItemSpawnerPresenter(ItemSpawnerModel model, ItemSpawnerView view)
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

    public void ActivateSpawner()
    {

    }

    public void DeactivateSpawner()
    {

    }

    #endregion

    #region Output

    public event Action OnSpawn;
    public event Action OnDestroy;

    #endregion
}
