using System;
using Unity.VisualScripting;
using UnityEngine;

public class MiniGameSceneEntryPoint : MonoBehaviour
{
    [SerializeField] private Sounds sounds;
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
        obstacleRocketMovePresenter?.Dispose();

        altitudePresenter?.Dispose();
        courseDisplacementPresenter?.Dispose();
        scoreMultiplierPresenter?.Dispose();
        scorePresenter?.Dispose();

        animationFramePresenter?.Dispose();
        obstacleEffectPresenter?.Dispose();
        obstacleSoundPresenter?.Dispose();

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
