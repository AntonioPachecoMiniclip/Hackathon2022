using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnState : GameState
{
    public override void Setup(GameStateMachine gameStateMachine)
    {
        base.Setup(gameStateMachine);
        totalDuration = 20;
    }
    
    public override void OnEnter()
    {
        StartPlayerTurn();
    }

    protected override void OnTimeEnded()
    {
        gameStateMachine.SetState<TurnEndState>();
    }

    private void StartPlayerTurn()
    {
        //List<Player> players = gameStateMachine.players;
        gameStateMachine.currentPlayerIndex++;
        if (gameStateMachine.currentPlayerIndex >= gameStateMachine.MaxPlayers)
            gameStateMachine.currentPlayerIndex = 0;
        
        Debug.Log($"Player {gameStateMachine.currentPlayerIndex} Turn Start");
        //players[gameStateMachine.currentPlayerIndex].StartTurn();
    }
}
