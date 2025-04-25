public class MainState_Menu : IState
{
    private readonly UIMenuRoot _sceneRoot;

    private readonly IGlobalStateMachineProvider _stateMachineProvider;

    private readonly FirebaseDatabasePresenter _firebaseDatabaseRealtimePresenter;

    public MainState_Menu(IGlobalStateMachineProvider stateMachineProvider, UIMenuRoot sceneRoot, FirebaseDatabasePresenter firebaseDatabaseRealtimePresenter)
    {
        _stateMachineProvider = stateMachineProvider;
        _sceneRoot = sceneRoot;
        _firebaseDatabaseRealtimePresenter = firebaseDatabaseRealtimePresenter;
    }

    public void EnterState()
    {
        _sceneRoot.OnClickToLeaderboard += _sceneRoot.OpenLeaderboardPanel;
        _sceneRoot.OnClickToCancel += _sceneRoot.OpenMainPanel;

        _firebaseDatabaseRealtimePresenter.SaveChangeToServer();
        _firebaseDatabaseRealtimePresenter.DisplayUsersRecords();
        _sceneRoot.OpenMainPanel();
    }

    public void ExitState()
    {
        _sceneRoot.OnClickToLeaderboard -= _sceneRoot.OpenLeaderboardPanel;
        _sceneRoot.OnClickToCancel -= _sceneRoot.OpenMainPanel;
    }
}
