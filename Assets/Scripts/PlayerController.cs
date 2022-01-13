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
}
