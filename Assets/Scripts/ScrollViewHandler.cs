using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class ScrollViewHandler
{
    private RectTransform _content;
    private RectTransform _viewPort;
    private GameObject _prefab;
    private List<GameObject> _prefabsList = new List<GameObject>();

    private float _prefabHeigth;
    private float _contentSpacingHalfHeight;
    private float _enlargeContentTriggerHeight;
    private bool _isAllowToEnlargeContent = true;

    public static Action<GameObject,int> OnStringCreated;

    private LinkImageChecker _linkImageChecker;

    public ScrollViewHandler()
    {
        GalleryView.OnAwake += GetGalleryViewData;
        GalleryView.OnScroll += EnlargeContentOnScrolling;
        PrefabImageLoader.OnError += StopEnlargingContent;

        _linkImageChecker = new LinkImageChecker();
    }

    private void GetGalleryViewData(GalleryView galleryView)
    {
        _content = galleryView.Content;
        _viewPort = galleryView.ViewPort;
        _prefab = galleryView.Prefab;

        _prefabHeigth = _prefab.GetComponent<RectTransform>().rect.height;
        _contentSpacingHalfHeight = _content.GetComponent<VerticalLayoutGroup>().spacing / 2;
        _enlargeContentTriggerHeight = _prefabHeigth;

        _prefabsList.Clear();
        _isAllowToEnlargeContent = true;

        CreateInitialRows();
    }

    private async void CreateInitialRows()
    {
        float numberOfRows = _viewPort.rect.height / _prefabHeigth;

        for (int i = 0; i < numberOfRows; i++)
        {
           //if (await _linkImageChecker.CheckImageRoutine(i))
           // {
                var galleryString = Object.Instantiate(_prefab, _content);
                _prefabsList.Add(galleryString);
                OnStringCreated?.Invoke(galleryString, _prefabsList.IndexOf(galleryString) + 1);
           // }
        }
    }

    private void EnlargeContentOnScrolling()
    {
        if (!_isAllowToEnlargeContent) return;

        if (_content.localPosition.y > _enlargeContentTriggerHeight)
        {
            var galleryString = Object.Instantiate(_prefab, _content);
            _prefabsList.Add(galleryString);
            OnStringCreated?.Invoke(galleryString, _prefabsList.IndexOf(galleryString) + 1);
            _enlargeContentTriggerHeight += _prefabHeigth + _contentSpacingHalfHeight;
        }
    }

    private void StopEnlargingContent(int imageNumber)
    {
        _isAllowToEnlargeContent = false;

        Debug.Log("Delete row: " + imageNumber / 2);
        Object.Destroy(_prefabsList[imageNumber / 2]);
        _prefabsList.RemoveAt(imageNumber / 2);
    }
}