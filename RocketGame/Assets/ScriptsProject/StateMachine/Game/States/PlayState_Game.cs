using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayState_Game : IState
{
    private readonly IGlobalStateMachineProvider _stateProvider;

    public PlayState_Game(IGlobalStateMachineProvider stateProvider)
    {
        _stateProvider = stateProvider;
    }

    public void EnterState()
    {

    }

    public void ExitState()
    {

    }

    private void ChangeStateToWin()
    {
        _stateProvider.SetState(_stateProvider.GetState<WinState_Game>());
    }
}
