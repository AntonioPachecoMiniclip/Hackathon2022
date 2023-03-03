using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public struct PlayerSettings
{
    public PlayerSettings(int characterIndex)
    {
        this.characterIndex = characterIndex;
    }
    public int characterIndex { get; }
}

public class GameManager : SceneBoundSingletonBehaviour<GameManager>
{
    public static Dictionary<ulong, NetworkPlayerBehaviour> networkPlayers;
    private int localPlayerIndex = 0;

    public CameraFollow Camera;
    [SerializeField]
    private GameResultsUI gameResultsUI;

    public List<PlayerController> players;
    public List<PlayerController> FinishedPlayers;

    public PlayerController localPlayer => players[localPlayerIndex];

    private void Start()
    {
        networkPlayers = new Dictionary<ulong, NetworkPlayerBehaviour>();
        if (NetworkManager.Singleton != null && NetworkManager.Singleton.IsListening)
        {
            foreach (NetworkPlayerBehaviour networkPlayerBehaviour in FindObjectsOfType<NetworkPlayerBehaviour>())
            {
                networkPlayers.Add(networkPlayerBehaviour.OwnerClientId, networkPlayerBehaviour);
            }

            if (networkPlayers.Count > players.Count)
            {
                Debug.LogError("Player Settings don't match number of player controllers");
                return;
            }

            int i = 0;
            foreach (NetworkPlayerBehaviour n in networkPlayers.Values)
            {
                if (n.IsOwner)
                {
                    localPlayerIndex = i;
                }
                players[i].SetupWithCharacterIndex(n.getCharacterIndex());
                players[i].SetNetworkPlayerId(n.OwnerClientId);
                i++;
            }
            for (int j = players.Count - 1; j >= i; j--)
            {
                GameObject.Destroy(players[j].gameObject);
                players.RemoveAt(j);
            }
        }
        else
        {
            Debug.Log("Fallback to offline players");
            for(int i=0; i < players.Count; i++)
            {
                players[i].SetupWithCharacterIndex(0);
            }
        }

        FinishedPlayers = new List<PlayerController>(players.Count);
    }

    public void OnShotInputUpdated(Vector3 previousValue, Vector3 newValue, ulong networkPlayerId)
    {
        foreach (PlayerController p in players)
        {
            if (p.networkPlayerId == networkPlayerId)
            {
                p.ReceivedShotInput(newValue);
            }
        }
    }

    public NetworkPlayerBehaviour getNetworkPlayerForId(ulong ownerClientId)
    {
        NetworkPlayerBehaviour n;
        networkPlayers.TryGetValue(ownerClientId, out n);
        return n;
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

    public bool AllPlayersReadyToShoot()
    {
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