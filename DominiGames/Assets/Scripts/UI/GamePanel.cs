using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;


public class GamePanel : BasePanel
{
    [SerializeField] private Button _mainMenu;

    private void Awake()
    {
        _mainMenu.onClick.AddListener(() => EventManager.Instance.Events[EventType.MainMenu].Invoke());
    }

    public override void Hide()
    {
        GetComponent<RectTransform>().DOAnchorPos(new Vector2(-350, 0), 0.3f);
        gameObject.SetActive(false);
    }

    public override void Show()
    {
        gameObject.SetActive(true);
        GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, 0), 0.3f);
    }
}