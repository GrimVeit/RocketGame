using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProjectEntryPoint
{
    private readonly Coroutines coroutines;
    private readonly UIRootView rootView;
    private static ProjectEntryPoint instance;
    public ProjectEntryPoint()
    {
        coroutines = new GameObject("[Coroutines]").AddComponent<Coroutines>();
        Object.DontDestroyOnLoad(coroutines.gameObject);

        var prefabUIRoot = Resources.Load<UIRootView>("UIRootView");
        rootView = Object.Instantiate(prefabUIRoot);
        Object.DontDestroyOnLoad(rootView.gameObject);

    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Auto()
    {
        GlobalSettings();


        instance = new ProjectEntryPoint();
        instance.Run();

    }

    private static void GlobalSettings()
    {
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    private void Run()
    {
        coroutines.StartCoroutine(LoadAndStartMenu());
    }

    private IEnumerator LoadAndStartMenu()
    {
        rootView.SetLoadScreen(0);

        yield return rootView.ShowLoadingScreen();

        yield return LoadSceneCoroutine(Scenes.BOOT);
        yield return LoadSceneCoroutine(Scenes.MENU_SCENE);

        var sceneEntryPoint = Object.FindObjectOfType<MenuEntryPoint>();
        sceneEntryPoint.Run(rootView);

        sceneEntryPoint.OnGoToGame += () => coroutines.StartCoroutine(LoadAndStartGame());

        yield return rootView.HideLoadingScreen();
    }

    private IEnumerator LoadAndStartGame()
    {
        rootView.SetLoadScreen(1);

        yield return rootView.ShowLoadingScreen();

        yield return new WaitForSeconds(0.3f);

        yield return LoadSceneCoroutine(Scenes.BOOT);
        yield return LoadSceneCoroutine(Scenes.GAME_SCENE);

        yield return new WaitForSeconds(0.1f);

        var sceneEntryPoint = Object.FindObjectOfType<GameEntryPoint>();
        sceneEntryPoint.Run(rootView);

        sceneEntryPoint.OnGoToMenu += () => coroutines.StartCoroutine(LoadAndStartMenu());


        yield return rootView.HideLoadingScreen();
    }

    private IEnumerator LoadSceneCoroutine(string scene)
    {
        Debug.Log("Загрузка сцены с названием - " + scene);
        yield return SceneManager.LoadSceneAsync(scene);
    }
}
