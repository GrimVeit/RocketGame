public class RoomTransitionPresenter
{
    private readonly RoomTransitionModel _model;
    private readonly RoomTransitionView _view;

    public RoomTransitionPresenter(RoomTransitionModel model, RoomTransitionView view)
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
        _model.OnUnlockRoom += _view.Activate;
    }

    private void DeactivateEvents()
    {
        _model.OnUnlockRoom -= _view.Activate;
    }

    #region Input

    public void UnlockRoomTwo()
    {
        _model.UnlockRoom(0);
    }

    public void UnlockRoomThree()
    {
        _model.UnlockRoom(1);
    }

    #endregion
}
