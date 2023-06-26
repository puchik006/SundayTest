using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class ScrollViewHandler
{
    private RectTransform _content;
    private RectTransform _viewPort;
    private GameObject _prefab;
    private List<int> _imageNumbersList = new List<int>();

    private float _prefabHeight;
    private float _contentSpacingHeight;
    private float _enlargeContentTriggerHeight;
    private bool _isAllowToEnlargeContent = true;

    private PrefabImageLoader _imageLoader;

    public ScrollViewHandler()
    {
        GalleryView.OnAwake += GetGalleryViewData;
        GalleryView.OnScroll += EnlargeContentOnScrolling;
        PrefabImageLoader.OnError += StopEnlargingContent;

        _imageLoader = new PrefabImageLoader();
    }

    private void GetGalleryViewData(GalleryView galleryView)
    {
        _content = galleryView.Content;
        _viewPort = galleryView.ViewPort;
        _prefab = galleryView.Prefab;

        _prefabHeight = _prefab.GetComponent<RectTransform>().rect.height;
        _contentSpacingHeight = _content.GetComponent<GridLayoutGroup>().spacing.y;
        float topPadding = _content.GetComponent<GridLayoutGroup>().padding.top;

        _enlargeContentTriggerHeight = topPadding + _prefabHeight + _contentSpacingHeight / 2;

        _imageNumbersList.Clear();
        _isAllowToEnlargeContent = true;

        CreateInitialRows();
    }

    private async void CreateInitialRows()
    {
        float numberOfRows = _viewPort.rect.height / _prefabHeight;

        for (int i = 1; i < numberOfRows * 2 + 2; i++)
        {
            await LoadAndInstantiateImageAsync(i);
        }
    }

    async Task LoadAndInstantiateImageAsync(int i)
    {
        var sprite = await _imageLoader.LoadImageAsync(i);

        if (sprite != null && !_imageNumbersList.Contains(i))
        {
            _imageNumbersList.Add(i);
            var prefab = Object.Instantiate(_prefab, _content);
            prefab.GetComponent<GalleryStringView>().ImageOne.sprite = sprite;
        }
    }

    private async void EnlargeContentOnScrolling()
    {
        if (!_isAllowToEnlargeContent) return;

        if (_content.localPosition.y > _enlargeContentTriggerHeight)
        {
            _enlargeContentTriggerHeight += _prefabHeight;

            await LoadAndInstantiateImageAsync(_imageNumbersList.Count + 1);
            await LoadAndInstantiateImageAsync(_imageNumbersList.Count + 1);
        }
    }

    private void StopEnlargingContent(int imageNumber)
    {
        _isAllowToEnlargeContent = false;
    }
}