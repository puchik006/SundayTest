using System;
using UnityEngine;
using UnityEngine.UI;

public class GalleryStringView : MonoBehaviour
{
    private static int _rowsQuantity = 1;
    private int _rowNumber;

    [SerializeField] private Image _imageOne;
    [SerializeField] private Image _imageTwo;

    public Image ImageOne { get => _imageOne; set => _imageOne = value; }
    public Image ImageTwo { get => _imageTwo; set => _imageTwo = value; }

    public static Action<GameObject,int> OnFrameCreated;

    private void Awake()
    {
        _rowNumber = _rowsQuantity;
        OnFrameCreated?.Invoke(gameObject,_rowsQuantity++);
    }

    private void OnDisable()
    {
        _rowsQuantity = 1;
    }
}
