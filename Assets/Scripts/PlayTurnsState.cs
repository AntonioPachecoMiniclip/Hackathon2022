using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayTurnsState : GameState
{
    public override void OnEnter()
    {
        base.OnEnter();
        GameManager.Instance.PlayShots();
        GameManager.Instance.RunCallbackWhenAllPlayerStopMoving(() => TriggerEndState());
    }

    protected override void TriggerEndState()
    {
        gameStateMachine.SetState<TurnEndState>();
    }

    public override void OnUpdate()
    {
        //Override so it does not end after X seconds
    }
}
