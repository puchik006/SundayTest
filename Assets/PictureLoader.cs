using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PictureLoader : MonoBehaviour
{
    private string _picURL = "http://data.ikppbb.com/test-task-unity-data/pics/67.jpg";

    //[SerializeField] private Image _picture;

    //[SerializeField] private 

    private void Start()
    {
        StartCoroutine(LoadImage(_picURL));
        CreateFrames();
    }

    IEnumerator LoadImage(string url)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log("Error: " + request.error);
        }
        else
        {
            //Texture2D texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            //_picture.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(.5f, .5f), 100);
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
}
