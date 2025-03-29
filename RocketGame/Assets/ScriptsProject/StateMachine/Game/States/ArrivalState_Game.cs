using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrivalState_Game : IState
{
    private readonly IGlobalStateMachineProvider _stateProvider;

    private readonly RocketMovePresenter _rocketMovePresenter;
    private readonly PlatformPresenter _platformPresenter;
    private readonly UIMiniGameSceneRoot _sceneRoot;

    public ArrivalState_Game(IGlobalStateMachineProvider stateProvider, RocketMovePresenter rocketMovePresenter, PlatformPresenter platformPresenter, UIMiniGameSceneRoot sceneRoot)
    {
        _stateProvider = stateProvider;
        _rocketMovePresenter = rocketMovePresenter;
        _platformPresenter = platformPresenter;
        _sceneRoot = sceneRoot;
    }

    public void EnterState()
    {
        Debug.Log("ACTIVATE STATE - ARRIVAL(1)");

        _rocketMovePresenter.OnPauseMoveToBase += _platformPresenter.ActivatePlatform;
        _rocketMovePresenter.OnEndMoveToBase += ChangeStateToPrepare;

        _rocketMovePresenter.MoveToBase();
        _sceneRoot.CloseFooterPanel();
    }

    public void ExitState()
    {
        Debug.Log("DEACTIVATE STATE - ARRIVAL(1)");

        _rocketMovePresenter.OnPauseMoveToBase -= _platformPresenter.ActivatePlatform;
        _rocketMovePresenter.OnEndMoveToBase -= ChangeStateToPrepare;
    }

    private void ChangeStateToPrepare()
    {
        _stateProvider.SetState(_stateProvider.GetState<PrepareState_Game>());
    }
}
