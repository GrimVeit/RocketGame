using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchState_Game : IState
{
    private readonly IGlobalStateMachineProvider _stateProvider;

    private readonly RocketMovePresenter _rocketMovePresenter;
    private readonly UIMiniGameSceneRoot _sceneRoot;

    public LaunchState_Game(IGlobalStateMachineProvider stateProvider, RocketMovePresenter rocketMovePresenter, UIMiniGameSceneRoot sceneRoot)
    {
        _stateProvider = stateProvider;
        _rocketMovePresenter = rocketMovePresenter;
        _sceneRoot = sceneRoot;
    }

    public void EnterState()
    {
        Debug.Log("ACTIVATE STATE - LAUNCH(3)");

        _rocketMovePresenter.OnEndMoveToStart += ChangeStateToMain;

        _rocketMovePresenter.MoveToStart();
        _sceneRoot.CloseFooterPanel();
    }

    public void ExitState()
    {
        Debug.Log("ACTIVATE STATE - LAUNCH(3)");

        _rocketMovePresenter.OnEndMoveToStart -= ChangeStateToMain;
    }

    private void ChangeStateToMain()
    {
        _stateProvider.SetState(_stateProvider.GetState<MainGameState_Game>());
    }
}
