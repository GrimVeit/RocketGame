using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrepareState_Game : IState
{
    private readonly IGlobalStateMachineProvider _stateProvider;
    private readonly UIMiniGameSceneRoot _sceneRoot;
    private readonly StoreBetPresenter _storeBetPresenter;
    private readonly BetPreparePresenter _betPreparePresenter;

    public PrepareState_Game(IGlobalStateMachineProvider stateProvider, UIMiniGameSceneRoot sceneRoot, StoreBetPresenter storeBetPresenter, BetPreparePresenter betPreparePresenter)
    {
        _stateProvider = stateProvider;
        _sceneRoot = sceneRoot;
        _storeBetPresenter = storeBetPresenter;
        _betPreparePresenter = betPreparePresenter;
    }

    public void EnterState()
    {
        Debug.Log("ACTIVATE STATE - PREPARE(2)");

        _betPreparePresenter.OnPlay += ChangeStateToLaunch;

        _sceneRoot.OpenFooterPanel();   

        _storeBetPresenter.Activate();
        _betPreparePresenter.Activate();
    }

    public void ExitState()
    {
        Debug.Log("DEACTIVATE STATE - PREPARE(2)");

        _betPreparePresenter.OnPlay -= ChangeStateToLaunch;

        _storeBetPresenter.Deactivate();
        _betPreparePresenter.Deactivate();
    }

    private void ChangeStateToLaunch()
    {
        _stateProvider.SetState(_stateProvider.GetState<LaunchState_Game>());
    }
}
