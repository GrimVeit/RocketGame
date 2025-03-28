using System;
using System.Collections.Generic;

public class GameGlobalStateMachine : IGlobalStateMachineProvider
{
    private Dictionary<Type, IState> states = new();

    private IState currentState;

    public GameGlobalStateMachine(RocketMovePresenter rocketMovePresenter)
    {
        states[typeof(ArrivalState_Game)] = new ArrivalState_Game(this, rocketMovePresenter);
        states[typeof(LaunchState_Game)] = new LaunchState_Game(this);
        states[typeof(MainGameState_Game)] = new MainGameState_Game(this);
        states[typeof(WinState_Game)] = new WinState_Game(this);
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
