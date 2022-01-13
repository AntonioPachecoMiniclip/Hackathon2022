using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private GameManager gameManager;
    public float speed;
    
    private GameObject targetPlayer;
    private InterpolatedTransform interpolatedTransform;
    private InterpolatedTransformUpdater interpolatedTransformUpdater;
    
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        targetPlayer = gameManager.currentPlayer;
        var transform1 = transform;
        var position = targetPlayer.transform.position;
        transform1.position = new Vector3(position.x, position.y, transform1.position.z);
        interpolatedTransform = GetComponent<InterpolatedTransform>();
        interpolatedTransformUpdater = GetComponent<InterpolatedTransformUpdater>();
        interpolatedTransform.enabled = true;
        interpolatedTransformUpdater.enabled = true;
    }

    void FixedUpdate()
    {
        Vector3 targetPosition = targetPlayer.transform.position;
        var position = transform.position;
        position = Vector3.Lerp(
            position,
            new Vector3(targetPosition.x, targetPosition.y, position.z),
            speed * Time.deltaTime
        );
        transform.position = position;
    }

}
