using UnityEngine;

[CreateAssetMenu(fileName = "New GameConfig", menuName = "Game Config", order = 51)]
public class GameConfig: ScriptableObject
{
    [Header("URL")]
    [Tooltip("Url link to website with images, use '*' insted of picture number")]
    [TextAreaAttribute]
    [SerializeField] private string _url = "http://data.ikppbb.com/test-task-unity-data/pics/*.jpg";

    [Header("Scene loading")]
    [Tooltip("Time in seconds that need to load new scene")]
    [SerializeField][Range(1,5)] int _timeToLoad = 2;

    public int TimeToLoad { get => _timeToLoad;}
    public string Url { get => _url;}
}