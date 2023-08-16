using HathoraGameJam.CubicleEscape;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.AI;

public class BossCollider : NetworkBehaviour
{

    private void OnTriggerEnter(Collider playerCaught)
    {
        if (playerCaught.gameObject.tag == "Player")
        {
            //TODO if boss collide with a player
            //player lose points
            StartCoroutine(StopBoss());

            BossCaughtPlayerServerRpc(playerCaught.gameObject.GetComponent<PlayerManager>().ClientId.Value);
            StartCoroutine(StopPlayer(playerCaught.gameObject));

        }
    }



    [ServerRpc(RequireOwnership = false)]
    private void BossCaughtPlayerServerRpc(ulong clientId)
    {

        BossCaughtPlayerClientRpc(clientId);
    }

    public int howMuchPointLosePlayer = -40;

    [ClientRpc]
    private void BossCaughtPlayerClientRpc(ulong clientId)
    {


        if (GetCurrentClientId() == clientId)
        {
            NetworkObject playerCaught = GetCurrentPlayer();

            playerCaught.gameObject.GetComponent<PlayerGainPoints>().GainPoints(howMuchPointLosePlayer);
        }
    }



    private static ulong GetCurrentClientId()
    {
        GameObject networkManager = GameObject.Find("NetworkManager");
        return networkManager.GetComponent<NetworkManager>().LocalClient.PlayerObject.OwnerClientId;
    }

    private static NetworkObject GetCurrentPlayer()
    {
        GameObject networkManager = GameObject.Find("NetworkManager");
        return networkManager.GetComponent<NetworkManager>().LocalClient.PlayerObject;
    }

    float bossStopDuration = 7f;
    IEnumerator StopBoss()
    {
        transform.parent.gameObject.GetComponent<NavMeshAgent>().speed = 0;
        yield return new WaitForSeconds(bossStopDuration);
        transform.parent.gameObject.GetComponent<NavMeshAgent>().speed = 3.5f;
    }

    float playerCaughtStopDuration = 2.5f;
    IEnumerator StopPlayer(GameObject playerCaught)
    {
        playerCaught.gameObject.GetComponent<RollingChairMovement>().moveState = RollingChairMovement.MoveState.Stun;
        yield return new WaitForSeconds(playerCaughtStopDuration);
        playerCaught.gameObject.GetComponent<RollingChairMovement>().moveState = RollingChairMovement.MoveState.Normal;

    }
}
