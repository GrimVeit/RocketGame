public class MainState_Menu : IState
{
    private readonly UIMainMenuRoot _sceneRoot;

    private readonly IGlobalStateMachineProvider _stateMachineProvider;

    private readonly FirebaseDatabaseRealtimePresenter _firebaseDatabaseRealtimePresenter;

    public MainState_Menu(IGlobalStateMachineProvider stateMachineProvider, UIMainMenuRoot sceneRoot, FirebaseDatabaseRealtimePresenter firebaseDatabaseRealtimePresenter)
    {
        _stateMachineProvider = stateMachineProvider;
        _sceneRoot = sceneRoot;
        _firebaseDatabaseRealtimePresenter = firebaseDatabaseRealtimePresenter;
    }

    public void EnterState()
    {
        _sceneRoot.OnClickToLeaderboard += _sceneRoot.OpenLeaderboardPanel;
        _sceneRoot.OnClickToCancel += _sceneRoot.OpenMainPanel;

        _firebaseDatabaseRealtimePresenter.DisplayUsersRecords();
        _sceneRoot.OpenMainPanel();
    }

    public void ExitState()
    {
        _sceneRoot.OnClickToLeaderboard -= _sceneRoot.OpenLeaderboardPanel;
        _sceneRoot.OnClickToCancel -= _sceneRoot.OpenMainPanel;
    }
}
