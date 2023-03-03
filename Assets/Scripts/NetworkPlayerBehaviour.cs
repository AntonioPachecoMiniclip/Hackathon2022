using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Networking;
using Unity.Netcode;

public class NetworkPlayerBehaviour : NetworkBehaviour
{
    private NetworkVariable<int> characterIndex = new NetworkVariable<int>(1,NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    private NetworkVariable<Vector3> shotInput = new NetworkVariable<Vector3>(Vector3.zero, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void setShotInput(Vector3 shotInput)
    {
        if (IsOwner)
            this.shotInput.Value = shotInput;
    }
    public void resetShotInput()
    {
        if(IsOwner)
            shotInput.Value = Vector3.zero;
    }

    public void setCharacterIndex(int characterIndex)
    {
        if (IsOwner)
            this.characterIndex.Value = characterIndex;
    }
    public int getCharacterIndex()
    {
        return characterIndex.Value;
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        shotInput.OnValueChanged += OnShotInputChanged;
    }

    void OnShotInputChanged(Vector3 previousValue, Vector3 newValue)
    {
        if (newValue.sqrMagnitude > float.Epsilon) {
            GameManager.Instance.OnShotInputUpdated(previousValue, newValue, OwnerClientId);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(OwnerClientId + " - my number" + characterIndex.Value);

        if (!IsOwner) {
            return;
        }

        if (Input.GetKeyDown(KeyCode.T)) {
            characterIndex.Value = (characterIndex.Value + 1) % 3;
        }

    }
}
