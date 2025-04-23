using System;
using UnityEngine;

public class UIMenuRoot : UIRoot
{
    [SerializeField] private MainPanel_Menu mainPanel;
    [SerializeField] private LeaderboardPanel_Menu leaderboardPanel;
    [SerializeField] private Panel_Move authorizationPanel;

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

        if(currentPanel != null)
            CloseOtherPanel(currentPanel);
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
        soundProvider.PlayOneShot("Click");

        OnClickToPlay?.Invoke();
    }

    private void HandleClickToLeaderboard()
    {
        soundProvider.PlayOneShot("Click");

        OnClickToLeaderboard?.Invoke();
    }

    #endregion

    #region LeaderboardPanel

    public event Action OnClickToCancel;

    private void HandleClickToCancel()
    {
        soundProvider.PlayOneShot("Click");

        OnClickToCancel?.Invoke();
    }

    #endregion

    #endregion

}
