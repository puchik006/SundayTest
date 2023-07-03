using System.Collections.Generic;
using UnityEngine;

public class ImageHolder
{
    public Sprite TempSprite;
    private static ImageHolder _instance;
    
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
