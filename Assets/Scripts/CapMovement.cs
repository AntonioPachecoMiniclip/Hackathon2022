using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapMovement : MonoBehaviour
{
    public Vector3 movementVector;
    public float speed = 5f;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + movementVector, speed * Time.fixedDeltaTime);
    }
}
