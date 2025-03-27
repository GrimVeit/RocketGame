using System;
using UnityEngine;

public class UIMiniGameSceneRoot : UIRoot
{
    [SerializeField] private MainPanel_Game mainPanel;
    [SerializeField] private FooterPanel_Game footerPanel;
    [SerializeField] private BetPanel_Game betPanel;

    private ISoundProvider soundProvider;

    public void SetSoundProvider(ISoundProvider soundProvider)
    {
        this.soundProvider = soundProvider;
    }

    public void Initialize()
    {
        mainPanel.Initialize();
        footerPanel.Initialize();
        betPanel.Initialize();
    }

    public void Dispose()
    {
        mainPanel.Dispose();
        footerPanel.Dispose();
        betPanel.Dispose();
    }

    public void Activate()
    {
        mainPanel.OnClickToExit += HandleClickToExit_MainPanel;
        footerPanel.OnClickToOpenBet += HandleClickToBet_FooterPanel;
        betPanel.OnClickToExit += HandleClickToExit_BetPanel;
    }

    public void Deactivate()
    {
        mainPanel.OnClickToExit -= HandleClickToExit_MainPanel;
        footerPanel.OnClickToOpenBet -= HandleClickToBet_FooterPanel;
        betPanel.OnClickToExit -= HandleClickToExit_BetPanel;

        if (currentPanel != null)
            CloseOtherPanel(currentPanel);
    }

    public void OpenFooterPanel()
    {
        if(footerPanel.IsActive) return;

        OpenOtherPanel(footerPanel);
    }

    public void CloseOtherPanel()
    {
        if(!footerPanel.IsActive) return;

        CloseOtherPanel(footerPanel);
    }

    public void OpenBetPanel()
    {
        if(betPanel.IsActive) return;

        OpenOtherPanel(betPanel);
    }

    public void CloseBetPanel()
    {
        if(!betPanel.IsActive) return;

        CloseOtherPanel(betPanel);
    }

    #region Input

    #region MainPanel

    public event Action OnClickToExit_MainPanel;

    private void HandleClickToExit_MainPanel()
    {
        OnClickToExit_MainPanel?.Invoke();
    }

    #endregion

    #region FooterPanel

    public event Action OnClickToBet_FooterPanel;

    private void HandleClickToBet_FooterPanel()
    {
        OnClickToBet_FooterPanel?.Invoke();
    }

    #endregion

    #region BetPanel

    public event Action OnClickToExit_BetPanel;

    private void HandleClickToExit_BetPanel()
    {
        OnClickToExit_BetPanel?.Invoke();
    }

    #endregion

    #endregion
}
