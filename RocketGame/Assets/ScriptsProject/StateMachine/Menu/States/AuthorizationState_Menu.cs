public class AuthorizationState_Menu : IState
{
    private readonly IGlobalStateMachineProvider _globalStateMachineProvider;

    private readonly NicknameRandomPresenter _nicknameRandomPresenter;
    private readonly FirebaseAuthenticationPresenter _firebaseAuthenticationPresenter;
    private readonly FirebaseDatabaseRealtimePresenter _firebaseDatabaseRealtimePresenter;
    private readonly UIMainMenuRoot _sceneRoot;

    public AuthorizationState_Menu(IGlobalStateMachineProvider globalStateMachineProvider, NicknameRandomPresenter nicknameRandomPresenter, FirebaseAuthenticationPresenter firebaseAuthenticationPresenter, FirebaseDatabaseRealtimePresenter firebaseDatabaseRealtimePresenter, UIMainMenuRoot sceneRoot)
    {
        _globalStateMachineProvider = globalStateMachineProvider;
        _nicknameRandomPresenter = nicknameRandomPresenter;
        _firebaseAuthenticationPresenter = firebaseAuthenticationPresenter;
        _firebaseDatabaseRealtimePresenter = firebaseDatabaseRealtimePresenter;
        _sceneRoot = sceneRoot;
    }

    public void EnterState()
    {
        _nicknameRandomPresenter.OnFailure += CreateRandomNickname;
        _nicknameRandomPresenter.OnSuccess += _firebaseAuthenticationPresenter.SignUp;

        _nicknameRandomPresenter.OnCreateNickname += _firebaseAuthenticationPresenter.SetNickname;
        _nicknameRandomPresenter.OnCreateNickname += _firebaseDatabaseRealtimePresenter.SetNickname;

        _firebaseAuthenticationPresenter.OnSignUpError += CreateRandomNickname;
        _firebaseAuthenticationPresenter.OnSignUp += _firebaseDatabaseRealtimePresenter.CreateEmptyDataToServer;
        _firebaseAuthenticationPresenter.OnSignUp += ChangeStateToMainState;

        _sceneRoot.OpenAuthorizationPanel();

        CreateRandomNickname();
    }

    public void ExitState()
    {
        _nicknameRandomPresenter.OnFailure -= CreateRandomNickname;
        _nicknameRandomPresenter.OnSuccess -= _firebaseAuthenticationPresenter.SignUp;

        _nicknameRandomPresenter.OnCreateNickname -= _firebaseAuthenticationPresenter.SetNickname;
        _nicknameRandomPresenter.OnCreateNickname -= _firebaseDatabaseRealtimePresenter.SetNickname;

        _firebaseAuthenticationPresenter.OnSignUpError -= CreateRandomNickname;
        _firebaseAuthenticationPresenter.OnSignUp -= _firebaseDatabaseRealtimePresenter.CreateEmptyDataToServer;
        _firebaseAuthenticationPresenter.OnSignUp -= ChangeStateToMainState;
    }

    private void CreateRandomNickname()
    {
        _nicknameRandomPresenter.CreateRandomNickname(5, 17);
    }

    private void ChangeStateToMainState()
    {
        _globalStateMachineProvider.SetState(_globalStateMachineProvider.GetState<MainState_Menu>());
    }
}
