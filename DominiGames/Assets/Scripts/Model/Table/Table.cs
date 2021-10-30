using UnityEngine;


public class Table : MonoBehaviour
{
    public Cell[,] Cells { get; set; }

    public void TurnOn()
    {
        StartCellsPositions();
    }

    public void TurnOff()
    {
        foreach (Cell cell in Cells)
        {
            cell.CellType = CellType.None;
        }
    }

    private void StartCellsPositions()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int k = 5; k < 8; k++)
            {
                Cells[k, i].CellType = CellType.White;
            }
        }

        for (int i = 5; i < 8; i++)
        {
            for (int k = 0; k < 3; k++)
            {
                Cells[k, i].CellType = CellType.Black;
            }
        }
    }

    private void SetActiveCell(Vector2 cellPosition)
    {
        
    }
}
