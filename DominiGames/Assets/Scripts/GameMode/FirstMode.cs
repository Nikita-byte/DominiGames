using UnityEngine;
using System;
using System.Collections.Generic;


public class FirstMode : BaseMode ,IDisposable
{
    public FirstMode(Table table, EnemyType enemyType) : base(table, enemyType)
    {
        _table = table;
        _enemyType = enemyType;
        _transparentCells = new List<Cell>();
        _whiteCells = new List<Cell>();
        EventManager.Instance.ChoseCell += ProcessCell;
    }

    public void Dispose()
    {
        EventManager.Instance.ChoseCell -= ProcessCell;
    }

    public override void TurnOff()
    {
        Dispose();
    }

    protected override void CheckAges()
    {
        if (_transparentCells.Count != 0)
        {
            ClearTrasparentCells();
        }

        for (int i = -1; i < 2; i++)
        {
            for (int k = -1; k < 2; k++)
            {
                Vector2 temp = new Vector2(_chosenCell.CellPosition.x + k, _chosenCell.CellPosition.y + i);

                if (!(temp.x < 0) && !(temp.x > 7) &&
                    !(temp.y < 0) && !(temp.y > 7))
                {
                    if (_table.Cells[(int)temp.x, (int)temp.y].CellType == CellType.None)
                    {
                        _table.Cells[(int)temp.x, (int)temp.y].CellType = CellType.Transparent;
                        _transparentCells.Add(_table.Cells[(int)temp.x, (int)temp.y]);
                    }

                    if (_table.Cells[(int)temp.x, (int)temp.y].CellType == CellType.Black
                        || _table.Cells[(int)temp.x, (int)temp.y].CellType == CellType.White)
                    {
                        Vector2 cellPos;

                        if (temp.x != _chosenCell.CellPosition.x && temp.y != _chosenCell.CellPosition.y)
                        {
                            cellPos = new Vector2((temp.x + k < 0 || temp.x + k > 7 ? temp.x : temp.x + k), (temp.y + i < 0 || temp.y + i > 7 ? temp.y : temp.y + i));

                            if (_table.Cells[(int)cellPos.x, (int)cellPos.y].CellType == CellType.None)
                            {
                                _table.Cells[(int)cellPos.x, (int)cellPos.y].CellType = CellType.Transparent;
                                _transparentCells.Add(_table.Cells[(int)cellPos.x, (int)cellPos.y]);
                            }
                        }
                    }
                }
            }
        }

        AITurn();
    }
}
