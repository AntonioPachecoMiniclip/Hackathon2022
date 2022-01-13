using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerController : MonoBehaviour
{
    private const float MIN_VELOCITY_EPSILON = 0.2f;
    
    Touch touch;

    private PlayerMovement playerMovement;

    private bool isMoving;
    private bool isOutOfBounds;

    private bool canMove;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }
    
    public void StartTurn()
    {
        canMove = true;
    }
    
    public void EndTurn()
    {
        canMove = false;
    }
    
    private void Update()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began || Input.GetMouseButtonDown(0))
            {
                playerMovement.DragStart(touch.position);
            }

            if (touch.phase == TouchPhase.Moved)
            {
                playerMovement.Dragging(touch.position);
            }

            if (touch.phase == TouchPhase.Ended || Input.GetMouseButtonUp(0))
            {
                playerMovement.DragRelease(touch.position);
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                playerMovement.MouseDragStart(Input.mousePosition);
            }

            playerMovement.MouseDragging(Input.mousePosition);

            if (touch.phase == TouchPhase.Ended || Input.GetMouseButtonUp(0))
            {
                playerMovement.MouseDragRelease(Input.mousePosition);
            }
        }

        isMoving = playerMovement.rb.velocity.magnitude > MIN_VELOCITY_EPSILON;
        
        if (isOutOfBounds && !isMoving)
        {
            transform.position = playerMovement.dragStartPos;
            isOutOfBounds = false;
            playerMovement.rb.velocity = Vector2.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Map"))
        {
            isOutOfBounds = true;
        }
    }

    void MouseDragStart() {
        mouseStartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseStartPos.z = 0f;
        
        //lr.positionCount = 1;
        //lr.SetPosition(0, dragStartPos);
    }

    void MouseDragging() {
        Vector3 draggingPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        draggingPos.z = 0f;

        //lr.positionCount = 2;
        //lr.SetPosition(1, draggingPos);
    }

    void MouseDragRelease() {
        //lr.positionCount = 0;

        Vector3 dragReleasePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dragReleasePos.z = 0f;

        Vector3 force = mouseStartPos - dragReleasePos;
        //Vector3 clampedForce = Vector3.ClampMagnitude(force, maxDrag) * power;

        rb.AddForce(force, ForceMode2D.Impulse);
    }

    void DragStart() {
        dragStartPos = Camera.main.ScreenToWorldPoint(touch.position);
        dragStartPos.z = 0f;
        
        lr.positionCount = 1;
        lr.SetPosition(0, dragStartPos);
    }

    void Dragging() {
        Vector3 draggingPos = Camera.main.ScreenToWorldPoint(touch.position);
        draggingPos.z = 0f;

        lr.positionCount = 2;
        lr.SetPosition(1, draggingPos);
    }

    void DragRelease() {
        lr.positionCount = 0;

        Vector3 dragReleasePos = Camera.main.ScreenToWorldPoint(touch.position);
        dragReleasePos.z = 0f;

        Vector3 force = dragStartPos - dragReleasePos;
        Vector3 clampedForce = Vector3.ClampMagnitude(force, maxDrag) * power;


        rb.AddForce(clampedForce, ForceMode2D.Impulse);
    }
}
