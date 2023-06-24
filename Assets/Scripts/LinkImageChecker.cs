using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class LinkImageChecker: MonoBehaviour
{
    private string _picURL = "http://data.ikppbb.com/test-task-unity-data/pics/*.jpg";

    private void Start()
    {
        CheckImage();
    }

    public void CheckImage()
    {
        int maxImageNumber = -1;

        StartCoroutine(CheckImageRoutine(maxImageNumber));
    }

    private IEnumerator CheckImageRoutine(int maxImageNumber)
    {
        int i = 1;

        while (true)
        {
            var url = _picURL.Replace("*", i.ToString());
            UnityWebRequest request = UnityWebRequest.Get(url);
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                maxImageNumber = i - 1;
                Debug.Log("Max image number: " + maxImageNumber);
                break;
            }
            else
            {
                Debug.Log("Current number: " + i);
            }

            i++;
        }

        if (maxImageNumber == -1)
        {
            Debug.Log("No images found.");
        }

        
        //onMaxImageNumberFound?.Invoke(maxImageNumber);
    }
}