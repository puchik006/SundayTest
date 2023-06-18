using UnityEngine;
using UnityEngine.UI;

public class GalleryStringView : MonoBehaviour
{
    [SerializeField] private Image _imageOne;
    [SerializeField] private Image _imageTwo;

    public Image ImageOne { get => _imageOne; set => _imageOne = value; }
    public Image ImageTwo { get => _imageTwo; set => _imageTwo = value; }
}
