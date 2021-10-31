using UnityEngine;
using System;
using System.Collections.Generic;


public class ThirdMode : BaseMode, IDisposable
{
    public ThirdMode(Table table, EnemyType enemyType) : base(table, enemyType) { }

    protected override void CheckPossibleMoves()
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

                if (CheckTableAges(temp))
                {
                    if (_table.Cells[(int)temp.x, (int)temp.y].CellType == CellType.None)
                    {
                        _table.Cells[(int)temp.x, (int)temp.y].CellType = CellType.Transparent;
                        _transparentCells.Add(_table.Cells[(int)temp.x, (int)temp.y]);
                    }
                }
            }
        }

        AITurn();
    }
}