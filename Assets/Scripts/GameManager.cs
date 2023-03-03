using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SceneBoundSingletonBehaviour<GameManager>
{
    private int localPlayerIndex = 0;

    public CameraFollow Camera;
    [SerializeField]
    private GameResultsUI gameResultsUI;

    public List<PlayerController> players;
    public List<PlayerController> FinishedPlayers;

    public PlayerController localPlayer => players[localPlayerIndex];

    private void Start()
    {
        FinishedPlayers = new List<PlayerController>(players.Count);
    }

    public void ShowGameResults()
    {
        gameResultsUI.gameObject.SetActive(true);
        gameResultsUI.Setup(FinishedPlayers);
        FindObjectOfType<SoundManager>().playCheer();
    }

    public void SetPlayerFinished(PlayerController playerController)
    {
        if (FinishedPlayers.Contains(playerController))
            return;

        FinishedPlayers.Add(playerController);

        if (FinishedPlayers.Count == players.Count - 1)
        {
            for (int i = 0; i < players.Count; i++)
            {
                if (!FinishedPlayers.Contains(players[i]))
                    FinishedPlayers.Add(players[i]);
            }
        }
    }

    public bool IsAnyPlayerMoving()
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].IsMoving)
                return true;
        }

        return false;
    }

    public void RunCallbackWhenAllPlayerStopMoving(Action callback)
    {
        StartCoroutine(nameof(CheckIfAllPlayersStopped), callback);
    }

    private IEnumerator CheckIfAllPlayersStopped(Action callback)
    {
        yield return new WaitForSeconds(1.0f);
        while (IsAnyPlayerMoving())
        {
            yield return new WaitForSeconds(0.2f);
        }

        callback();
    }

    public void PlayShots()
    {
        for (int i = 0; i < players.Count; i++)
        {
            PlayerController player = players[i];
            player.playQeuedShot();
            //if (player.IsReadyToShoot() && !FinishedPlayers.Contains(player))
            //{
            //    player.playQeuedShot();
            //}
        }
    }

    public bool AllPlayersReadyToShoot() {
        for (int i = 0; i < players.Count; i++)
        {
            if (!players[i].IsReadyToShoot() && !FinishedPlayers.Contains(players[i]))
            {
                return false;
            }
        }
        return true;
    }
}