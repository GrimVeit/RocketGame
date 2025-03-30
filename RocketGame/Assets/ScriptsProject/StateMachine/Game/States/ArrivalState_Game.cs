using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrivalState_Game : IState
{
    private readonly IGlobalStateMachineProvider _stateProvider;

    private readonly RocketMovePresenter _rocketMovePresenter;
    private readonly PlatformPresenter _platformPresenter;
    private readonly UIMiniGameSceneRoot _sceneRoot;
    private readonly ObstaclePresenter _obstaclePresenter;
    private readonly AltitudePresenter _altitudePresenter;
    private readonly CourseDisplacementPresenter _courseDisplacementPresenter;

    public ArrivalState_Game(IGlobalStateMachineProvider stateProvider, RocketMovePresenter rocketMovePresenter, PlatformPresenter platformPresenter, UIMiniGameSceneRoot sceneRoot, ObstaclePresenter obstaclePresenter, AltitudePresenter altitudePresenter, CourseDisplacementPresenter courseDisplacementPresenter)
    {
        _stateProvider = stateProvider;
        _rocketMovePresenter = rocketMovePresenter;
        _platformPresenter = platformPresenter;
        _sceneRoot = sceneRoot;
        _obstaclePresenter = obstaclePresenter;
        _altitudePresenter = altitudePresenter;
        _courseDisplacementPresenter = courseDisplacementPresenter;
    }

    public void EnterState()
    {
        Debug.Log("ACTIVATE STATE - ARRIVAL(1)");

        _rocketMovePresenter.OnPauseMoveToBase += _platformPresenter.ActivatePlatform;
        _rocketMovePresenter.OnEndMoveToBase += ChangeStateToPrepare;

        _rocketMovePresenter.MoveToBase();
        _sceneRoot.CloseFooterPanel();
        _obstaclePresenter.ClearObstacles();
        _altitudePresenter.Clear();
        _courseDisplacementPresenter.Clear();
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
