using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseBedroomState_Game : IState
{
    private readonly IGlobalStateMachineProvider _stateMachineProvider;

    private readonly UIGameRoot _sceneRoot;

    private readonly ISoundProvider _soundProvider;
    private readonly ISound _soundGameStart;
    private readonly ISound _soundHouse;

    public HouseBedroomState_Game(IGlobalStateMachineProvider stateMachineProvider, UIGameRoot sceneRoot, ISoundProvider soundProvider)
    {
        _stateMachineProvider = stateMachineProvider;
        _sceneRoot = sceneRoot;
        _soundProvider = soundProvider;
        _soundGameStart = _soundProvider.GetSound("Background_GameStart");
        _soundHouse = _soundProvider.GetSound("Background_House");
    }

    public void EnterState()
    {
        _sceneRoot.OnClickToGame_HouseBedroomPanel += ChangeStateToPrepare;
        _sceneRoot.OnClickToBioreactor_HouseBedroomPanel += ChangeStateToBioreactor;

        _sceneRoot.OnClickToSelectItems_HouseBedroomPanel += _sceneRoot.OpenHouseBedroomSelectItemPanel;
        _sceneRoot.OnClickToBuyItems_HouseBedroomPanel += _sceneRoot.OpenHouseBedroomBuyItemPanel;
        _sceneRoot.OnClickToExit_SelectItemsBedroomPanel += _sceneRoot.CloseHouseBedroomSelectItemPanel;
        _sceneRoot.OnClickToExit_BuyItemsBedroomPanel += _sceneRoot.CloseHouseBedroomBuyItemPanel;

        _sceneRoot.OpenHouseBedroomPanel();
        _sceneRoot.OpenHouseBalancePanel();

        _soundGameStart.SetVolume(1, 0, 0.1f);
        _soundHouse.SetVolume(0, 0.6f, 0.1f);
    }

    public void ExitState()
    {
        _sceneRoot.OnClickToGame_HouseBedroomPanel -= ChangeStateToPrepare;
        _sceneRoot.OnClickToBioreactor_HouseBedroomPanel -= ChangeStateToBioreactor;

        _sceneRoot.OnClickToSelectItems_HouseBedroomPanel -= _sceneRoot.OpenHouseBedroomSelectItemPanel;
        _sceneRoot.OnClickToBuyItems_HouseBedroomPanel -= _sceneRoot.OpenHouseBedroomBuyItemPanel;
        _sceneRoot.OnClickToExit_SelectItemsBedroomPanel -= _sceneRoot.CloseHouseBedroomSelectItemPanel;
        _sceneRoot.OnClickToExit_BuyItemsBedroomPanel -= _sceneRoot.CloseHouseBedroomBuyItemPanel;

        _sceneRoot.CloseHouseBedroomPanel();

        _soundGameStart.SetVolume(0, 1, 0.1f);
        _soundHouse.SetVolume(0.6f, 0, 0.1f);
    }

    private void ChangeStateToPrepare()
    {
        _stateMachineProvider.SetState(_stateMachineProvider.GetState<PrepareState_Game>());
    }

    private void ChangeStateToBioreactor()
    {
        _stateMachineProvider.SetState(_stateMachineProvider.GetState<HouseBioreactorState_Game>());
    }
}
