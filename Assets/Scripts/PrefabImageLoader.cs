using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class PrefabImageLoader
{
    public static Action OnError;
    private string _picURL;
    
    public PrefabImageLoader(GameConfig gameConfig)
    {
        _picURL = gameConfig.Url;
    }

    public IEnumerator LoadImage(int picNumber, Action<Sprite,int?> callback)
    {
        string url = _picURL.Replace("*", (picNumber).ToString());
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        var operation = request.SendWebRequest();

        yield return operation;

        if (operation.webRequest.result == UnityWebRequest.Result.ConnectionError || operation.webRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            OnError?.Invoke();
            callback?.Invoke(null,null);
        }
        else
        {
            Texture2D texture = ((DownloadHandlerTexture)operation.webRequest.downloadHandler).texture;
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            callback?.Invoke(sprite,picNumber);
        }
    }
}
