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

        sceneRoot.SetSoundProvider(soundPresenter);
        sceneRoot.Activate();

        ActivateEvents();

        sceneRoot.Initialize();

        bankPresenter.Initialize();
        soundPresenter.Initialize();
        particleEffectPresenter.Initialize();

        scrollBackgroundPresenter.Initialize();
        rocketControlPresenter.Initialize();

    }

    private void ActivateEvents()
    {
        ActivateTransitionsSceneEvents();

    }

    private void DeactivateEvents()
    {
        DeactivateTransitionsSceneEvents();

    }

    private void ActivateTransitionsSceneEvents()
    {
        sceneRoot.OnClickToExit += HandleGoToMenu;
    }

    private void DeactivateTransitionsSceneEvents()
    {
        sceneRoot.OnClickToExit -= HandleGoToMenu;
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
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            scrollBackgroundPresenter.ActivateScroll();
        }

        if (Input.GetKeyUp(KeyCode.Z))
        {
            scrollBackgroundPresenter.DeactivateScroll();
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
