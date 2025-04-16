using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseBioreactorState_Game : IState
{
    private readonly IGlobalStateMachineProvider _stateMachineProvider;

    private readonly UIMiniGameSceneRoot _sceneRoot;

    private readonly ISoundProvider _soundProvider;
    private readonly ISound _soundGameStart;

    public HouseBioreactorState_Game(IGlobalStateMachineProvider stateMachineProvider, UIMiniGameSceneRoot sceneRoot, ISoundProvider soundProvider)
    {
        _stateMachineProvider = stateMachineProvider;
        _sceneRoot = sceneRoot;
        _soundProvider = soundProvider;
        _soundGameStart = _soundProvider.GetSound("Background_GameStart");
    }

    public void EnterState()
    {
        _sceneRoot.OnClickToBedroom_HouseBioreactorPanel += ChangeStateToBedroom;

        _sceneRoot.OnClickToSelectItems_HouseBioreactorPanel += _sceneRoot.OpenHouseBioreactorSelectItemPanel;
        _sceneRoot.OnClickToBuyItems_HouseBioreactorPanel += _sceneRoot.OpenHouseBioreactorBuyItemPanel;
        _sceneRoot.OnClickToExit_SelectItemsBioreactorPanel += _sceneRoot.CloseHouseBioreactorSelectItemPanel;
        _sceneRoot.OnClickToExit_BuyItemsBioreactorPanel += _sceneRoot.CloseHouseBioreactorBuyItemPanel;

        _sceneRoot.OpenHouseBioreactorPanel();

        _soundGameStart.Pause();
        _soundGameStart.SetVolume(0);
    }

    public void ExitState()
    {
        _sceneRoot.OnClickToBedroom_HouseBioreactorPanel -= ChangeStateToBedroom;

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
}
