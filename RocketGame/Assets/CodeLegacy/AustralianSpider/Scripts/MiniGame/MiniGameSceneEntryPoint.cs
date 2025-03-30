using System;
using Unity.VisualScripting;
using UnityEngine;

public class MiniGameSceneEntryPoint : MonoBehaviour
{
    [SerializeField] private Sounds sounds;
    [SerializeField] private SpawnPointsData spawnPointsData;
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

    private AltitudePresenter altitudePresenter;
    private CourseDisplacementPresenter courseDisplacementPresenter;

    private GameGlobalStateMachine stateMachine;

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
        betSelectPresenter = new BetSelectPresenter(new BetSelectModel(), viewContainer.GetView<BetSelectView>());
        betPreparePresenter = new BetPreparePresenter(new BetPrepareModel(bankPresenter), viewContainer.GetView<BetPrepareView>());

        obstacleSpawnerPresenter = new ObstacleSpawnerPresenter(new ObstacleSpawnerModel(spawnPointsData, 0.4f, 2), viewContainer.GetView<ObstacleSpawnerView>());
        obstaclePresenter = new ObstaclePresenter(viewContainer.GetView<ObstacleView>());

        altitudePresenter = new AltitudePresenter(new AltitudeModel(), viewContainer.GetView<AltitudeView>());
        courseDisplacementPresenter = new CourseDisplacementPresenter(new CourseDisplacementModel(), viewContainer.GetView<CourseDisplacementView>());

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
            courseDisplacementPresenter);

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

        storeBetPresenter.Initialize();

        altitudePresenter.Initialize();
        courseDisplacementPresenter.Initialize();

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

        obstacleSpawnerPresenter.OnSpawnObstacle += obstaclePresenter.AddObstacle;

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

        obstacleSpawnerPresenter.OnSpawnObstacle -= obstaclePresenter.AddObstacle;
    }

    private void ActivateTransitionsSceneEvents()
    {
        sceneRoot.OnClickToBet_FooterPanel += sceneRoot.OpenBetPanel;
        sceneRoot.OnClickToExit_BetPanel += sceneRoot.CloseBetPanel;

        sceneRoot.OnClickToExit_MainPanel += HandleGoToMenu;
    }

    private void DeactivateTransitionsSceneEvents()
    {
        sceneRoot.OnClickToBet_FooterPanel += sceneRoot.OpenBetPanel;
        sceneRoot.OnClickToExit_BetPanel += sceneRoot.CloseBetPanel;

        sceneRoot.OnClickToExit_MainPanel -= HandleGoToMenu;
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

        altitudePresenter?.Dispose();
        courseDisplacementPresenter?.Dispose();

        stateMachine?.Dispose();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            scrollBackgroundPresenter.ActivateScroll();
            platformPresenter.DeactivatePlatform();
        }

        if (Input.GetKeyUp(KeyCode.Z))
        {
            scrollBackgroundPresenter.DeactivateScroll();
            platformPresenter.ActivatePlatform();
        }
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
