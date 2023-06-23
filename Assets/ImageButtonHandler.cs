using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ImageButtonHandler : MonoBehaviour
{
    private Button _btnImage;
    public static Action OnButtonPressed;

    private void Awake()
    {
        _btnImage = GetComponent<Button>();
        _btnImage.onClick.AddListener(() => OnButtonPressed?.Invoke());
    }
}
