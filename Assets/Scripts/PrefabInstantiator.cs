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
    private int _currentLoadingImage = 1;
    private GameObject _prefab;
    private Dictionary<int?, Sprite> _loadedSprites = new Dictionary<int?, Sprite>();

    public PrefabInstantiator(PrefabImageLoader prefabImageLoader)
    {
        GalleryView.OnAwake += GetGalleryViewData;
        MainSceneButtonHandler.OnButtonPressed += SetInstantiatorToWork;
        ExitButtonHandler.OnButtonPressed += SetInstantiatorToWork;

        _prefabImageLoader = prefabImageLoader;
    }

    private void GetGalleryViewData(GalleryView galleryView)
    {
        _content = galleryView.Content;
        _prefab = galleryView.Prefab;
        _context = galleryView;
    }

    private void SetInstantiatorToWork()
    {
        _currentLoadingImage = 1;
        _loadedSprites.Clear();
    }

    public void LoadAndInstantiatePrefab(int picNumber)
    {
        _context.StartCoroutine(_prefabImageLoader.LoadImage(picNumber, OnImageLoaded));
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
            ImageHolder.Instance.TempSprite = sprite;
        });
    }
}
