using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader
{
    private LoadingScreen _loadingScreenView;
    private Timer _timer;
    private List<AsyncOperation> _scenesLoading = new List<AsyncOperation>();

    public SceneLoader(LoadingScreen loadingScreenView)
    {
        _loadingScreenView = loadingScreenView;
        _timer = new Timer(_loadingScreenView);

        SceneManager.LoadSceneAsync((int)SceneIndexes.Main, LoadSceneMode.Additive);

        MainSceneButtonHandler.OnButtonPressed += StartLoadingGalleryScene;
    }

    public void StartLoadingGalleryScene()
    {
        _loadingScreenView.TurnScreenOn();

        _timer.Set(3); //TODO: dont forget to change for some variable
        _timer.StartCountingTime();
        _timer.OnHasBeenUpdated += _loadingScreenView.ShowLoadingProgress;
        _timer.OnTimeIsOver += LoadGalleryScene;
    }

    private void LoadGalleryScene()
    {
        _scenesLoading.Add(SceneManager.UnloadSceneAsync((int)SceneIndexes.Main));
        _scenesLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndexes.Gallery, LoadSceneMode.Additive));
        _loadingScreenView.StartCoroutine(GetSceneLoadProgress());
    }

    private IEnumerator GetSceneLoadProgress()
    {
        yield return new WaitUntil(() => _scenesLoading.All(scene => scene.isDone));
        _scenesLoading.Clear();

        _loadingScreenView.TurnScreenOff();
    }
}