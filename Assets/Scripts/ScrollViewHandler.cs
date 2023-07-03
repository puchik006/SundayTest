using UnityEngine;
using UnityEngine.UI;

public class ScrollViewHandler
{
    private PrefabInstantiator _prefabInstantiator;
    private IGalleryView _galleryView;
    private float _prefabHeight;
    private float _enlargeContentTriggerHeight;
    private int _numberOfColumns;
    private int _picCounter;
    private const int START_COUNTER_INDEX = 1;
    private bool _isAllowToEnlargeContent = true;

    public ScrollViewHandler(PrefabInstantiator prefabInstantiator)
    {
        _prefabInstantiator = prefabInstantiator;

        GalleryView.OnGalleryViewAwake += GetGalleryViewData;
        GalleryView.OnScroll += EnlargeContentOnScrolling;
        PrefabImageLoader.OnError += () => _isAllowToEnlargeContent = false;
    }

    private void GetGalleryViewData(IGalleryView galleryView)
    {
        _galleryView = galleryView;

        _prefabHeight = _galleryView.Prefab.GetComponent<RectTransform>().rect.height;
        _enlargeContentTriggerHeight = _galleryView.Content.localPosition.y;
        _numberOfColumns = _galleryView.Content.GetComponent<GridLayoutGroup>().constraintCount;

        _isAllowToEnlargeContent = true;
        _picCounter = START_COUNTER_INDEX;

        CreateInitialRows();
    }

    private void CreateInitialRows()
    {
        float numberOfRows = _galleryView.ViewPort.rect.height / _prefabHeight;

        for (int i = 1; i < numberOfRows * _numberOfColumns; i++)
        {
            _prefabInstantiator.LoadAndInstantiatePrefab(i);
            _picCounter = i;
        }
    }

    private void EnlargeContentOnScrolling()
    {
        if (!_isAllowToEnlargeContent) return;

        if (_galleryView.Content.localPosition.y > _enlargeContentTriggerHeight)
        {
            _enlargeContentTriggerHeight += _prefabHeight;
            _picCounter += _numberOfColumns;

            for (int i = _picCounter - _numberOfColumns; i < _picCounter; i++)
            {
                _prefabInstantiator.LoadAndInstantiatePrefab(i);
            }
        }
    }
}

