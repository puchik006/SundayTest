using System;
using UnityEngine;

public class ProgressBarController
{
    private ProgressBarView _progressBarView;

    public ProgressBarController(ProgressBarView progressBarView, Action<float> progress)
    {
        _progressBarView = progressBarView;
        progress += _progressBarView.ShowLoadingProgress;
    }
}
