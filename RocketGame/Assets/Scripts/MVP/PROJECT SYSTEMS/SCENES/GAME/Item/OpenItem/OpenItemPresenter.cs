using System;

public class OpenItemPresenter
{
    private readonly OpenItemModel _model;
    private readonly OpenItemView _view;

    public OpenItemPresenter(OpenItemModel model, OpenItemView view)
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
        _view.OnChooseSelectItemGroup += _model.ChooseSelectItemGroupForSelectItem;

        _model.OnActivateOpenItem += _view.Activate;
        _model.OnDeactivateOpenItem += _view.Deactivate;
    }

    private void DeactivateEvents()
    {
        _view.OnChooseSelectItemGroup -= _model.ChooseSelectItemGroupForSelectItem;

        _model.OnActivateOpenItem -= _view.Activate;
        _model.OnDeactivateOpenItem -= _view.Deactivate;
    }

    #region Input

    public void ActivateOpenItem(ItemGroup itemGroup)
    {
        _model.ActivateOpenItem(itemGroup);
    }

    public void DeactivateOpenItem(ItemGroup itemGroup)
    {
        _model.DeactivateOpenItem(itemGroup);
    }

    #endregion

    #region Output

    public event Action<int> OnChooseSelectItemGroupForSelectItem
    {
        add => _model.OnChooseSelectItemGroupForSelectItem += value;
        remove => _model.OnChooseSelectItemGroupForSelectItem -= value;
    }

    #endregion
}
