using System;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using UnityEngine;

public class MenuEntryPoint : MonoBehaviour
{
    [SerializeField] private Sounds sounds;
    [SerializeField] private UIMenuRoot uiMenuRootPrefab;

    private UIMenuRoot sceneRoot;
    private ViewContainer viewContainer;

    private MoneyPresenter bankPresenter;
    private ParticlePresenter particleEffectPresenter;
    private SoundPresenter soundPresenter;

    private MenuGlobalStateMachine stateMachine;

    private InternetPresenter internetPresenter;
    private FirebaseAuthenticationPresenter firebaseAuthenticationPresenter;
    private FirebaseDatabasePresenter firebaseDatabaseRealtimePresenter;
    private NicknameRandomPresenter nicknameRandomPresenter;

    public void Run(UIRootView uIRootView)
    {
        sceneRoot = uiMenuRootPrefab;
 

        uIRootView.AttachSceneUI(sceneRoot.gameObject, Camera.main);


        viewContainer = sceneRoot.GetComponent<ViewContainer>();
        viewContainer.Initialize();

        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            var dependencyStatus = task.Result;

            if (dependencyStatus == DependencyStatus.Available)
            {
                Debug.Log("1");

                FirebaseDatabase.DefaultInstance.SetPersistenceEnabled(false);
                FirebaseAuth firebaseAuth = FirebaseAuth.DefaultInstance;
                DatabaseReference databaseReference = FirebaseDatabase.DefaultInstance.RootReference;

                soundPresenter = new SoundPresenter
                    (new SoundModel(sounds.sounds, PrefsKeys.IS_MUTE_SOUNDS),
                    viewContainer.GetView<SoundView>());

                particleEffectPresenter = new ParticlePresenter
                    (new ParticleModel(),
                    viewContainer.GetView<ParticleView>());

                bankPresenter = new MoneyPresenter(new MoneyModel(), viewContainer.GetView<MoneyView>());

                internetPresenter = new InternetPresenter(new InternetModel(), viewContainer.GetView<InternetView>());

                firebaseAuthenticationPresenter = new FirebaseAuthenticationPresenter
                    (new FirebaseAuthenticationModel(firebaseAuth, soundPresenter, particleEffectPresenter),
                viewContainer.GetView<FirebaseAuthenticationView>());

                firebaseDatabaseRealtimePresenter = new FirebaseDatabasePresenter
                (new FirebaseDatabaseModel(firebaseAuth, databaseReference, soundPresenter),
                    viewContainer.GetView<FirebaseDatabaseView>());

                nicknameRandomPresenter = new NicknameRandomPresenter(new NicknameRandomModel());

                stateMachine = new MenuGlobalStateMachine(internetPresenter, firebaseAuthenticationPresenter, firebaseDatabaseRealtimePresenter, nicknameRandomPresenter, sceneRoot);

                Debug.Log("CHECK");

                sceneRoot.SetSoundProvider(soundPresenter);
                sceneRoot.Activate();

                ActivateEvents();

                soundPresenter.Initialize();
                particleEffectPresenter.Initialize();
                sceneRoot.Initialize();
                bankPresenter.Initialize();

                internetPresenter.Initialize();
                firebaseAuthenticationPresenter.Initialize();
                firebaseDatabaseRealtimePresenter.Initialize();

                stateMachine.Initialize();
            }
            else
            {
                Debug.LogError(string.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });
    }

    private void ActivateEvents()
    {
        ActivateTransitions();
    }

    private void DeactivateEvents()
    {
        DeactivateTransitions();
    }

    private void ActivateTransitions()
    {
        sceneRoot.OnClickToPlay += HandleGoToGame;
    }

    private void DeactivateTransitions()
    {
        sceneRoot.OnClickToPlay -= HandleGoToGame;
    }

    private void Deactivate()
    {
        sceneRoot.Deactivate();
        soundPresenter?.Dispose();
    }

    private void Dispose()
    {
        DeactivateEvents();

        soundPresenter?.Dispose();
        sceneRoot?.Dispose();
        particleEffectPresenter?.Dispose();
        bankPresenter?.Dispose();

        internetPresenter?.Dispose();
        firebaseAuthenticationPresenter?.Dispose();
        firebaseDatabaseRealtimePresenter?.Dispose();

        stateMachine.Dispose();
    }

    private void OnDestroy()
    {
        Dispose();
    }

    #region InputActions

    public event Action OnGoToGame;

    private void HandleGoToGame()
    {
        Deactivate();
        OnGoToGame?.Invoke();
    }

    #endregion
}
