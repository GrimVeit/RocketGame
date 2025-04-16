using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseBioreactorState_Game : IState
{
    private readonly IGlobalStateMachineProvider _stateMachineProvider;

    private readonly UIMiniGameSceneRoot _sceneRoot;

    public HouseBioreactorState_Game(IGlobalStateMachineProvider stateMachineProvider, UIMiniGameSceneRoot sceneRoot)
    {
        _stateMachineProvider = stateMachineProvider;
        _sceneRoot = sceneRoot;
    }

    public void EnterState()
    {
        _sceneRoot.OnClickToBedroom_HouseBioreactorPanel += ChangeStateToBedroom;

        _sceneRoot.OnClickToSelectItems_HouseBioreactorPanel += _sceneRoot.OpenHouseBioreactorSelectItemPanel;
        _sceneRoot.OnClickToBuyItems_HouseBioreactorPanel += _sceneRoot.OpenHouseBioreactorBuyItemPanel;
        _sceneRoot.OnClickToExit_SelectItemsBioreactorPanel += _sceneRoot.CloseHouseBioreactorSelectItemPanel;
        _sceneRoot.OnClickToExit_BuyItemsBioreactorPanel += _sceneRoot.CloseHouseBioreactorBuyItemPanel;

        _sceneRoot.OpenHouseBioreactorPanel();
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
