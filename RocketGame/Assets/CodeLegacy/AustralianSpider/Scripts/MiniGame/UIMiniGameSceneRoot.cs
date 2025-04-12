using System;
using UnityEngine;

public class UIMiniGameSceneRoot : UIRoot
{
    [SerializeField] private MainPanel_Game mainPanel;
    [SerializeField] private FooterPanel_Game footerPanel;
    [SerializeField] private BetPanel_Game betPanel;
    [SerializeField] private WinPanel_Game winPanel;

    [SerializeField] private HouseChoosePanel_Game houseChoosePanel;

    [SerializeField] private HouseBedroomPanel_Game houseBedroomPanel;
    [SerializeField] private HouseBedroomBuyItemPanel_Game houseBedroomBuyItemPanel;
    [SerializeField] private HouseBedroomSelectItemPanel_Game houseBedroomSelectItemPanel;

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

        houseBedroomPanel.Initialize();
        houseBedroomBuyItemPanel.Initialize();
        houseBedroomSelectItemPanel.Initialize();
    }

    public void Dispose()
    {
        mainPanel.Dispose();
        footerPanel.Dispose();
        betPanel.Dispose();
        winPanel.Dispose();

        houseChoosePanel.Dispose();

        houseBedroomPanel.Dispose();
        houseBedroomBuyItemPanel.Dispose();
        houseBedroomSelectItemPanel.Dispose();
    }

    public void Activate()
    {
        mainPanel.OnClickToExit += HandleClickToExit_MainPanel;
        footerPanel.OnClickToOpenBet += HandleClickToBet_FooterPanel;
        betPanel.OnClickToExit += HandleClickToExit_BetPanel;
        houseChoosePanel.OnClickToHouse += HandleClickToHouse_HouseChoosePanel;


        houseBedroomPanel.OnClickToGame += HandleClickToGame_HouseBedroomPanel;
        houseBedroomPanel.OnClickToSelectItems += HandleClickToSelectItems_HouseBedroomPanel;
        houseBedroomPanel.OnClickToBuyItems += HandleClickToBuyItems_HouseBedroomPanel;

        houseBedroomBuyItemPanel.OnClickToExit += HandleClickToExit_BuyItemsBedroomPanel;
        houseBedroomSelectItemPanel.OnClickToExit += HandleClickToExit_SelectItemsBedroomPanel;

        OpenMainPanel();
    }

    public void Deactivate()
    {
        mainPanel.OnClickToExit -= HandleClickToExit_MainPanel;
        footerPanel.OnClickToOpenBet -= HandleClickToBet_FooterPanel;
        betPanel.OnClickToExit -= HandleClickToExit_BetPanel;
        houseChoosePanel.OnClickToHouse -= HandleClickToHouse_HouseChoosePanel;


        houseBedroomPanel.OnClickToGame -= HandleClickToGame_HouseBedroomPanel;
        houseBedroomPanel.OnClickToSelectItems -= HandleClickToSelectItems_HouseBedroomPanel;
        houseBedroomPanel.OnClickToBuyItems -= HandleClickToBuyItems_HouseBedroomPanel;

        houseBedroomBuyItemPanel.OnClickToExit -= HandleClickToExit_BuyItemsBedroomPanel;
        houseBedroomSelectItemPanel.OnClickToExit -= HandleClickToExit_SelectItemsBedroomPanel;

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

    #region House

    #region BEDROOM

    public void OpenHouseBedroomPanel()
    {
        if(houseBedroomPanel.IsActive) return;

        OpenOtherPanel(houseBedroomPanel);
    }

    public void CloseHouseBedroomPanel()
    {
        if(!houseBedroomPanel.IsActive) return;
        CloseOtherPanel(houseBedroomPanel);
    }



    public void OpenHouseBedroomSelectItemPanel()
    {
        if (houseBedroomSelectItemPanel.IsActive) return;

        OpenOtherPanel(houseBedroomSelectItemPanel);
    }

    public void CloseHouseBedroomSelectItemPanel()
    {
        if (!houseBedroomSelectItemPanel.IsActive) return;
        CloseOtherPanel(houseBedroomSelectItemPanel);
    }


    
    public void OpenHouseBedroomBuyItemPanel()
    {
        if (houseBedroomBuyItemPanel.IsActive) return;

        OpenOtherPanel(houseBedroomBuyItemPanel);
    }

    public void CloseHouseBedroomBuyItemPanel()
    {
        if (!houseBedroomBuyItemPanel.IsActive) return;

        CloseOtherPanel(houseBedroomBuyItemPanel);
    }

    #endregion

    #endregion




    #region Output

    #region MainPanel

    public event Action OnClickToExit_MainPanel;

    private void HandleClickToExit_MainPanel()
    {
        soundProvider.PlayOneShot("Click");

        OnClickToExit_MainPanel?.Invoke();
    }

    #endregion

    #region HouseChoosePanel

    public event Action OnClickToHouse_HouseChoosePanel;

    private void HandleClickToHouse_HouseChoosePanel()
    {
        OnClickToHouse_HouseChoosePanel?.Invoke();
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

    #region HOUSE

    #region BEDROOM

    public event Action OnClickToGame_HouseBedroomPanel;

    private void HandleClickToGame_HouseBedroomPanel()
    {
        OnClickToGame_HouseBedroomPanel?.Invoke();
    }


    public event Action OnClickToBuyItems_HouseBedroomPanel;

    private void HandleClickToBuyItems_HouseBedroomPanel()
    {
        OnClickToBuyItems_HouseBedroomPanel.Invoke();
    }


    public event Action OnClickToSelectItems_HouseBedroomPanel;

    private void HandleClickToSelectItems_HouseBedroomPanel()
    {
        OnClickToSelectItems_HouseBedroomPanel?.Invoke();
    }





    public event Action OnClickToExit_SelectItemsBedroomPanel;

    private void HandleClickToExit_SelectItemsBedroomPanel()
    {
        OnClickToExit_SelectItemsBedroomPanel?.Invoke();
    }




    public event Action OnClickToExit_BuyItemsBedroomPanel;

    private void HandleClickToExit_BuyItemsBedroomPanel()
    {
        OnClickToExit_BuyItemsBedroomPanel?.Invoke();
    }

    #endregion

    #endregion

    #endregion
}
