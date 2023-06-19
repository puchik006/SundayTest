using UnityEngine;

public class PictureManager: MonoBehaviour
{
    [SerializeField] private GalleryView _galleryView;
    private PictureLoader _pictureLoader;

    private void Awake()
    {
        _pictureLoader = new PictureLoader(ref _galleryView);
    }
}
