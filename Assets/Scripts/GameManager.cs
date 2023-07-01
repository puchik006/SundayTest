using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameConfig _gameConfig;
    [SerializeField] private LoadingScreen _loadingScreenView;
    private SceneLoader _sceneLoader;
    private MobileNativeFunctions _backFunctions;
    private ScreenOrientationHandler _orientationHandler;
    private PrefabImageLoader _prefabImageLoader;
    private PrefabInstantiator _prefabInstantiator;
    private ScrollViewHandler _scrollViewHandler;

    private void Awake()
    {
        _sceneLoader = new SceneLoader(_loadingScreenView,_gameConfig);
        _backFunctions = new MobileNativeFunctions();
        _orientationHandler = new ScreenOrientationHandler();
        _prefabImageLoader = new PrefabImageLoader(_gameConfig);
        _prefabInstantiator = new PrefabInstantiator(_prefabImageLoader);
        _scrollViewHandler = new ScrollViewHandler(_prefabInstantiator);
    }

    private void Update()
    {
        _backFunctions.HandleMobileBackButtonPress();
    }
}
