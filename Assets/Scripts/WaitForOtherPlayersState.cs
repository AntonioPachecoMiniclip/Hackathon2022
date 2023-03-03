using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaitForOtherPlayersState : GameState
{
    GameObject check;

    public override void OnEnter()
    {
        base.OnEnter();
        Debug.Log("Waiting For Other Players");

        if (check == null)
            check = GameObject.FindGameObjectWithTag("Checkmark");

        check.GetComponent<Image>().enabled = true;
    }

    protected override void TriggerEndState()
    {
        if (check == null)
            check = GameObject.FindGameObjectWithTag("Checkmark");
        check.GetComponent<Image>().enabled = false;

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