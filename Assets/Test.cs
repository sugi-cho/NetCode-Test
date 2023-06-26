using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Collections;
using System.Runtime.InteropServices;

public class Test : NetworkBehaviour
{
    [SerializeField] Data data;
    [SerializeField] NetworkVariable<Data> networkData;
    [SerializeField] NetworkVariable<int> i;
    [SerializeField] string text;

    [SerializeField] FixedList128Bytes<Data> dataList;
    private void OnEnable()
    {
        var manager = NetworkManager.Singleton;
        Debug.Log(Marshal.SizeOf(typeof(Data)));
        Debug.Log(dataList.Capacity);
    }

    private void Update()
    {
        if (IsServer && IsOwner && Input.GetKeyDown(KeyCode.Space))
        {
            SetDataClientRPC(data);
            var val = networkData.Value;
            val.val2 = new FixedString32Bytes(text);
            networkData.Value = val;
            i.Value++;
        }
        if (!IsOwner)
            text = networkData.Value.val2.Value;
    }

    [ClientRpc]
    void SetDataClientRPC(Data data, ClientRpcParams clientRpcParams = default)
    {
        this.data = data;
    }

    [System.Serializable]
    public struct Data : INetworkSerializeByMemcpy
    {
        public int val0;
        public int val1;
        public FixedString32Bytes val2;
    }
}
