using System;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;


public class MainMenu : BasePanel
{
    [SerializeField] private Button _playAI;
    [SerializeField] private Button _playPlayers;

    private void Awake()
    {
        _playAI.onClick.AddListener(()=> PlayAI());
        _playPlayers.onClick.AddListener(() => PlayPlayers());
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

    private void PlayAI()
    {
        EventManager.Instance.Events[EventType.StartGame].Invoke();
    }

    private void PlayPlayers()
    {
        EventManager.Instance.Events[EventType.StartGame].Invoke();
    }
}
