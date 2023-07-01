using UnityEngine;

public class ImageHolder
{
    private static ImageHolder _instance;
    public Sprite TempSprite;

    public static ImageHolder Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new ImageHolder();               
            }
            return _instance;
        }
    }
}
