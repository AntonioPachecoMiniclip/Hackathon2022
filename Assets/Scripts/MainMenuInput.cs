using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuInput : MonoBehaviour
{
    public GameObject canvas;

    public GameObject codeText;

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
        RelayHelper.CreateRelay(codeText.GetComponent<TMP_Text>());
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
            RelayHelper.JoinRelay(inputField.text, codeText.GetComponent<TMP_Text>());
        }
    }

    public void enterGameCallback()
    {
        SceneManager.loadGame();
    }
}
