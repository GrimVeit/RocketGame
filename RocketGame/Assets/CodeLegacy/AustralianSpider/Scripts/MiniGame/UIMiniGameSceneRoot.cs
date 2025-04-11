using System;
using UnityEngine;

public class UIMiniGameSceneRoot : UIRoot
{
    [SerializeField] private MainPanel_Game mainPanel;
    [SerializeField] private FooterPanel_Game footerPanel;
    [SerializeField] private BetPanel_Game betPanel;
    [SerializeField] private WinPanel_Game winPanel;
    [SerializeField] private HouseChoosePanel_Game houseChoosePanel;

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
        winPanel.Initialize();
        houseChoosePanel.Initialize();
    }

    public void Dispose()
    {
        mainPanel.Dispose();
        footerPanel.Dispose();
        betPanel.Dispose();
        winPanel.Dispose();
        houseChoosePanel?.Dispose();
    }

    public void Activate()
    {
        mainPanel.OnClickToExit += HandleClickToExit_MainPanel;
        footerPanel.OnClickToOpenBet += HandleClickToBet_FooterPanel;
        betPanel.OnClickToExit += HandleClickToExit_BetPanel;

        OpenMainPanel();
    }

    public void Deactivate()
    {
        mainPanel.OnClickToExit -= HandleClickToExit_MainPanel;
        footerPanel.OnClickToOpenBet -= HandleClickToBet_FooterPanel;
        betPanel.OnClickToExit -= HandleClickToExit_BetPanel;

        CloseHouseChoosePanel();
        CloseFooterPanel();

        if (currentPanel != null)
            CloseOtherPanel(currentPanel);
    }

    public void OpenMainPanel()
    {
        OpenPanel(mainPanel);
    }




    public void OpenFooterPanel()
    {
        if(footerPanel.IsActive) return;

        OpenOtherPanel(footerPanel);
    }

    public void CloseFooterPanel()
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




    public void OpenWinPanel()
    {
        if(winPanel.IsActive) return;

        OpenOtherPanel(winPanel);
    }

    public void CloseWinPanel()
    {
        if(!winPanel.IsActive) return;

        CloseOtherPanel(winPanel);
    }




    public void OpenHouseChoosePanel()
    {
        if(houseChoosePanel.IsActive) return;

        OpenOtherPanel(houseChoosePanel);
    }

    public void CloseHouseChoosePanel()
    {
        if(!houseChoosePanel.IsActive) return;

        CloseOtherPanel(houseChoosePanel);
    }

    #region Input

    #region MainPanel

    public event Action OnClickToExit_MainPanel;

    private void HandleClickToExit_MainPanel()
    {
        soundProvider.PlayOneShot("Click");

        OnClickToExit_MainPanel?.Invoke();
    }

    #endregion

    #region FooterPanel

    public event Action OnClickToBet_FooterPanel;

    private void HandleClickToBet_FooterPanel()
    {
        soundProvider.PlayOneShot("Click");

        OnClickToBet_FooterPanel?.Invoke();
    }

    #endregion

    #region BetPanel

    public event Action OnClickToExit_BetPanel;

    private void HandleClickToExit_BetPanel()
    {
        soundProvider.PlayOneShot("Click");

        OnClickToExit_BetPanel?.Invoke();
    }

    #endregion

    #endregion
}
