using System;
using UnityEngine;

public class GalleryView : MonoBehaviour
{
    [SerializeField] private RectTransform _content;
    [SerializeField] private RectTransform _viewPort;
    [SerializeField] private GameObject _prefab;

    public RectTransform Content { get => _content;}
    public RectTransform ViewPort { get => _viewPort;}
    public GameObject Prefab { get => _prefab;}

    public Action OnScroll;

    public void ScrollAction()
    {
        OnScroll?.Invoke();
    }
}