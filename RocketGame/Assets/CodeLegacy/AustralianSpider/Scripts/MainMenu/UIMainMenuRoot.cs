using System;
using UnityEngine;

public class UIMainMenuRoot : UIRoot
{
    [SerializeField] private MainPanel_Menu mainPanel;
    [SerializeField] private LeaderboardPanel_Menu leaderboardPanel;
    [SerializeField] private MovePanel authorizationPanel;

    private ISoundProvider soundProvider;

    public void SetSoundProvider(ISoundProvider soundProvider)
    {
        this.soundProvider = soundProvider;
    }

    public void Initialize()
    {
        mainPanel.Initialize();
        leaderboardPanel.Initialize();
        authorizationPanel.Initialize();
    }

    public void Activate()
    {
        mainPanel.OnClickToPlay += HandleClickToPlay;
        mainPanel.OnClickToLeaderboard += HandleClickToLeaderboard;

        leaderboardPanel.OnClickToCancelFromLeaderboard += HandleClickToCancel;
    }


    public void Deactivate()
    {
        mainPanel.OnClickToPlay -= HandleClickToPlay;
        mainPanel.OnClickToLeaderboard -= HandleClickToLeaderboard;

        leaderboardPanel.OnClickToCancelFromLeaderboard -= HandleClickToCancel;
    }

    public void Dispose()
    {
        mainPanel.Dispose();
        leaderboardPanel.Dispose();
        authorizationPanel.Dispose();
    }





    public void OpenMainPanel()
    {
        if(mainPanel.IsActive) return;

        OpenPanel(mainPanel);
    }

    public void OpenLeaderboardPanel()
    {
        if (leaderboardPanel.IsActive) return;

        OpenPanel(leaderboardPanel);
    }

    public void OpenAuthorizationPanel()
    {
        if (authorizationPanel.IsActive) return;

        OpenPanel(authorizationPanel);
    }

    #region Input

    #region MainPanel

    public event Action OnClickToPlay;
    public event Action OnClickToLeaderboard;

    private void HandleClickToPlay()
    {
        OnClickToPlay?.Invoke();
    }

    private void HandleClickToLeaderboard()
    {
        OnClickToLeaderboard?.Invoke();
    }

    #endregion

    #region LeaderboardPanel

    public event Action OnClickToCancel;

    private void HandleClickToCancel()
    {
        OnClickToCancel?.Invoke();
    }

    #endregion

    #endregion

}
