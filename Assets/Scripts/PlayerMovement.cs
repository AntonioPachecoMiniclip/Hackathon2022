using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float power = 10f;
    public float maxDrag = 5f;
    private Rigidbody2D rb;
    private LineRenderer lr;
    private float capPosZ;
    
    public Vector3 dragStartPos;
    public Vector3 movementStartPosition;

    public CapStats capStats;
    
    public Rigidbody2D Rb
    {
        get => rb;

        set => rb = value;
    }
    
    private void Awake()
    { 
        rb = GetComponent<Rigidbody2D>();
        lr = GetComponent<LineRenderer>();
        lr.enabled = false;

        capPosZ = transform.position.z;

        rb.mass = capStats.mass;
        rb.drag = capStats.linearDrag;
        GetComponent<CircleCollider2D>().sharedMaterial = capStats.physicsMaterial;
        maxDrag = capStats.maxDrag;
        power = capStats.power;
        transform.GetChild(0).gameObject.GetComponent<Renderer>().material = capStats.capMaterial;
    }

    public void DragStart(Vector3 touchPosition)
    {
        lr.enabled = true;

        movementStartPosition = transform.position;
        dragStartPos = Camera.main.ScreenToWorldPoint(touchPosition);
        dragStartPos.z = capPosZ;
        
        lr.positionCount = 1;
        lr.SetPosition(0, movementStartPosition);
    }

    public void Dragging(Vector3 touchPosition)
    {
        Vector3 draggingPos = Camera.main.ScreenToWorldPoint(touchPosition);
        draggingPos.z = capPosZ;

        lr.positionCount = 2;
        Vector3 newVector = Vector3.ClampMagnitude(dragStartPos - draggingPos, (maxDrag * 0.33f));
        lr.SetPosition(1, newVector + movementStartPosition);
    }

    public void DragRelease(Vector3 touchPosition)
    {
        lr.positionCount = 0;
        
        Vector3 dragReleasePos = Camera.main.ScreenToWorldPoint(touchPosition);
        dragReleasePos.z = capPosZ;

        Vector3 force = dragStartPos - dragReleasePos;
        Vector3 clampedForce = Vector3.ClampMagnitude(force, maxDrag) * power;

        rb.AddForce(clampedForce, ForceMode2D.Impulse);
        playSound();
        lr.enabled = false;
    }

    void OnCollisionEnter2D(Collision2D col) {
        if(col.otherCollider.tag == "Player") {
            playSound();
        }
    }

    void playSound() {
        FindObjectOfType<SoundManager>().playCapSound(capStats.capType);
    }
}