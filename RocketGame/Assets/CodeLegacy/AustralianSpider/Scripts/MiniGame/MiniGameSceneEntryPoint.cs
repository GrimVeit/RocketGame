using System;
using Unity.VisualScripting;
using UnityEngine;

public class MiniGameSceneEntryPoint : MonoBehaviour
{
    [SerializeField] private Sounds sounds;
    [SerializeField] private StrategyGroup strategyGroup;
    [SerializeField] private ChipGroup chipGroup;
    [SerializeField] private WinPrices winPrices;
    [SerializeField] private TutorialDescriptionGroup tutorialDescriptionGroup;
    [SerializeField] private UIMiniGameSceneRoot sceneRootPrefab;

    private UIMiniGameSceneRoot sceneRoot;
    private ViewContainer viewContainer;
    private BankPresenter bankPresenter;
    private SoundPresenter soundPresenter;
    private ParticleEffectPresenter particleEffectPresenter;

    private ScrollBackgroundPresenter scrollBackgroundPresenter;
    private RocketControlPresenter rocketControlPresenter;
    private PlatformPresenter platformPresenter;

    private StoreBetPresenter storeBetPresenter;
    private BetSelectPresenter betSelectPresenter;

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
        rocketControlPresenter = new RocketControlPresenter(new RocketControlModel(), viewContainer.GetView<RocketControlView>());
        platformPresenter = new PlatformPresenter(new  PlatformModel(), viewContainer.GetView<PlatformView>());

        storeBetPresenter = new StoreBetPresenter(new StoreBetModel(PlayerPrefsKeys.BET, 2.4f));
        betSelectPresenter = new BetSelectPresenter(new BetSelectModel(), viewContainer.GetView<BetSelectView>());

        sceneRoot.SetSoundProvider(soundPresenter);
        sceneRoot.Activate();

        ActivateEvents();

        sceneRoot.Initialize();

        bankPresenter.Initialize();
        soundPresenter.Initialize();
        particleEffectPresenter.Initialize();

        scrollBackgroundPresenter.Initialize();
        rocketControlPresenter.Initialize();
        platformPresenter.Initialize();

        betSelectPresenter.Initialize();
        storeBetPresenter.Initialize();

    }

    private void ActivateEvents()
    {
        ActivateTransitionsSceneEvents();

        storeBetPresenter.OnChooseBet += betSelectPresenter.SetBet;

        betSelectPresenter.OnIncreaseBet += storeBetPresenter.IncreaseBet;
        betSelectPresenter.OnDecreaseBet += storeBetPresenter.DecreaseBet;

    }

    private void DeactivateEvents()
    {
        DeactivateTransitionsSceneEvents();

        storeBetPresenter.OnChooseBet -= betSelectPresenter.SetBet;

        betSelectPresenter.OnIncreaseBet -= storeBetPresenter.IncreaseBet;
        betSelectPresenter.OnDecreaseBet -= storeBetPresenter.DecreaseBet;
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
        rocketControlPresenter?.Dispose();
        platformPresenter.Dispose();

        betSelectPresenter?.Dispose();
        storeBetPresenter?.Dispose();
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
