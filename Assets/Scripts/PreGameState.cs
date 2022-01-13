using UnityEngine;

public class PreGameState : GameState
{
    public override void Setup(GameStateMachine gameStateMachine)
    {
        base.Setup(gameStateMachine);
        totalDuration = 10;
    }

    public override void OnEnter()
    {
        Debug.Log("Preparing Game");
    }

    protected override void OnTimeEnded()
    {
        gameStateMachine.SetState<PreGameState>();
    }

    public override void OnLeave()
    {
        Debug.Log("Game is Starting");
    }
}
