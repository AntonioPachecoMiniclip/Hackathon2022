using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [NonSerialized]
    public int currentPlayerIndex;

    public CameraFollow Camera;
    
    public List<PlayerController> players;
    
    public PlayerController CurrentPlayer => players[currentPlayerIndex];
    
}
