using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float power = 10f;
    public float maxDrag = 5f;
    public Rigidbody2D rb;
    public LineRenderer lr;

    Vector3 dragStartPos;
    Touch touch;

    Vector3 mouseStartPos;

    private bool canMove;
    
    public void StartTurn()
    {
        canMove = true;
    }
    
    public void EndTurn()
    {
        canMove = false;
    }
    
    private void Update() {
        if (Input.touchCount > 0) {
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began) {
                DragStart();
            }

            if (touch.phase == TouchPhase.Moved) {
                Dragging();
            }

            if (touch.phase == TouchPhase.Ended) {
                DragRelease();
            }
        } else {
            if (Input.GetMouseButtonDown(0)) {
                MouseDragStart();
            }

            MouseDragging();

            if (Input.GetMouseButtonUp(0)) {
                MouseDragRelease();
            }
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
