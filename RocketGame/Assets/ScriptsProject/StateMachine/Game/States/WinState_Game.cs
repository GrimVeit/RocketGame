using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinState_Game : IState
{
    private readonly IGlobalStateMachineProvider _stateProvider;

    private readonly UIMiniGameSceneRoot _sceneRoot;
    private readonly RocketMovePresenter _rocketMovePresenter;
    private readonly ObstaclePresenter _obstaclePresenter;

    private IEnumerator coroutineTimer;

    public WinState_Game(IGlobalStateMachineProvider stateProvider, UIMiniGameSceneRoot sceneRoot, RocketMovePresenter rocketMovePresenter, ObstaclePresenter obstaclePresenter)
    {
        _stateProvider = stateProvider;
        _sceneRoot = sceneRoot;
        _rocketMovePresenter = rocketMovePresenter;
        _obstaclePresenter = obstaclePresenter;
    }

    public void EnterState()
    {
        Debug.Log("ACTIVATE STATE - WIN(5)");

        _sceneRoot.CloseFooterPanel();
        _sceneRoot.CloseBetPanel();
        _sceneRoot.OpenWinPanel();

        _rocketMovePresenter.Restart();
        _obstaclePresenter.StopObstacles();

        if(coroutineTimer != null ) Coroutines.Stop(coroutineTimer);

        coroutineTimer = Timer();
        Coroutines.Start(coroutineTimer);
    }

    public void ExitState()
    {
        Debug.Log("DEACTIVATE STATE - WIN(5)");

        if (coroutineTimer != null) Coroutines.Stop(coroutineTimer);

        _sceneRoot.CloseWinPanel();
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(5);

        ChangeStateToPreparePlay();
    }

    private void ChangeStateToPreparePlay()
    {
        _stateProvider.SetState(_stateProvider.GetState<ArrivalState_Game>());
    }
}
