using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Networking;

public class NetworkPlayerBehaviour : MonoBehaviour
{
    private NetworkVariable<int> myNumber = new NetworkVariable<int>(1);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(OwnerClientId + " - my number" + myNumber.Value);

        if (!IsOwner) {
            return;
        }

        if (Input.GetDownKey(KeyCode.T)) {
            myNumber.Value = myNumber.Value + 1;
        }

    }
}
