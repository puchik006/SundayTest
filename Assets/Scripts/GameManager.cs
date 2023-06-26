using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private LoadingScreen _loadingScreenView;
    private SceneLoader _sceneLoader;
    private ScrollViewHandler _scrollViewHandler;

    private void Awake()
    {
        _sceneLoader = new SceneLoader(_loadingScreenView);
        _scrollViewHandler = new ScrollViewHandler();
    }
}
