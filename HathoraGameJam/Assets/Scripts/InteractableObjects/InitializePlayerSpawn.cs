using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class InitializePlayerSpawn : NetworkBehaviour
{
    public GameObject ChairToSpawn;


    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            BossAI.Instance.OnPlayerJoined();
        }
        if (IsClient && IsOwner)
        {
            SpawnChairServerRpc();
        }
    }

    public override void OnNetworkDespawn()
    {
        if (IsServer)
        {
            BossAI.Instance.OnPlayerLeft();
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void SpawnChairServerRpc()
    {
        SpawnChairClientRpc();
    }

    [ClientRpc]
    private void SpawnChairClientRpc()
    {
        GameObject chair = Instantiate(ChairToSpawn, transform.position, Quaternion.identity);
        chair.GetComponent<ChairFollow>().sphereTransform = transform;
    }
}
