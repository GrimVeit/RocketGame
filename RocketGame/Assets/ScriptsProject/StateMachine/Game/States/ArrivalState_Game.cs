using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrivalState_Game : IState
{
    private readonly IGlobalStateMachineProvider _stateProvider;

    private readonly RocketMovePresenter _rocketMovePresenter;

    public ArrivalState_Game(IGlobalStateMachineProvider stateProvider, RocketMovePresenter rocketMovePresenter)
    {
        _stateProvider = stateProvider;
        _rocketMovePresenter = rocketMovePresenter;
    }

    public void EnterState()
    {
        _rocketMovePresenter.MoveToBase();
    }

    public void ExitState()
    {

    }

    private void ChangeStateToPlay()
    {
        _stateProvider.SetState(_stateProvider.GetState<LaunchState_Game>());
    }
}
