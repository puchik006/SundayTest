using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ProgressBarView _progressBarView;
    private ProgressBarController _progressBarController;

    [SerializeField] private LoadingScreenView _loadingScreenView;
    private LoadingScreenController _loadingScreenController;



    private void Start()
    {
       // _progressBarController = new ProgressBarController(_progressBarView,3);
       _loadingScreenController = new LoadingScreenController(_loadingScreenView);
    }
}
