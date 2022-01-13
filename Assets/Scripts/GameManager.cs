using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [NonSerialized]
    public int currentPlayerIndex;
    
    public List<PlayerController> players;
    
    public GameObject currentPlayer;
    
    private void Awake()
    {
        currentPlayer = GameObject.FindWithTag("Player");
    }
}
