using System;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerController : MonoBehaviour
{
    private const float MIN_VELOCITY_EPSILON = 0.2f;

    public static Action PlayerShoot;

    Touch touch;
    public LineRenderer lr;

    private PlayerMovement playerMovement;

    private bool isMoving;
    private bool isOutOfBounds;

    private bool canMove;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        lr = GetComponent<LineRenderer>();
    }
    
    public void StartTurn()
    {
        canMove = true;
    }
    
    public void EndTurn()
    {
        canMove = false;
        lr.positionCount = 0;
        lr.enabled = false;
    }
    
    private void Update()
    {
        if (!canMove) 
        {
            return;
        }

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
                Shoot();
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                playerMovement.DragStart(Input.mousePosition);
            }

            playerMovement.Dragging(Input.mousePosition);

            if (touch.phase == TouchPhase.Ended || Input.GetMouseButtonUp(0))
            {
                playerMovement.DragRelease(Input.mousePosition);
                Shoot();
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
    
    private void Shoot()
    {
        PlayerShoot.Invoke();
    }
}
