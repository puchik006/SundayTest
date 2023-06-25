using System;
using UnityEngine;
using UnityEngine.UI;

public class GalleryStringView : MonoBehaviour
{  
    [SerializeField] private Image _imageOne;

    public Image ImageOne { get => _imageOne; set => _imageOne = value; }
}


