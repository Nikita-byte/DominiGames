using UnityEngine;


public class GameSession : ITurnOn, IUpdate
{
    private Table _table;
    private IMode _mode;

    public void TurnOn()
    {
        if (_table == null)
        {
            _table = ObjectPool.Instance.GetObject(ObjectType.Table).GetComponent<Table>();
        }

        _table.TurnOn();

        EventManager.Instance.AddListener(EventType.AIMode, SetAIMode);
        EventManager.Instance.AddListener(EventType.PlayerMode, SetPlayerMode);
    }

    public void TurnOff()
    {
        _table.TurnOff();

        EventManager.Instance.RemoveListener(EventType.AIMode, SetAIMode);
        EventManager.Instance.RemoveListener(EventType.PlayerMode, SetPlayerMode);
    }

    public void Update()
    {
    }

    public void FixedUpdate()
    {
    }

    public void LateUpdate()
    {
    }

    private void SetAIMode()
    {
        Debug.Log("AI");
        _mode = new AIMode(_table);
    }

    private void SetPlayerMode()
    {
        Debug.Log("Player");
        _mode = new PlayerVSPlayerMode(_table);
    }
}
