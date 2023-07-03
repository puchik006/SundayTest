using UnityEngine;

public interface IGalleryView
{
    RectTransform Content { get; }
    RectTransform ViewPort { get; }
    GameObject Prefab { get; }
}
