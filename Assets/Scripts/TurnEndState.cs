using UnityEngine;

public class TurnEndState : GameState
{
    public override void Setup(GameStateMachine gameStateMachine, GameManager gameManager)
    {
        base.Setup(gameStateMachine, gameManager);
        totalDuration = 5;
    }
    
    public override void OnEnter()
    {
        base.OnEnter();
        Debug.Log("Player turn Ended");
    }

    protected override void OnTimeEnded()
    {
        gameStateMachine.SetState<PlayerTurnState>();
    }
}
