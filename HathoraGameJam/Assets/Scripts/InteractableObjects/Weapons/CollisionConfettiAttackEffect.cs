using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class CollisionConfettiAttackEffect : MonoBehaviour
{
    public ulong ClientId;
    public bool canBlind;
    NetworkObject player;
    private void Start()
    {
          player = GetCurrenPlayer();
    }

    private void OnParticleCollision(GameObject other)
    {

       
        if (player.OwnerClientId == ClientId)
        {
       
        }

        if (other.CompareTag("Player")   && canBlind)
        {
            BlindTarget(  other);
        }
    }


    public void BlindTarget(GameObject other)
    {
        BlindTargetServerRpc(other.gameObject.GetComponent<NetworkObject>().OwnerClientId);
        Debug.Log("attack confetti collision ");
    }

    [ServerRpc(RequireOwnership = true)]
    private void BlindTargetServerRpc(ulong clientId)//, ServerRpcParams serverRpcParams = default
    {
        BlindTargetClientRpc(clientId);
    }


    [ClientRpc]
    private void BlindTargetClientRpc(ulong clientId)
    {
        NetworkObject player = GetCurrenPlayer();
        if (player.OwnerClientId == clientId)
        {
            player.gameObject.GetComponent<PlayerManager>().SetBlackScreen();
        }
    }

    private static NetworkObject GetCurrenPlayer()
    {
        GameObject networkManager = GameObject.Find("NetworkManager");
        return networkManager.GetComponent<NetworkManager>().LocalClient.PlayerObject;
    }
}
