using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayPrepareState_Game : IState
{
    private readonly IGlobalStateMachineProvider _stateProvider;

    public PlayPrepareState_Game(IGlobalStateMachineProvider stateProvider)
    {
        _stateProvider = stateProvider;
    }

    public void EnterState()
    {

    }

    public void ExitState()
    {

    }

    private void ChangeStateToPlay()
    {
        _stateProvider.SetState(_stateProvider.GetState<PlayState_Game>());
    }
}
