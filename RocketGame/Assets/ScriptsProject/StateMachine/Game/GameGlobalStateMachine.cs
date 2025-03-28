using System;
using System.Collections.Generic;

public class GameGlobalStateMachine : IGlobalStateMachineProvider
{
    private Dictionary<Type, IState> states = new();

    private IState currentState;

    public GameGlobalStateMachine()
    {
        states[typeof(PlayPrepareState_Game)] = new PlayPrepareState_Game(this);
        states[typeof(PlayState_Game)] = new PlayState_Game(this);
        states[typeof(WinState_Game)] = new WinState_Game(this);
    }

    public void Initialize()
    {
        SetState(GetState<PlayPrepareState_Game>());
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
