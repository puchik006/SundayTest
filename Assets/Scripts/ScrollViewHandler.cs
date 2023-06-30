using UnityEngine;

public class ScrollViewHandler
{
    private PrefabInstantiator _prefabInstantiator;

    private RectTransform _content;
    private RectTransform _viewPort;
    private GameObject _prefab;

    private float _prefabHeight;
    private float _enlargeContentTriggerHeight;
    private bool _isAllowToEnlargeContent = true;

    public ScrollViewHandler()
    {
        GalleryView.OnAwake += GetGalleryViewData;
        GalleryView.OnScroll += EnlargeContentOnScrolling;
        PrefabImageLoader.OnError += StopEnlargingContent;
    }

    private void GetGalleryViewData(GalleryView galleryView)
    {
        _content = galleryView.Content;
        _viewPort = galleryView.ViewPort;
        _prefab = galleryView.Prefab;

        _prefabInstantiator = new PrefabInstantiator(_prefab, _content);
        _prefabHeight = _prefab.GetComponent<RectTransform>().rect.height;
        _enlargeContentTriggerHeight = _prefabHeight/3;

        _isAllowToEnlargeContent = true;

        CreateInitialRows();
    }

    private async void CreateInitialRows()
    {
        float numberOfRows = _viewPort.rect.height / _prefabHeight;

        for (int i = 1; i < numberOfRows * 2; i++)
        {
            await _prefabInstantiator.LoadAndInstantiatePrefabAsync();
        }
    }

    private async void EnlargeContentOnScrolling()
    {
        if (!_isAllowToEnlargeContent) return;

        if (_content.localPosition.y > _enlargeContentTriggerHeight)
        {
            _enlargeContentTriggerHeight += _prefabHeight/10;

            await _prefabInstantiator.LoadAndInstantiatePrefabAsync();
        }
    }

    private void StopEnlargingContent(int imageNumber)
    {
        _isAllowToEnlargeContent = false;
    }
}

public class ImageHolder
{
    public static Sprite _tempSprite;
}