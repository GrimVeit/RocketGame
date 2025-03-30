using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameState_Game : IState
{
    private IGlobalStateMachineProvider _stateMachineProvider;

    private readonly PlatformPresenter _platformPresenter;
    private readonly RocketMovePresenter _rocketMovePresenter;
    private readonly ScrollBackgroundPresenter _scrollBackgroundPresenter;
    private readonly UIMiniGameSceneRoot _sceneRoot;
    private readonly ObstacleSpawnerPresenter _obstacleSpawnerPresenter;
    private readonly CourseDisplacementPresenter _courseDisplacementPresenter;

    public MainGameState_Game(IGlobalStateMachineProvider stateMachineProvider, PlatformPresenter platformPresenter, RocketMovePresenter rocketMovePresenter, ScrollBackgroundPresenter scrollBackgroundPresenter, UIMiniGameSceneRoot sceneRoot, ObstacleSpawnerPresenter obstacleSpawnerPresenter, CourseDisplacementPresenter courseDisplacementPresenter)
    {
        _stateMachineProvider = stateMachineProvider;
        _platformPresenter = platformPresenter;
        _rocketMovePresenter = rocketMovePresenter;
        _scrollBackgroundPresenter = scrollBackgroundPresenter;
        _sceneRoot = sceneRoot;
        _obstacleSpawnerPresenter = obstacleSpawnerPresenter;
        _courseDisplacementPresenter = courseDisplacementPresenter;
    }

    public void EnterState()
    {
        Debug.Log("ACTIVATE STATE - MAIN GAME(4)");
        _rocketMovePresenter.OnMoveToLeft += _courseDisplacementPresenter.Left;
        _rocketMovePresenter.OnMoveToRight += _courseDisplacementPresenter.Right;
        _rocketMovePresenter.OnMoveToWinLeft += ChangeStateToWin;
        _rocketMovePresenter.OnMoveToWinRight += ChangeStateToWin;

        _scrollBackgroundPresenter.ActivateScroll();
        _platformPresenter.DeactivatePlatform();
        _sceneRoot.OpenFooterPanel();
        _obstacleSpawnerPresenter.ActivateSpawner();
    }

    public void ExitState()
    {
        Debug.Log("DEACTIVATE STATE - MAIN GAME(4)");

        _rocketMovePresenter.OnMoveToLeft -= _courseDisplacementPresenter.Left;
        _rocketMovePresenter.OnMoveToRight -= _courseDisplacementPresenter.Right;
        _rocketMovePresenter.OnMoveToWinLeft -= ChangeStateToWin;
        _rocketMovePresenter.OnMoveToWinRight -= ChangeStateToWin;

        _scrollBackgroundPresenter.DeactivateScroll();
        _obstacleSpawnerPresenter.DeactivateSpawner();
    }

    private void ChangeStateToWin()
    {
        _stateMachineProvider.SetState(_stateMachineProvider.GetState<WinState_Game>());
    }
}