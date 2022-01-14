using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake() {
        DontDestroyOnLoad(this);
        loadMainMenu();
    }

    public static void loadMainMenu() {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    public static void loadGame() {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
    }
    
    public static void loadCredits() {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Credits");
    }
    
    public static void loadSettings() {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Settings");
    }

    public static void loadTierSelector() {
        UnityEngine.SceneManagement.SceneManager.LoadScene("TierSelector");
    }
}
