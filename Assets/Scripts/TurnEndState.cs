using UnityEngine;

public class TurnEndState : GameState
{
    public override void Setup(GameStateMachine gameStateMachine)
    {
        base.Setup(gameStateMachine);
        totalDuration = 2;
    }
    
    public override void OnEnter()
    {
        base.OnEnter();
        Debug.Log("Player turn Ended");
    }

    protected override void TriggerEndState()
    {
        gameStateMachine.SetState<PlayerTurnState>();
    }
}
