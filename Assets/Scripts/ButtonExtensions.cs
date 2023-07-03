using UnityEngine.Events;
using UnityEngine.UI;

public static class ButtonExtensions
{
    public static void Add(this Button button, UnityAction action) => button.onClick.AddListener(action);
}
