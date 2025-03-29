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

    private IEnumerator coroutineTimer;

    public MainGameState_Game(IGlobalStateMachineProvider stateMachineProvider, PlatformPresenter platformPresenter, RocketMovePresenter rocketMovePresenter, ScrollBackgroundPresenter scrollBackgroundPresenter, UIMiniGameSceneRoot sceneRoot)
    {
        _stateMachineProvider = stateMachineProvider;
        _platformPresenter = platformPresenter;
        _rocketMovePresenter = rocketMovePresenter;
        _scrollBackgroundPresenter = scrollBackgroundPresenter;
        _sceneRoot = sceneRoot;
    }

    public void EnterState()
    {
        Debug.Log("ACTIVATE STATE - MAIN GAME(4)");

        _rocketMovePresenter.OnMoveToWinLeft += ChangeStateToWin;
        _rocketMovePresenter.OnMoveToWinRight += ChangeStateToWin;

        _scrollBackgroundPresenter.ActivateScroll();
        _platformPresenter.DeactivatePlatform();
        _sceneRoot.OpenFooterPanel();

        if (coroutineTimer != null) Coroutines.Stop(coroutineTimer);

        coroutineTimer = Timer();
        Coroutines.Start(coroutineTimer);
    }

    public void ExitState()
    {
        Debug.Log("DEACTIVATE STATE - MAIN GAME(4)");

        _rocketMovePresenter.OnMoveToWinLeft -= ChangeStateToWin;
        _rocketMovePresenter.OnMoveToWinRight -= ChangeStateToWin;

        _scrollBackgroundPresenter.DeactivateScroll();

        if (coroutineTimer != null) Coroutines.Stop(coroutineTimer);
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(0.3f);

        for (int i = 0; i < 10; i++)
        {
            _rocketMovePresenter.MoveRight();

            yield return new WaitForSeconds(1f);
        }
    }

    private void ChangeStateToWin()
    {
        _stateMachineProvider.SetState(_stateMachineProvider.GetState<WinState_Game>());
    }
}