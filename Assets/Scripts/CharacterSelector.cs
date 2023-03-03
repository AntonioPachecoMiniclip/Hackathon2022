using TMPro;
using UnityEngine;

public class CharacterSelector : MonoBehaviour
{

    public enum LayerPosition { Front, Back };

    // Serializabele properties
    [SerializeField] private GameObject charactersContainer;
    [SerializeField] private TMPro.TextMeshProUGUI label;
    [SerializeField] public bool debugLabelIsVisible;
    [SerializeField] public float circleRadius;
    [SerializeField] public float shiftRads;
    [SerializeField] public float fieldDepth;
    [SerializeField] public float minimumScale;
    [SerializeField] public float maximumScale;

    // Private properties
    private MeshRenderer[] characters;
    private TextMeshProUGUI characterLabel;
    private TextMeshProUGUI debugLabel;
    private int numberOfElements;

    private int selectedCharacterIndex = 0;

    
    private float posX;
    private float posY;
    private float posZ;
    private float scale;

    // Start is called before the first frame update
    void Start()
    {
        characters = charactersContainer.GetComponentsInChildren<MeshRenderer>();
        characterLabel = label.GetComponentInChildren<TextMeshProUGUI>();
        numberOfElements = characters.Length;

        // Initialize the characters position
        for (int i = 0; i < numberOfElements; i++)
        {
            CalculateCharactersPosition(characters[i], i);
        }
        SnapToCharacter(selectedCharacterIndex);
        
    }

    private void Update()
    {
        for (int i = 0; i < numberOfElements; i++)
        {
            CalculateCharactersPosition(characters[i], i);
        }

        // Update the label with the selected character
        // characterLabel.text = "CHEETAH";

        // shiftRads += 0.001f;
    }

    public void characterSelectButtonCallback()
    {
        // find where to put the character identifier for the current player and set it here
        Debug.Log("characterSelectButtonCallback :: find where to put the character identifier for the current player and set it here");
        if(null != characters) {
            Debug.Log($"Number of characters: {numberOfElements}");
        }
    }

    public void characterRotateCallback()
    {
        // rotate the existing characters
        Debug.Log($"{this.name}");
        Debug.Log($"characterRotateCallback :: rotate the existing characters");
    }

    public void SnapToCharacter(int selectedCharacterIndex) {

        if (numberOfElements > 0 && selectedCharacterIndex >= 0 && selectedCharacterIndex < numberOfElements) {

        }
    }

    public void CalculateCharactersPosition(MeshRenderer character, int i)
    {

        // Initialize debug label
        debugLabel = character.GetComponentInChildren<TextMeshProUGUI>();
        debugLabel.enabled = debugLabelIsVisible;

        // Original angle
        float angle = i * Mathf.PI * 2 / numberOfElements;
        float relativeAngle = angle + Mathf.Abs(shiftRads);
        float finalAngle = Mathf.Abs(relativeAngle % Mathf.PI);
        float fullAngle = Mathf.Abs(relativeAngle % (Mathf.PI * 2));
        LayerPosition layerPosition = finalAngle >= fullAngle ? LayerPosition.Front : LayerPosition.Back;

        // Define position based on angle
        posX = Mathf.Cos(angle + Mathf.Abs(shiftRads)) * circleRadius;
        posY = -50;
        posZ = layerPosition.Equals(LayerPosition.Front) ? -1000 : -200;
        Vector3 newPos = new Vector3(posX, posY, posZ);

        // Define scale based on angle
        float deltaScale = maximumScale - minimumScale;
        float deltaMidpoint = deltaScale / 2;
        scale = layerPosition.Equals(LayerPosition.Front)? deltaMidpoint + Mathf.Sin(finalAngle) * (deltaMidpoint) : deltaMidpoint - Mathf.Sin(finalAngle) * (deltaMidpoint / fieldDepth);
        Vector3 newScale = new Vector3(scale, scale, scale);

        // Transform the character's position and scale
        character.transform.localPosition = newPos;
        character.transform.localScale = newScale;

        // Populate debug label
        if (debugLabelIsVisible)
            debugLabel.text = $"{character.name}[{i}]:\r\n" +
                $"- {finalAngle}::{fullAngle} RADs\r\n" +
                $"- Pos({newPos.x}, {newPos.y}, {newPos.y})\r\n" +
                $"- Scale({newScale.x}, {newScale.y}, {newScale.z})\r\n" +
                $"[{layerPosition}]";

        if (layerPosition.Equals(LayerPosition.Front) && finalAngle > (0.3 * Mathf.PI) && finalAngle < (0.7 * Mathf.PI))
        {
            string characterName = character.name;
            characterLabel.text = characterName;
        }
        
    } 
}
