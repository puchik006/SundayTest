using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class PrefabInstantiator
{
    private MonoBehaviour _context;
    private PrefabImageLoader _prefabImageLoader;
    public static Action OnButtonPressed;

    private RectTransform _content;
    private int _currentImageNumber = 1;
    private int _currentLoadingImage = 1;
    private GameObject _prefab;
    private Dictionary<int?, Sprite> _loadedSprites = new Dictionary<int?, Sprite>();

    public PrefabInstantiator(GameObject prefab, RectTransform content, MonoBehaviour context)
    {
        _prefab = prefab;
        _content = content;
        _context = context;

        _prefabImageLoader = new PrefabImageLoader();
    }

    public void LoadAndInstantiatePrefab()
    {
        int picNumber = _currentImageNumber;
        _context.StartCoroutine(_prefabImageLoader.LoadImage(picNumber, OnImageLoaded));
        _currentImageNumber++;
    }

    private void OnImageLoaded(Sprite sprite, int? loadedPictureNumber)
    {
        if (sprite == null) return;

        _loadedSprites.Add(loadedPictureNumber, sprite);

        if (_currentLoadingImage == loadedPictureNumber)
        {
            while (_loadedSprites.ContainsKey(_currentLoadingImage))
            {
                InstantiateAndSetPrefabBehaviour(_loadedSprites[_currentLoadingImage]);
                _loadedSprites.Remove(_currentLoadingImage);
                _currentLoadingImage++;
            }
        }
    }

    private void InstantiateAndSetPrefabBehaviour(Sprite sprite)
    {
        var prefab = Object.Instantiate(_prefab, _content);

        prefab.GetComponentInChildren<Image>().sprite = sprite;
        prefab.GetComponentInChildren<Button>().onClick.AddListener(() =>
        {
            OnButtonPressed?.Invoke();
            ImageHolder._tempSprite = sprite;
        });
    }
}

//public class PrefabInstantiator
//{
//    private MonoBehaviour _context;
//    private PrefabImageLoader _prefabImageLoader;
//    private static List<int> _imageNumbersList = new();
//    public static Action OnButtonPressed;

//    private RectTransform _content;
//    private int _currentImageNumber = 1;
//    private int _currentLoadingImage = 1;
//    private GameObject _prefab;

//    public PrefabInstantiator(GameObject prefab, RectTransform content, MonoBehaviour context)
//    {
//        _prefab = prefab;
//        _content = content;
//        _context = context;

//        MainSceneButtonHandler.OnButtonPressed += () => _imageNumbersList.Clear();
//        ExitButtonHandler.OnButtonPressed += () => _imageNumbersList.Clear();
//        _prefabImageLoader = new PrefabImageLoader();
//    }

//    public void LoadAndInstantiatePrefab()
//    {
//        int picNumber = _currentImageNumber;
//        _currentImageNumber++;

//        _context.StartCoroutine(_prefabImageLoader.LoadImage(picNumber, OnImageLoaded));
//    }

//    private Dictionary<int?, Sprite> spriteDict = new();

//    private void OnImageLoaded(Sprite sprite, int? loadedPictureNumber)
//    {
//        if (sprite == null) return;
//        spriteDict.Add(loadedPictureNumber, sprite);
//        GetSpriteFromDictionary();
//    }


//    private void GetSpriteFromDictionary()
//    {
//        if (spriteDict.Count == 0) return;

//        for (int i = 1; i <= _currentLoadingImage; i++)
//        {
//            if (spriteDict.ContainsKey(i))
//            {
//                InstantiateAndSetPrefabBehaviour(spriteDict[i]);
//                spriteDict.Remove(i);
//                Debug.Log(i + " cli " + _currentLoadingImage);
//            }
//        }
//        _currentLoadingImage++;
//    }

//    private void InstantiateAndSetPrefabBehaviour(Sprite sprite)
//    {
//        var prefab = Object.Instantiate(_prefab, _content);

//        prefab.GetComponentInChildren<Image>().sprite = sprite;
//        prefab.GetComponentInChildren<Button>().onClick.AddListener(() =>
//        {
//            OnButtonPressed?.Invoke();
//            ImageHolder._tempSprite = sprite;
//        });

//        _imageNumbersList.Add(_currentImageNumber);
//    }
//}
