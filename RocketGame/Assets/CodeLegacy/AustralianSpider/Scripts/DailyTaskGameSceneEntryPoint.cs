using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyTaskGameSceneEntryPoint : MonoBehaviour
{
    [SerializeField] private Sounds sounds;
    [SerializeField] private GameDesignGroup gameDesignGroup;
    [SerializeField] private CoverCardDesignGroup coverCardDesignGroup;
    [SerializeField] private FaceCardDesignGroup faceCardDesignGroup;
    [SerializeField] private GameTypeGroup gameTypeGroup;
    [SerializeField] private UIDailyTaskGameSceneRoot sceneRootPrefab;

    private UIDailyTaskGameSceneRoot sceneRoot;
    private ViewContainer viewContainer;
    private SoundPresenter soundPresenter;
    private ParticleEffectPresenter particleEffectPresenter;
    private BankPresenter bankPresenter;

    public void Run(UIRootView uIRootView)
    {
        sceneRoot = sceneRootPrefab;

        uIRootView.AttachSceneUI(sceneRoot.gameObject, Camera.main);

        sceneRoot.Activate();

        viewContainer = sceneRoot.GetComponent<ViewContainer>();
        viewContainer.Initialize();

        soundPresenter = new SoundPresenter(new SoundModel(sounds.sounds, PlayerPrefsKeys.IS_MUTE_SOUNDS), viewContainer.GetView<SoundView>());
        soundPresenter.Initialize();

        sceneRoot.SetSoundProvider(soundPresenter);
        sceneRoot.Initialize();

        particleEffectPresenter = new ParticleEffectPresenter(new ParticleEffectModel(), viewContainer.GetView<ParticleEffectView>());
        particleEffectPresenter.Initialize();

        bankPresenter = new BankPresenter(new BankModel(), viewContainer.GetView<BankView>());
        bankPresenter.Initialize();

        ActivateEvents();
    }

    private void ActivateEvents()
    {
        ActivateTransitionEvents();
    }

    private void DeactivateEvents()
    {
        DeactivateTransitionEvents();
    }

    private void ActivateTransitionEvents()
    {
        sceneRoot.OnClickToExit += HandleGoToMainMenu;
    }

    private void DeactivateTransitionEvents()
    {
        sceneRoot.OnClickToExit -= HandleGoToMainMenu;
    }

    public void Dispose()
    {
        DeactivateEvents();

        sceneRoot?.Dispose();
        soundPresenter?.Dispose();
        bankPresenter?.Dispose();
    }

    private void OnDestroy()
    {
        Dispose();
    }

    #region Input

    public event Action OnGoToMainMenu;

    private void HandleGoToMainMenu()
    {
        sceneRoot.Deactivate();
        soundPresenter.Dispose();
        OnGoToMainMenu?.Invoke();
    }

    #endregion
}
