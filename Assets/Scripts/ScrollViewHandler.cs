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

    private int _picCounter;

    public ScrollViewHandler(PrefabInstantiator prefabInstantiator)
    {
        _prefabInstantiator = prefabInstantiator;

        GalleryView.OnAwake += GetGalleryViewData;
        GalleryView.OnScroll += EnlargeContentOnScrolling;
        PrefabImageLoader.OnError += StopEnlargingContent;
    }

    private void GetGalleryViewData(GalleryView galleryView)
    {
        _content = galleryView.Content;
        _viewPort = galleryView.ViewPort;
        _prefab = galleryView.Prefab;

        _prefabHeight = _prefab.GetComponent<RectTransform>().rect.height;
        _enlargeContentTriggerHeight = _content.localPosition.y;

        _isAllowToEnlargeContent = true;

        CreateInitialRows();
    }

    private void CreateInitialRows()
    {
        float numberOfRows = _viewPort.rect.height / _prefabHeight;

        for (int i = 1; i < numberOfRows * 2; i++) // 2 replace to number of columns
        {
            _prefabInstantiator.LoadAndInstantiatePrefab(i);
            _picCounter = i;
        }
    }

    private void EnlargeContentOnScrolling()
    {
        if (!_isAllowToEnlargeContent) return;

        int previousLoadedPicturesNumber = _picCounter;

        if (_content.localPosition.y > _enlargeContentTriggerHeight)
        {
            _enlargeContentTriggerHeight += _prefabHeight;
            _picCounter += 2;

            for (int i = previousLoadedPicturesNumber; i < _picCounter; i++)
            {
                _prefabInstantiator.LoadAndInstantiatePrefab(i);
            }
        }
    }

    private void StopEnlargingContent()
    {
        _isAllowToEnlargeContent = false;
    }
}

