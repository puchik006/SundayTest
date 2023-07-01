using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader
{
    private LoadingScreen _loadingScreenView;
    private Timer _timer;
    private List<AsyncOperation> _scenesLoading = new List<AsyncOperation>();
    private Stack<int> _scenes = new Stack<int>();
    private Action _loadSceneCallback;
    public static Action<SceneIndexes> OnSceneLoaded;
    private int _timeToLoadScene;

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
    }

    private void StartLoadingScene(SceneIndexes sceneIndex)
    {
        _loadingScreenView.TurnScreenOn();
        _timer.Set(_timeToLoadScene);
        _timer.StartCountingTime();
        _timer.OnHasBeenUpdated += _loadingScreenView.ShowLoadingProgress;
        _loadSceneCallback = () => LoadScene(sceneIndex);
        _timer.OnTimeIsOver += OnTimeIsOverHandler;
    }

    private void OnTimeIsOverHandler()
    {
        _loadSceneCallback?.Invoke(); 
    }

    private void LoadScene(SceneIndexes sceneIndex)
    {
        _scenesLoading.Add(SceneManager.UnloadSceneAsync(_scenes.Pop()));
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
