using UnityEngine;

public class MainMenuInput : MonoBehaviour
{
    public void GoToMainMenu()
    {
        SceneManager.loadMainMenu();
    }
    
    public void playButtonCallback()
    {
        SceneManager.loadGame();
    }
    
    public void settingsCallback()
    {
        SceneManager.loadSettings();
    }
    
    public void creditCallback()
    {
        SceneManager.loadCredits();
    }
}
