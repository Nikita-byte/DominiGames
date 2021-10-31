using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public sealed class ObjectPool
{
    private static ObjectPool _objectPool;
    private GameObject _pool;

    private int _countOfText = 20;
    private Queue<GameObject> _texts;

    private GameObject _camera;
    private GameObject _table;
    private ObjectFactory _objectFactory;
    private TableFactory _tableFactory;

    public static ObjectPool Instance
    {
        get
        {
            if (_objectPool == null)
            {
                _objectPool = new ObjectPool();
            }
            return _objectPool;
        }
    }

    public ObjectPool()
    {
        _objectFactory = new ObjectFactory();
        _tableFactory = new TableFactory();
        _pool = new GameObject("[Pool]");
        _texts = new Queue<GameObject>();
        _camera = _objectFactory.Camera;
        _table = _tableFactory.Table;

        GameObject go;

        for (int i = 0; i < _countOfText; i++)
        {
            go = _objectFactory.Text;
            go.transform.SetParent(_pool.transform);
            go.SetActive(false);
            _texts.Enqueue(go);
        }
    }

    public GameObject GetObject(ObjectType objectType)
    {
        GameObject go;

        switch (objectType)
        {
            case ObjectType.Camera:
                go = _camera;
                break;
            case ObjectType.Table:
                go = _table;
                break;
            case ObjectType.Text:
                go = _texts.Dequeue(); ;
                break;

            default:
                go = null;
                break;
        }

        //go.SetActive(true);

        return go;
    }

    public void ReturnInPool(ObjectType objectType, GameObject gameObject)
    {
        gameObject.SetActive(false);
        gameObject.transform.SetParent(_pool.transform);

        switch (objectType)
        {
            case ObjectType.Camera:
                break;
            case ObjectType.Text:
                _texts.Enqueue(gameObject);
                break;
        }
    }

    public Dictionary<string, Sprite> GetSprites()
    {
        return _objectFactory.Sprites;
    }
}
