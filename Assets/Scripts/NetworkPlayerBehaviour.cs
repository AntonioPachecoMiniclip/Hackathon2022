using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Networking;
using Unity.Netcode;

public class NetworkPlayerBehaviour : NetworkBehaviour
{
    private NetworkVariable<int> myNumber = new NetworkVariable<int>(1,NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

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

        if (Input.GetKeyDown(KeyCode.T)) {
            myNumber.Value = myNumber.Value + 1;
        }

    }
}
