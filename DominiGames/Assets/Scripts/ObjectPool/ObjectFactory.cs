using UnityEngine;
using System.Collections.Generic;


public class ObjectFactory
{
    private Dictionary<string, Sprite> _sprites;

    public ObjectFactory()
    {
        _sprites = new Dictionary<string, Sprite>();
        Sprite[] sprites = Resources.LoadAll<Sprite>(AssetsPath.Path[ObjectType.Sprites]);

        foreach (Sprite sprite in sprites)
        {
            _sprites.Add(sprite.name, sprite);
        }
    }

    public Dictionary<string, Sprite> Sprites 
    {
        get 
        {
            return _sprites;
        }
    }

    public GameObject Camera
    {
        get
        {
            GameObject camera = GameObject.Instantiate(Resources.Load<GameObject>(AssetsPath.Path[ObjectType.Camera]));
            return camera;
        }
    }

    public GameObject Table
    {
        get
        {
            GameObject table = GameObject.Instantiate(Resources.Load<GameObject>(AssetsPath.Path[ObjectType.Table]));
            return table;
        }
    }

    public GameObject Text
    {
        get
        {
            GameObject text = GameObject.Instantiate(Resources.Load<GameObject>(AssetsPath.Path[ObjectType.Text]));
            return text;
        }
    }
}
