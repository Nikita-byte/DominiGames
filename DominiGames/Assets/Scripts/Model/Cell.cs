using UnityEngine;


public class Cell : MonoBehaviour
{
    [SerializeField] private CellType _cellType;
    [SerializeField] private SpriteRenderer _cellItem;
    [SerializeField] private Sprite _circle;
    [SerializeField] private Vector2 _cellPosition;

    private Table _table;

    public Vector2 CellPosition { get => _cellPosition; set { _cellPosition = value; } }
    public Table Table { get => _table; set { _table = value; } }
    public CellType CellType { get => _cellType; 
        set 
        {
            switch (value)
            {
                case CellType.None:
                    _cellItem.sprite = null;
                    break;
                case CellType.Black:
                    _cellItem.sprite = _circle;
                    _cellItem.color = Color.black;
                    break;
                case CellType.White:
                    _cellItem.sprite = _circle;
                    _cellItem.color = Color.white;
                    break;
                case CellType.Transparent:
                    _cellItem.sprite = _circle;
                    _cellItem.color = Color.gray;
                    break;
                case CellType.Chosen:
                    _cellItem.sprite = _circle;
                    _cellItem.color = Color.green;
                    break;
            }

            _cellType = value; 
        }
    }

    private void OnMouseDown()
    {
        if (CellType != CellType.None)
        {
            EventManager.Instance.ChoseCell.Invoke(_cellPosition, _cellType);
        }
    }
}
