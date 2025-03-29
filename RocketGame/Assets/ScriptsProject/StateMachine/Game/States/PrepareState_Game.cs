using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrepareState_Game : IState
{
    private readonly IGlobalStateMachineProvider _stateProvider;
    private readonly UIMiniGameSceneRoot _sceneRoot;

    private IEnumerator coroutineTimer;

    public PrepareState_Game(IGlobalStateMachineProvider stateProvider, UIMiniGameSceneRoot sceneRoot)
    {
        _stateProvider = stateProvider;
        _sceneRoot = sceneRoot;
    }

    public void EnterState()
    {
        Debug.Log("ACTIVATE STATE - PREPARE(2)");

        if(coroutineTimer != null) Coroutines.Stop(coroutineTimer);

        coroutineTimer = Timer();
        Coroutines.Start(coroutineTimer);

        _sceneRoot.OpenFooterPanel();   
    }

    public void ExitState()
    {
        Debug.Log("DEACTIVATE STATE - PREPARE(2)");

        if (coroutineTimer != null) Coroutines.Stop(coroutineTimer);
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(3);

        ChangeStateToLaunch();
    }

    private void ChangeStateToLaunch()
    {
        _stateProvider.SetState(_stateProvider.GetState<LaunchState_Game>());
    }
}
