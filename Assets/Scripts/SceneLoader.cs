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

    public SceneLoader(LoadingScreen loadingScreenView)
    {
        _loadingScreenView = loadingScreenView;
        _timer = new Timer(_loadingScreenView);

        SceneManager.LoadSceneAsync((int)SceneIndexes.Main, LoadSceneMode.Additive);
        _scenes.Push((int)SceneIndexes.Main);

        MainSceneButtonHandler.OnButtonPressed += () => StartLoadingScene(SceneIndexes.Gallery);
        ScrollViewHandler.OnButtonPressed += () => StartLoadingScene(SceneIndexes.FullPaigeView);
        ExitButtonHandler.OnButtonPressed += () => StartLoadingScene(SceneIndexes.Gallery);
    }

    private void StartLoadingScene(SceneIndexes sceneIndex)
    {
        _loadingScreenView.TurnScreenOn();
        _timer.Set(2);
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
