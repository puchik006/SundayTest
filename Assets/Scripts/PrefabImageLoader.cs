using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class PrefabImageLoader
{
    private string _picURL = "http://data.ikppbb.com/test-task-unity-data/pics/*.jpg"; // put in the constructor
    public static Action<int> OnError;

    public async Task<Sprite> LoadImageAsync(int picNumber)
    {
        var url = _picURL.Replace("*", (picNumber).ToString());
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        request.SendWebRequest();

        while (!request.isDone)
        {
            await Task.Yield();
        }

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            OnError?.Invoke(picNumber);
            return null;
        }
        else
        {
            Texture2D texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(.5f, .5f), 100);
        }
    }

}
