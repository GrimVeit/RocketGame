using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseBioreactorState_Game : IState
{
    private readonly IGlobalStateMachineProvider _stateMachineProvider;

    private readonly UIGameRoot _sceneRoot;

    private readonly ISoundProvider _soundProvider;
    private readonly ISound _soundGameStart;
    private readonly ISound _soundHouse;

    public HouseBioreactorState_Game(IGlobalStateMachineProvider stateMachineProvider, UIGameRoot sceneRoot, ISoundProvider soundProvider)
    {
        _stateMachineProvider = stateMachineProvider;
        _sceneRoot = sceneRoot;
        _soundProvider = soundProvider;
        _soundGameStart = _soundProvider.GetSound("Background_GameStart");
        _soundHouse = _soundProvider.GetSound("Background_House");
    }

    public void EnterState()
    {
        _sceneRoot.OnClickToBedroom_HouseBioreactorPanel += ChangeStateToBedroom;
        _sceneRoot.OnClickToStorage_HouseBioreactorPanel += ChangeStateToStorage;

        _sceneRoot.OnClickToSelectItems_HouseBioreactorPanel += _sceneRoot.OpenHouseBioreactorSelectItemPanel;
        _sceneRoot.OnClickToBuyItems_HouseBioreactorPanel += _sceneRoot.OpenHouseBioreactorBuyItemPanel;
        _sceneRoot.OnClickToExit_SelectItemsBioreactorPanel += _sceneRoot.CloseHouseBioreactorSelectItemPanel;
        _sceneRoot.OnClickToExit_BuyItemsBioreactorPanel += _sceneRoot.CloseHouseBioreactorBuyItemPanel;

        _sceneRoot.OpenHouseBioreactorPanel();

        _soundGameStart.SetVolume(0, 0, 0.1f);
        _soundHouse.SetVolume(0.6f, 0.6f, 0.1f);
    }

    public void ExitState()
    {
        _sceneRoot.OnClickToBedroom_HouseBioreactorPanel -= ChangeStateToBedroom;
        _sceneRoot.OnClickToStorage_HouseBioreactorPanel -= ChangeStateToStorage;

        _sceneRoot.OnClickToSelectItems_HouseBioreactorPanel -= _sceneRoot.OpenHouseBioreactorSelectItemPanel;
        _sceneRoot.OnClickToBuyItems_HouseBioreactorPanel -= _sceneRoot.OpenHouseBioreactorBuyItemPanel;
        _sceneRoot.OnClickToExit_SelectItemsBioreactorPanel -= _sceneRoot.CloseHouseBioreactorSelectItemPanel;
        _sceneRoot.OnClickToExit_BuyItemsBioreactorPanel -= _sceneRoot.CloseHouseBioreactorBuyItemPanel;

        _sceneRoot.CloseHouseBioreactorPanel();
    }

    private void ChangeStateToBedroom()
    {
        _stateMachineProvider.SetState(_stateMachineProvider.GetState<HouseBedroomState_Game>());
    }

    private void ChangeStateToStorage()
    {
        _stateMachineProvider.SetState(_stateMachineProvider.GetState<HouseStorageState_Game>());
    }
}
