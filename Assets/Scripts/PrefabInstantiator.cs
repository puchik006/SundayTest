using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class PrefabInstantiator
{
    private MonoBehaviour _context;
    private PrefabImageLoader _prefabImageLoader;
    private static List<int> _imageNumbersList = new();
    public static Action OnButtonPressed;


    public static Action<Sprite> OnSpriteLoaded;

    private RectTransform _content;
    private int _currentImageNumber = 1;
    private GameObject _prefab;

    public PrefabInstantiator(GameObject prefab, RectTransform content, MonoBehaviour context)
    {
        _prefab = prefab;
        _content = content;
        _context = context;

        MainSceneButtonHandler.OnButtonPressed += () => _imageNumbersList.Clear();
        ExitButtonHandler.OnButtonPressed += () => _imageNumbersList.Clear();
        _prefabImageLoader = new PrefabImageLoader();
    }

    public void LoadAndInstantiatePrefab()
    {
        int picNumber = _currentImageNumber;
        _currentImageNumber++;

        _context.StartCoroutine(_prefabImageLoader.LoadImage(picNumber, OnImageLoaded));
    }

    private void OnImageLoaded(Sprite sprite)
    {
        if (sprite != null)
        {
            SetPrefabBehaviour(sprite);
        }
    }

    private void SetPrefabBehaviour(Sprite sprite)
    {
        var prefab = Object.Instantiate(_prefab, _content);

        prefab.GetComponentInChildren<Image>().sprite = sprite;
        prefab.GetComponentInChildren<Button>().onClick.AddListener(() =>
        {
            OnButtonPressed?.Invoke();
            ImageHolder._tempSprite = sprite;
        });

        _imageNumbersList.Add(_currentImageNumber);
        OnSpriteLoaded?.Invoke(sprite);
    }

    //public void LoadAndInstantiatePrefab()
    //{
    //    int picNumber = _imageNumbersList.Count > 0 ? _imageNumbersList.Max() + 1 : 1;

    //    _imageNumbersList.Add(picNumber);

    //    _context.StartCoroutine(_prefabImageLoader.LoadImage(picNumber, OnImageLoaded));
    //}

    //private void OnImageLoaded(Sprite sprite)
    //{
    //    if (sprite != null)
    //    {
    //        SetPrefabBehaviour(sprite);
    //    }
    //}

    //private void SetPrefabBehaviour(Sprite sprite)
    //{
    //    var prefab = Object.Instantiate(_prefab, _content);

    //    prefab.GetComponentInChildren<Image>().sprite = sprite;
    //    prefab.GetComponentInChildren<Button>().onClick.AddListener(() =>
    //    {
    //        OnButtonPressed?.Invoke();
    //        ImageHolder._tempSprite = sprite;
    //    });
    //}

    //public async Task LoadAndInstantiatePrefabAsync()
    //{
    //    int nextNumber = _imageNumbersList.Count > 0 ? _imageNumbersList.Max() + 1 : 1;

    //    var sprite = await _prefabImageLoader.LoadImageAsync(nextNumber);

    //    if (sprite == null) return;

    //    while (_imageNumbersList.Contains(nextNumber))
    //    {
    //        nextNumber++;
    //    }

    //    _imageNumbersList.Add(nextNumber);

    //    var prefab = Object.Instantiate(_prefab, _content);

    //    prefab.GetComponentInChildren<Image>().sprite = sprite;
    //    prefab.GetComponentInChildren<Button>().onClick.AddListener(() =>
    //    {
    //        OnButtonPressed?.Invoke();
    //        ImageHolder._tempSprite = sprite;
    //    });


    //    //var i = _imageNumbersList.Count + 1;
    //    //var sprite = await _prefabImageLoader.LoadImageAsync(i);

    //    //if (sprite == null) return;

    //    //if (!_imageNumbersList.Contains(i))
    //    //{
    //    //    _imageNumbersList.Add(i);

    //    //    var prefab = Object.Instantiate(_prefab, _content);

    //    //    prefab.GetComponentInChildren<Image>().sprite = sprite;
    //    //    prefab.GetComponentInChildren<Button>().onClick.AddListener(() =>
    //    //    {
    //    //        OnButtonPressed?.Invoke();
    //    //        ImageHolder._tempSprite = sprite;
    //    //    });
    //    //}
    //}



    //public async Task LoadAndInstantiatePrefabAsync(int imageNumber)
    //{
    //    var sprite = await _prefabImageLoader.LoadImageAsync(imageNumber);

    //    if (sprite != null && !_imageNumbersList.Contains(imageNumber))
    //    {
    //        _imageNumbersList.Add(imageNumber);

    //        var prefab = Object.Instantiate(_prefab, _content);

    //        prefab.GetComponentInChildren<Image>().sprite = sprite;
    //        prefab.GetComponentInChildren<Button>().onClick.AddListener(() =>
    //        {
    //            OnButtonPressed?.Invoke();
    //            ImageHolder._tempSprite = sprite;
    //        });
    //    }
    //}
}
