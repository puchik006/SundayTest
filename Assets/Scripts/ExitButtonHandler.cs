using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ExitButtonHandler : MonoBehaviour
{
    private Button _btnExit;
    public static Action OnButtonPressed;

    private void Awake()
    {
        _btnExit = GetComponent<Button>();
        _btnExit.onClick.AddListener(() => OnButtonPressed?.Invoke());
    }
}
