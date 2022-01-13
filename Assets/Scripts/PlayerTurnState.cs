using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnState : GameState
{
    public override void Setup(GameStateMachine gameStateMachine)
    {
        base.Setup(gameStateMachine);
        totalDuration = 20;

        PlayerController.PlayerShoot += OnPlayerShoot;
    }

    private void OnPlayerShoot()
    {
        TriggerEndState();
    }
    
    public override void OnEnter()
    {
        base.OnEnter();
        StartPlayerTurn();
    }

    protected override void TriggerEndState()
    {
        gameStateMachine.SetState<TurnEndState>();
    }

    private void StartPlayerTurn()
    {
        GameManager gameManager = GameManager.Instance;
        List<PlayerController> players = gameManager.players;
        gameManager.currentPlayerIndex++;
        if (gameManager.currentPlayerIndex >= players.Count)
            gameManager.currentPlayerIndex = 0;
        
        Debug.Log($"Player {gameManager.currentPlayerIndex} Turn Start");
        
        PlayerController currentPlayer = players[gameManager.currentPlayerIndex];
        gameManager.Camera.SetTarget(currentPlayer.gameObject);
        
        players[gameManager.currentPlayerIndex].StartTurn();
    }

    public override void OnLeave()
    {
        base.OnLeave();
        GameManager.Instance.CurrentPlayer.EndTurn();
    }
}
