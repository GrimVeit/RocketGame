using System;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using UnityEngine;

public class MainMenuEntryPoint : MonoBehaviour
{
    [SerializeField] private Sounds sounds;
    [SerializeField] private UIMainMenuRoot menuRootPrefab;

    private UIMainMenuRoot sceneRoot;
    private ViewContainer viewContainer;

    private BankPresenter bankPresenter;
    private ParticleEffectPresenter particleEffectPresenter;
    private SoundPresenter soundPresenter;

    private FirebaseAuthenticationPresenter firebaseAuthenticationPresenter;
    private FirebaseDatabaseRealtimePresenter firebaseDatabaseRealtimePresenter;

    public void Run(UIRootView uIRootView)
    {
        sceneRoot = menuRootPrefab;
 
        uIRootView.AttachSceneUI(sceneRoot.gameObject, Camera.main);

        viewContainer = sceneRoot.GetComponent<ViewContainer>();
        viewContainer.Initialize();

        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            var dependencyStatus = task.Result;

            if (dependencyStatus == DependencyStatus.Available)
            {
                FirebaseDatabase.DefaultInstance.SetPersistenceEnabled(false);
                FirebaseAuth firebaseAuth = FirebaseAuth.DefaultInstance;
                DatabaseReference databaseReference = FirebaseDatabase.DefaultInstance.RootReference;

                soundPresenter = new SoundPresenter
                    (new SoundModel(sounds.sounds, PlayerPrefsKeys.IS_MUTE_SOUNDS),
                    viewContainer.GetView<SoundView>());

                particleEffectPresenter = new ParticleEffectPresenter
                    (new ParticleEffectModel(),
                    viewContainer.GetView<ParticleEffectView>());

                bankPresenter = new BankPresenter(new BankModel(), viewContainer.GetView<BankView>());

                firebaseAuthenticationPresenter = new FirebaseAuthenticationPresenter
                    (new FirebaseAuthenticationModel(firebaseAuth, soundPresenter, particleEffectPresenter),
                viewContainer.GetView<FirebaseAuthenticationView>());

                firebaseDatabaseRealtimePresenter = new FirebaseDatabaseRealtimePresenter
                (new FirebaseDatabaseRealtimeModel(firebaseAuth, databaseReference, soundPresenter),
                    viewContainer.GetView<FirebaseDatabaseRealtimeView>());

                sceneRoot.SetSoundProvider(soundPresenter);
                sceneRoot.Activate();

                ActivateEvents();

                soundPresenter.Initialize();
                particleEffectPresenter.Initialize();
                sceneRoot.Initialize();
                bankPresenter.Initialize();

                firebaseAuthenticationPresenter.Initialize();
                firebaseDatabaseRealtimePresenter.Initialize();

                if (firebaseAuthenticationPresenter.CheckAuthenticated())
                {
                    Debug.Log("AUTH");
                }
                else
                {
                    Debug.Log("NONE");
                }
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
        sceneRoot.OnClickToLeaderboard += sceneRoot.OpenLeaderboardPanel;
        sceneRoot.OnClickToCancel += sceneRoot.OpenMainPanel;

        sceneRoot.OnClickToPlay += HandleGoToGame;
    }

    private void DeactivateTransitions()
    {
        sceneRoot.OnClickToLeaderboard -= sceneRoot.OpenLeaderboardPanel;
        sceneRoot.OnClickToCancel -= sceneRoot.OpenMainPanel;

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

        firebaseAuthenticationPresenter.Dispose();
        firebaseDatabaseRealtimePresenter.Dispose();
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
