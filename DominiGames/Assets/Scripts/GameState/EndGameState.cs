using UnityEngine;


public class EndGameState : BaseState
{
    private GameController _gameController;

    public EndGameState(GameController gameController)
    {
        _gameController = gameController;
        EventManager.Instance.AddListener(EventType.EndGame, OpenRecords);
    }

    public override void Enter()
    {
        ScreenInterface.Instance.Execute(PanelType.EndGamePanel);
    }

    public override void Exit()
    {
    }

    public override void FixedUpdate()
    {
    }

    public override void LateUpdate()
    {
    }

    public override void Update()
    {
    }

    public void OpenRecords()
    {
        _gameController.SetState(StateCreator.Instance.EndGameState);
    }
}
