using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader 
{
    //[SerializeField] private GameObject _loadingScreen;
    //[SerializeField] private Slider _loadingSlider;

    private MonoBehaviour _context;

    private Timer _timer;

    public static Action OnSceneStartLoading;
    public static Action OnSceneStopLoading;

    public SceneLoader(MonoBehaviour context, Timer timer)
    {
        //_timer = new Timer(gameObject.GetComponent<MonoBehaviour>());
        _timer = new Timer(_context.GetComponent<MonoBehaviour>());
        _timer.Set(3);
        _timer.OnTimeIsOver += LoadScene;
        //_timer.OnHasBeenUpdated += ShowLoadingProgress;

        MainSceneButtonHandler.OnButtonPressed += StartLoadingScene;

        SceneManager.LoadSceneAsync((int)SceneIndexes.Main, LoadSceneMode.Additive);
    }

    public void StartLoadingScene()
    {
        _timer.StartCountingTime();
        OnSceneStartLoading?.Invoke();
        //_loadingScreen.SetActive(true);
    }

    //private void ShowLoadingProgress(float progress)
    //{
    //    _loadingSlider.value = progress;
    //}

    List<AsyncOperation> _scenesLoading = new List<AsyncOperation>();

    private void LoadScene()
    {
       _scenesLoading.Add(SceneManager.UnloadSceneAsync((int)SceneIndexes.Main));
       _scenesLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndexes.Gallery, LoadSceneMode.Additive));

        StartCoroutine(GetSceneLoadProgress());
    }

    private IEnumerator GetSceneLoadProgress()
    {
        for (int i = 0; i < _scenesLoading.Count; i++)
        {
            while (!_scenesLoading[i].isDone)
            {
                yield return null;
            }
        }

        OnSceneStopLoading?.Invoke();

        //_loadingScreen.SetActive(false);
    }
}
