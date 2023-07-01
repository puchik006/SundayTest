using System.Collections.Generic;
using UnityEngine;

public class ScreenOrientationHandler
{
    private Dictionary<SceneIndexes, ScreenOrientation> _screenOrientation = new Dictionary<SceneIndexes, ScreenOrientation>()
    {
        {SceneIndexes.Main,ScreenOrientation.Portrait},
        {SceneIndexes.Gallery,ScreenOrientation.Portrait},
        {SceneIndexes.FullPaigeView,ScreenOrientation.AutoRotation}
    };

    public ScreenOrientationHandler()
    {
        SceneLoader.OnSceneLoaded += SetScreenOrientation;
    }

    public void Dispose()
    {
        SceneLoader.OnSceneLoaded -= SetScreenOrientation;
    }

    private void SetScreenOrientation(SceneIndexes sceneIndex)
    {
        Screen.orientation = _screenOrientation[sceneIndex];
    }
}
