using UnityEngine;
using System;
using System.Collections.Generic;


public class SecondMode : BaseMode, IDisposable
{
    public SecondMode(Table table, EnemyType enemyType) : base(table, enemyType) { }

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

                    if (_table.Cells[(int)temp.x, (int)temp.y].CellType == CellType.Black
                        || _table.Cells[(int)temp.x, (int)temp.y].CellType == CellType.White)
                    {
                        float cellPos;

                        if (temp.x == _chosenCell.CellPosition.x)
                        {
                            cellPos = temp.y + i < 0 || temp.y + i > 7 ? temp.y : temp.y + i;

                            if (_table.Cells[(int)_chosenCell.CellPosition.x, (int)cellPos].CellType == CellType.None)
                            {
                                _table.Cells[(int)_chosenCell.CellPosition.x, (int)cellPos].CellType = CellType.Transparent;
                                _transparentCells.Add(_table.Cells[(int)_chosenCell.CellPosition.x, (int)cellPos]);
                            }
                        }

                        if (temp.y == _chosenCell.CellPosition.y)
                        {
                            cellPos = temp.x + k < 0 || temp.x + k > 7 ? temp.x : temp.x + k;

                            if (_table.Cells[(int)cellPos, (int)_chosenCell.CellPosition.y].CellType == CellType.None)
                            {
                                _table.Cells[(int)cellPos, (int)_chosenCell.CellPosition.y].CellType = CellType.Transparent;
                                _transparentCells.Add(_table.Cells[(int)cellPos, (int)_chosenCell.CellPosition.y]);
                            }
                        }
                    }
                }
            }
        }

        AITurn();
    }
}
