using UnityEngine;
using System;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class MainSceneButtonHandler : MonoBehaviour
{
    private Button _btnGallery;
    public static Action OnButtonPressed;

    private void Awake()
    {
        _btnGallery = GetComponent<Button>();
        _btnGallery.onClick.AddListener(() => OnButtonPressed?.Invoke());
    }
}
