using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class FullPageView: MonoBehaviour
{
    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _image.sprite = ImageHolder.Instance.TempSprite;
    }
}
