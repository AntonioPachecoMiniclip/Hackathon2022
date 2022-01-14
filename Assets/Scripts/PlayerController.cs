using System;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerController : MonoBehaviour
{
    private const float MIN_VELOCITY_EPSILON = 0.2f;

    public static Action PlayerShoot;

    public GameObject decal;
    
    Touch touch;

    private PlayerMovement playerMovement;
    private PlayerTimer playerTimer;
    
    [HideInInspector]
    public bool IsMoving;
    
    private bool isOutOfBounds;
    private bool canMove;
    
    private bool hasFinishedTrack;
    public bool HasFinishedTrack => hasFinishedTrack;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerTimer = GetComponent<PlayerTimer>();
    }
    
    public void StartTurn(float duration)
    {
        canMove = true;
        decal.SetActive(true);
        playerTimer.startTimer(duration);
    }
    
    public void EndTurn()
    {
        playerMovement.Lr.positionCount = 0;
        playerMovement.Lr.enabled = false;
        playerTimer.endTimer();
        DisablePlayer();
    }

    public void OnEndTrack() 
    {
        Debug.Log("Finished track! " + gameObject.name);
        hasFinishedTrack = true;
        GameManager.Instance.SetPlayerFinished(this);
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

            if (touch.phase == TouchPhase.Began)
            {
                playerMovement.DragStart(touch.position);
            }

            if (touch.phase == TouchPhase.Moved)
            {
                playerMovement.Dragging(touch.position);
            }

            if (touch.phase == TouchPhase.Ended)
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

            if (Input.GetMouseButtonUp(0))
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
        DisablePlayer();
        IsMoving = true;
        PlayerShoot.Invoke();
    }

    private void DisablePlayer()
    {
        canMove = false;
        decal.SetActive(false);
    }
}
