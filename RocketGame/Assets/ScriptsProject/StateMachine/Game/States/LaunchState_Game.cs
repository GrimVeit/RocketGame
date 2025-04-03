using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchState_Game : IState
{
    private readonly IGlobalStateMachineProvider _stateProvider;

    private readonly RocketMovePresenter _rocketMovePresenter;
    private readonly UIMiniGameSceneRoot _sceneRoot;
    private readonly AltitudePresenter _altitudePresenter;

    private readonly ISoundProvider _soundProvider;
    private readonly ISound _soundGameLaunch;

    public LaunchState_Game(IGlobalStateMachineProvider stateProvider, RocketMovePresenter rocketMovePresenter, UIMiniGameSceneRoot sceneRoot, AltitudePresenter altitudePresenter, ISoundProvider soundProvider)
    {
        _stateProvider = stateProvider;
        _rocketMovePresenter = rocketMovePresenter;
        _sceneRoot = sceneRoot;
        _altitudePresenter = altitudePresenter;

        _soundProvider = soundProvider;
        _soundGameLaunch = _soundProvider.GetSound("Background_GameLaunch");
    }

    public void EnterState()
    {
        Debug.Log("ACTIVATE STATE - LAUNCH(3)");

        _rocketMovePresenter.OnEndMoveToStart += ChangeStateToMain;

        _rocketMovePresenter.MoveToStart();
        _sceneRoot.CloseFooterPanel();
        _altitudePresenter.ActivateAltitude();

        _soundGameLaunch.Play();
        _soundGameLaunch.SetVolume(0, 1, 0.1f);
    }

    public void ExitState()
    {
        Debug.Log("ACTIVATE STATE - LAUNCH(3)");

        _rocketMovePresenter.OnEndMoveToStart -= ChangeStateToMain;
        _soundGameLaunch?.Stop();
    }

    private void ChangeStateToMain()
    {
        _stateProvider.SetState(_stateProvider.GetState<MainGameState_Game>());
    }
}
