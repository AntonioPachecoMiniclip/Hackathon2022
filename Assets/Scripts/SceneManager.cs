using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    private static GameObject canvasMainMenu;
    private static GameObject canvasSettings;

    void Awake() {
        DontDestroyOnLoad(this);
        loadMainMenu();
        RelayHelper.Start();
        Debug.Log("SceneManager awake");
    }

    public static void loadMainMenu() {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    public static void unloadSceneToMainMenu(string scene) {
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(scene);
        if (scene == "Credits" && SceneManager.canvasSettings != null) 
        {
            SceneManager.canvasSettings.SetActive(true);
        }
        else
        {
            if (SceneManager.canvasMainMenu != null) 
            {
                SceneManager.canvasMainMenu.SetActive(true);
            }

        }
    }

    public static void loadGame() {
        UnityEngine.SceneManagement.SceneManager.LoadScene("FinalLevel");
    }
    
    public static void loadCredits(GameObject canvas) {
        SceneManager.canvasSettings = canvas;
        canvas.SetActive(false);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Credits", LoadSceneMode.Additive);
    }
    
    public static void loadSettings(GameObject canvas) {
        SceneManager.canvasMainMenu = canvas;
        canvas.SetActive(false);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Settings", LoadSceneMode.Additive);
    }

    public static void loadTierSelector(GameObject canvas) {
        SceneManager.canvasMainMenu = canvas;
        canvas.SetActive(false);
        UnityEngine.SceneManagement.SceneManager.LoadScene("TierSelector", LoadSceneMode.Additive);
    }

    public static void loadLobby(GameObject canvas)
    {
        SceneManager.canvasMainMenu = canvas;
        canvas.SetActive(false);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Lobby", LoadSceneMode.Additive);
    }

    public static void loadInventory(GameObject canvas) {
        SceneManager.canvasMainMenu = canvas;
        canvas.SetActive(false);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Inventory", LoadSceneMode.Additive);
    }
}
