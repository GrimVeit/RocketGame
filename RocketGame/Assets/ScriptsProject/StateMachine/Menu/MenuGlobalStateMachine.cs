using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuGlobalStateMachine : IGlobalStateMachineProvider
{
    private readonly Dictionary<Type, IState> states = new();

    private IState currentState;

    public MenuGlobalStateMachine(InternetPresenter internetPresenter, FirebaseAuthenticationPresenter firebaseAuthenticationPresenter, FirebaseDatabaseRealtimePresenter firebaseDatabaseRealtimePresenter, NicknameRandomPresenter nicknameRandomPresenter, UIMainMenuRoot sceneRoot)
    {
        states[typeof(CheckAuthorizationState_Menu)] = new CheckAuthorizationState_Menu(this, firebaseAuthenticationPresenter);
        states[typeof(AuthorizationState_Menu)] = new AuthorizationState_Menu(this, nicknameRandomPresenter, firebaseAuthenticationPresenter, firebaseDatabaseRealtimePresenter, sceneRoot);
        states[typeof(MainState_Menu)] = new MainState_Menu(this, sceneRoot, firebaseDatabaseRealtimePresenter);
    }

    public void Initialize()
    {
        SetState(GetState<CheckAuthorizationState_Menu>());
    }

    public void Dispose()
    {

    }

    public IState GetState<T>() where T : IState
    {
        return states[typeof(T)];
    }

    public void SetState(IState state)
    {
        if (state == null)
        {
            Debug.LogError($"State not found");
            return; 
        }

        currentState?.ExitState();

        currentState = state;
        currentState.EnterState();
    }
}
