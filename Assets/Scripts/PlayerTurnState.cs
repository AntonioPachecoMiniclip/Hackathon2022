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
        gameManager.CurrentPlayerIndex = ++gameManager.CurrentPlayerIndex % players.Count;
        PlayerController currentPlayer = players[gameManager.CurrentPlayerIndex];
        for(int i=0; currentPlayer.HasFinishedTrack && i<=players.Count; i++) {
            currentPlayer = players[++gameManager.CurrentPlayerIndex];
        }

        if (gameManager.CurrentPlayerIndex >= players.Count)
            gameManager.CurrentPlayerIndex = 0;
        
        Debug.Log($"Player {gameManager.CurrentPlayerIndex} Turn Start");
        
        gameManager.Camera.SetTarget(currentPlayer.gameObject);
        currentPlayer.StartTurn();
    }

    public override void OnLeave()
    {
        base.OnLeave();
        GameManager.Instance.CurrentPlayer.EndTurn();
    }
}
