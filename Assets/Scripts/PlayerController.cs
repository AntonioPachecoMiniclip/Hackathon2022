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
    
    [HideInInspector]
    public bool IsMoving;
    
    private bool isOutOfBounds;
    private bool canMove;
    
    private bool hasFinishedTrack;
    public bool HasFinishedTrack => hasFinishedTrack;

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

    public void OnEndTrack() 
    {
        Debug.Log("Finished track! " + gameObject.name);
        hasFinishedTrack = true;
    }
    
    private void Update()
    {
        if (canMove) 
        {
            CheckInput();
        }

        IsMoving = playerMovement.Rb.velocity.magnitude > MIN_VELOCITY_EPSILON;
        
        if (isOutOfBounds && !IsMoving)
        {
            transform.position = playerMovement.movementStartPosition;
            isOutOfBounds = false;
            playerMovement.Rb.velocity = Vector2.zero;
        }
    }

    private void CheckInput()
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
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Map"))
        {
            isOutOfBounds = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Map"))
        {
            isOutOfBounds = false;
        }
	}
    
    private void Shoot()
    {
        PlayerShoot.Invoke();
    }
}
