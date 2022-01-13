using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col) {
        PlayerController controller = col.GetComponent<PlayerController>();
        if(controller != null) {
            controller.OnEndTrack();
        }
    }
}
