using UnityEngine;

public class PrepareState_Game : IState
{
    private readonly IGlobalStateMachineProvider _stateProvider;
    private readonly UIGameRoot _sceneRoot;
    private readonly StoreBetPresenter _storeBetPresenter;
    private readonly BetPreparePresenter _betPreparePresenter;

    private readonly ISoundProvider _soundProvider;
    private readonly ISound _soundGameStart;

    public PrepareState_Game(IGlobalStateMachineProvider stateProvider, UIGameRoot sceneRoot, StoreBetPresenter storeBetPresenter, BetPreparePresenter betPreparePresenter, ISoundProvider soundProvider)
    {
        _stateProvider = stateProvider;
        _sceneRoot = sceneRoot;
        _storeBetPresenter = storeBetPresenter;
        _betPreparePresenter = betPreparePresenter;
        _soundProvider = soundProvider;
        _soundGameStart = _soundProvider.GetSound("Background_GameStart");
    }

    public void EnterState()
    {
        Debug.Log("ACTIVATE STATE - PREPARE(2)");

        _betPreparePresenter.OnPlay += ChangeStateToLaunch;
        _sceneRoot.OnClickToHouse_HouseChoosePanel += ChangeStateToHouseBedroom;

        _sceneRoot.CloseHouseBalancePanel();
        _sceneRoot.OpenFooterPanel();   
        _sceneRoot.OpenExitPanel();
        _sceneRoot.OpenHouseChoosePanel();

        _storeBetPresenter.Activate();
        _betPreparePresenter.Activate();
    }

    public void ExitState()
    {
        Debug.Log("DEACTIVATE STATE - PREPARE(2)");

        _betPreparePresenter.OnPlay -= ChangeStateToLaunch;
        _sceneRoot.OnClickToHouse_HouseChoosePanel -= ChangeStateToHouseBedroom;

        _sceneRoot.CloseHouseChoosePanel();
        _sceneRoot.CloseExitPanel();
        _sceneRoot.CloseFooterPanel();
        _storeBetPresenter.Deactivate();
        _betPreparePresenter.Deactivate();

        _soundGameStart.SetVolume(1, 0, 0.1f, _soundGameStart.Stop);
    }

    private void ChangeStateToLaunch()
    {
        _stateProvider.SetState(_stateProvider.GetState<LaunchState_Game>());
    }

    private void ChangeStateToHouseBedroom()
    {
        _stateProvider.SetState(_stateProvider.GetState<HouseBedroomState_Game>());
    }
}
