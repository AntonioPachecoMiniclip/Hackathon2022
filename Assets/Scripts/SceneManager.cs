using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    private static GameObject canvas;

    // Start is called before the first frame update
    void Awake() {
        DontDestroyOnLoad(this);
        loadMainMenu();
        Debug.Log("SceneManager awake");
    }

    public static void loadMainMenu() {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    public static void unloadSceneToMainMenu(string scene) {
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(scene);
        if (SceneManager.canvas != null) 
        {
            SceneManager.canvas.SetActive(true);
        }
    }

    public static void loadGame() {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
    }
    
    public static void loadCredits(GameObject canvas) {
        SceneManager.canvas = canvas;
        canvas.SetActive(false);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Credits", LoadSceneMode.Additive);
    }
    
    public static void loadSettings(GameObject canvas) {
        SceneManager.canvas = canvas;
        canvas.SetActive(false);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Settings", LoadSceneMode.Additive);
    }

    public static void loadTierSelector(GameObject canvas) {
        SceneManager.canvas = canvas;
        canvas.SetActive(false);
        UnityEngine.SceneManagement.SceneManager.LoadScene("TierSelector", LoadSceneMode.Additive);
    }
}
