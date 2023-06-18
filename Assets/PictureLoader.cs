using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PictureLoader : MonoBehaviour
{
    private string _picURL = "http://data.ikppbb.com/test-task-unity-data/pics/*.jpg";

    //[SerializeField] private Image _picture;

    [SerializeField] private int _picturesCount = 0;

    private void Start()
    {
        CreateFrames();

        PicturesCountWWW();

        _enlargeListTargetHeigth = _prefab.GetComponent<RectTransform>().rect.height;
    }

    private string _urlError;

    private async void PicturesCountWWW()
    {
        while(_urlError == null)
        {
            _picturesCount++;
            var url = _picURL.Replace("*", _picturesCount.ToString());
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
                break;
            }
            else
            {
                //Texture2D texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
                //_picture.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(.5f, .5f), 100);
            }
        }
    }

    [SerializeField] private RectTransform _content;
    [SerializeField] private RectTransform _viewPort;
    [SerializeField] private GameObject _prefab;

    private List<GameObject> _prefabsList = new List<GameObject>();

    private void CreateFrames()
    {
        float numberOfFrames = _viewPort.rect.height / _prefab.GetComponent<RectTransform>().rect.height;

        for (int i = 0; i < numberOfFrames; i++)
        {
             _prefabsList.Add(Instantiate(_prefab, _content));
        }
    }

    private float _enlargeListTargetHeigth;

    public void ScrollAction()
    {
        if (_content.localPosition.y >= _enlargeListTargetHeigth)
        {
            _prefabsList.Add(Instantiate(_prefab, _content));

            _enlargeListTargetHeigth += _prefab.GetComponent<RectTransform>().rect.height;
        }
    }
}
