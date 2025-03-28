using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameState_Game : IState
{
    private IGlobalStateMachineProvider _stateMachineProvider;

    public MainGameState_Game(IGlobalStateMachineProvider stateMachineProvider)
    {
        _stateMachineProvider = stateMachineProvider;
    }

    public void EnterState()
    {

    }

    public void ExitState()
    {

    }

    private void ChangeStateToWin()
    {
        _stateMachineProvider.SetState(_stateMachineProvider.GetState<WinState_Game>());
    }
}