using UnityEngine;
using UnityEngine.UI;

public abstract class BasicButton: MonoBehaviour
{
    protected Button _button;

    protected void Awake()
    {
        _button = GetComponent<Button>();
        _button.Add(ButtonAction);
    }

    protected virtual void ButtonAction() { }
}