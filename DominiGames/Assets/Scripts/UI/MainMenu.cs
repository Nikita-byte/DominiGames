using System;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;


public class MainMenu : BasePanel
{
    [SerializeField] private Button _playAI;
    [SerializeField] private Button _playPlayers;
    [SerializeField] private Button _first;
    [SerializeField] private Button _second;
    [SerializeField] private Button _third;
    [SerializeField] private InputField _inputField;

    private EnemyType _enemytype;

    private void Awake()
    {
        _playAI.onClick.AddListener(()=> PlayAI());
        _playPlayers.onClick.AddListener(() => PlayPlayers());

        _first.onClick.AddListener(() => FirstMode());
        _second.onClick.AddListener(() => SecondMode());
        _third.onClick.AddListener(() => ThirdMode());
        _inputField.onEndEdit.AddListener(delegate { SetName(_inputField); });
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
        _playAI.gameObject.SetActive(true);
        _playPlayers.gameObject.SetActive(true);

        _first.gameObject.SetActive(false);
        _second.gameObject.SetActive(false);
        _third.gameObject.SetActive(false);
    }

    private void PlayAI()
    {
        _enemytype = EnemyType.AI;

        OpenModeButtons();
    }

    private void PlayPlayers()
    {
        _enemytype = EnemyType.Player;

        OpenModeButtons();
    }

    private void FirstMode()
    {
        EventManager.Instance.Events[EventType.StartGame].Invoke();
        EventManager.Instance.GameSessionEvent.Invoke(_enemytype, PlayModeType.First);
    }

    private void SecondMode()
    {
        EventManager.Instance.Events[EventType.StartGame].Invoke();
        EventManager.Instance.GameSessionEvent.Invoke(_enemytype, PlayModeType.Second);
    }

    private void ThirdMode()
    {
        EventManager.Instance.Events[EventType.StartGame].Invoke();
        EventManager.Instance.GameSessionEvent.Invoke(_enemytype, PlayModeType.Third);
    }

    private void OpenModeButtons()
    {
        _playAI.gameObject.SetActive(false);
        _playPlayers.gameObject.SetActive(false);

        _first.gameObject.SetActive(true);
        _second.gameObject.SetActive(true);
        _third.gameObject.SetActive(true);
    }

    private void SetName(InputField input)
    {
        PlayerPrefs.SetString("Name", input.text);
    }
}
