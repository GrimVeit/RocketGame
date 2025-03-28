using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchState_Game : IState
{
    private readonly IGlobalStateMachineProvider _stateProvider;

    public LaunchState_Game(IGlobalStateMachineProvider stateProvider)
    {
        _stateProvider = stateProvider;
    }

    public void EnterState()
    {

    }

    public void ExitState()
    {

    }

    private void ChangeStateToMain()
    {
        _stateProvider.SetState(_stateProvider.GetState<MainGameState_Game>());
    }
}
