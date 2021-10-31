using UnityEngine;


public class GameSession : ITurnOn
{
    private Table _table;
    private BaseMode _mode;

    public void TurnOn()
    {
        if (_table == null)
        {
            _table = ObjectPool.Instance.GetObject(ObjectType.Table).GetComponent<Table>();
        }

        _table.TurnOn();

        EventManager.Instance.GameSessionEvent += SetMode;
    }

    public void TurnOff()
    {
        _table.TurnOff();
        _mode.TurnOff();

        EventManager.Instance.GameSessionEvent -= SetMode;
    }

    private void SetMode(EnemyType enemyType, PlayModeType playModeType)
    {
        switch (playModeType)
        {
            case PlayModeType.First:
                _mode = new FirstMode(_table, enemyType);
                break;
            case PlayModeType.Second:
                _mode = new SecondMode(_table, enemyType);
                break;
            case PlayModeType.Third:
                _mode = new ThirdMode(_table, enemyType);
                break;
        }

        _mode.Start();
    }
}
