using System.Collections.Generic;
using UnityEngine;


public class TableFactory
{
    private int _countOfCells = 8;
    private float _distanceBetweenCells = 1.2f;
    private Vector3 _firstCellPosition;
    private Cell[,] _cells;
    private GameObject _table;

    public GameObject Table => _table;

    public TableFactory()
    {
        CreateTable();
    }

    private void CreateTable()
    {
        _cells = new Cell[_countOfCells, _countOfCells];
        _table = GameObject.Instantiate(Resources.Load<GameObject>(AssetsPath.Path[ObjectType.Table]));
        float distance = -(_countOfCells/2 * _distanceBetweenCells) + _distanceBetweenCells / 2;
        _firstCellPosition = new Vector3(distance, distance,0);

        Cell go;

        for (int i = 0; i < _countOfCells; i++)
        {
            for (int k = 0; k < _countOfCells; k++)
            {
                go = GameObject.Instantiate(Resources.Load<GameObject>(AssetsPath.Path[ObjectType.Cell])).GetComponent<Cell>();
                go.transform.SetParent(_table.transform);
                go.transform.position = new Vector3(_firstCellPosition.x + (k * _distanceBetweenCells), _firstCellPosition.y + (i * _distanceBetweenCells),0);
                go.Table = _table.GetComponent<Table>();
                _cells[k,i] = go;
                _cells[k, i].CellPosition = new Vector2(k,i);
                //go.SetActive(false);
            }
        }

        _table.GetComponent<Table>().Cells = _cells;
    }
}
