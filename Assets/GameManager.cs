using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ProgressBarView _progressBarView;
    [SerializeField] private LoadingScreenView _loadingScreenView;

    private SceneLoader _sceneLoader;

    private void Awake()
    {
        _sceneLoader = new SceneLoader(this,_progressBarView,_loadingScreenView);
    }
}
