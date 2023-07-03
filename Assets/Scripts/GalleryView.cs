using System;
using UnityEngine;

public class GalleryView : MonoBehaviour, IGalleryView, IContext
{
    [SerializeField] private RectTransform _content;
    [SerializeField] private RectTransform _viewPort;
    [SerializeField] private GameObject _prefab;

    public RectTransform Content { get => _content;}
    public RectTransform ViewPort { get => _viewPort;}
    public GameObject Prefab { get => _prefab;}
    public MonoBehaviour Context => this;

    public static Action OnScroll;
    public static Action<IGalleryView> OnGalleryViewAwake;
    public static Action<IContext> OnGalleryContextAwake;

    private void Awake()
    {
        OnGalleryContextAwake?.Invoke(this);
        OnGalleryViewAwake?.Invoke(this);
    }

    public void ScrollAction()
    {
        OnScroll?.Invoke();
    }
}
