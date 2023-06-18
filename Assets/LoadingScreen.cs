using System;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen: MonoBehaviour
{
    [SerializeField] private GameObject _loadingScreen;
    [SerializeField] private Slider _loadingSlider;

    public static Action OnSceneStartLoading;
    public static Action OnSceneStopLoading;

    public void TurnScreenOn()
    {
        _loadingScreen.SetActive(true);
        OnSceneStartLoading?.Invoke();
    }

    public void TurnScreenOff()
    {
        _loadingScreen.SetActive(false);
        OnSceneStopLoading?.Invoke();
    }

    public void ShowLoadingProgress(float progress)
    {
        _loadingSlider.value = progress;
    }
}
