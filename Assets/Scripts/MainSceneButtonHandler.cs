using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class MainSceneButtonHandler : BasicButton
{
    public static Action OnButtonPressed;

    protected override void ButtonAction()
    {
        OnButtonPressed?.Invoke();
    }
}
