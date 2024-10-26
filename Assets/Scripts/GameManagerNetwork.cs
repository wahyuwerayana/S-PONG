using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class GameManagerNetwork : NetworkBehaviour
{
    public string charL, charR;
    public TMP_Text playerSide;
    public GameObject startPanel;
    public GameObject endPanel;
    public NetworkVariable<int> playerScoreLServer = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    public NetworkVariable<int> playerScoreRServer = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    private NetworkVariable<bool> hasSpawned = new NetworkVariable<bool>(false);
    private int playerScoreL = 0;
    private int playerScoreR = 0;
    public const int SCORE = 100;

    public TMP_Text txtPlayerScoreL;
    public TMP_Text txtPlayerScoreR;

    [SerializeField] private GameObject ballPrefab;

    public static GameManagerNetwork instance;
    public void Awake(){
        if(instance == null){
            instance = this;
        } else{
            Destroy(gameObject);
        }
    }

    void Start()
    {
        startPanel.SetActive(true);
        txtPlayerScoreL.text = playerScoreL.ToString();
        txtPlayerScoreR.text = playerScoreR.ToString();
        playerScoreLServer.OnValueChanged += OnScoreLChanged;
        playerScoreRServer.OnValueChanged += OnScoreRChanged;
    }

    [Rpc(SendTo.Server)]
    private void SpawnBallRpc(){
        GameObject ball = Instantiate(ballPrefab);
        ball.GetComponent<NetworkObject>().Spawn(true);
    }

    IEnumerator SpawnBallCountdown(){
        yield return new WaitForSeconds(2f);
        SpawnBallRpc();
    }

    private void OnScoreLChanged(int previous, int current){
        txtPlayerScoreL.text = current.ToString();
        ScoreCheckRpc();
    }

    private void OnScoreRChanged(int previous, int current){
        txtPlayerScoreR.text = current.ToString();
        ScoreCheckRpc();
    }

    private void FixedUpdate() {
        if(IsServer){
            Debug.Log("OK");
            if(NetworkManager.Singleton.ConnectedClientsList.Count >= 2 && !hasSpawned.Value){
                StartCoroutine(SpawnBallCountdown());
                hasSpawned.Value = true;
            }
        }
    }

    [ServerRpc]
    public void ScoreServerRpc(string wallID){
        if(wallID == "Line L"){
            playerScoreRServer.Value += 10;
        } else if(wallID == "Line R"){
            playerScoreLServer.Value += 10;
        }
    }

    public void ChangetoMenu(){
        NetworkManager.Singleton.Shutdown();
        this.gameObject.SendMessage("ChangeScene", "MainMenu");
    }

    [Rpc(SendTo.Everyone)]
    public void ScoreCheckRpc(){
        if(playerScoreLServer.Value == SCORE){
            playerSide.text = charL;
            endPanel.SetActive(true);
        } else if(playerScoreRServer.Value == SCORE){
            playerSide.text = charR;
            endPanel.SetActive(true);
        }

        if((playerScoreLServer.Value == SCORE) || (playerScoreRServer.Value == SCORE)){
            Invoke("ChangetoMenu", 3);
        }
    }
}
