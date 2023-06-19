using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class PictureLoader : MonoBehaviour
{
    private string _picURL = "http://data.ikppbb.com/test-task-unity-data/pics/*.jpg";

    [SerializeField] private Sprite _tempSrite;

    private void Start()
    {
        CreateFramesAsync();

        _enlargeListTargetHeigth = _prefab.GetComponent<RectTransform>().rect.height;

        _contentSpacingHalfHeight = _content.GetComponent<VerticalLayoutGroup>().spacing/2;
    }

    private string _urlError;

    private async Task<Sprite> LoadImageAsync(int picNumber)
    {
        var url = _picURL.Replace("*", (picNumber).ToString());
        Debug.Log("URL: " + url);

        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        request.SendWebRequest();

        while (!request.isDone)
        {
            await Task.Yield();
        }

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("Error: " + request.error);
            _urlError = request.error;
            return _tempSrite;
        }
        else
        {
            Texture2D texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(.5f, .5f), 100);        
        }   
    }

    [SerializeField] private RectTransform _content;
    [SerializeField] private RectTransform _viewPort;
    [SerializeField] private GameObject _prefab;

    private List<GameObject> _prefabsList = new List<GameObject>();

    private async void CreateFramesAsync()
    {
        float numberOfFrames = _viewPort.rect.height / _prefab.GetComponent<RectTransform>().rect.height;

        for (int i = 0; i < numberOfFrames; i++)
        {
             _prefabsList.Add(Instantiate(_prefab, _content));
        }

        for (int i = 0; i < _prefabsList.Count; i++)
        {
            _prefabsList[i].GetComponent<GalleryStringView>().ImageOne.sprite = await LoadImageAsync(i + i + 1);
            _prefabsList[i].GetComponent<GalleryStringView>().ImageTwo.sprite = await LoadImageAsync(i + i + 2);
        }
    }

    private float _enlargeListTargetHeigth;
    private float _contentSpacingHalfHeight;

    public async void ScrollAction()
    {
        if (_content.localPosition.y >= _enlargeListTargetHeigth)
        {
            GameObject aaa;
            _prefabsList.Add(aaa = Instantiate(_prefab, _content));
            var i = _prefabsList.IndexOf(aaa);
            aaa.GetComponent<GalleryStringView>().ImageOne.sprite = await LoadImageAsync(i + i + 1);
            aaa.GetComponent<GalleryStringView>().ImageTwo.sprite = await LoadImageAsync(i + i + 2);

            _enlargeListTargetHeigth += _prefab.GetComponent<RectTransform>().rect.height + _contentSpacingHalfHeight;
        }
    }
}
