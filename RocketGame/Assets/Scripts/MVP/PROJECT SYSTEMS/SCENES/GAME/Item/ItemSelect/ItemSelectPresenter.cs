using System;
public class ItemSelectPresenter
{
    private readonly ItemSelectModel _model;
    private readonly ItemSelectView _view;

    public ItemSelectPresenter(ItemSelectModel model, ItemSelectView view)
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
        _view.OnSelectItem += _model.ChooseItemForSelect;

        _model.OnSetItemData += _view.SetItems;
        _model.OnSelectItem += _view.Select;
        _model.OnDeselectItem += _view.Deselect;
    }

    private void DeactivateEvents()
    {
        _view.OnSelectItem -= _model.ChooseItemForSelect;

        _model.OnSetItemData -= _view.SetItems;
        _model.OnSelectItem -= _view.Select;
        _model.OnDeselectItem -= _view.Deselect;
    }

    #region Input

    public void SetItemGroup(ItemGroup itemGroup)
    {
        _model.SetItemGroup(itemGroup);
    }

    public void Select(ItemGroup itemGroup, Item item)
    {
        _model.Select(itemGroup, item);
    }

    public void Deselect(ItemGroup itemGroup, Item item)
    {
        _model.Deselect(itemGroup, item);
    }

    #endregion

    #region Output

    public event Action<int, int> OnChooseItemForSelect
    {
        add => _model.OnChooseItemForSelect += value;
        remove => _model.OnChooseItemForSelect -= value;
    }

    #endregion
}
