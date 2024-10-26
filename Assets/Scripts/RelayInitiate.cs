using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine;
using UnityEngine.UI;

public class RelayInitiate : MonoBehaviour
{
    [SerializeField] private TMP_Text lobbyCodeText;
    [SerializeField] private Button createGameButton;
    [SerializeField] private Button joinGameButton;
    [SerializeField] private GameObject networkUI;
    [SerializeField] private TMP_Text connectingText;

    private async void Start() {
        await UnityServices.InitializeAsync();

        AuthenticationService.Instance.SignedIn += () => {
            Debug.Log("Signed in " + AuthenticationService.Instance.PlayerId);
        };
        await AuthenticationService.Instance.SignInAnonymouslyAsync();

        createGameButton.interactable = true;
        joinGameButton.interactable = true;
    }

    public async void CreateRelay(){
        try{
            connectingText.text = "CONNECTING...";
            Allocation allocation = await RelayService.Instance.CreateAllocationAsync(1); // inisialisasi relay untuk brp player (ini berarti 1 client dan 1 host)

            string joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId); // Untuk dapetin kode join

            Debug.Log(joinCode);

            RelayServerData relayServerData = new RelayServerData(allocation, "dtls");

            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);

            NetworkManager.Singleton.StartHost();

            lobbyCodeText.text = "Lobby Code: " + joinCode; // Menampilkan kode join di UI

            connectingText.text = "";
        } catch (RelayServiceException e){
            Debug.Log(e);
            networkUI.SetActive(true);
        }
    }

    public async void JoinRelay(string joinCode){
        Debug.Log(joinCode);
        try{
            connectingText.text = "CONNECTING...";
            JoinAllocation joinAllocation = await RelayService.Instance.JoinAllocationAsync(joinCode);

            RelayServerData relayServerData = new RelayServerData(joinAllocation, "dtls");

            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);

            NetworkManager.Singleton.StartClient();

            lobbyCodeText.text = "Lobby Code: " + joinCode; // Menampilkan kode join di UI
            
            connectingText.text = "";
        } catch(RelayServiceException e){
            Debug.Log(e);
            networkUI.SetActive(true);
        }
    }
}