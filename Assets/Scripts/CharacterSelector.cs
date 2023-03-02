using TMPro;
using UnityEngine;

public class CharacterSelector : MonoBehaviour
{

    public enum LayerPosition { Front, Back };

    // Serializabele properties
    [SerializeField] private GameObject CharactersContainer;
    [SerializeField] public bool debugLabelIsVisible;
    [SerializeField] public float circleRadius;
    [SerializeField] public float shiftRads;
    [SerializeField] public float fieldDepth;
    [SerializeField] public float minimumScale;
    [SerializeField] public float maximumScale;

    // Private properties
    private MeshRenderer[] characters;
    private TextMeshProUGUI debugLabel;
    public int numberOfElements;

    
    private float posX;
    private float posY;
    private float posZ;
    private float scale;

    // Start is called before the first frame update
    void Start()
    {
        characters = CharactersContainer.GetComponentsInChildren<MeshRenderer>();
        numberOfElements = characters.Length;
        circleRadius = 250f;
        shiftRads = 0f;
        fieldDepth = 2f;
        minimumScale = 500000f;
        maximumScale = 1500000f;
    }

    private void Update()
    {
        for (int i = 0; i < numberOfElements; i++)
        {
            CalculateCharactersPosition(characters[i], i);
        }

        shiftRads += 0.001f;
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
        Debug.Log("characterRotateCallback :: rotate the existing characters");
        if (null != characters)
        {
            Debug.Log($"Number of characters: {numberOfElements}");
        }
    }

    public void CalculateCharactersPosition(MeshRenderer character, int i)
    {
        // Initialize debug label
        debugLabel = character.GetComponentInChildren<TextMeshProUGUI>();
        debugLabel.enabled = debugLabelIsVisible;

        if(debugLabelIsVisible)
            Debug.Log($"Character affected :: {character.name}");

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
        if (debugLabelIsVisible)
            Debug.Log($"POS::Character[{i}] == Vector3({newPos.x}, {newPos.y}, {newPos.z})");


        // Define scale based on angle
        float deltaScale = maximumScale - minimumScale;
        float deltaMidpoint = deltaScale / 2;
        scale = layerPosition.Equals(LayerPosition.Front)? deltaMidpoint + Mathf.Sin(finalAngle) * (deltaMidpoint) : deltaMidpoint - Mathf.Sin(finalAngle) * (deltaMidpoint / fieldDepth);
        Vector3 newScale = new Vector3(scale, scale, scale);
        if (debugLabelIsVisible)
            Debug.Log($"SCALE::Character[{i}] == Vector3({newScale.x}, {newScale.y}, {newScale.z})");

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
    }
}
