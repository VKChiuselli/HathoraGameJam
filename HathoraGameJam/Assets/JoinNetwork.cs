using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using TMPro;
using Unity.Netcode.Transports.UTP;
using System;
using System.Net;
using System.Text.RegularExpressions;

public class JoinNetwork : NetworkBehaviour
{
    [SerializeField] GameObject textFieldPort;
    [SerializeField] GameObject textFieldAddress;
    [SerializeField] GameObject player;
    [SerializeField] GameObject cameraPlayer;
    GameObject ut;

    string port;
    string address;

    private void Start()
    {
        ut = GameObject.Find("NetworkManager");
    }

    public void JoinButton()
    {
        UnityTransport m_Transport = ut.GetComponent<UnityTransport>();
        address = SanitizeInput(textFieldAddress.GetComponent<TextMeshProUGUI>().text);
        port = SanitizeInput(textFieldPort.GetComponent<TextMeshProUGUI>().text);


        if (address == "")
        {
            Debug.Log("eRROR address!!");
            StopAllCoroutines();
            //todo show in the UI an error
            return;
        }
        if (port == "")
        {
            Debug.Log("eRROR port!!");
            StopAllCoroutines();
            //todo show in the UI an error
            return;
        }

        if (ushort.TryParse(port, out ushort uport))
        {
            m_Transport.SetConnectionData(address, uport);
        }
        else
        {
            m_Transport.SetConnectionData(address, 7777);
        }


        //ut.GetComponent<UnityTransport>().ConnectionData.Port = portUshort;
        //ut.GetComponent<UnityTransport>().ConnectionData.Address = address;

        Debug.Log("port " + port + " address " + address);
        NetworkManager.Singleton.StartClient();
        Hide();
    }

    private void Hide()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }

    private void Show()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }

    static string SanitizeInput(string dirtyString)
    {
        // sanitize the input for the ip address
        return Regex.Replace(dirtyString, "[^0-9.]", "");
    }


    public static string gethostbyname(string URL)
    {
        IPHostEntry Hosts = Dns.GetHostEntry(URL);
        return Hosts.AddressList[0].ToString();
    }

    //private string ConvertHostNameToIPAddress(string address)
    //{
    //    try
    //    {
    //        IPAddress[] addresses = Dns.GetHostAddresses(address);
    //        foreach (IPAddress addressZ in addresses)
    //        {
    //            Debug.Log("IP Address for " + address + ": " + addressZ.ToString());
    //            return addressZ.ToString();
    //        }
    //    }
    //    catch (Exception e)
    //    {
    //        Debug.LogError("Error converting host name to IP address: " + e.Message);
    //    }
    //    return "ConvertHostNameToIPAddress error";
    //}


    public void StartLocalHosting()
    {
        NetworkManager.Singleton.StartHost();

     //   InitializePLayerAndCameraServerRpc();

        Hide();
    }

    [ServerRpc(RequireOwnership = false)]
    private void InitializePLayerAndCameraServerRpc()
    {
        SpawnPlayer();
        //   SpawnCamera(playa);
    }

    public void StartLocalServer()
    {
        NetworkManager.Singleton.StartServer();
        Hide();
    }
    public void StartLocalClient()
    {


        NetworkManager.Singleton.StartClient();

        InitializePLayerAndCameraServerRpc();


        Hide();
    }

    private void SpawnCamera(GameObject playa)
    {
        GameObject cameraSpawn = Instantiate(cameraPlayer, playa.transform);
     
    }

    private void SpawnPlayer()
    {
        Instantiate(player);
    }
}
