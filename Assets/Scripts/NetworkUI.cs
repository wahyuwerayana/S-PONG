using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using TMPro;

public class NetworkUI : MonoBehaviour
{
    [SerializeField] private Button hostButton;
    [SerializeField] private Button clientButton;

    [SerializeField] private TMP_InputField joinCodeInputField;

    private void Awake() {
        hostButton.onClick.AddListener(() => {
            NetworkManager.Singleton.GetComponent<RelayInitiate>().CreateRelay();
        });

        clientButton.onClick.AddListener(() => {
            NetworkManager.Singleton.GetComponent<RelayInitiate>().JoinRelay(joinCodeInputField.text.ToString());
        });
    }    
}