using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnState : GameState
{
    public override void Setup(GameStateMachine gameStateMachine, GameManager gameManager)
    {
        base.Setup(gameStateMachine, gameManager);
        totalDuration = 20;
    }
    
    public override void OnEnter()
    {
        base.OnEnter();
        StartPlayerTurn();
    }

    protected override void OnTimeEnded()
    {
        gameStateMachine.SetState<TurnEndState>();
    }

    private void StartPlayerTurn()
    {
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
        gameManager.players[gameManager.currentPlayerIndex].EndTurn();
    }
}
