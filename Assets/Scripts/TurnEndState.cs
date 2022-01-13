using UnityEngine;

public class TurnEndState : GameState
{
    public override void Setup(GameStateMachine gameStateMachine)
    {
        base.Setup(gameStateMachine);
        totalDuration = 5;
    }
    
    public override void OnEnter()
    {
        Debug.Log("Player turn Ended");
    }

    protected override void OnTimeEnded()
    {
        gameStateMachine.SetState<PlayerTurnState>();
    }
}
