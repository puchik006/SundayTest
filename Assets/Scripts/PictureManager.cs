using UnityEngine;

public class PictureManager: MonoBehaviour
{
    [SerializeField] private GalleryView _galleryView;
    private ScrollViewHandler _scrollViewHandler;
    
    private void Awake()
    {
        _scrollViewHandler = new ScrollViewHandler(_galleryView);
    }
}
