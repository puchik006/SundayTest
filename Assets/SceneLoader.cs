using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader
{
    LoadingScreenView _loadingScreenView;
    ProgressBarView _progressBarView;
    
    private MonoBehaviour _context;
    private Timer _timer;

    List<AsyncOperation> _scenesLoading = new List<AsyncOperation>();

    public SceneLoader(MonoBehaviour context, ProgressBarView progressBarView, LoadingScreenView loadingScreenView)
    {
        _context = context;
        _progressBarView = progressBarView;
        _loadingScreenView = loadingScreenView;
        _timer = new Timer(_context);

        SceneManager.LoadSceneAsync((int)SceneIndexes.Main, LoadSceneMode.Additive);

        MainSceneButtonHandler.OnButtonPressed += StartLoadingGalleryScene;

    }

    public void StartLoadingGalleryScene()
    {
        _loadingScreenView.TurnScreenOn();

        _timer.Set(3);
        _timer.StartCountingTime();
        _timer.OnHasBeenUpdated += _progressBarView.ShowLoadingProgress;
        _timer.OnTimeIsOver += LoadGalleryScene;
    }

    private void LoadGalleryScene()
    {
        _scenesLoading.Add(SceneManager.UnloadSceneAsync((int)SceneIndexes.Main));
        _scenesLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndexes.Gallery, LoadSceneMode.Additive));
        _progressBarView.StartCoroutine(GetSceneLoadProgress());
    }

    private IEnumerator GetSceneLoadProgress()
    {
        yield return new WaitUntil(() => _scenesLoading.All(scene => scene.isDone));
        _scenesLoading.Clear();

        _loadingScreenView.TurnScreenOff();
    }
}