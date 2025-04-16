using System;
using Unity.VisualScripting;
using UnityEngine;

public class MiniGameSceneEntryPoint : MonoBehaviour
{
    [SerializeField] private Sounds sounds;
    [SerializeField] private ItemGroups itemGroups_Bedroom;
    [SerializeField] private ItemGroups itemGroups_Bioreactor;
    [SerializeField] private SpawnPointsData spawnPointsData;
    [SerializeField] private PathData pathData;
    [SerializeField] private UIMiniGameSceneRoot sceneRootPrefab;

    private UIMiniGameSceneRoot sceneRoot;
    private ViewContainer viewContainer;
    private BankPresenter bankPresenter;
    private SoundPresenter soundPresenter;
    private ParticleEffectPresenter particleEffectPresenter;

    private ScrollBackgroundPresenter scrollBackgroundPresenter;
    private PlatformPresenter platformPresenter;

    private RocketMovePresenter rocketMovePresenter;
    private RocketControlPresenter rocketControlPresenter;

    private StoreBetPresenter storeBetPresenter;
    private BetSelectPresenter betSelectPresenter;
    private BetPreparePresenter betPreparePresenter;

    private ObstacleSpawnerPresenter obstacleSpawnerPresenter;
    private ObstaclePresenter obstaclePresenter;
    private ObstacleRocketMovePresenter obstacleRocketMovePresenter;

    private AltitudePresenter altitudePresenter;
    private CourseDisplacementPresenter courseDisplacementPresenter;
    private ScoreMultiplierPresenter scoreMultiplierPresenter;
    private ScorePresenter scorePresenter;

    private AnimationFramePresenter animationFramePresenter;
    private ObstacleEffectPresenter obstacleEffectPresenter;
    private ObstacleSoundPresenter obstacleSoundPresenter;

    private GameGlobalStateMachine stateMachine;

    private RoomTransitionPresenter roomTransitionPresenter;

    private StoreItemPresenter storeItemPresenter_Bedroom;
    private ItemPreviewPresenter itemPreviewPresenter_Bedroom;
    private ItemsBuyPresenter itemsBuyPresenter_Bedroom;
    private OpenItemPresenter itemOpenPresenter_Bedroom;
    private ItemSelectPresenter itemSelectPresenter_Bedroom;
    private ItemVisualPresenter itemVisualPresenter_Bedroom;

    private StoreItemPresenter storeItemPresenter_Bioreactor;
    private ItemPreviewPresenter itemPreviewPresenter_Bioreactor;
    private ItemsBuyPresenter itemsBuyPresenter_Bioreactor;
    private OpenItemPresenter itemOpenPresenter_Bioreactor;
    private ItemSelectPresenter itemSelectPresenter_Bioreactor;
    private ItemVisualPresenter itemVisualPresenter_Bioreactor;

    public void Run(UIRootView uIRootView)
    {
        sceneRoot = sceneRootPrefab;

        uIRootView.AttachSceneUI(sceneRoot.gameObject, Camera.main);

        viewContainer = sceneRoot.GetComponent<ViewContainer>();
        viewContainer.Initialize();

        soundPresenter = new SoundPresenter(new SoundModel(sounds.sounds, PlayerPrefsKeys.IS_MUTE_SOUNDS), viewContainer.GetView<SoundView>());
        bankPresenter = new BankPresenter(new BankModel(), viewContainer.GetView<BankView>());
        particleEffectPresenter = new ParticleEffectPresenter(new ParticleEffectModel(), viewContainer.GetView<ParticleEffectView>());

        scrollBackgroundPresenter = new ScrollBackgroundPresenter(new ScrollBackgroundModel(), viewContainer.GetView<ScrollBackgroundView>());
        platformPresenter = new PlatformPresenter(new  PlatformModel(), viewContainer.GetView<PlatformView>());

        rocketMovePresenter = new RocketMovePresenter(new RocketMoveModel(), viewContainer.GetView<RocketMoveView>());
        rocketControlPresenter = new RocketControlPresenter(new RocketControlModel(), viewContainer.GetView<RocketControlView>());

        storeBetPresenter = new StoreBetPresenter(new StoreBetModel(PlayerPrefsKeys.BET, 2.4f));
        betSelectPresenter = new BetSelectPresenter(new BetSelectModel(soundPresenter), viewContainer.GetView<BetSelectView>());
        betPreparePresenter = new BetPreparePresenter(new BetPrepareModel(bankPresenter), viewContainer.GetView<BetPrepareView>());

        obstacleSpawnerPresenter = new ObstacleSpawnerPresenter(new ObstacleSpawnerModel(spawnPointsData, 0.1f, 0.3f), viewContainer.GetView<ObstacleSpawnerView>());
        obstaclePresenter = new ObstaclePresenter(viewContainer.GetView<ObstacleView>());
        obstacleRocketMovePresenter = new ObstacleRocketMovePresenter(new ObstacleRocketMoveModel(rocketMovePresenter, pathData));

        altitudePresenter = new AltitudePresenter(new AltitudeModel(), viewContainer.GetView<AltitudeView>());
        courseDisplacementPresenter = new CourseDisplacementPresenter(new CourseDisplacementModel(), viewContainer.GetView<CourseDisplacementView>());
        scoreMultiplierPresenter = new ScoreMultiplierPresenter(new ScoreMultiplierModel(), viewContainer.GetView<ScoreMultiplierView>());
        scorePresenter = new ScorePresenter(new ScoreModel(bankPresenter, PlayerPrefsKeys.WIN_RECORD, PlayerPrefsKeys.WIN_LAST), viewContainer.GetView<ScoreView>());

        animationFramePresenter = new AnimationFramePresenter(new AnimationFrameModel(), viewContainer.GetView<AnimationFrameView>());
        obstacleEffectPresenter = new ObstacleEffectPresenter(new ObstacleEffectModel(animationFramePresenter));
        obstacleSoundPresenter = new ObstacleSoundPresenter(new ObstacleSoundModel(soundPresenter));

        roomTransitionPresenter = new RoomTransitionPresenter(new RoomTransitionModel(), viewContainer.GetView<RoomTransitionView>());

        storeItemPresenter_Bedroom = new StoreItemPresenter(new StoreItemModel("BedroomItems", itemGroups_Bedroom));
        itemPreviewPresenter_Bedroom = new ItemPreviewPresenter(new ItemPreviewModel(), viewContainer.GetView<ItemPreviewView>("Bedroom"));
        itemsBuyPresenter_Bedroom = new ItemsBuyPresenter(new ItemsBuyModel(storeItemPresenter_Bedroom, bankPresenter, soundPresenter), viewContainer.GetView<ItemsBuyView>("Bedroom"));
        itemOpenPresenter_Bedroom = new OpenItemPresenter(new OpenItemModel(), viewContainer.GetView<OpenItemView>("Bedroom"));
        itemSelectPresenter_Bedroom = new ItemSelectPresenter(new ItemSelectModel(soundPresenter), viewContainer.GetView<ItemSelectView>("Bedroom"));
        itemVisualPresenter_Bedroom = new ItemVisualPresenter(viewContainer.GetView<ItemVisualView>("Bedroom"));

        storeItemPresenter_Bioreactor = new StoreItemPresenter(new StoreItemModel("BioreactorItems", itemGroups_Bioreactor));
        itemPreviewPresenter_Bioreactor = new ItemPreviewPresenter(new ItemPreviewModel(), viewContainer.GetView<ItemPreviewView>("Bioreactor"));
        itemsBuyPresenter_Bioreactor = new ItemsBuyPresenter(new ItemsBuyModel(storeItemPresenter_Bioreactor, bankPresenter, soundPresenter), viewContainer.GetView<ItemsBuyView>("Bioreactor"));
        itemOpenPresenter_Bioreactor = new OpenItemPresenter(new OpenItemModel(), viewContainer.GetView<OpenItemView>("Bioreactor"));
        itemSelectPresenter_Bioreactor = new ItemSelectPresenter(new ItemSelectModel(soundPresenter), viewContainer.GetView<ItemSelectView>("Bioreactor"));
        itemVisualPresenter_Bioreactor = new ItemVisualPresenter(viewContainer.GetView<ItemVisualView>("Bioreactor"));

        stateMachine = new GameGlobalStateMachine(
            rocketMovePresenter, 
            platformPresenter, 
            scrollBackgroundPresenter,
            sceneRoot, 
            obstacleSpawnerPresenter, 
            obstaclePresenter, 
            storeBetPresenter,
            betPreparePresenter,
            altitudePresenter,
            courseDisplacementPresenter,
            scoreMultiplierPresenter,
            obstacleEffectPresenter,
            obstacleRocketMovePresenter,
            scorePresenter,
            soundPresenter,
            particleEffectPresenter);

        sceneRoot.SetSoundProvider(soundPresenter);
        sceneRoot.Activate();

        ActivateEvents();

        sceneRoot.Initialize();

        bankPresenter.Initialize();
        soundPresenter.Initialize();
        particleEffectPresenter.Initialize();

        scrollBackgroundPresenter.Initialize();
        platformPresenter.Initialize();

        rocketMovePresenter.Initialize();
        rocketControlPresenter.Initialize();

        betSelectPresenter.Initialize();
        betPreparePresenter.Initialize();

        obstaclePresenter.Initialize();
        obstacleSpawnerPresenter.Initialize();
        obstacleRocketMovePresenter.Initialize();

        storeBetPresenter.Initialize();

        altitudePresenter.Initialize();
        courseDisplacementPresenter.Initialize();
        scoreMultiplierPresenter.Initialize();
        scorePresenter.Initialize();

        animationFramePresenter.Initialize();
        obstacleEffectPresenter.Initialize();
        obstacleSoundPresenter.Initialize();

        roomTransitionPresenter.Initialize();


        itemVisualPresenter_Bedroom.Initialize();
        itemSelectPresenter_Bedroom.Initialize();
        itemOpenPresenter_Bedroom.Initialize();
        itemPreviewPresenter_Bedroom.Initialize();
        itemsBuyPresenter_Bedroom.Initialize();
        storeItemPresenter_Bedroom.Initialize();

        itemVisualPresenter_Bioreactor.Initialize();
        itemSelectPresenter_Bioreactor.Initialize();
        itemOpenPresenter_Bioreactor.Initialize();
        itemPreviewPresenter_Bioreactor.Initialize();
        itemsBuyPresenter_Bioreactor.Initialize();
        storeItemPresenter_Bioreactor.Initialize();

        stateMachine.Initialize();

    }

    private void ActivateEvents()
    {
        ActivateTransitionsSceneEvents();

        storeBetPresenter.OnChooseBet += betSelectPresenter.SetBet;
        storeBetPresenter.OnChooseBet += betPreparePresenter.SetBet;
        betSelectPresenter.OnIncreaseBet += storeBetPresenter.IncreaseBet;
        betSelectPresenter.OnDecreaseBet += storeBetPresenter.DecreaseBet;


        rocketControlPresenter.OnMoveLeft += rocketMovePresenter.MoveLeft;
        rocketControlPresenter.OnMoveRight += rocketMovePresenter.MoveRight;


        storeBetPresenter.OnChooseBet += scorePresenter.SetBet;
        scoreMultiplierPresenter.OnChangeScoreMultipliyer += scorePresenter.SetMultiplier;


        obstacleSpawnerPresenter.OnSpawnObstacle += obstaclePresenter.AddObstacle;

        obstacleSpawnerPresenter.OnSpawnObstacle += scoreMultiplierPresenter.AddObstacle;
        obstaclePresenter.OnDestroyObstacle += scoreMultiplierPresenter.RemoveObstacle;

        obstacleSpawnerPresenter.OnSpawnObstacle += obstacleEffectPresenter.AddObstacle;
        obstaclePresenter.OnDestroyObstacle += obstacleEffectPresenter.RemoveObstacle;

        obstacleSpawnerPresenter.OnSpawnObstacle += obstacleSoundPresenter.AddObstacle;
        obstaclePresenter.OnDestroyObstacle += obstacleSoundPresenter.RemoveObstacle;

        obstacleSpawnerPresenter.OnSpawnObstacle += obstacleRocketMovePresenter.AddObstacle;
        obstaclePresenter.OnDestroyObstacle += obstacleRocketMovePresenter.RemoveObstacle;



        itemPreviewPresenter_Bedroom.OnChooseBuyItemGroup += storeItemPresenter_Bedroom.SelectItemGroupForBuyItemGroup;
        storeItemPresenter_Bedroom.OnOpenItems += itemPreviewPresenter_Bedroom.Deactivate;
        storeItemPresenter_Bedroom.OnCloseItems += itemPreviewPresenter_Bedroom.Activate;

        itemOpenPresenter_Bedroom.OnChooseSelectItemGroupForSelectItem += storeItemPresenter_Bedroom.SelectItemGroupForSelectItem;
        storeItemPresenter_Bedroom.OnOpenItems += itemOpenPresenter_Bedroom.ActivateOpenItem;
        storeItemPresenter_Bedroom.OnCloseItems += itemOpenPresenter_Bedroom.DeactivateOpenItem;

        storeItemPresenter_Bedroom.OnSelectItem += itemVisualPresenter_Bedroom.SetVisual;

        itemSelectPresenter_Bedroom.OnChooseItemForSelect += storeItemPresenter_Bedroom.SelectItem;
        storeItemPresenter_Bedroom.OnSelectItemGroupForSelectItem += itemSelectPresenter_Bedroom.SetItemGroup;
        storeItemPresenter_Bedroom.OnSelectItem += itemSelectPresenter_Bedroom.Select;
        storeItemPresenter_Bedroom.OnDeselectItem += itemSelectPresenter_Bedroom.Deselect;

        storeItemPresenter_Bedroom.OnOpenAllItems += roomTransitionPresenter.UnlockRoomTwo;




        itemPreviewPresenter_Bioreactor.OnChooseBuyItemGroup += storeItemPresenter_Bioreactor.SelectItemGroupForBuyItemGroup;
        storeItemPresenter_Bioreactor.OnOpenItems += itemPreviewPresenter_Bioreactor.Deactivate;
        storeItemPresenter_Bioreactor.OnCloseItems += itemPreviewPresenter_Bioreactor.Activate;

        itemOpenPresenter_Bioreactor.OnChooseSelectItemGroupForSelectItem += storeItemPresenter_Bioreactor.SelectItemGroupForSelectItem;
        storeItemPresenter_Bioreactor.OnOpenItems += itemOpenPresenter_Bioreactor.ActivateOpenItem;
        storeItemPresenter_Bioreactor.OnCloseItems += itemOpenPresenter_Bioreactor.DeactivateOpenItem;

        storeItemPresenter_Bioreactor.OnSelectItem += itemVisualPresenter_Bioreactor.SetVisual;

        itemSelectPresenter_Bioreactor.OnChooseItemForSelect += storeItemPresenter_Bioreactor.SelectItem;
        storeItemPresenter_Bioreactor.OnSelectItemGroupForSelectItem += itemSelectPresenter_Bioreactor.SetItemGroup;
        storeItemPresenter_Bioreactor.OnSelectItem += itemSelectPresenter_Bioreactor.Select;
        storeItemPresenter_Bioreactor.OnDeselectItem += itemSelectPresenter_Bioreactor.Deselect;





    }

    private void DeactivateEvents()
    {
        DeactivateTransitionsSceneEvents();

        storeBetPresenter.OnChooseBet -= betSelectPresenter.SetBet;
        storeBetPresenter.OnChooseBet -= betPreparePresenter.SetBet;
        betSelectPresenter.OnIncreaseBet -= storeBetPresenter.IncreaseBet;
        betSelectPresenter.OnDecreaseBet -= storeBetPresenter.DecreaseBet;


        rocketControlPresenter.OnMoveLeft -= rocketMovePresenter.MoveLeft;
        rocketControlPresenter.OnMoveRight -= rocketMovePresenter.MoveRight;


        storeBetPresenter.OnChooseBet -= scorePresenter.SetBet;
        scoreMultiplierPresenter.OnChangeScoreMultipliyer -= scorePresenter.SetMultiplier;


        obstacleSpawnerPresenter.OnSpawnObstacle -= obstaclePresenter.AddObstacle;

        obstacleSpawnerPresenter.OnSpawnObstacle -= scoreMultiplierPresenter.AddObstacle;
        obstaclePresenter.OnDestroyObstacle -= scoreMultiplierPresenter.RemoveObstacle;

        obstacleSpawnerPresenter.OnSpawnObstacle -= obstacleEffectPresenter.AddObstacle;
        obstaclePresenter.OnDestroyObstacle -= obstacleEffectPresenter.RemoveObstacle;

        obstacleSpawnerPresenter.OnSpawnObstacle -= obstacleSoundPresenter.AddObstacle;
        obstaclePresenter.OnDestroyObstacle -= obstacleSoundPresenter.RemoveObstacle;

        obstacleSpawnerPresenter.OnSpawnObstacle -= obstacleRocketMovePresenter.AddObstacle;
        obstaclePresenter.OnDestroyObstacle -= obstacleRocketMovePresenter.RemoveObstacle;



    }

    private void ActivateTransitionsSceneEvents()
    {
        sceneRoot.OnClickToBet_FooterPanel += sceneRoot.OpenBetPanel;
        sceneRoot.OnClickToExit_BetPanel += sceneRoot.CloseBetPanel;

        sceneRoot.OnClickToExit_ExitPanel += HandleGoToMenu;

        storeItemPresenter_Bedroom.OnOpenItems_None += sceneRoot.CloseHouseBedroomBuyItemPanel;
        storeItemPresenter_Bedroom.OnSelectItem_None += sceneRoot.CloseHouseBedroomSelectItemPanel;
        storeItemPresenter_Bioreactor.OnOpenItems_None += sceneRoot.CloseHouseBioreactorBuyItemPanel;
        storeItemPresenter_Bioreactor.OnSelectItem_None += sceneRoot.CloseHouseBioreactorSelectItemPanel;
    }

    private void DeactivateTransitionsSceneEvents()
    {
        sceneRoot.OnClickToBet_FooterPanel += sceneRoot.OpenBetPanel;
        sceneRoot.OnClickToExit_BetPanel += sceneRoot.CloseBetPanel;

        sceneRoot.OnClickToExit_ExitPanel -= HandleGoToMenu;

        storeItemPresenter_Bedroom.OnOpenItems_None -= sceneRoot.CloseHouseBedroomBuyItemPanel;
        storeItemPresenter_Bedroom.OnSelectItem_None -= sceneRoot.CloseHouseBedroomSelectItemPanel;
        storeItemPresenter_Bioreactor.OnOpenItems_None -= sceneRoot.CloseHouseBioreactorBuyItemPanel;
        storeItemPresenter_Bioreactor.OnSelectItem_None -= sceneRoot.CloseHouseBioreactorSelectItemPanel;
    }

    public void Dispose()
    {
        sceneRoot.Deactivate();
        sceneRoot.Dispose();

        DeactivateEvents();

        bankPresenter.Dispose();
        particleEffectPresenter.Dispose();

        scrollBackgroundPresenter?.Dispose();
        platformPresenter.Dispose();

        rocketMovePresenter?.Dispose();
        rocketControlPresenter?.Dispose();

        betSelectPresenter?.Dispose();
        storeBetPresenter?.Dispose();
        betPreparePresenter?.Dispose();

        obstaclePresenter?.Dispose();
        obstacleSpawnerPresenter?.Dispose();
        obstacleRocketMovePresenter?.Dispose();

        altitudePresenter?.Dispose();
        courseDisplacementPresenter?.Dispose();
        scoreMultiplierPresenter?.Dispose();
        scorePresenter?.Dispose();

        animationFramePresenter?.Dispose();
        obstacleEffectPresenter?.Dispose();
        obstacleSoundPresenter?.Dispose();

        roomTransitionPresenter?.Dispose();


        itemVisualPresenter_Bedroom.Dispose();
        itemSelectPresenter_Bedroom.Dispose();
        itemOpenPresenter_Bedroom.Dispose();
        itemPreviewPresenter_Bedroom.Dispose();
        itemsBuyPresenter_Bedroom.Dispose();
        storeItemPresenter_Bedroom.Dispose();

        itemVisualPresenter_Bioreactor.Dispose();
        itemSelectPresenter_Bioreactor.Dispose();
        itemOpenPresenter_Bioreactor.Dispose();
        itemPreviewPresenter_Bioreactor.Dispose();
        itemsBuyPresenter_Bioreactor.Dispose();
        storeItemPresenter_Bioreactor.Dispose();

        stateMachine?.Dispose();
    }

    private void OnDestroy()
    {
        Dispose();
    }

    #region Input

    public event Action OnGoToMenu;

    private void HandleGoToMenu()
    {
        sceneRoot.Deactivate();
        soundPresenter.Dispose();
        OnGoToMenu?.Invoke();
    }

    #endregion
}
