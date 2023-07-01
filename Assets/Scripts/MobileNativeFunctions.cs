using System;
using UnityEngine;

public class MobileNativeFunctions
{
    public static Action GoBack;

    public void HandleMobileBackButtonPress()
    {
        if (IsMobilePlatform() && Input.GetKeyDown(KeyCode.Escape))
        {
            GoBack?.Invoke();
        }
    }

    private bool IsMobilePlatform()
    {
        return Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer;
    }
}