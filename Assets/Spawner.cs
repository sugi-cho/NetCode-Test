using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Spawner : NetworkBehaviour
{
    [SerializeField] NetworkVariable<int> test;

    private void Start()
    {
        var manager = NetworkManager.Singleton;

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsServer)
        {
            ShowMessageClientRPC();
            test.Value = 15;
        }
    }

    [ClientRpc]
    void ShowMessageClientRPC()
    {
        Debug.Log("show");
    }

}
