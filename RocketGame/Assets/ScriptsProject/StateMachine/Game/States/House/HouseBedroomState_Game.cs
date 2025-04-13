using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseBedroomState_Game : IState
{
    private readonly IGlobalStateMachineProvider _stateMachineProvider;

    private readonly UIMiniGameSceneRoot _sceneRoot;

    public HouseBedroomState_Game(IGlobalStateMachineProvider stateMachineProvider, UIMiniGameSceneRoot sceneRoot)
    {
        _stateMachineProvider = stateMachineProvider;
        _sceneRoot = sceneRoot;
    }

    public void EnterState()
    {
        _sceneRoot.OnClickToGame_HouseBedroomPanel += ChangeStateToPrepare;

        _sceneRoot.OnClickToSelectItems_HouseBedroomPanel += _sceneRoot.OpenHouseBedroomSelectItemPanel;
        _sceneRoot.OnClickToBuyItems_HouseBedroomPanel += _sceneRoot.OpenHouseBedroomBuyItemPanel;
        _sceneRoot.OnClickToExit_SelectItemsBedroomPanel += _sceneRoot.CloseHouseBedroomSelectItemPanel;
        _sceneRoot.OnClickToExit_BuyItemsBedroomPanel += _sceneRoot.CloseHouseBedroomBuyItemPanel;

        _sceneRoot.OpenHouseBedroomPanel();
        _sceneRoot.OpenHouseBalancePanel();
    }

    public void ExitState()
    {
        _sceneRoot.OnClickToGame_HouseBedroomPanel -= ChangeStateToPrepare;

        _sceneRoot.OnClickToSelectItems_HouseBedroomPanel -= _sceneRoot.OpenHouseBedroomSelectItemPanel;
        _sceneRoot.OnClickToBuyItems_HouseBedroomPanel -= _sceneRoot.OpenHouseBedroomBuyItemPanel;
        _sceneRoot.OnClickToExit_SelectItemsBedroomPanel -= _sceneRoot.CloseHouseBedroomSelectItemPanel;
        _sceneRoot.OnClickToExit_BuyItemsBedroomPanel -= _sceneRoot.CloseHouseBedroomBuyItemPanel;

        _sceneRoot.CloseHouseBedroomPanel();
        _sceneRoot.CloseHouseBalancePanel();
    }

    private void ChangeStateToPrepare()
    {
        _stateMachineProvider.SetState(_stateMachineProvider.GetState<PrepareState_Game>());
    }
}
