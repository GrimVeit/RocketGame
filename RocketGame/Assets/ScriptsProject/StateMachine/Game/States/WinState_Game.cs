using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinState_Game : IState
{
    private readonly IGlobalStateMachineProvider _stateProvider;

    public WinState_Game(IGlobalStateMachineProvider stateProvider)
    {
        _stateProvider = stateProvider;
    }

    public void EnterState()
    {

    }

    public void ExitState()
    {

    }

    private void ChangeStateToPreparePlay()
    {
        _stateProvider.SetState(_stateProvider.GetState<PlayPrepareState_Game>());
    }
}
