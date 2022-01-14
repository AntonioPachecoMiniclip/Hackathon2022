using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class SettingsInput : MonoBehaviour
{


   

    [SerializeField] Color activeColor;
    Color inactiveColor = new Color(0, 0, 0, 255);

    // Start is called before the first frame update
    void Start()
    {
 //       soundON.color = 0 == AudioListener.volume ? inactiveColor : activeColor;
 //       soundOFF.color = 0 == AudioListener.volume ? activeColor : inactiveColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
