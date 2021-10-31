using UnityEngine;
using System.Collections.Generic;


public abstract class BaseMode 
{
    protected Cell _chosenCell;
    protected Cell _previousCell;
    protected EnemyType _enemyType;
    protected ActivePlayer _activePlayer;
    protected Table _table;
    protected List<Cell> _transparentCells;
    protected List<Cell> _whiteCells;

    public BaseMode(Table table, EnemyType enemyType)
    {
        _table = table;
        _enemyType = enemyType;
        _transparentCells = new List<Cell>();
        _whiteCells = new List<Cell>();
        EventManager.Instance.ChoseCell += ProcessCell;
    }

    public abstract void TurnOff();
    protected abstract void CheckAges();

    public virtual void Start()
    {
        _activePlayer = ActivePlayer.Black;
        CheckWinCondition();

        Announcer.Instance.DisplayText("Black Turn");
    }

    protected virtual void SwitchTurn()
    {
        if (_activePlayer == ActivePlayer.White)
        {
            _activePlayer = ActivePlayer.Black;
            Announcer.Instance.DisplayText("Black Turn");
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
        _previousCell = _chosenCell;
        _chosenCell = _table.Cells[(int)cellPosition.x, (int)cellPosition.y];

        switch (_chosenCell.CellType)
        {
            case CellType.None:
                break;
            case CellType.Chosen:
                break;
            case CellType.Black:
                if (_activePlayer == ActivePlayer.Black)
                {
                    _chosenCell.CellType = CellType.Chosen;

                    if (_previousCell == null)
                    {
                        _previousCell = _chosenCell;
                        _previousCell.CellType = CellType.Chosen;
                    }
                    else if (_previousCell != null && _previousCell.CellType == CellType.Chosen)
                    {
                        _previousCell.CellType = CellType.Black;
                    }

                    CheckAges();
                }
                else
                {
                    _chosenCell = _previousCell;
                }
                break;
            case CellType.White:
                if (_activePlayer == ActivePlayer.White)
                {
                    _chosenCell.CellType = CellType.Chosen;

                    if (_previousCell == null)
                    {
                        _previousCell = _chosenCell;
                        _previousCell.CellType = CellType.Chosen;
                    }
                    else if (_previousCell != null && _previousCell.CellType == CellType.Chosen)
                    {
                        _previousCell.CellType = CellType.White;
                    }

                    CheckAges();
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

    protected virtual void MakeTurn()
    {
        ClearTrasparentCells();

        _previousCell.CellType = CellType.None;

        _chosenCell.CellType = _activePlayer == ActivePlayer.Black ? CellType.Black : CellType.White;

        CheckWinCondition();
        SwitchTurn();
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
                if (_chosenCell.CellPosition.x < _transparentCells[0].CellPosition.x || _chosenCell.CellPosition.y > _transparentCells[0].CellPosition.y)
                {
                    AIChoseСhecker();
                }
                else
                {
                    CheckCell(_transparentCells[0].CellPosition);
                }
            }
            else
            {
                Cell tempCell = null;

                foreach (Cell cell in _transparentCells)
                {
                    if (_chosenCell.CellPosition.x < cell.CellPosition.x || _chosenCell.CellPosition.y > cell.CellPosition.y)
                    {
                        continue;
                    }

                    tempCell = cell;
                }

                if (tempCell == null)
                {
                    AIChoseСhecker();
                }
                else
                {
                    CheckCell(tempCell.CellPosition);
                }

                //do
                //{
                //    random = UnityEngine.Random.Range(0, _transparentCells.Count - 1);

                //} while (_chosenCell.CellPosition.x < _transparentCells[random].CellPosition.x || _chosenCell.CellPosition.y > _transparentCells[random].CellPosition.y);

                //if (_chosenCell.CellPosition.x < _transparentCells[random].CellPosition.x || _chosenCell.CellPosition.y > _transparentCells[random].CellPosition.y)
                //{
                //    random = UnityEngine.Random.Range(0, _transparentCells.Count - 1);
                //}
            }
        }
    }

    protected virtual void AIChoseСhecker()
    {
        _whiteCells.Clear();

        foreach (Cell cell in _table.Cells)
        {
            if (cell.CellType == CellType.White)
            {
                _whiteCells.Add(cell);
            }
        }

        int random = UnityEngine.Random.Range(0, _whiteCells.Count - 1);

        CheckCell(_whiteCells[random].CellPosition);
    }
}
