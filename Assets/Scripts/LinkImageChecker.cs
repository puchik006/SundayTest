using System.Threading.Tasks;
using UnityEngine.Networking;

public class LinkImageChecker
{
    private string _picURL = "http://data.ikppbb.com/test-task-unity-data/pics/*.jpg";

    public async Task<bool> CheckImageRoutine(int imageNumber)
    {
        var url = _picURL.Replace("*", imageNumber.ToString());
        UnityWebRequest request = UnityWebRequest.Get(url);
        request.SendWebRequest();

        while (!request.isDone)
        {
            await Task.Yield();
        }

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}