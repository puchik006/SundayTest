using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class PrefabImageLoader
{
    private string _picURL = "http://data.ikppbb.com/test-task-unity-data/pics/*.jpg";
    public static Action<int> OnError;

    public PrefabImageLoader()
    {
        ScrollViewHandler.OnStringCreated += ImageLoader;
    }

    private async void ImageLoader(GameObject gameObject, int stringNumber)
    {
        int i = stringNumber;
        int imageOneNumber = 1 + (i - 1) * 2;
        int imageTwoNumber = 1 + (i - 1) * 2 + 1;

        gameObject.GetComponent<GalleryStringView>().ImageOne.sprite = await LoadImageAsync(imageOneNumber);
        gameObject.GetComponent<GalleryStringView>().ImageTwo.sprite = await LoadImageAsync(imageTwoNumber);
    }

    private async Task<Sprite> LoadImageAsync(int picNumber)
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
