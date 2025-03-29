using System;
using System.Collections.Generic;

public class GameGlobalStateMachine : IGlobalStateMachineProvider
{
    private Dictionary<Type, IState> states = new();

    private IState currentState;

    public GameGlobalStateMachine(RocketMovePresenter rocketMovePresenter, PlatformPresenter platformPresenter, ScrollBackgroundPresenter scrollBackgroundPresenter, UIMiniGameSceneRoot sceneRoot, ObstacleSpawnerPresenter obstacleSpawnerPresenter, ObstaclePresenter obstaclePresenter)
    {
        states[typeof(ArrivalState_Game)] = new ArrivalState_Game(this, rocketMovePresenter, platformPresenter, sceneRoot, obstaclePresenter);
        states[typeof(PrepareState_Game)] = new PrepareState_Game(this, sceneRoot);
        states[typeof(LaunchState_Game)] = new LaunchState_Game(this, rocketMovePresenter, sceneRoot);
        states[typeof(MainGameState_Game)] = new MainGameState_Game(this, platformPresenter, rocketMovePresenter, scrollBackgroundPresenter, sceneRoot, obstacleSpawnerPresenter);
        states[typeof(WinState_Game)] = new WinState_Game(this, sceneRoot, rocketMovePresenter, obstaclePresenter);
    }

    public void Initialize()
    {
        SetState(GetState<ArrivalState_Game>());
    }

    public void Dispose()
    {

    }

    public void SetState(IState state)
    {
        currentState?.ExitState();

        currentState = state;
        currentState.EnterState();
    }

    public IState GetState<T>() where T : IState
    {
        return states[typeof(T)];
    }
}
