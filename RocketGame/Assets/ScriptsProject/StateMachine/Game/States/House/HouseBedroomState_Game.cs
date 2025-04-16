using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseBedroomState_Game : IState
{
    private readonly IGlobalStateMachineProvider _stateMachineProvider;

    private readonly UIMiniGameSceneRoot _sceneRoot;

    private readonly ISoundProvider _soundProvider;
    private readonly ISound _soundGameStart;

    public HouseBedroomState_Game(IGlobalStateMachineProvider stateMachineProvider, UIMiniGameSceneRoot sceneRoot, ISoundProvider soundProvider)
    {
        _stateMachineProvider = stateMachineProvider;
        _sceneRoot = sceneRoot;
        _soundProvider = soundProvider;
        _soundGameStart = _soundProvider.GetSound("Background_GameStart");
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

        _soundGameStart.SetVolume(1, 0, 0.1f, _soundGameStart.Pause);
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
        _sceneRoot.CloseHouseBalancePanel();

        _soundGameStart.Play();
        _soundGameStart.SetVolume(0, 1, 0.1f);
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
