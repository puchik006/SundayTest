using UnityEngine;

public class LoadingScreenView
{
    [SerializeField] private GameObject _loadingScreen;

    public void TurnScreenOn()
    {
        _loadingScreen.SetActive(true);
    }

    public void TurnScreenOff()
    {
        _loadingScreen.SetActive(false);
    }
}
