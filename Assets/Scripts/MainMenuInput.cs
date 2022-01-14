using UnityEngine;

public class MainMenuInput : MonoBehaviour
{
    public void GoToMainMenu()
    {
        SceneManager.loadMainMenu();
    }
    
    public void playButtonCallback()
    {
        SceneManager.loadTierSelector();
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
