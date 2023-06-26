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

    private List<Button> _galleryButtons = new List<Button>();

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

        _enlargeContentTriggerHeight = _prefabHeight/3;

        _imageNumbersList.Clear();
        _isAllowToEnlargeContent = true;

        CreateInitialRows();
    }

    private async void CreateInitialRows()
    {
        float numberOfRows = _viewPort.rect.height / _prefabHeight;

        for (int i = 1; i < numberOfRows * 2; i++)
        {
            await LoadAndInstantiateImageAsync(i);
        }
    }

    public static Action OnButtonPressed;

    async Task LoadAndInstantiateImageAsync(int imageNumber)
    {
        var sprite = await _imageLoader.LoadImageAsync(imageNumber);

        if (sprite != null && !_imageNumbersList.Contains(imageNumber))
        {
            _imageNumbersList.Add(imageNumber);
            var prefab = Object.Instantiate(_prefab, _content);
            prefab.GetComponentInChildren<Image>().sprite = sprite;
            prefab.GetComponentInChildren<Button>().onClick.AddListener(() => OnButtonPressed?.Invoke());
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