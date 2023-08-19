using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Hathora.Core.Scripts.Runtime.Client.ApiWrapper;
using Hathora.Cloud.Sdk.Model;
using System.Text.RegularExpressions;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;

namespace HathoraGameJam.CubicleEscape
{
    public class RoomEntryPrefabLogic :  MonoBehaviour
    {
        public TextMeshProUGUI roomName, playerCount;
        public TextMeshProUGUI hostText, portText;
        public Button joinButton;
        public string roomId;
        public GameObject secondPanel;
        public GameObject thirdPanel;

        public string port;
        public string address;

        private NetworkManager networkManager;
        private UnityTransport unityTransport;

        private void Awake()
        {
            Debug.Log(port);
            Debug.Log(address);
            joinButton.onClick.AddListener(Join_Room);
            //[MainMenuPageCanvas]
        }

        //private void JoinRoom()
        //{
        //    Debug.Log("Join Room");
        //    GameObject goNetworkManager = GameObject.Find("NetworkManager");

        //    if (goNetworkManager != null)
        //    {
        //        networkManager = goNetworkManager.GetComponent<NetworkManager>();

        //        if (networkManager != null)
        //        {
        //            unityTransport = networkManager.GetComponent<UnityTransport>();
        //        }
        //    }

 
        //    if (networkManager && unityTransport != null)
        //    {
        //        unityTransport.ConnectionData.Address = "35.71.157.211";
        //       // unityTransport.ConnectionData.Address = address;
        //        unityTransport.ConnectionData.Port = (ushort)port;

        //        networkManager.StartClient();
        //    }

        //    secondPanel.SetActive(false);
        //    thirdPanel.SetActive(true);

        //}

         private async void Join_Room()
        {
            GameObject mainMenuPageCanvas = GameObject.Find("[MainMenuPageCanvas]");

            ConnectionInfoV2 connectionInfoV2 = await      mainMenuPageCanvas.GetComponent<HathoraClientRoomApi>().ClientGetConnectionInfoAsync(roomId);

            if (connectionInfoV2 == null)
            {
                Debug.Log("connectionInfoV2 is null!!!");
                return;
            }

            // GetComponent<JoinNetwork>().JoinRoomn();
            ShowDataToJoinNetwork(connectionInfoV2.ExposedPort.Host, connectionInfoV2.ExposedPort.Port);
            thirdPanel.SetActive(true);
            secondPanel.SetActive(false);

           GameObject goNetworkManager = GameObject.Find("NetworkManager");

            StartClient(goNetworkManager);

            //if (goNetworkManager != null)
            //{
            //    networkManager = goNetworkManager.GetComponent<NetworkManager>();

            //    if (networkManager != null)
            //    {
            //        unityTransport = networkManager.GetComponent<UnityTransport>();
            //    }
            //}


            //if (networkManager && unityTransport != null)
            //{
            //    unityTransport.ConnectionData.Address = "35.71.157.211";
            //    // unityTransport.ConnectionData.Address = address;
            //    unityTransport.ConnectionData.Port = PlayerPrefs.GetString("port") ;

            //    networkManager.StartClient();
            //}


        }


        public void StartClient( GameObject goNetworkManager)
        {
            UnityTransport m_Transport = goNetworkManager.GetComponent<UnityTransport>();
            address = SanitizeInput(address);
            port = SanitizeInput(port);


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


        }


        public void ShowDataToJoinNetwork(string host, double doublePort)
        {
          //  UnityTransport m_Transport = ut.GetComponent<UnityTransport>();
            //TODO convert the host to the ip address   address = host;
            address = "35.71.157.211";

            port = doublePort.ToString("F0");

            port = SanitizeInput(port);

            PlayerPrefs.SetString("Port", port);
            PlayerPrefs.SetString("Address", address );

            hostText.text = address;
            portText.text = port;

      //      if (address == "")
      //      {
      //          Debug.Log("eRROR address!!");
      //          StopAllCoroutines();
      //          //todo show in the UI an error
      //          return;
      //      }
      //      if (port == "")
      //      {
      //          Debug.Log("eRROR port!!");
      //          StopAllCoroutines();
      //          //todo show in the UI an error
      //          return;
      //      }

      //      if (ushort.TryParse(port, out ushort uport))
      //      {
      //   //       m_Transport.SetConnectionData(address, uport);
      //        //  m_Transport.SetConnectionData(address, uport);
      //      }
      //      else
      //      {
      //      //    m_Transport.SetConnectionData(address, 7777);
      //      }


      //      //ut.GetComponent<UnityTransport>().ConnectionData.Port = portUshort;
      //      //ut.GetComponent<UnityTransport>().ConnectionData.Address = address;

      //      Debug.Log("port " + port + " address " + address);
      ////      NetworkManager.Singleton.StartClient();

        } 


        static string SanitizeInput(string dirtyString)
        {
            // sanitize the input for the ip address
            return Regex.Replace(dirtyString, "[^0-9.]", "");
        }

        private void Start()
        {
            transform.localScale = Vector3.one;
        }

    }
}

