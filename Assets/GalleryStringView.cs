using System;
using UnityEngine;
using UnityEngine.UI;

public class GalleryStringView : MonoBehaviour
{
    private static int _frameNumber = 1;

    [SerializeField] private Image _imageOne;
    [SerializeField] private Image _imageTwo;

    public Image ImageOne { get => _imageOne; set => _imageOne = value; }
    public Image ImageTwo { get => _imageTwo; set => _imageTwo = value; }

    public static Action<GameObject,int> OnFrameCreated;

    private void Awake()
    {
        OnFrameCreated?.Invoke(gameObject,_frameNumber++);
        Debug.Log(_frameNumber);
    }
}
