using Hathora.Demos.Shared.Scripts.Client.ClientMgr;
using System.Threading.Tasks;
using UnityEngine;


namespace Hathora.Demos._1_FishNetDemo.HathoraScripts.Client.ClientMgr
{

    public class HathoraClientMgrNGO : HathoraClientMgrBase
    {



        public static HathoraClientMgrNGO Singleton { get; private set; }


        private GameObject networkManager;

         

        protected override void OnAwake()
        {
            Debug.Log("[hathoraNGO] OnAwake");
            base.OnAwake();


        }

        protected override void OnStart()
        {
            base.OnStart();

        }

        void Start()
        {
            networkManager = GameObject.Find("NetworkManager");
            OnStart();
        }


        void Update()
        {


        }

        public override Task<bool> ConnectAsClient()
        {
            throw new System.NotImplementedException();
        }

        public override Task StartClient(string _hostPort = null)
        {

            // networkManager.GetComponent<Unity.Netcode.NetworkManager>().StartClient();
            return Task.CompletedTask;
        }

        public override Task StartHost()
        {
            //   networkManager.GetComponent<Unity.Netcode.NetworkManager>().StartHost();
            return Task.CompletedTask;
        }

        public override Task StartServer()
        {
            //   networkManager.GetComponent<Unity.Netcode.NetworkManager>().StartServer();
            return Task.CompletedTask;
        }

        public override Task StopClient()
        {
            throw new System.NotImplementedException();
        }

        public override Task StopHost()
        {
            throw new System.NotImplementedException();
        }

        public override Task StopServer()
        {
            throw new System.NotImplementedException();
        }

        protected override void SetSingleton()
        {
            if (Singleton != null)
            {
                Debug.LogError("[HathoraNGO]**ERR @ SetSingleton: Destroying dupe");
                Destroy(gameObject);
                return;
            }

            Singleton = this;
        }


    }
}