using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject currentPlayer;

    private void Awake()
    {
        currentPlayer = GameObject.FindWithTag("Player");
    }
}
