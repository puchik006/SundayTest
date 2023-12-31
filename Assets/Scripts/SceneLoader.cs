using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader
{
    public static Action<SceneIndexes> OnSceneLoaded;
    private LoadingScreen _loadingScreenView;
    private Timer _timer;
    private List<AsyncOperation> _scenesLoading = new List<AsyncOperation>();
    private Stack<int> _scenes = new Stack<int>();
    private Action _loadSceneCallback;
    private int _timeToLoadScene;
    private const int SCENE_STEP_BACK = 1;

    public SceneLoader(LoadingScreen loadingScreenView, GameConfig gameConfig)
    {
        _loadingScreenView = loadingScreenView;
        _timer = new Timer(_loadingScreenView);
        _timeToLoadScene = gameConfig.TimeToLoad;

        SceneManager.LoadSceneAsync((int)SceneIndexes.Main, LoadSceneMode.Additive);
        _scenes.Push((int)SceneIndexes.Main);

        MainSceneButtonHandler.OnButtonPressed += () => StartLoadingScene(SceneIndexes.Gallery);
        PrefabInstantiator.OnButtonPressed += () => StartLoadingScene(SceneIndexes.FullPaigeView);
        ExitButtonHandler.OnButtonPressed += () => StartLoadingScene(SceneIndexes.Gallery);
        MobileNativeFunctions.GoBack += () => StartLoadingScene(_scenes.Peek() > (int)SceneIndexes.Main ? (SceneIndexes)_scenes.Peek() - SCENE_STEP_BACK : SceneIndexes.Main);
    }

    private void StartLoadingScene(SceneIndexes sceneIndex)
    {
        _loadingScreenView.TurnScreenOn();
        _timer.Set(_timeToLoadScene);
        _timer.StartCountingTime();
        _timer.OnHasBeenUpdated += _loadingScreenView.ShowLoadingProgress;
        _loadSceneCallback = () => LoadScene(sceneIndex);
        _timer.OnTimeIsOver += OnTimeIsOverHandler;
        _scenesLoading.Add(SceneManager.UnloadSceneAsync(_scenes.Pop()));
    }

    private void OnTimeIsOverHandler()
    {
        _loadSceneCallback?.Invoke(); 
    }

    private void LoadScene(SceneIndexes sceneIndex)
    {
        _scenesLoading.Add(SceneManager.LoadSceneAsync((int)sceneIndex, LoadSceneMode.Additive));
        _scenes.Push((int)sceneIndex);
        _loadingScreenView.StartCoroutine(GetSceneLoadProgress());
        OnSceneLoaded?.Invoke(sceneIndex);
        _timer.OnHasBeenUpdated -= _loadingScreenView.ShowLoadingProgress;
        _timer.OnTimeIsOver -= OnTimeIsOverHandler;
    }

    private IEnumerator GetSceneLoadProgress()
    {
        yield return new WaitUntil(() => _scenesLoading.TrueForAll(scene => scene.isDone));
        _scenesLoading.Clear();
        _loadingScreenView.TurnScreenOff();
    }
}
