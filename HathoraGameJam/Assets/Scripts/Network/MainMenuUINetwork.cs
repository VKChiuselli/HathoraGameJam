using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class MainMenuUINetwork : NetworkBehaviour
{
    public void StartLocalHosting()
    {
        NetworkManager.Singleton.StartHost();

        //   InitializePLayerAndCameraServerRpc();

        Hide();
    }

    public void StartLocalServer()
    {
        NetworkManager.Singleton.StartServer();
        Hide();
    }
    public void StartLocalClient()
    {


        NetworkManager.Singleton.StartClient();

     


        Hide();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

     
}
