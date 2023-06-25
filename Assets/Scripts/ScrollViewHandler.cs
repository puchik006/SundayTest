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

        _prefabHeigth = _prefab.GetComponent<RectTransform>().rect.height;
        _contentSpacingHalfHeight = _content.GetComponent<GridLayoutGroup>().spacing.y / 2;
        _enlargeContentTriggerHeight = _prefabHeigth;

        _prefabsList.Clear();
        _isAllowToEnlargeContent = true;

        CreateInitialRows();
    }

    private async void CreateInitialRows()
    {
        float numberOfRows = _viewPort.rect.height / _prefabHeigth;

        for (int i = 1; i < numberOfRows * 2 + 2; i++)
        {
            var sprite = await _imageLoader.LoadImageAsync(i);

            if (sprite != null)
            {
                var prefab = Object.Instantiate(_prefab, _content);
                _prefabsList.Add(prefab);
                prefab.GetComponent<GalleryStringView>().ImageOne.sprite = sprite;
            }
        }
    }

    private int i = 1;

    private async void EnlargeContentOnScrolling()
    {
        Debug.Log("Content position " + _content.localPosition.y + " cont height " + _content.rect.height + " " + 
            _viewPort.rect.height + " " + _viewPort.localPosition.y);

        //if (!_isAllowToEnlargeContent) return;

        //if (_content.localPosition.y > _enlargeContentTriggerHeight)
        //{
        //    //var galleryString = Object.Instantiate(_prefab, _content);
        //    //_prefabsList.Add(galleryString);
        //    //OnStringCreated?.Invoke(galleryString, _prefabsList.IndexOf(galleryString) + 1);
        //    //_enlargeContentTriggerHeight += _prefabHeigth + _contentSpacingHalfHeight;

        //    var sprite = await _imageLoader.LoadImageAsync(_prefabsList.Count + i);

        //    if (sprite != null)
        //    {
        //        var prefab = Object.Instantiate(_prefab, _content);
        //        _prefabsList.Add(prefab);
        //        prefab.GetComponent<GalleryStringView>().ImageOne.sprite = sprite;
        //        //_enlargeContentTriggerHeight += _prefabHeigth + _contentSpacingHalfHeight;
        //        i++;
        //    }
        //}
    }

    private void StopEnlargingContent(int imageNumber)
    {
        _isAllowToEnlargeContent = false;

        Debug.Log("Delete row: " + imageNumber / 2);
        Object.Destroy(_prefabsList[imageNumber / 2]);
        _prefabsList.RemoveAt(imageNumber / 2);
    }
}