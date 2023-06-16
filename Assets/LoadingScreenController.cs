public class LoadingScreenController
{
    private LoadingScreenView _loadingScreenView;

    public LoadingScreenController(LoadingScreenView loadingScreenView)
    {
        _loadingScreenView = loadingScreenView;

        SceneLoader.OnSceneStartLoading += _loadingScreenView.TurnScreenOn;
        SceneLoader.OnSceneStopLoading += _loadingScreenView.TurnScreenOff;
    }
}
