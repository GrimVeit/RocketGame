using System;

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

        _view.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        _view.Dispose();
    }

    private void ActivateEvents()
    {
        _view.OnChooseBuyItemGroup += _model.ChooseBuyItemGroup;

        _model.OnActivateItemGroup += _view.Activate;
        _model.OnDeactivateItemGroup += _view.Deactivate;
    }

    private void DeactivateEvents()
    {
        _view.OnChooseBuyItemGroup -= _model.ChooseBuyItemGroup;

        _model.OnActivateItemGroup -= _view.Activate;
        _model.OnDeactivateItemGroup -= _view.Deactivate;
    }

    #region Input

    public void Activate(ItemGroup itemGroup)
    {
        _model.Activate(itemGroup);
    }

    public void Deactivate(ItemGroup itemGroup)
    {
        _model.Deactivate(itemGroup);
    }

    #endregion

    #region Output

    public event Action<int> OnChooseBuyItemGroup
    {
        add => _model.OnChooseBuyItemGroup += value;
        remove => _model.OnChooseBuyItemGroup -= value;
    }

    #endregion
}
