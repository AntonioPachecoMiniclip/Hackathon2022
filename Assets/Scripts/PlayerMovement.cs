using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float power = 10f;
    public float maxDrag = 5f;
    private Rigidbody2D rb;
    private LineRenderer lr;
    
    public Vector3 dragStartPos;
    public Vector3 movementStartPosition;
    
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
    }

    public void DragStart(Vector3 touchPosition)
    {
        lr.enabled = true;

        movementStartPosition = transform.position;
        dragStartPos = Camera.main.ScreenToWorldPoint(touchPosition);
        dragStartPos.z = 0f;
        
        lr.positionCount = 1;
        lr.SetPosition(0, dragStartPos);
    }

    public void Dragging(Vector3 touchPosition)
    {
        Vector3 draggingPos = Camera.main.ScreenToWorldPoint(touchPosition);
        draggingPos.z = 0f;

        lr.positionCount = 2;
        lr.SetPosition(1, draggingPos);
    }

    public void DragRelease(Vector3 touchPosition)
    {
        lr.positionCount = 0;
        
        Vector3 dragReleasePos = Camera.main.ScreenToWorldPoint(touchPosition);
        dragReleasePos.z = 0f;

        Vector3 force = dragStartPos - dragReleasePos;
        Vector3 clampedForce = Vector3.ClampMagnitude(force, maxDrag) * power;

        rb.AddForce(clampedForce, ForceMode2D.Impulse);
        lr.enabled = false;
    }
}