using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseStorageState_Game : IState
{
    private readonly IGlobalStateMachineProvider _stateMachineProvider;

    private readonly UIMiniGameSceneRoot _sceneRoot;

    private readonly ISoundProvider _soundProvider;
    private readonly ISound _soundGameStart;

    public HouseStorageState_Game(IGlobalStateMachineProvider stateMachineProvider, UIMiniGameSceneRoot sceneRoot, ISoundProvider soundProvider)
    {
        _stateMachineProvider = stateMachineProvider;
        _sceneRoot = sceneRoot;
        _soundProvider = soundProvider;
        _soundGameStart = _soundProvider.GetSound("Background_GameStart");
    }

    public void EnterState()
    {
        _sceneRoot.OnClickToBioreactor_HouseStoragePanel += ChangeStateToBioreactor;

        _sceneRoot.OnClickToSelectItems_HouseStoragePanel += _sceneRoot.OpenHouseStorageSelectItemPanel;
        _sceneRoot.OnClickToBuyItems_HouseStoragePanel += _sceneRoot.OpenHouseStorageBuyItemPanel;
        _sceneRoot.OnClickToExit_SelectItemsStoragePanel += _sceneRoot.CloseHouseStorageSelectItemPanel;
        _sceneRoot.OnClickToExit_BuyItemsStoragePanel += _sceneRoot.CloseHouseStorageBuyItemPanel;

        _sceneRoot.OpenHouseStoragePanel();

        _soundGameStart.SetVolume(0);
    }

    public void ExitState()
    {
        _sceneRoot.OnClickToBioreactor_HouseStoragePanel -= ChangeStateToBioreactor;

        _sceneRoot.OnClickToSelectItems_HouseStoragePanel -= _sceneRoot.OpenHouseStorageSelectItemPanel;
        _sceneRoot.OnClickToBuyItems_HouseStoragePanel -= _sceneRoot.OpenHouseStorageBuyItemPanel;
        _sceneRoot.OnClickToExit_SelectItemsStoragePanel -= _sceneRoot.CloseHouseStorageSelectItemPanel;
        _sceneRoot.OnClickToExit_BuyItemsStoragePanel -= _sceneRoot.CloseHouseStorageBuyItemPanel;

        _sceneRoot.CloseHouseStoragePanel();
    }

    private void ChangeStateToBioreactor()
    {
        _stateMachineProvider.SetState(_stateMachineProvider.GetState<HouseBioreactorState_Game>());
    }
}
