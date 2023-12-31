// Created by dylan@hathora.dev

using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Hathora.Cloud.Sdk.Api;
using Hathora.Cloud.Sdk.Client;
using Hathora.Cloud.Sdk.Model;
using Hathora.Core.Scripts.Runtime.Client.Config;
using HathoraGameJam.CubicleEscape;
using Newtonsoft.Json;
using UnityEngine;

namespace Hathora.Core.Scripts.Runtime.Client.ApiWrapper
{
    /// <summary>
    /// High-level API wrapper for the low-level Hathora SDK's Lobby API.
    /// * Caches SDK Config and HathoraClientConfig for API use. 
    /// * Try/catches async API calls and [Base] automatically handlles API Exceptions.
    /// * Due to code autogen, the SDK exposes too much: This simplifies and minimally exposes.
    /// * Due to code autogen, the SDK sometimes have nuances: This provides fixes/workarounds.
    /// * Call Init() to pass HathoraClientConfig + Hathora SDK Config (see HathoraClientMgr).
    /// * Does not handle UI (see HathoraClientMgrUi).
    /// * Does not handle Session caching (see HathoraClientSession).
    /// </summary>
    public class HathoraClientLobbyApi : HathoraClientApiWrapperBase
    {
        private LobbyV2Api lobbyApi;

        
        public class LobbyData
        {
            public string lobbyName;
        }

        /// <summary>
        /// </summary>
        /// <param name="_hathoraClientConfig"></param>
        /// <param name="_hathoraSdkConfig">
        /// Passed along to base for API calls as `HathoraSdkConfig`; potentially null in child.
        /// </param>
        public override void Init(
            HathoraClientConfig _hathoraClientConfig,
            Configuration _hathoraSdkConfig = null)
        {
            Debug.Log("[NetHathoraClientLobbyApi] Initializing API...");
            
            base.Init(_hathoraClientConfig, _hathoraSdkConfig);
            this.lobbyApi = new LobbyV2Api(base.HathoraSdkConfig);
        }

        [SerializeField] GameObject secondPanel;
        [SerializeField] GameObject thirdPanel;

        #region Client Lobby Async Hathora SDK Calls
        /// <summary>
        /// Create a new Player Client Lobby.
        /// </summary>
        /// <param name="_playerAuthToken">Player Auth Token (likely from a cached session)</param>
        /// <param name="lobbyVisibility"></param>
        /// <param name="_initConfigJsonStr"></param>
        /// <param name="roomId">Null will auto-generate</param>
        /// <param name="_cancelToken"></param>
        /// <param name="_region">(!) Index starts at 1 (not 0)</param>
        /// <returns>Lobby on success</returns>
        public async Task<Lobby> ClientCreateLobbyAsync(
            string _playerAuthToken,
            CreateLobbyRequest.VisibilityEnum lobbyVisibility,
            Region _region = Region.London,
            string _initConfigJsonStr = "{}",
            string roomId = null,
            CancellationToken _cancelToken = default)
        {
            CreateLobbyRequest request = new(
                lobbyVisibility, 
                _initConfigJsonStr, 
                _region);

            Debug.Log("[NetHathoraClientLobbyApi.ClientCreateLobbyAsync] " +
                $"<color=yellow>request: {request.ToJson()}</color>");

            Lobby lobby;
            try
            {
                lobby = await lobbyApi.CreateLobbyAsync(
                    HathoraClientConfig.AppId,
                    _playerAuthToken, // Player token; not dev
                    request,
                    roomId,
                    _cancelToken);
            }
            catch (ApiException apiException)
            {
                HandleApiException(
                    nameof(HathoraClientLobbyApi),
                    nameof(ClientCreateLobbyAsync), 
                    apiException);
                return null; // fail
            }

            Debug.Log($"[NetHathoraClientAuthApi.ClientCreateLobbyAsync] Success: " +
                $"<color=yellow>lobby: {lobby.ToJson()}</color>");
            
            return lobby;
        }

        /// <summary>
        /// Gets Lobby info, if we already know the roomId.
        /// (!) Creating a room will also return Lobby info; you probably want to
        ///     do this if interested in *joining*.
        /// </summary>
        /// <param name="roomId"></param>
        /// <param name="_cancelToken"></param>
        /// <returns></returns>
        public async Task<Lobby> ClientGetLobbyInfoAsync(
            string roomId,
            CancellationToken _cancelToken = default)
        {
            Debug.Log("[NetHathoraClientLobbyApi.ClientCreateLobbyAsync] " +
                $"<color=yellow>roomId: {roomId}</color>");
            
            Lobby lobby;
            try
            {
                lobby = await lobbyApi.GetLobbyInfoAsync(
                    HathoraClientConfig.AppId,
                    roomId,
                    _cancelToken);
            }
            catch (ApiException apiException)
            {
                HandleApiException(
                    nameof(HathoraClientLobbyApi),
                    nameof(ClientGetLobbyInfoAsync), 
                    apiException);
                
                if (apiException.ErrorCode == 404)
                    Debug.LogError("[404] Tip: If a server made a Room without a lobby, " +
                        "instead use the Room api (rather than Lobby api)");
                
                return null; // fail
            }

            Debug.Log($"[NetHathoraClientAuthApi.ClientGetLobbyInfoAsync] Success: " +
                $"<color=yellow>lobby: {lobby.ToJson()}</color>");            
            
            return lobby;
        }

        [SerializeField] GameObject roomEntryPrefabLogic;
        [SerializeField] GameObject RoomListContent;

        public async void LoadLobbies()
        {
            List<Lobby> listOfLobbies = new List<Lobby>();
            listOfLobbies =  await   ClientListPublicLobbiesAsync();

            if (listOfLobbies.Count == 0)
            {
                return;
            }

            if (RoomListContent.transform.childCount > 0)
            {
                foreach (Transform lobby in RoomListContent.transform)
                {
                    Destroy(lobby.gameObject);
                }
            }


            foreach (Lobby lobby in listOfLobbies)
            {
                Debug.Log(lobby.InitialConfig);

                string json = lobby.InitialConfig.ToString(); // Your JSON string

                // Deserialize the JSON string into a LobbyData object
                LobbyData lobbyData = JsonConvert.DeserializeObject<LobbyData>(json);

                ConnectionInfoV2 connectioninfo = null;
                if (lobby.AdditionalProperties.TryGetValue("connectionInfo", out var value))
                {
                    if (value is ConnectionInfoV2 connectionInfoValue)
                    {
                        connectioninfo = connectionInfoValue;
                        Debug.Log("connectioninfo = connectionInfoValue");
                        // Now you can use the 'connectioninfo' variable
                    }
                    else
                    {
                        Debug.LogWarning("Did not get Connection Info!!!");
                    }
                }
                else
                {
                    Debug.LogWarning("'connectionInfo' did not exist in the dictionary!!!");
                }

                GameObject  newRoom =   Instantiate(roomEntryPrefabLogic, RoomListContent.transform);
                RoomEntryPrefabLogic logic = newRoom.GetComponent<RoomEntryPrefabLogic>();

                logic.roomId=lobby.RoomId;
                logic.roomName.text = lobbyData.lobbyName + "'s Room";
                logic.playerCount.text = "";
                logic.secondPanel = secondPanel;
                logic.thirdPanel = thirdPanel;

                if (connectioninfo != null)
                {
                 
                    logic.port = connectioninfo.ExposedPort.Port.ToString("F0");

                    string host = connectioninfo.ExposedPort.Host;
                    IPAddress[] addresslist = Dns.GetHostAddresses(host);

                    logic.address = addresslist[0].ToString();
                }
                

            }
        }


        /// <summary>
        /// </summary>
        /// <param name="_region">Leave null to return ALL Regions</param>
        /// <param name="_cancelToken"></param>
        /// <returns></returns>
        public async Task<List<Lobby>> ClientListPublicLobbiesAsync(
            Region? _region = null, // null == ALL regions
            CancellationToken _cancelToken = default)
        {
            Debug.Log("[NetHathoraClientLobbyApi.ClientCreateLobbyAsync] " +
                $"<color=yellow>region: {_region}</color> (This will exclude " +
                $"private lobbies and server Rooms created without a lobby)");
            
            List<Lobby> lobbies;
            try
            {
                lobbies = await lobbyApi.ListActivePublicLobbiesAsync(
                    HathoraClientConfig.AppId,
                    region: _region,
                    _cancelToken);
            }
            catch (ApiException apiException)
            {
                HandleApiException(
                    nameof(HathoraClientLobbyApi),
                    nameof(ClientListPublicLobbiesAsync), 
                    apiException);
                
                if (apiException.ErrorCode == 404)
                    Debug.LogError("[404] Tip: If a server made a Room without a lobby, " +
                        "instead use the Room api (rather than Lobby api)");
                
                return null; // fail
            }

            Debug.Log($"[NetHathoraClientLobbyApi] ClientListPublicLobbiesAsync => " +
                $"numLobbiesFound: {lobbies?.Count ?? 0}");
            
            return lobbies;
        }
        #endregion // Client Lobby Async Hathora SDK Calls
    }
}
