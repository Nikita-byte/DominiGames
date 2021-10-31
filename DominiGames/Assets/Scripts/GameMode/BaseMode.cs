using UnityEngine;
using System.Collections.Generic;
using System;


public abstract class BaseMode : IDisposable
{
    protected Cell _chosenCell;
    protected Cell _previousCell;
    protected EnemyType _enemyType;
    protected ActivePlayer _activePlayer;
    protected Table _table;
    protected List<Cell> _transparentCells;
    protected List<Cell> _whiteCells;
    protected string _name;

    public BaseMode(Table table, EnemyType enemyType)
    {
        _table = table;
        _enemyType = enemyType;
        _transparentCells = new List<Cell>();
        _whiteCells = new List<Cell>();
        EventManager.Instance.ChoseCell += ProcessCell;
    }

    protected abstract void CheckPossibleMoves();

    public void Dispose()
    {
        EventManager.Instance.ChoseCell -= ProcessCell;
    }

    public virtual void Start()
    {
        _activePlayer = ActivePlayer.Black;
        CheckWinCondition();
        _name = PlayerPrefs.GetString("Name", "Black");

        Announcer.Instance.DisplayText(_name + " Turn");
    }

    public virtual void TurnOff()
    {
        Dispose();
        _table = null;
        _transparentCells.Clear();
        _whiteCells.Clear();
    }

    protected virtual bool CheckTableAges(Vector2 cellposition)
    {
        return !(cellposition.x < 0 || cellposition.x > 7 || cellposition.y  < 0 || cellposition.y  > 7) ? true : false;
    }


    protected virtual void SwitchTurn()
    {
        if (_activePlayer == ActivePlayer.White)
        {
            _activePlayer = ActivePlayer.Black;
            Announcer.Instance.DisplayText(_name + " Turn");
        }
        else
        {
            _activePlayer = ActivePlayer.White;

            if (_enemyType == EnemyType.AI)
            {
                AITurn();
            }
            else
            {
                Announcer.Instance.DisplayText("White Turn");
            }
        }
    }

    protected virtual void ProcessCell(Vector2 cellPosition, CellType cellType)
    {
        CheckCell(cellPosition);
    }


    protected virtual void CheckCell(Vector2 cellPosition)
    {
        if (_table != null)
        {
            _previousCell = _chosenCell;
            _chosenCell = _table.Cells[(int)cellPosition.x, (int)cellPosition.y];

            switch (_chosenCell.CellType)
            {
                case CellType.None:
                    break;
                case CellType.Chosen:
                    break;
                case CellType.Black:
                case CellType.White:
                    if (_activePlayer == ActivePlayer.Black && _chosenCell.CellType == CellType.Black ||
                        _activePlayer == ActivePlayer.White && _chosenCell.CellType == CellType.White)
                    {
                        _chosenCell.CellType = CellType.Chosen;

                        if (_previousCell == null)
                        {
                            _previousCell = _chosenCell;
                            _previousCell.CellType = CellType.Chosen;
                        }
                        else if (_previousCell != null && _previousCell.CellType == CellType.Chosen)
                        {
                            _previousCell.CellType = _activePlayer == ActivePlayer.Black ? CellType.Black : CellType.White;
                        }

                        CheckPossibleMoves();
                    }
                    else
                    {
                        _chosenCell = _previousCell;
                    }
                    break;
                case CellType.Transparent:
                    MakeTurn();
                    break;
            }
        }
    }

    protected virtual void MakeTurn()
    {
        ClearTrasparentCells();

        _previousCell.CellType = CellType.None;

        _chosenCell.CellType = _activePlayer == ActivePlayer.Black ? CellType.Black : CellType.White;

        CheckWinCondition();
        SwitchTurn();
        SoundManager.Instance.PlaySound(SoundType.Turn);
    }

    protected virtual void ClearTrasparentCells()
    {
        foreach (Cell cell in _transparentCells)
        {
            cell.CellType = CellType.None;
        }

        _transparentCells.Clear();
    }

    protected virtual void CheckWinCondition()
    {
        bool _isWhiteWin = true;
        bool _isBlackWin = true;

        for (int i = 0; i < 3; i++)
        {
            for (int k = 5; k < 8; k++)
            {
                if (_table.Cells[k, i].CellType != CellType.Black)
                {
                    _isBlackWin = false;
                }
            }
        }

        for (int i = 5; i < 8; i++)
        {
            for (int k = 0; k < 3; k++)
            {
                if (_table.Cells[k, i].CellType != CellType.White)
                {
                    _isWhiteWin = false;
                }
            }
        }

        if (_isBlackWin)
        {
            Debug.Log("Black");
            EventManager.Instance.Events[EventType.MainMenu].Invoke();
        }

        if (_isWhiteWin)
        {
            Debug.Log("White");
            EventManager.Instance.Events[EventType.MainMenu].Invoke();
        }
    }

    protected virtual void AITurn()
    {
        if (_enemyType == EnemyType.AI && _activePlayer == ActivePlayer.White)
        {
            if (_transparentCells.Count == 0)
            {
                AIChoseСhecker();
            }
            else if (_transparentCells.Count == 1)
            {
                CheckCell(_transparentCells[0].CellPosition);
            }
            else
            {
                Cell tempCell = null;

                foreach (Cell cell in _transparentCells)
                {
                    if (tempCell == null)
                    {
                        tempCell = cell;
                    }
                    else
                    {
                        if (cell.CellPosition.x <= tempCell.CellPosition.x && cell.CellPosition.y >= tempCell.CellPosition.y)
                        {
                            tempCell = cell;
                        }
                    }
                }

                if (tempCell == null)
                {
                    AIChoseСhecker();
                }
                else
                {
                    CheckCell(tempCell.CellPosition);
                }
            }
        }
    }

    protected virtual void AIChoseСhecker()
    {
        _whiteCells.Clear();

        if (_table != null)
        {
            foreach (Cell cell in _table.Cells)
            {
                if (cell.CellType == CellType.White)
                {
                    _whiteCells.Add(cell);
                }
            }

            int random = UnityEngine.Random.Range(0, _whiteCells.Count);

            CheckCell(_whiteCells[random].CellPosition);
        }
    }
}
