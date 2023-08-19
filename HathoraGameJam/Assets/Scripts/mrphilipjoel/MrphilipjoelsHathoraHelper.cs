using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;

public class MrphilipjoelsHathoraHelper : MonoBehaviour
{
    public static MrphilipjoelsHathoraHelper instance;
    public NetworkManager networkManager;
    private UnityTransport unityTransport;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        if (unityTransport == null)
        {
            unityTransport = networkManager.GetComponent<UnityTransport>();
        }
    }

    public void SetUpTransport(string address, ushort port)
    {
        
    }

}
