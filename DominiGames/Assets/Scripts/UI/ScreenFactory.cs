using UnityEngine.UI;
using UnityEngine;


public class ScreenFactory
{
    private GameObject _canvas;
    private MainMenu _mainMenu;
    private GamePanel _gamePanel;
    private EndGamePanel _endGamePanel;


    public ScreenFactory()
    {
        if (_canvas == null)
        {
            GameObject go = Resources.Load<GameObject>(AssetsPath.Path[ObjectType.Canvas]);
            _canvas = GameObject.Instantiate(go);
            _canvas.gameObject.SetActive(true);
        }
    }

    public MainMenu MainMenu
    {
        get
        {
            if (_mainMenu == null)
            {
                GameObject go = GameObject.Instantiate(Resources.Load<GameObject>(AssetsPath.Path[ObjectType.MainMenu]), _canvas.transform.position, Quaternion.identity, _canvas.transform);
                _mainMenu = go.GetComponent<MainMenu>();
            }
            return _mainMenu;
        }
    }

    public GamePanel GamePanel
    {
        get
        {
            if (_gamePanel == null)
            {
                GameObject go = GameObject.Instantiate(Resources.Load<GameObject>(AssetsPath.Path[ObjectType.GamePanel]), _canvas.transform.position, Quaternion.identity, _canvas.transform);
                _gamePanel = go.GetComponent<GamePanel>();
            }

            return _gamePanel;
        }
    }

    public EndGamePanel EndGamePanel
    {
        get
        {
            if (_endGamePanel == null)
            {
                GameObject go = GameObject.Instantiate(Resources.Load<GameObject>(AssetsPath.Path[ObjectType.EndGamePanel]), _canvas.transform.position, Quaternion.identity, _canvas.transform);
                _endGamePanel = go.GetComponent<EndGamePanel>();
            }
            return _endGamePanel;
        }
    }
}
