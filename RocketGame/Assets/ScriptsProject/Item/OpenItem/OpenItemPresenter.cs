using System;

public class OpenItemPresenter
{
    private readonly OpenItemView _view;

    public OpenItemPresenter(OpenItemView view)
    {
        _view = view;
    }

    public void Initialize()
    {
        _view.Initialize();
    }

    public void Dispose()
    {
        _view?.Dispose();
    }

    #region Input

    public void ActivateOpenItem(int index)
    {
        _view.Activate(index);
    }

    public void DeactivateOpenItem(int index)
    {
        _view.Deactivate(index);
    }

    #endregion

    #region Output

    public event Action<int> OnChooseSelectItemGroupForSelectItem
    {
        add => _view.OnChooseSelectItemGroup += value;
        remove => _view.OnChooseSelectItemGroup -= value;
    }

    #endregion
}
