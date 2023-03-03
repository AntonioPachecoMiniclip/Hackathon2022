using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnState : GameState
{
    public override void Setup(GameStateMachine gameStateMachine)
    {
        base.Setup(gameStateMachine);
        totalDuration = 20;

        PlayerController.PlayerInputGiven += TriggerEndState;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        StartPlayerTurn();
    }

    protected override void TriggerEndState()
    {
        gameStateMachine.SetState<WaitForOtherPlayersState>();
    }

    private void StartPlayerTurn()
    {
        GameManager gameManager = GameManager.Instance;

        PlayerController localPlayer = gameManager.localPlayer;
        GameManager.Instance.Camera.SetTarget(localPlayer.gameObject);

        localPlayer.StartTurn(totalDuration);
    }
}
