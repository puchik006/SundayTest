using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private LoadingScreen _loadingScreenView;
    private SceneLoader _sceneLoader;
    private PrefabImageLoader _pictureLoaderPrefabImageLoader;

    private void Awake()
    {
        _sceneLoader = new SceneLoader(_loadingScreenView);
        _pictureLoaderPrefabImageLoader = new PrefabImageLoader();
    }
}
