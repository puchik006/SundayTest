using System;
using UnityEngine;

public class LoadingScreenView: MonoBehaviour
{
    [SerializeField] private GameObject _loadingScreen;

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
}
