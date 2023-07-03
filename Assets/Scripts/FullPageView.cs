using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class FullPageView: MonoBehaviour
{
    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();

        if (ImageHolder.Instance.TempSprite != null)
        {
            _image.sprite = ImageHolder.Instance.TempSprite;
        }
    }
}
