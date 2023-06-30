using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class PrefabInstantiator
{
    PrefabImageLoader _prefabImageLoader;
    private static List<int> _imageNumbersList = new List<int>();
    public static Action OnButtonPressed;

    private RectTransform _content;
    private GameObject _prefab;

    public PrefabInstantiator(GameObject prefab, RectTransform content)
    {
        _prefab = prefab;
        _content = content;

        MainSceneButtonHandler.OnButtonPressed += () => _imageNumbersList.Clear();
        ExitButtonHandler.OnButtonPressed += () => _imageNumbersList.Clear();
        _prefabImageLoader = new PrefabImageLoader();
    }

    public async Task LoadAndInstantiatePrefabAsync()
    {
        var i = _imageNumbersList.Count + 1;
        var sprite = await _prefabImageLoader.LoadImageAsync(i);

        if (sprite != null && !_imageNumbersList.Contains(i))
        {
            _imageNumbersList.Add(i);

            var prefab = Object.Instantiate(_prefab, _content);

            prefab.GetComponentInChildren<Image>().sprite = sprite;
            prefab.GetComponentInChildren<Button>().onClick.AddListener(() =>
            {
                OnButtonPressed?.Invoke();
                ImageHolder._tempSprite = sprite;
            });
        }
    }

    public async Task LoadAndInstantiatePrefabAsync(int imageNumber)
    {
        var sprite = await _prefabImageLoader.LoadImageAsync(imageNumber);

        if (sprite != null && !_imageNumbersList.Contains(imageNumber))
        {
            _imageNumbersList.Add(imageNumber);

            var prefab = Object.Instantiate(_prefab, _content);

            prefab.GetComponentInChildren<Image>().sprite = sprite;
            prefab.GetComponentInChildren<Button>().onClick.AddListener(() =>
            {
                OnButtonPressed?.Invoke();
                ImageHolder._tempSprite = sprite;
            });
        }
    }
}
