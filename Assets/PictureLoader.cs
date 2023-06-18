using System;
using System.Collections;
using System.Collections.Generic;
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
        //StartCoroutine(LoadImage());
        CreateFrames();
        //CountImages();

        _enlargeListTargetHeigth = _prefab.GetComponent<RectTransform>().rect.height;
    }

    IEnumerator LoadImage()
    {
        for (int i = 1; i < 6; i++)
        {
            var url = _picURL.Replace('*', i.ToString()[0]);
            Debug.Log("URL: " + url);

            UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log("Error: " + request.error);
            }
            else
            {
                //Texture2D texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
                //_picture.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(.5f, .5f), 100);
                _picturesCount++;
            }
        }

        //UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        //yield return request.SendWebRequest();

        //if (request.result == UnityWebRequest.Result.ConnectionError || request.result ==  UnityWebRequest.Result.ProtocolError)
        //{
        //    Debug.Log("Error: " + request.error);
        //    _requestError = request.error;
        //}
        //else
        //{
        //    //Texture2D texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
        //    //_picture.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(.5f, .5f), 100);
        //}
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
        Debug.Log("Content: " + _enlargeListTargetHeigth);

        if (_content.localPosition.y >= _enlargeListTargetHeigth)
        {
            _prefabsList.Add(Instantiate(_prefab, _content));

            _enlargeListTargetHeigth += _prefab.GetComponent<RectTransform>().rect.height;
        }
    }
}
