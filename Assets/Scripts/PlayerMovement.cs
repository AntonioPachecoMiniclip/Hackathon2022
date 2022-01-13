using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float power = 10f;
    public float maxDrag = 5f;
    public Rigidbody2D rb;

    public Vector3 dragStartPos;

    private void Awake()
    { 
        rb = GetComponent<Rigidbody2D>();
    }

    public void MouseDragStart(Vector3 touchPosition)
    {
        dragStartPos = Camera.main.ScreenToWorldPoint(touchPosition);
        dragStartPos.z = 0f;
    }

    public void MouseDragging(Vector3 touchPosition)
    {
        Vector3 draggingPos = Camera.main.ScreenToWorldPoint(touchPosition);
        draggingPos.z = 0f;
    }

    public void MouseDragRelease(Vector3 touchPosition)
    {
        Vector3 dragReleasePos = Camera.main.ScreenToWorldPoint(touchPosition);
        dragReleasePos.z = 0f;

        Vector3 force = dragStartPos - dragReleasePos;

        rb.AddForce(force, ForceMode2D.Impulse);
    }

    public void DragStart(Vector3 touchPosition)
    {
        dragStartPos = Camera.main.ScreenToWorldPoint(touchPosition);
        dragStartPos.z = 0f;
    }

    public void Dragging(Vector3 touchPosition)
    {
        Vector3 draggingPos = Camera.main.ScreenToWorldPoint(touchPosition);
        draggingPos.z = 0f;
    }

    public void DragRelease(Vector3 touchPosition)
    {
        Vector3 dragReleasePos = Camera.main.ScreenToWorldPoint(touchPosition);
        dragReleasePos.z = 0f;

        Vector3 force = dragStartPos - dragReleasePos;

        rb.AddForce(force, ForceMode2D.Impulse);
    }
}