using System;
using UnityEngine;

public class UIMiniGameSceneRoot : UIRoot
{
    [SerializeField] private MainPanel_Game mainPanel;
    [SerializeField] private FooterPanel_Game footerPanel;
    [SerializeField] private BetPanel_Game betPanel;
    [SerializeField] private WinPanel_Game winPanel;
    [SerializeField] private ExitPanel_Game exitPanel;

    [SerializeField] private HouseChoosePanel_Game houseChoosePanel;

    [SerializeField] private BalanceHousePanel_Game houseBalancePanel;

    [SerializeField] private HouseBedroomPanel_Game houseBedroomPanel;
    [SerializeField] private HouseBedroomBuyItemPanel_Game houseBedroomBuyItemPanel;
    [SerializeField] private HouseBedroomSelectItemPanel_Game houseBedroomSelectItemPanel;

    [SerializeField] private HouseBioreactorPanel_Game houseBioreactorPanel;
    [SerializeField] private HouseBioreactorBuyItemPanel_Game houseBioreactorBuyItemPanel;
    [SerializeField] private HouseBioreactorSelectItemPanel_Game houseBioreactorSelectItemPanel;

    [SerializeField] private HouseStoragePanel_Game houseStoragePanel;
    [SerializeField] private HouseStorageBuyItemPanel_Game houseStorageBuyItemPanel;
    [SerializeField] private HouseStorageSelectItemPanel_Game houseStorageSelectItemPanel;

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
        exitPanel.Initialize();

        houseChoosePanel.Initialize();

        houseBalancePanel.Initialize();

        houseBedroomPanel.Initialize();
        houseBedroomBuyItemPanel.Initialize();
        houseBedroomSelectItemPanel.Initialize();


        houseBioreactorPanel.Initialize();
        houseBioreactorBuyItemPanel.Initialize();
        houseBioreactorSelectItemPanel.Initialize();


        houseStoragePanel.Initialize();
        houseStorageBuyItemPanel.Initialize();
        houseStorageSelectItemPanel.Initialize();
    }

    public void Dispose()
    {
        mainPanel.Dispose();
        footerPanel.Dispose();
        betPanel.Dispose();
        winPanel.Dispose();
        exitPanel.Dispose();

        houseChoosePanel.Dispose();

        houseBalancePanel.Dispose();


        houseBedroomPanel.Dispose();
        houseBedroomBuyItemPanel.Dispose();
        houseBedroomSelectItemPanel.Dispose();

        houseBioreactorPanel.Dispose();
        houseBioreactorBuyItemPanel.Dispose();
        houseBioreactorSelectItemPanel.Dispose();

        houseStoragePanel.Dispose();
        houseStorageBuyItemPanel.Dispose();
        houseStorageSelectItemPanel.Dispose();
    }

    public void Activate()
    {
        exitPanel.OnClickToExit += HandleClickToExit_ExitPanel;
        footerPanel.OnClickToOpenBet += HandleClickToBet_FooterPanel;
        betPanel.OnClickToExit += HandleClickToExit_BetPanel;
        houseChoosePanel.OnClickToHouse += HandleClickToHouse_HouseChoosePanel;


        houseBedroomPanel.OnClickToBioreactor += HandleClickToBioreactor_HouseBedroomPanel;
        houseBedroomPanel.OnClickToGame += HandleClickToGame_HouseBedroomPanel;
        houseBedroomPanel.OnClickToSelectItems += HandleClickToSelectItems_HouseBedroomPanel;
        houseBedroomPanel.OnClickToBuyItems += HandleClickToBuyItems_HouseBedroomPanel;
        houseBedroomBuyItemPanel.OnClickToExit += HandleClickToExit_BuyItemsBedroomPanel;
        houseBedroomSelectItemPanel.OnClickToExit += HandleClickToExit_SelectItemsBedroomPanel;


        houseBioreactorPanel.OnClickToBedroom += HandleClickToBedroom_HouseBioreactorPanel;
        houseBioreactorPanel.OnClickToStorage += HandleClickToStorage_HouseBioreactorPanel;
        houseBioreactorPanel.OnClickToSelectItems += HandleClickToSelectItems_HouseBioreactorPanel;
        houseBioreactorPanel.OnClickToBuyItems += HandleClickToBuyItems_HouseBioreactorPanel;
        houseBioreactorBuyItemPanel.OnClickToExit += HandleClickToExit_BuyItemsBioreactorPanel;
        houseBioreactorSelectItemPanel.OnClickToExit += HandleClickToExit_SelectItemsBioreactorPanel;


        houseStoragePanel.OnClickToBioreactor += HandleClickToBioreactor_HouseStoragePanel;
        houseStoragePanel.OnClickToSelectItems += HandleClickToSelectItems_HouseStoragePanel;
        houseStoragePanel.OnClickToBuyItems += HandleClickToBuyItems_HouseStoragePanel;
        houseStorageBuyItemPanel.OnClickToExit += HandleClickToExit_BuyItemsStoragePanel;
        houseStorageSelectItemPanel.OnClickToExit += HandleClickToExit_SelectItemsStoragePanel;

        OpenMainPanel();
    }

    public void Deactivate()
    {
        exitPanel.OnClickToExit -= HandleClickToExit_ExitPanel;
        footerPanel.OnClickToOpenBet -= HandleClickToBet_FooterPanel;
        betPanel.OnClickToExit -= HandleClickToExit_BetPanel;
        houseChoosePanel.OnClickToHouse -= HandleClickToHouse_HouseChoosePanel;


        houseBedroomPanel.OnClickToGame -= HandleClickToGame_HouseBedroomPanel;
        houseBedroomPanel.OnClickToSelectItems -= HandleClickToSelectItems_HouseBedroomPanel;
        houseBedroomPanel.OnClickToBuyItems -= HandleClickToBuyItems_HouseBedroomPanel;
        houseBedroomBuyItemPanel.OnClickToExit -= HandleClickToExit_BuyItemsBedroomPanel;
        houseBedroomSelectItemPanel.OnClickToExit -= HandleClickToExit_SelectItemsBedroomPanel;


        houseBioreactorPanel.OnClickToBedroom -= HandleClickToBedroom_HouseBioreactorPanel;
        houseBioreactorPanel.OnClickToStorage -= HandleClickToStorage_HouseBioreactorPanel;
        houseBioreactorPanel.OnClickToSelectItems -= HandleClickToSelectItems_HouseBioreactorPanel;
        houseBioreactorPanel.OnClickToBuyItems -= HandleClickToBuyItems_HouseBioreactorPanel;
        houseBioreactorBuyItemPanel.OnClickToExit -= HandleClickToExit_BuyItemsBioreactorPanel;
        houseBioreactorSelectItemPanel.OnClickToExit -= HandleClickToExit_SelectItemsBioreactorPanel;


        houseStoragePanel.OnClickToBioreactor -= HandleClickToBioreactor_HouseStoragePanel;
        houseStoragePanel.OnClickToSelectItems -= HandleClickToSelectItems_HouseStoragePanel;
        houseStoragePanel.OnClickToBuyItems -= HandleClickToBuyItems_HouseStoragePanel;
        houseStorageBuyItemPanel.OnClickToExit -= HandleClickToExit_BuyItemsStoragePanel;
        houseStorageSelectItemPanel.OnClickToExit -= HandleClickToExit_SelectItemsStoragePanel;

        CloseHouseChoosePanel();
        CloseFooterPanel();
        CloseExitPanel();

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




    public void OpenExitPanel()
    {
        if(exitPanel.IsActive) return;

        OpenOtherPanel(exitPanel);
    }

    public void CloseExitPanel()
    {
        if(!exitPanel.IsActive) return;

        CloseOtherPanel(exitPanel);
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

    public void OpenHouseBalancePanel()
    {
        if(houseBalancePanel.IsActive) return;

        OpenOtherPanel(houseBalancePanel);
    }

    public void CloseHouseBalancePanel()
    {
        if(!houseBalancePanel.IsActive) return;

        CloseOtherPanel(houseBalancePanel);
    }




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

    #region BIOREACTOR


    public void OpenHouseBioreactorPanel()
    {
        if (houseBioreactorPanel.IsActive) return;

        OpenOtherPanel(houseBioreactorPanel);
    }

    public void CloseHouseBioreactorPanel()
    {
        if (!houseBioreactorPanel.IsActive) return;
        CloseOtherPanel(houseBioreactorPanel);
    }



    public void OpenHouseBioreactorSelectItemPanel()
    {
        if (houseBioreactorSelectItemPanel.IsActive) return;

        OpenOtherPanel(houseBioreactorSelectItemPanel);
    }

    public void CloseHouseBioreactorSelectItemPanel()
    {
        if (!houseBioreactorSelectItemPanel.IsActive) return;
        CloseOtherPanel(houseBioreactorSelectItemPanel);
    }



    public void OpenHouseBioreactorBuyItemPanel()
    {
        if (houseBioreactorBuyItemPanel.IsActive) return;

        OpenOtherPanel(houseBioreactorBuyItemPanel);
    }

    public void CloseHouseBioreactorBuyItemPanel()
    {
        if (!houseBioreactorBuyItemPanel.IsActive) return;

        CloseOtherPanel(houseBioreactorBuyItemPanel);
    }

    #endregion

    #region BIOREACTOR


    public void OpenHouseStoragePanel()
    {
        if (houseStoragePanel.IsActive) return;

        OpenOtherPanel(houseStoragePanel);
    }

    public void CloseHouseStoragePanel()
    {
        if (!houseStoragePanel.IsActive) return;

        CloseOtherPanel(houseStoragePanel);
    }



    public void OpenHouseStorageSelectItemPanel()
    {
        if (houseStorageSelectItemPanel.IsActive) return;

        OpenOtherPanel(houseStorageSelectItemPanel);
    }

    public void CloseHouseStorageSelectItemPanel()
    {
        if (!houseStorageSelectItemPanel.IsActive) return;

        CloseOtherPanel(houseStorageSelectItemPanel);
    }



    public void OpenHouseStorageBuyItemPanel()
    {
        if (houseStorageBuyItemPanel.IsActive) return;

        OpenOtherPanel(houseStorageBuyItemPanel);
    }

    public void CloseHouseStorageBuyItemPanel()
    {
        if (!houseStorageBuyItemPanel.IsActive) return;

        CloseOtherPanel(houseStorageBuyItemPanel);
    }

    #endregion

    #endregion




    #region Output

    #region ExitPanel

    public event Action OnClickToExit_ExitPanel;

    private void HandleClickToExit_ExitPanel()
    {
        soundProvider.PlayOneShot("Click");

        OnClickToExit_ExitPanel?.Invoke();
    }

    #endregion

    #region HouseChoosePanel

    public event Action OnClickToHouse_HouseChoosePanel;

    private void HandleClickToHouse_HouseChoosePanel()
    {
        soundProvider.PlayOneShot("Click");

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
        soundProvider.PlayOneShot("Click");

        OnClickToGame_HouseBedroomPanel?.Invoke();
    }


    public event Action OnClickToBioreactor_HouseBedroomPanel;

    private void HandleClickToBioreactor_HouseBedroomPanel()
    {
        soundProvider.PlayOneShot("Click");

        OnClickToBioreactor_HouseBedroomPanel?.Invoke();
    }


    public event Action OnClickToBuyItems_HouseBedroomPanel;

    private void HandleClickToBuyItems_HouseBedroomPanel()
    {
        soundProvider.PlayOneShot("Click");

        OnClickToBuyItems_HouseBedroomPanel.Invoke();
    }


    public event Action OnClickToSelectItems_HouseBedroomPanel;

    private void HandleClickToSelectItems_HouseBedroomPanel()
    {
        soundProvider.PlayOneShot("Click");

        OnClickToSelectItems_HouseBedroomPanel?.Invoke();
    }





    public event Action OnClickToExit_SelectItemsBedroomPanel;

    private void HandleClickToExit_SelectItemsBedroomPanel()
    {
        soundProvider.PlayOneShot("Click");

        OnClickToExit_SelectItemsBedroomPanel?.Invoke();
    }




    public event Action OnClickToExit_BuyItemsBedroomPanel;

    private void HandleClickToExit_BuyItemsBedroomPanel()
    {
        soundProvider.PlayOneShot("Click");

        OnClickToExit_BuyItemsBedroomPanel?.Invoke();
    }

    #endregion

    #region BIOREACTOR

    public event Action OnClickToBedroom_HouseBioreactorPanel;

    private void HandleClickToBedroom_HouseBioreactorPanel()
    {
        soundProvider.PlayOneShot("Click");

        OnClickToBedroom_HouseBioreactorPanel?.Invoke();
    }

    public event Action OnClickToStorage_HouseBioreactorPanel;

    private void HandleClickToStorage_HouseBioreactorPanel()
    {
        soundProvider.PlayOneShot("Click");

        OnClickToStorage_HouseBioreactorPanel?.Invoke();
    }


    public event Action OnClickToBuyItems_HouseBioreactorPanel;

    private void HandleClickToBuyItems_HouseBioreactorPanel()
    {
        soundProvider.PlayOneShot("Click");

        OnClickToBuyItems_HouseBioreactorPanel.Invoke();
    }


    public event Action OnClickToSelectItems_HouseBioreactorPanel;

    private void HandleClickToSelectItems_HouseBioreactorPanel()
    {
        soundProvider.PlayOneShot("Click");

        OnClickToSelectItems_HouseBioreactorPanel?.Invoke();
    }





    public event Action OnClickToExit_SelectItemsBioreactorPanel;

    private void HandleClickToExit_SelectItemsBioreactorPanel()
    {
        soundProvider.PlayOneShot("Click");

        OnClickToExit_SelectItemsBioreactorPanel?.Invoke();
    }




    public event Action OnClickToExit_BuyItemsBioreactorPanel;

    private void HandleClickToExit_BuyItemsBioreactorPanel()
    {
        soundProvider.PlayOneShot("Click");

        OnClickToExit_BuyItemsBioreactorPanel?.Invoke();
    }

    #endregion

    #region STORAGE

    public event Action OnClickToBioreactor_HouseStoragePanel;

    private void HandleClickToBioreactor_HouseStoragePanel()
    {
        soundProvider.PlayOneShot("Click");

        OnClickToBioreactor_HouseStoragePanel?.Invoke();
    }


    public event Action OnClickToBuyItems_HouseStoragePanel;

    private void HandleClickToBuyItems_HouseStoragePanel()
    {
        soundProvider.PlayOneShot("Click");

        OnClickToBuyItems_HouseStoragePanel.Invoke();
    }


    public event Action OnClickToSelectItems_HouseStoragePanel;

    private void HandleClickToSelectItems_HouseStoragePanel()
    {
        soundProvider.PlayOneShot("Click");

        OnClickToSelectItems_HouseStoragePanel?.Invoke();
    }





    public event Action OnClickToExit_SelectItemsStoragePanel;

    private void HandleClickToExit_SelectItemsStoragePanel()
    {
        soundProvider.PlayOneShot("Click");

        OnClickToExit_SelectItemsStoragePanel?.Invoke();
    }




    public event Action OnClickToExit_BuyItemsStoragePanel;

    private void HandleClickToExit_BuyItemsStoragePanel()
    {
        soundProvider.PlayOneShot("Click");

        OnClickToExit_BuyItemsStoragePanel?.Invoke();
    }

    #endregion

    #endregion

    #endregion
}
