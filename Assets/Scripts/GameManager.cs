using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SceneBoundSingletonBehaviour<GameManager>
{
    [HideInInspector]
    public int CurrentPlayerIndex;

    public CameraFollow Camera;
    
    public List<PlayerController> players;
    
    public PlayerController CurrentPlayer => players[CurrentPlayerIndex];
    
}