using UnityEngine;

public class PictureManager: MonoBehaviour
{
    [SerializeField] private GalleryView _galleryView;
    private ScrollViewHandler _scrollViewHandler;
    private PrefabImageLoader _pictureLoaderPrefabImageLoader;

    private void Awake()
    {
        _pictureLoaderPrefabImageLoader = new PrefabImageLoader();
        _scrollViewHandler = new ScrollViewHandler(_galleryView);
    }
}
