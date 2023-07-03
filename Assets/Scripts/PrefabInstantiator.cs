using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class PrefabInstantiator
{
    public static Action OnButtonPressed;
    private PrefabImageLoader _prefabImageLoader;
    private IGalleryView _galleryView;
    private IContext _context;
    private Dictionary<int?, Sprite> _loadedSprites = new Dictionary<int?, Sprite>();
    private const int STARTING_INDEX = 1;
    private int _currentLoadingImage = STARTING_INDEX;

    public PrefabInstantiator(PrefabImageLoader prefabImageLoader)
    {
        _prefabImageLoader = prefabImageLoader;
        GalleryView.OnGalleryContextAwake += (context) => _context = context;
        GalleryView.OnGalleryViewAwake += GetGalleryViewData;
    }

    public void LoadAndInstantiatePrefab(int picNumber)
    {
        _context.Context.StartCoroutine(_prefabImageLoader.LoadImage(picNumber, OnImageLoaded));
    }

    private void GetGalleryViewData(IGalleryView galleryView)
    {       
        _galleryView = galleryView;
        _currentLoadingImage = STARTING_INDEX;
        _loadedSprites.Clear();
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
        var prefab = Object.Instantiate(_galleryView.Prefab, _galleryView.Content);

        prefab.GetComponentInChildren<Image>().sprite = sprite;
        prefab.GetComponentInChildren<Button>().onClick.AddListener(() =>
        {
            OnButtonPressed?.Invoke();
            ImageHolder.Instance.TempSprite = sprite;
        });
    }
}
