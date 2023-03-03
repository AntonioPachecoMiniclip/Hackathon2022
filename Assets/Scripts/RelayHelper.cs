using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;
using Unity.Networking.Transport;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using Unity.Services.Authentication;
using TMPro;

public class RelayHelper : MonoBehaviour
{
    static Allocation serverAllocation = null;
    public static string joinCode = "";
    
    public static async void Start() {
        await UnityServices.InitializeAsync();

        AuthenticationService.Instance.SignedIn += () =>
        {
            Debug.Log("Signed in " + AuthenticationService.Instance.PlayerId);
        };

        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }

    public static async void CreateRelay(TMP_Text textMesh)
    {
        if(NetworkManager.Singleton.IsHost)
        {
            return;
        }
        try {
            serverAllocation = await RelayService.Instance.CreateAllocationAsync(3);

            joinCode = await RelayService.Instance.GetJoinCodeAsync(serverAllocation.AllocationId);
            Debug.Log(joinCode);
            textMesh.SetText(joinCode);

            RelayServerData relayServerData = new RelayServerData(serverAllocation, "dtls");
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);
            NetworkManager.Singleton.StartHost();
        } catch (RelayServiceException e) {
            Debug.Log(e);
        }
    }

    public static NetworkDriver CreateServerDriver()
    {
        // Extract the Relay server data from the Join Allocation response.
        var relayServerData = new RelayServerData(serverAllocation, "dtls");

        // Create NetworkSettings using the Relay server data.
        var settings = new NetworkSettings();
        settings.WithRelayParameters(ref relayServerData);

        // Create the Player's NetworkDriver from the NetworkSettings object.
        return NetworkDriver.Create(settings);
    }

    public static async void JoinRelay(string joinCode, TMP_Text textMesh)
    {
        try {
            JoinAllocation joinAllocation = await RelayService.Instance.JoinAllocationAsync(joinCode);

            RelayServerData relayServerData = new RelayServerData(joinAllocation, "dtls");
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);
            NetworkManager.Singleton.StartClient();

            Debug.Log(" " + NetworkManager.Singleton.IsListening);

            textMesh.SetText("Joined");
        } catch (RelayServiceException e) {
            Debug.Log(e);
            textMesh.SetText("Error");
        }
    }
}
