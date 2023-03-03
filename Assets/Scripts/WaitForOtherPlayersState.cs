using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForOtherPlayersState : GameState
{
    public override void OnEnter()
    {
        base.OnEnter();
        Debug.Log("Waiting For Other Players");
    }

    protected override void TriggerEndState()
    {
        gameStateMachine.SetState<PlayTurnsState>();
    }

    public override void OnUpdate()
    {
        if (GameManager.Instance.AllPlayersReadyToShoot() || /*Debug*/Input.GetKeyDown(KeyCode.K))
            TriggerEndState();
    }

    public void DebugTriggerEndState()
    {
        TriggerEndState();
    }
}