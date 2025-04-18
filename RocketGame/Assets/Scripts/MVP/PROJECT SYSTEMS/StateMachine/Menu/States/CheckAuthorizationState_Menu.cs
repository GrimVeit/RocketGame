public class CheckAuthorizationState_Menu : IState
{
    private readonly FirebaseAuthenticationPresenter _authenticationPresenter;

    private IGlobalStateMachineProvider _stateMachineProvider;

    public CheckAuthorizationState_Menu(IGlobalStateMachineProvider stateMachineProvider, FirebaseAuthenticationPresenter authenticationPresenter)
    {
        _stateMachineProvider = stateMachineProvider;
        _authenticationPresenter = authenticationPresenter;
    }

    public void EnterState()
    {
        if (_authenticationPresenter.IsAuthorization())
            ChangeStateToMain();
        else
            ChangeStateToAuthorization();
    }

    public void ExitState()
    {

    }

    private void ChangeStateToMain()
    {
        _stateMachineProvider.SetState(_stateMachineProvider.GetState<MainState_Menu>());
    }

    private void ChangeStateToAuthorization()
    {
        _stateMachineProvider.SetState(_stateMachineProvider.GetState<AuthorizationState_Menu>());
    }
}
