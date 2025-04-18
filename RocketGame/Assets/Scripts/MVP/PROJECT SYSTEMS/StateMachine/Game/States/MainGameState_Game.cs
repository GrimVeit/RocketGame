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

    private readonly ISoundProvider _soundProvider;
    private readonly ISound _soundGameMain;

    public MainGameState_Game(IGlobalStateMachineProvider stateMachineProvider, PlatformPresenter platformPresenter, RocketMovePresenter rocketMovePresenter, ScrollBackgroundPresenter scrollBackgroundPresenter, UIMiniGameSceneRoot sceneRoot, ObstacleSpawnerPresenter obstacleSpawnerPresenter, CourseDisplacementPresenter courseDisplacementPresenter, ISoundProvider soundProvider)
    {
        _stateMachineProvider = stateMachineProvider;
        _platformPresenter = platformPresenter;
        _rocketMovePresenter = rocketMovePresenter;
        _scrollBackgroundPresenter = scrollBackgroundPresenter;
        _sceneRoot = sceneRoot;
        _obstacleSpawnerPresenter = obstacleSpawnerPresenter;
        _courseDisplacementPresenter = courseDisplacementPresenter;

        _soundProvider = soundProvider;
        _soundGameMain = _soundProvider.GetSound("Background_GameMain");
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
        _sceneRoot.OpenExitPanel();
        _obstacleSpawnerPresenter.ActivateSpawner();

        _soundGameMain.SetVolume(1);
        _soundGameMain.Play();
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

        _soundGameMain.SetVolume(1, 0, 0.1f, _soundGameMain.Stop);
    }

    private void ChangeStateToWin()
    {
        _stateMachineProvider.SetState(_stateMachineProvider.GetState<WinState_Game>());
    }
}