using UnityEngine;
using UnityEngine.UI;

public class MainMenuInput : MonoBehaviour
{
    public GameObject canvas;

    public void GoToMainMenu()
    {
        SceneManager.loadMainMenu();
    }
    
    public void playButtonCallback()
    {
        SceneManager.loadLobby(canvas);
    }

    public void characterSelectorButtonCallback()
    {
        SceneManager.loadCharacterSelector(canvas);
    }

    public void settingsCallback()
    {
        SceneManager.loadSettings(canvas);
    }
    
    public void creditCallback()
    {
        SceneManager.loadCredits(canvas);
    }

    public void inventoryCallback()
    {
        SceneManager.loadInventory(canvas);
    }

    public void createGameCallback()
    {
        RelayHelper.CreateRelay();
    }

    public void joinGameCallback()
    {
        TMPro.TMP_InputField inputField = FindObjectOfType<TMPro.TMP_InputField>();
        string joinCode = inputField.text;
        if (joinCode.Length != 6)
        {
            inputField.text = "Code is 6 characters";
        }
        else
        {
            RelayHelper.JoinRelay(inputField.text);
        }
    }
}
