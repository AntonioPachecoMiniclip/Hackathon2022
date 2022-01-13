using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuInput : MonoBehaviour
{
    public void playButtonCallback() {
        SceneManager.loadGame();
    }
    public void settingsCallback() {
        SceneManager.loadSettings();
    }
    public void creditCallback() {
        SceneManager.loadCredits();
    }
}
