using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollViewHandler
{
    private float _enlargeListTargetHeigth;
    private float _contentSpacingHalfHeight;
    private float _triggerHeight;

    private GalleryView _galleryView;

    private RectTransform _content;
    private RectTransform _viewPort;
    private GameObject _prefab;

    private List<GameObject> _prefabsList = new List<GameObject>();

    public ScrollViewHandler(GalleryView galleryView)
    {
        _galleryView = galleryView;

        _content = _galleryView.Content;
        _viewPort = _galleryView.ViewPort;
        _prefab = _galleryView.Prefab;

        _enlargeListTargetHeigth = _prefab.GetComponent<RectTransform>().rect.height;
        _triggerHeight = _enlargeListTargetHeigth;
        _contentSpacingHalfHeight = _content.GetComponent<VerticalLayoutGroup>().spacing / 2;

        _galleryView.OnScroll += ScrollAction;

        CreateInitialFrames();
    }

    private void CreateInitialFrames()
    {
        float numberOfFrames = _viewPort.rect.height / _prefab.GetComponent<RectTransform>().rect.height;

        //for (int i = 0; i < numberOfFrames; i++)
        //{
        //    _prefabsList.Add(UnityEngine.Object.Instantiate(_prefab, _content));
        //}

        _prefabsList.Add(UnityEngine.Object.Instantiate(_prefab, _content));
    }

    private void ScrollAction()
    {
        if (_content.localPosition.y > _triggerHeight)
        {
            _prefabsList.Add(UnityEngine.Object.Instantiate(_prefab, _content));

            _triggerHeight += _enlargeListTargetHeigth + _contentSpacingHalfHeight;
        }
    }
}




