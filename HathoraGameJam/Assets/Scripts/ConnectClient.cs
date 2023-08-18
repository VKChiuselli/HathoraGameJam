using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using TMPro;
using Unity.Netcode.Transports.UTP;
using System;
using System.Net;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;

public class ConnectClient : NetworkBehaviour
{
    GameObject ut;

    string port;
    string address;

    private void Awake()
    {
        ut = GameObject.Find("NetworkManager");
        port = PlayerPrefs.GetString("Port");
        address = PlayerPrefs.GetString("Address");
        StartClient();
    }
 

    //private void ChangeScene()
    //{
    //    Debug.Log("hostText  "+ hostText.GetComponent<TextMeshProUGUI>().text);
    //    Debug.Log("portText  " + portText.GetComponent<TextMeshProUGUI>().text);
    //    SceneManager.LoadScene("GameFieldOfficial");
    //}

    public void StartClient()
    {
        UnityTransport m_Transport = ut.GetComponent<UnityTransport>();
        address = SanitizeInput(address  );
        port = SanitizeInput(port );


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

        NetworkManager.Singleton.StartClient();


        //ut.GetComponent<UnityTransport>().ConnectionData.Port = portUshort;
        //ut.GetComponent<UnityTransport>().ConnectionData.Address = address;


        //   SceneManager.LoadScene("GameFieldOfficial" );
        //SceneManager.UnloadSceneAsync("MainMenuScene" );

        //if (status != SceneEventProgressStatus.Started)
        //{
        //    Debug.LogWarning($"Failed to load GameFieldOfficial" +
        //          $"with a {nameof(SceneEventProgressStatus)}: {status}");
        //}


        //MainMenuScene
        //      Hide(); //TODO Change Scene
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

}
