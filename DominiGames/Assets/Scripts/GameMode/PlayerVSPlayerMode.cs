

public class PlayerVSPlayerMode : IMode
{
    private Cell _activeCell;
    private ActivePlayer _activePlayer;
    private Table _table;

    public PlayerVSPlayerMode(Table table)
    {
        _table = table;
    }

    public void SwitchTurn()
    {
        _activePlayer = _activePlayer == ActivePlayer.White ? ActivePlayer.Black : ActivePlayer.White;
    }

    public void Start()
    {

    }
}
