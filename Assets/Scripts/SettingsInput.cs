using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsInput : MonoBehaviour
{

    public void exitButtonCallback() {
        SceneManager.loadMainMenu();
    }
}
