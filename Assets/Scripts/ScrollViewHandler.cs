﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollViewHandler
{
    private GalleryView _galleryView;

    private RectTransform _content;
    private RectTransform _viewPort;
    private GameObject _prefab;
    private List<GameObject> _prefabsList = new List<GameObject>();

    private float _prefabHeigth;
    private float _contentSpacingHalfHeight;
    private float _enlargeContentTriggerHeight;
    private bool _isAllowToEnlargeContent = true;

    public ScrollViewHandler(GalleryView galleryView)
    {
        _galleryView = galleryView;

        _content = _galleryView.Content;
        _viewPort = _galleryView.ViewPort;
        _prefab = _galleryView.Prefab;

        _prefabHeigth = _prefab.GetComponent<RectTransform>().rect.height;
        _contentSpacingHalfHeight = _content.GetComponent<VerticalLayoutGroup>().spacing / 2;
        _enlargeContentTriggerHeight = _prefabHeigth;

        CreateInitialRows();

        PrefabImageLoader.OnError += StopEnlargingContent;
        _galleryView.OnScroll += EnlargeContentOnScrolling;
    }

    private void StopEnlargingContent(int imageNumber)
    {
        _isAllowToEnlargeContent = false;
    }

    private void CreateInitialRows()
    {
        float numberOfRows = _viewPort.rect.height / _prefabHeigth;

        for (int i = 0; i < numberOfRows; i++)
        {
            _prefabsList.Add(Object.Instantiate(_prefab, _content));
        }
    }

    private void EnlargeContentOnScrolling()
    {
        if (!_isAllowToEnlargeContent) return;

        if (_content.localPosition.y > _enlargeContentTriggerHeight)
        {
            _prefabsList.Add(Object.Instantiate(_prefab, _content));
            _enlargeContentTriggerHeight += _prefabHeigth + _contentSpacingHalfHeight;
        }
    }
}