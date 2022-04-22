using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.Netcode;
using Cysharp.Threading.Tasks;

public class Reconnect : MonoBehaviour
{
    [SerializeField] bool isHost;

    private void Start()
    {
        var networkManager = NetworkManager.Singleton;

        if (isHost)
            networkManager.StartHost();
        else
            networkManager.StartClient();

        ReconnectAsync().Forget();

        async UniTask ReconnectAsync()
        {
            while (true)
            {
                await UniTask.Delay(5000);
                if (!isHost && !networkManager.IsConnectedClient)
                {
                    Debug.Log("disconnected");
                    networkManager.StartClient();
                }
            }
        }
    }
}
